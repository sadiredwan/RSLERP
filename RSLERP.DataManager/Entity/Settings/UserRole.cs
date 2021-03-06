﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RSLERP.DataManager.Entity
{
    [Table("s_UserRole")]
    public class UserRole : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ur_ID { get; set; }
        public int ur_u_ID { get; set; }
        public int ur_rl_ID { get; set; }
        public bool ur_Status { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
        
        [NotMapped]
        public String UserName { get; set; }

        [NotMapped]
        public String RoleName { get; set; }
    }
}
