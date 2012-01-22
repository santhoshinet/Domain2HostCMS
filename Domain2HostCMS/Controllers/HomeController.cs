using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain2HostCMSDL;

namespace Domain2HostCMS.Controllers
{
    public class HomeController : Controller
    {
        public FileContentResult Photo(string pid)
        {
            try
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                List<File> files = (from c in scope.GetOqlQuery<File>().ExecuteEnumerable()
                                    where c.Id.Equals(pid)
                                    select c).ToList();
                if (files.Count > 0)
                {
                    return File(files[0].Filedata, files[0].MimeType, files[0].Filename);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}