using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enums
{
    public class Status
    {
        public static string GetStatusDescription(byte? status)
        {
            return status switch
            {
                1 => "Active",
                2 => "Deleted",
                _ => "Unknown"
            };
        }
    }
}
