using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3.Models
{
    public class Enrollment
    {
        public int idEnrollment { get; set; }
        public int semestr { get; set; }
        public int idStudies { get; set; }
        public DateTime startDate { get; set; }

    }
}
