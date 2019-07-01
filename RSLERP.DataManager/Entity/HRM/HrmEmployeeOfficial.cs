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
    [Table("HrmEmployeeOfficials")]
    public class HrmEmployeeOfficial : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        [HrmEmployeeOfficialEmployeeIdValidation(ErrorMessage = "Employee ID already exists.")]
        public String employee_id { get; set; }
        [Required]
        public String pabx_no { get; set; }
        [Required]
        public String name { get; set; }
        [Required]
        public String official_email { get; set; }
        [Required]
        public String official_contact_no { get; set; }
        [Required]
        public int reporting_employee_id { get; set; }
        [Required]
        public int designation_id { get; set; }
        [Required]
        public int department_id { get; set; }
        [Required]
        public int section_id { get; set; }
        [Required]
        public int subsection_id { get; set; }
        [Required]
        public int business_id { get; set; }
        [Required]
        public int project_segment_id { get; set; }
        [Required]
        public String basic { get; set; }
        [Required]
        public DateTime joining_date { get; set; }
        [Required]
        public DateTime confirmation_date { get; set; }
        [Required]
        public int shift_id { get; set; }
        [Required]
        public int employee_type_id { get; set; }
        [Required]
        public int employment_type_id { get; set; }
        [Required]
        public int bank_id { get; set; }
        [Required]
        [HrmEmployeeOfficialBankAccountNoValidation(ErrorMessage = "Bank Account already allocated.")]
        public String bank_account_no { get; set; }
        public String picture { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }


        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class HrmEmployeeOfficialEmployeeIdValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
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
                    var findHrmEmployeeOfficial = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.employee_id == val);
                    if (findHrmEmployeeOfficial.Count > 0)
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
        public class HrmEmployeeOfficialBankAccountNoValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                int COMPANY_ID = Convert.ToInt32(RSLERPApplication.CurrentState().CompanyId);
                if (!(value is string)) return new ValidationResult("DataType Error");
                var val = (string)value;
                try
                {
                    var idProperty = validationContext.ObjectInstance.GetType().GetProperty("id");
                    var idPropertyValue = idProperty.GetValue(validationContext.ObjectInstance, null);
                    var bankIdProperty = validationContext.ObjectInstance.GetType().GetProperty("bank_id");
                    var bankIdPropertyValue = idProperty.GetValue(validationContext.ObjectInstance, null);
                    if (idPropertyValue.ToString() != "0")
                    {
                        return ValidationResult.Success;
                    }
                    var findHrmEmployeeOfficial = new DBContext().HrmEmployeeOfficials.Where(x => x.CompanyId == COMPANY_ID).Where(x => x.bank_id == (int)bankIdPropertyValue).ToList().FindAll(x => x.bank_account_no == val);
                    if (findHrmEmployeeOfficial.Count > 0)
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