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
    [Table("SecRoles")]
    public class Role : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public int PriorityLevel { get; set; }
        public int CreatorId { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public int created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int modified_by { get; set; }
        public DateTime? modified_at { get; set; }
    }
}
