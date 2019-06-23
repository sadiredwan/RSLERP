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
    [Table("SlsSDPs")]
    public class SlsSDP : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        [SlsSDPNameValidation(ErrorMessage = "Sales & Distribution Point already exists.")]
        public String name { get; set; }
        [Required]
        public int slssdp_type_id { get; set; }
        [Required]
        public int parent_slssdp_id { get; set; }
        public String address { get; set; }
        [Required]
        public String contact_person { get; set; }
        [Required]
        public String contact_person_phone { get; set; }
        [Required]
        public Double credit_limit { get; set; }
        public bool is_credit_limit_active { get; set; }
        [Required]
        public int AnFCOA_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }


        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class SlsSDPNameValidation : ValidationAttribute
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
                    var findSlsSDP = new DBContext().SlsSDPs.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.name == val);
                    if (findSlsSDP.Count > 0)
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