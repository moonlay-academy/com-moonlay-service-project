using Com.Moonlay.Models;
using Com.Moonlay.Service.Project.Lib.Models.Validations.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Moonlay.Service.Project.Lib.Models
{
    public class Project : StandardEntity
    {
        [IsUnique]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Backlog> Backlogs { get; set; }
    }
}
