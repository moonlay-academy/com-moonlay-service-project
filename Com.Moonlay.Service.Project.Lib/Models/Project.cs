using Com.Moonlay.Models;
using Com.Moonlay.Service.Project.Lib.Models.Validations.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Moonlay.Service.Project.Lib.Models
{
    public class Project : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Backlog> Backlogs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Code))
                yield return new ValidationResult("Code is required", new List<string> { "Code" });

            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Name is required", new List<string> { "Name" });            
        }
    }
}
