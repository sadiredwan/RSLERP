using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Employees
    {
        public int id { get; set; }

        [Required]
        [UserIDValidation(ErrorMessage = "UserID already exists")]
        public int user_id { get; set; }

        [Required]
        public String name { get; set; }

        [Required]
        [EmailValidation(ErrorMessage = "E-mail already exists")]
        public String email { get; set; }

        [Required]
        [MobileValidation(ErrorMessage = "Mobile No. already exists")]
        public String mobile { get; set; }

        [Required]
        public String address { get; set; }

        [Required]
        public DateTime birthdate { get; set; }

        [Required]
        public DateTime joining_date { get; set; }

        //Extra
        public String username { get; set; }
        public int TotalRow { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UserIDValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is int)) return new ValidationResult("DataType Error");
            var val = Convert.ToInt32(value);
            try
            {
                var idProperty = validationContext.ObjectInstance.GetType().GetProperty("id");
                var idPropertyValue = idProperty.GetValue(validationContext.ObjectInstance, null);
                if (idPropertyValue.ToString() != "0")
                {
                    return ValidationResult.Success;
                }
                EmployeeBLL empbll = new EmployeeBLL();
                List<Employees> lstEmployee = empbll.GetEmployeesByUserID("" + val);
                if (lstEmployee.Count > 0)
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

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailValidation : ValidationAttribute
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
                EmployeeBLL empbll = new EmployeeBLL();
                List<Employees> lstEmployee = empbll.GetEmployeesByEmail(val);
                if (lstEmployee.Count > 0)
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

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MobileValidation : ValidationAttribute
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
                EmployeeBLL empbll = new EmployeeBLL();
                List<Employees> lstEmployee = empbll.GetEmployeesByMobile(val);
                if (lstEmployee.Count > 0)
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
