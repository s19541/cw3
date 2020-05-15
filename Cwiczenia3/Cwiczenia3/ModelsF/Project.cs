using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class Project
    {
        public Project()
        {
            Task = new HashSet<Task>();
        }

        public int IdProject { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<Task> Task { get; set; }
    }
}
