using Com.Moonlay.Models;
using Com.Moonlay.Service.Project.Lib.Models.Validations.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Moonlay.Service.Project.Lib.Models
{
    public class Backlog : StandardEntity
    {
        public int ProjectId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project Project { get; set; }
        public List<Task> Tasks { get; set; }
    }
}