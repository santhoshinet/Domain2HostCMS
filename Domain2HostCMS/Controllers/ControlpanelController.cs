using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain2HostCMS.Models;
using Domain2HostCMSDL;

namespace Domain2HostCMS.Controllers
{
    public class ControlpanelController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Menus()
        {
            if (User.Identity.IsAuthenticated)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                ViewData["menus"] = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                     where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                     select c).ToList();
                return View(new MenuModel());
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddMenu()
        {
            if (User.Identity.IsAuthenticated)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                ViewData["ContentPages"] = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                                            where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                            select c).ToList();
                ViewData["menus"] = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                     where c.ParentId.Equals(string.Empty) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                     select c).ToList(); //
                return View(new MenuModel());
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddMenu(MenuModel menuModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                if (ModelState.IsValid)
                {
                    ViewData["ContentPages"] = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                                                where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                                select c).ToList();
                    ViewData["menus"] = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                         where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                         select c).ToList();
                    List<Menu> menus = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                        where c.Name.ToLower().Equals(menuModel.MenuName.ToLower().Trim()) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                        select c).ToList();
                    if (menus.Count == 0)
                    {
                        var contentPages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                                            where c.Id.Equals(Request.Form["CmbPages"]) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                            select c).ToList();
                        if (contentPages.Count > 0)
                        {
                            string selectedMenu = Request.Form["CmbParentMenu"];
                            string parentId = string.Empty;
                            if (!string.IsNullOrEmpty(selectedMenu) && selectedMenu.ToLower().Trim() != "--root--")
                            {
                                var menuIds = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                               where c.Id != null && c.Id.Equals(selectedMenu) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                               select c.Id).ToList();
                                if (menuIds.Count > 0)
                                    parentId = menuIds[0];
                            }
                            scope.Transaction.Begin();
                            var menu = new Menu
                            {
                                Name = menuModel.MenuName,
                                ParentId = parentId,
                                Page = contentPages[0],
                                DomainName = domain
                            };
                            scope.Add(menu);
                            scope.Transaction.Commit();
                            ViewData["menus"] = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                                 where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                                 select c).ToList();
                            return View("menus");
                            //return RedirectToAction("Menus");
                        }
                        ModelState.AddModelError("", "The Link page is not selected.");
                    }
                    else
                        ModelState.AddModelError("Menu name", "The given menu is already exists");
                }
                return View(menuModel);
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditMenu(string mid)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            if (!string.IsNullOrEmpty(mid))
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                var contentPages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                                    where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                    select c).ToList();
                ViewData["ContentPages"] = contentPages;
                ViewData["menus"] = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                     where c.Id != null && !c.Id.Equals(mid) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                     select c).ToList();
                ViewData["Pagename"] = string.Empty;
                var menus = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                             where c.Id != null && c.Id.Equals(mid) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (menus.Count > 0)
                {
                    ViewData["ParentMenuId"] = menus[0].ParentId;
                    var menuModel = new MenuModel
                                        {
                                            MenuName = menus[0].Name,
                                            Id = menus[0].Id
                                        };
                    foreach (ContentPage contentPage in contentPages)
                    {
                        if (contentPage.Name.ToLower().Equals(menus[0].Page.Name.ToLower()))
                        {
                            ViewData["Pagename"] = contentPage.Name;
                            break;
                        }
                    }
                    return View(menuModel);
                }
            }
            return RedirectToAction("Menus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditMenu(MenuModel menuModel)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            if (ModelState.IsValid)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                var menus = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                             where c.Id != null && c.Id.Equals(menuModel.Id) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (menus.Count > 0)
                {
                    var contentPages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                                        where c.Id.Equals(Request.Form["CmbPages"]) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                        select c).ToList();
                    if (contentPages.Count > 0)
                    {
                        string selectedMenu = Request.Form["CmbParentMenu"];
                        string parentId = string.Empty;
                        if (!string.IsNullOrEmpty(selectedMenu) && selectedMenu.ToLower().Trim() != "--root--")
                        {
                            var menuIds = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                           where c.Id != null && c.Id.Equals(selectedMenu) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                           select c.Id).ToList();
                            if (menuIds.Count > 0)
                                parentId = menuIds[0];
                        }
                        foreach (Menu menu in menus)
                        {
                            scope.Transaction.Begin();
                            menu.Name = menuModel.MenuName;
                            menu.Page = contentPages[0];
                            menu.ParentId = parentId;
                            menu.DomainName = domain;
                            scope.Add(menu);
                            scope.Transaction.Commit();
                        }
                    }
                }
            }
            return RedirectToAction("Menus");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Pages()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            LoadPages();
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Images()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where c.Username.ToLower().Equals(User.Identity.Name.ToLower()) && c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            LoadImages();
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult InsertImage()
        {
            if (User.Identity.IsAuthenticated)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where
                                 c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                 c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                LoadImages();
                return View(new ImageModel());
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPage(string pid)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            if (pid != null)
            {
                var pages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                             where c.Id != null && c.Id.Equals(pid) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                var contentPage = new PageModel();
                foreach (ContentPage page in pages)
                {
                    contentPage.PageTitle = page.Name;
                    contentPage.Content = page.Content;
                    contentPage.Id = page.Id;
                    break;
                }
                return View(contentPage);
            }
            return RedirectToAction("Pages");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPage()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            return View(new PageModel());
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddImage()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            return View(new ImageModel());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Ajaxaddimage()
        {
            if (User.Identity.IsAuthenticated)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where
                                 c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                 c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                ViewData["Status"] = "";
                return View(new ImageModel());
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Ajaxaddimage(ImageModel file, HttpPostedFileBase image)
        {
            if (User.Identity.IsAuthenticated)
            {
                string domain = Utilities.GetMyDomain(Request.Url);
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                             where
                                 c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                                 c.Domain.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                if (users.Count == 0)
                {
                    ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                    return View("Status");
                }
                if (ModelState.IsValid)
                {
                    scope.Transaction.Begin();
                    var productFile = new Domain2HostCMSDL.File { Filename = image.FileName };
                    Stream fileStream = image.InputStream;
                    int fileLength = image.ContentLength;
                    productFile.Filedata = new byte[fileLength];
                    fileStream.Read(productFile.Filedata, 0, fileLength);
                    productFile.MimeType = image.ContentType;
                    productFile.Id = DateTime.Now.Ticks.ToString();
                    productFile.DomainName = domain;
                    scope.Add((productFile));
                    scope.Transaction.Commit();
                    ViewData["Status"] = "Image added successfully.";
                    return View(new ImageModel());
                }
                return View(file);
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult EditPage(PageModel pageModel)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            if (ModelState.IsValid)
            {
                var pages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                             where c.Id != null && c.Id.Equals(pageModel.Id) && c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                             select c).ToList();
                foreach (ContentPage page in pages)
                {
                    scope.Transaction.Begin();
                    page.Name = pageModel.PageTitle;
                    page.Content = pageModel.Content;
                    page.DomainName = domain;
                    scope.Add(page);
                    scope.Transaction.Commit();
                    try
                    {
                        using (var connection = new SqlConnection("Data Source=208.91.198.196;Initial Catalog=admin_domain2hostcms;Persist Security Info=True;User ID=domain2hostcms;Password=password@123"))
                        {
                            connection.Open();
                            string qry = "update content_page set [<_content>k___backing_field] = '" + pageModel.Content.Replace("'", "''") + "' where [<_id>k___backing_field]='" + page.Id + "'";
                            var command = new SqlCommand(qry, connection);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    break;
                }
                LoadPages();
                return View("Pages");
            }
            return View(pageModel);
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult AddPage(PageModel adPageModel)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            if (ModelState.IsValid)
            {
                var contentPage = new ContentPage { Name = adPageModel.PageTitle, Content = adPageModel.Content, Id = DateTime.Now.Ticks.ToString(), DomainName = domain };
                scope.Transaction.Begin();
                scope.Add(contentPage);
                scope.Transaction.Commit();
                try
                {
                    using (var connection = new SqlConnection("Data Source=208.91.198.196;Initial Catalog=admin_domain2hostcms;Persist Security Info=True;User ID=domain2hostcms;Password=password@123"))
                    {
                        connection.Open();
                        string qry = "update content_page set [<_content>k___backing_field] = '" + adPageModel.Content.Replace("'", "''") + "' where [<_id>k___backing_field]='" + contentPage.Id + "'";
                        var command = new SqlCommand(qry, connection);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    LoadPages();
                    return View("Pages");
                }
                LoadPages();
                return View("Pages");
            }
            return View(adPageModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddImage(ImageModel file, HttpPostedFileBase image)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            if (ModelState.IsValid)
            {
                scope.Transaction.Begin();
                var productFile = new Domain2HostCMSDL.File { Filename = image.FileName };
                Stream fileStream = image.InputStream;
                int fileLength = image.ContentLength;
                productFile.Filedata = new byte[fileLength];
                fileStream.Read(productFile.Filedata, 0, fileLength);
                productFile.MimeType = image.ContentType;
                productFile.Id = DateTime.Now.Ticks.ToString();
                productFile.DomainName = domain;
                scope.Add((productFile));
                scope.Transaction.Commit();
                LoadImages();
                return View("Images");
            }
            return View(file);
        }

        [Authorize]
        public ActionResult DeletePage(string pid)
        {
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            var pages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                         where c.Id != null && c.Id.Equals(pid)
                         select c).ToList();
            foreach (var contentPage in pages)
            {
                scope.Transaction.Begin();
                scope.Remove(contentPage);
                scope.Transaction.Commit();
            }
            LoadPages();
            return View("Pages");
        }

        [Authorize]
        public ActionResult DeleteImage(string id)
        {
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            var images = (from c in scope.GetOqlQuery<Domain2HostCMSDL.File>().ExecuteEnumerable()
                          where c.Id.Equals(id)
                          select c).ToList();
            foreach (var image in images)
            {
                scope.Transaction.Begin();
                scope.Remove(image);
                scope.Transaction.Commit();
            }
            LoadImages();
            return View("Images");
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteMenu(string mid)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            string domain = Utilities.GetMyDomain(Request.Url);
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var users = (from c in scope.GetOqlQuery<UserAuthentication>().ExecuteEnumerable()
                         where
                             c.Username.ToLower().Equals(User.Identity.Name.ToLower()) &&
                             c.Domain.ToLower().Equals(domain.ToLower())
                         select c).ToList();
            if (users.Count == 0)
            {
                ViewData["Status"] = "You are not authorized for this domain [" + domain + "] control panel.";
                return View("Status");
            }
            if (ModelState.IsValid)
                DeleteMenus(mid);
            ViewData["menus"] = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                 where c.DomainName != null && c.DomainName.ToLower().Equals(domain.ToLower())
                                 select c).ToList();
            return View("Menus");
        }

        private void DeleteMenus(string menuId)
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var menus = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                         where c.Id != null && c.Id.Equals(menuId)
                         select c).ToList();
            foreach (Menu menu in menus)
            {
                Menu menu1 = menu;
                var parentmenus = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                                   where c.ParentId != null && c.ParentId.Equals(menu1.Id)
                                   select c).ToList();
                foreach (var parentmenu in parentmenus)
                {
                    DeleteMenu(parentmenu.Id);
                }

                scope.Transaction.Begin();
                scope.Remove(menu);
                scope.Transaction.Commit();
            }
        }

        private void LoadImages()
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var files = (from c in scope.GetOqlQuery<Domain2HostCMSDL.File>().ExecuteEnumerable()
                         where c.DomainName != null && c.DomainName.ToLower().Equals(Utilities.GetMyDomain(Request.Url).ToLower())
                         select c).ToList();
            ViewData["fileList"] = files;
        }

        private void LoadPages()
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            var pages = (from c in scope.GetOqlQuery<ContentPage>().ExecuteEnumerable()
                         where c.DomainName != null && c.DomainName.ToLower().Equals(Utilities.GetMyDomain(Request.Url).ToLower())
                         select c).ToList();
            ViewData["webpageList"] = pages;
        }
    }
}