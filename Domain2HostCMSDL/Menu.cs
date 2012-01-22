using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace Domain2HostCMSDL
{
    [Persistent]
    public class Menu
    {
        public Menu()
        {
            ParentId = string.Empty;
            Id = DateTime.Now.ToString("yyddmmHHss");
            Page = new ContentPage();
        }

        public string DomainName { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public string ParentId { get; set; }

        public ContentPage Page { get; set; }

        public Menu Parent
        {
            get
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                var menus = (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                             where c.Id != null && c.Id.Equals(ParentId)
                             select c).ToList();
                if (menus.Count > 0)
                    return menus[0];
                return null;
            }
        }

        public List<Menu> Children
        {
            get
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                return (from c in scope.GetOqlQuery<Menu>().ExecuteEnumerable()
                        where c.ParentId != null && c.ParentId.Equals(Id)
                        select c).ToList();
            }
        }
    }
}