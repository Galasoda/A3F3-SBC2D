using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Shared
{
    public static class Enums
    {
        public enum Role
        {
            Operater = 0,
            Engineer = 1,
            Vendor = 2
        }

        public enum RecipeManageViewMode
        {
            Nothing,
            Open,
            Save,
            SaveAs,
            Delete
        }
    }
}
