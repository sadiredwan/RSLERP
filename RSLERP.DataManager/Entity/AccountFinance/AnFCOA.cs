using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{
    [Table("AnFCOAs")]
    public class AnFCOA : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public long? AnFCOAId { get; set; }
        public int? CmnCompanyId { get; set; }
        public String Code { get; set; }
        public String UnsedCode { get; set; }
        public int? DepthLevel { get; set; }
        public int? NotesNo { get; set; }
        public int? BLorPL { get; set; }
        public String Particular { get; set; }
        public String SubTitles { get; set; }
        public String Address { get; set; }
        public String AccNo { get; set; }
        public String ContactNo { get; set; }
        public bool? Status { get; set; }
        public bool? IsTransactionalHead { get; set; }
        public bool? IsFactoryAccess { get; set; }
        public int? CashOrBank { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
    }
}
