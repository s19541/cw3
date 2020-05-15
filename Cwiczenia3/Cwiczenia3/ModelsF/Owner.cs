using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class Owner
    {
        public Owner()
        {
            Animal = new HashSet<Animal>();
        }

        public int IdOwner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Animal> Animal { get; set; }
    }
}
