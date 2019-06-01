using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    [Table("s_Module")]
    public class Module : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int m_ID { get; set; }
        public String m_Name { get; set; }
        public DateTime m_InitialDate { get; set; }
        public String m_Remarks { get; set; }
        public bool m_status { get; set; }
        public String icon { get; set; }
        public String url { get; set; }

        public int? CompanyId { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }        public int? app_id { get; set; }
    }
}
