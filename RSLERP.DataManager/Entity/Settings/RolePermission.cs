using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    [Table("s_RolePermission")]
    public class RolePermission : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int rp_ID { get; set; }
        public int rp_rl_ID { get; set; }
        public int rp_r_ID { get; set; }
        public bool rp_add { get; set; }
        public bool rp_ReadOnly { get; set; }
        public bool rp_Edit { get; set; }
        public bool rp_Delete { get; set; }
        public bool rp_Print { get; set; }
        public int rp_m_ID { get; set; }
        public int rp_companyID { get; set; }

        public int CompanyId { get; set; }
        public int created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int modified_by { get; set; }
        public DateTime? modified_at { get; set; }        [NotMapped]        [ForeignKey("rp_rl_ID")]        public virtual Role ROLE { get; set; }
    }
}
