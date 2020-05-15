using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class TaskType
    {
        public TaskType()
        {
            Task = new HashSet<Task>();
        }

        public int IdTaskType { get; set; }
        public string Name { get; set; }

        public ICollection<Task> Task { get; set; }
    }
}
