using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Support_Users
    {
        public int id { get; set; }

        [Required]
        public String name { get; set; }

        [Required]
        [SupportEmailValidation(ErrorMessage = "E-mail already exists")]
        public String email { get; set; }

        [Required]
        [SupportMobileValidation(ErrorMessage = "Mobile No. already exists")]
        public String mobile { get; set; }

        [Required]
        public String address { get; set; }

        //Extra
        public int TotalRow { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SupportEmailValidation : ValidationAttribute
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
                SupportUserBLL spubll = new SupportUserBLL();
                List<Support_Users> lstSupportUser = spubll.GetSupportUsersByEmail(val);
                if (lstSupportUser.Count > 0)
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
    public class SupportMobileValidation : ValidationAttribute
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
                SupportUserBLL spubll = new SupportUserBLL();
                List<Support_Users> lstSupportUser = spubll.GetSupportUsersByMobile(val);
                if (lstSupportUser.Count > 0)
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
