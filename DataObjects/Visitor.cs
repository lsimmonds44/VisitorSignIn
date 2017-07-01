using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Visitor
    {
        public int VisitorID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string PersonVisiting { get; set; }
        public bool Citizen { get; set; }
        public bool SignedIn { get; set; }
        public DateTime SignedInTime { get; set; }

    }
}
