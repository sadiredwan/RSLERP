﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSLERP.DataManager.Entity
{
    [Table("HrmEmployeeTrainings")]
    public class HrmEmployeeTraining : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public int hrm_employee_official_id { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public String training_title { get; set; }
        public String venue { get; set; }
        public String dept_name { get; set; }
        public int duration_days { get; set; }
        public String subject { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_at { get; set; }
        public int? CompanyId { get; set; }
        public int? app_id { get; set; }
    }
}