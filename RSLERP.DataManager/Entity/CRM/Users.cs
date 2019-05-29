using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Users
    {
        public int id { get; set; }

        [Required]
        [UsernameValidation(ErrorMessage = "Username already exists")]
        public String username { get; set; }

        [PasswordValidation(ErrorMessage = "Password field is required")]
        public String password { get; set; }

        [ConfirmPasswordMatchValidation(ErrorMessage = "Your password and confirmation password do not match.")]
        public String confirmpassword { get; set; }

        [Required]
        public int role_id { get; set; }

        //[CompanyValidation(ErrorMessage = "Company field is required")]
        //public int company_id { get; set; }

        //Extra
        public String role_name { get; set; }
        //public String CompanyName { get; set; }
        public int TotalRow { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UsernameValidation : ValidationAttribute
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
                UserBLL usrbll = new UserBLL();
                List<s_User> lstUser = usrbll.GetUsersByName(val);
                if (lstUser.Count > 0)
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
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) value = "";
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
                if (val == "")
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
    public class ConfirmPasswordMatchValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) value = "";
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
                var passwordProperty = validationContext.ObjectInstance.GetType().GetProperty("password");
                var passwordPropertyValue = passwordProperty.GetValue(validationContext.ObjectInstance, null);
                if (passwordPropertyValue.ToString() != val)
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
