using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Moonlay.Service.Project.Lib.Models
{
    public class Task : StandardEntity
    {
        public int ProjectId { get; set; }
        public int BacklogId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
