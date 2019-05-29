using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSLERP.DataManager.BLL;

namespace RSLERP.DataManager.Entity
{
    public class Support_Types
    {
        public int id { get; set; }

        [Required]
        [TypeValidation(ErrorMessage = "Type already exists")]
        public String type { get; set; }

        [Required]
        [RemarksValidation(ErrorMessage = "Remarks already exist")]
        public String remarks { get; set; }

        public DateTime created_date { get; set; }

        //Extra
        public int TotalRow { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TypeValidation : ValidationAttribute
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
                SupportTypeBLL sptbll = new SupportTypeBLL();
                List<Support_Types> lstSupportType = sptbll.GetSupportTypesByType(val);
                if (lstSupportType.Count > 0)
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
    public class RemarksValidation : ValidationAttribute
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
                SupportTypeBLL sptbll = new SupportTypeBLL();
                List<Support_Types> lstSupportType = sptbll.GetSupportTypesByType(val);
                if (lstSupportType.Count > 0)
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
