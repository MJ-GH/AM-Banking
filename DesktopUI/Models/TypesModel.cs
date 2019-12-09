using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
