using System.Collections.Generic;

namespace DesktopUI.Models
{
    public class TypesModel
    {
        public TypesModel()
        {
            AccountTypes = new List<string>();
        }
        public string UserTypes { get; set; }
        public List<string> AccountTypes { get; set; }
    }
}