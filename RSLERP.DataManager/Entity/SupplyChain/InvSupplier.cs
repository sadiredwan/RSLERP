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
    [Table("InvSuppliers")]
    public class InvSupplier : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        [InvSupplierNameValidation(ErrorMessage = "Supplier already exists")]
        public String name { get; set; }
        [Required]
        public String type { get; set; }
        [Required]
        public String address { get; set; }
        public int country_id { get; set; }
        [Required]
        public String phone { get; set; }
        public String fax { get; set; }
        [Required]
        public String email { get; set; }
        public String website { get; set; }
        [Required]
        public String contact_person { get; set; }
        public String contact_person_phone { get; set; }
        public String remarks { get; set; }
        public bool status { get; set; }
        [Required]
        public int AnFCOA_id { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
        

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class InvSupplierNameValidation : ValidationAttribute
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
                    var findInvSupName = new DBContext().InvSuppliers.Where(x => x.CompanyId == COMPANY_ID).ToList().FindAll(x => x.name == val);
                    if (findInvSupName.Count > 0)
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