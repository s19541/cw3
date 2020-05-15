using System;
using System.Collections.Generic;

namespace Cwiczenia3.ModelsF
{
    public partial class Task
    {
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int IdProject { get; set; }
        public int IdTaskType { get; set; }
        public int IdAssignedTo { get; set; }
        public int IdCreator { get; set; }

        public TeamMember IdAssignedToNavigation { get; set; }
        public TeamMember IdCreatorNavigation { get; set; }
        public Project IdProjectNavigation { get; set; }
        public TaskType IdTaskTypeNavigation { get; set; }
    }
}
