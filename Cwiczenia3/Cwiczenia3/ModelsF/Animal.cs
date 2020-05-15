using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class Animal
    {
        public Animal()
        {
            ProcedureAnimal = new HashSet<ProcedureAnimal>();
        }

        public int IdAnimal { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int IdOwner { get; set; }

        public Owner IdOwnerNavigation { get; set; }
        public ICollection<ProcedureAnimal> ProcedureAnimal { get; set; }
    }
}
