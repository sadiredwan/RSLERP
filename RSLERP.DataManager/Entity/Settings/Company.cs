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
    [Table("s_Company")]
    public class Company : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int c_ID { get; set; }
        public int c_g_ID { get; set; }

        [Required]
        public String c_Name { get; set; }
        public String c_Address { get; set; }
        public String c_FactoryAddress { get; set; }
        public String c_Remarks { get; set; }
        public String c_ContactNo { get; set; }
        public String c_Email { get; set; }
        public String c_Web { get; set; }
        public String Prefix { get; set; }
        public int? created_by { get; set; }

        public int? CompanyId { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }        public int? app_id { get; set; }
    }


}
