using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Domain2HostCMS.Models;
using Domain2HostCMSDL;

namespace Domain2HostCMS.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }

        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where c.Username.ToLower().Equals(model.UserName.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ModelState.AddModelError("", "You are not authorized for this domain [" + domain + "] control panel.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Controlpanel");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsService.SignOut();
            return RedirectToAction("Index", "Controlpanel");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ModelState.AddModelError("", "You are not authorized for this domain [" + domain + "] control panel.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }
            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                if (model.AdminPassword == "password@123")
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        var scope = ObjectScopeProvider1.GetNewObjectScope();
                        scope.Transaction.Begin();
                        var userAuthenticate = new UserAuthentication { Domain = domain, Username = model.UserName };
                        scope.Add(userAuthenticate);
                        scope.Transaction.Commit();
                        FormsService.SignIn(model.UserName, true /* createPersistentCookie */);
                        return RedirectToAction("Index", "Controlpanel");
                        //return RedirectToAction("Pages", "Home", new RouteValueDictionary() { { "pid", "index" } });
                    }
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
                else
                    ModelState.AddModelError("", "Invalid admin password entered.");
            }
            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }
    }
}