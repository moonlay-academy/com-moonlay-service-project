using Com.Moonlay.Service.Project.Lib.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Moonlay.Service.Project.Lib.Models.Validations.Project
{
    public class IsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ProjectService service = validationContext.GetService(typeof(ProjectService)) as ProjectService;
            var valid = service.DbContext.Set<Lib.Models.Project>().Count(r => r.Name == value.ToString()) == 0;
            if (valid)
                return ValidationResult.Success;
            else
                return new ValidationResult("Name sudah ada", new List<string> { validationContext.MemberName });
            //return base.IsValid(value, validationContext);
        }
    }
}
