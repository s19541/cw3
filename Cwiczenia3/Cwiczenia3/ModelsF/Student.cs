using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int IdEnrollment { get; set; }
        public string Password { get; set; }

        public Enrollment IdEnrollmentNavigation { get; set; }
    }
}
