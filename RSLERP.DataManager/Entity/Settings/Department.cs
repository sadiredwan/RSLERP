using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSLERP.DataManager.Entity
{
    [Table("s_Departments")]
    public class Department : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        [DepartmentNameValidation(ErrorMessage = "Department already exists")]
        public string name { get; set; }
        public string description { get; set; }
        public int? created_by { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? app_id { get; set; }
        
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class DepartmentNameValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (!(value is string)) return new ValidationResult("DataType Error");
                var val = (string)value;
                try
                {
                    var idProperty = validationContext.ObjectInstance.GetType().GetProperty("id");
                    var idPropertyValue = idProperty.GetValue(validationContext.ObjectInstance, null);
                    if (idPropertyValue.ToString() != "0")
                    {
                        return ValidationResult.Success;
                    }
                    var findDepartmentName = new DBContext().Departments.ToList().FindAll(x => x.name == val);
                    if (findDepartmentName.Count > 0)
                    {
                        return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
                    }
                }
                catch (Exception e)
                {
                    return ValidationResult.Success;
                }
                return ValidationResult.Success;
            }
            public override string FormatErrorMessage(string name)
            {
                return base.FormatErrorMessage(name);
            }
        }

    }
}
