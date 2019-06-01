using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    [Table("s_Resource")]
    public class Resource :IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int R_ID { get; set; }
        public String R_ResourceName { get; set; }
        public String R_DisplayName { get; set; }
        public String R_M_ID { get; set; }
        public bool? R_Status { get; set; }
        public String R_Tag { get; set; }
        public int? Parent_R_ID { get; set; }
        public bool? Is_Menu { get; set; }
        public int? R_Order { get; set; }
        public String R_SM_ID { get; set; }
        public String R_Url { get; set; }
        public String icon { get; set; }
        public int? Menu_Order { get; set; }

        public int? CompanyId { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? app_id { get; set; }


    }
}
