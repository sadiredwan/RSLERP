using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.Entity
{

    public interface IBaseModel
    {
        DateTime? created_at { get; set; }
        int created_by { get; set; }
        DateTime? modified_at { get; set; }
        int modified_by { get; set; }


    }
}
