using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RSLERP.DataManager.Entity
{
    [Table("s_Group")]
    public class Group : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int g_ID { get; set; }
        public String g_Name { get; set; }
        public String g_Address { get; set; }
        public String g_Remarks { get; set; }
        public String g_ContactNo { get; set; }
        public String g_Email { get; set; }
        public String g_Web { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }

       }
}
