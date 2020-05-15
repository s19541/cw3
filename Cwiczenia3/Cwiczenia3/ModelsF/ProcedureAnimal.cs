using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class ProcedureAnimal
    {
        public int ProcedureIdProcedure { get; set; }
        public int AnimalIdAnimal { get; set; }
        public DateTime Date { get; set; }

        public Animal AnimalIdAnimalNavigation { get; set; }
        public Procedure ProcedureIdProcedureNavigation { get; set; }
    }
}
