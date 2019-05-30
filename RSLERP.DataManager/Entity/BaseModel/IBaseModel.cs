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
        int CompanyId { get; set; }

    }
}
/*
 * SELECT 'ALTER TABLE ' + QUOTENAME(ss.name) + '.' + QUOTENAME(st.name) + ' ADD [CompanyId] int NULL;'
FROM sys.tables st
INNER JOIN sys.schemas ss on st.[schema_id] = ss.[schema_id]
WHERE st.is_ms_shipped = 0
AND NOT EXISTS (
    SELECT 1
    FROM sys.columns sc
    WHERE sc.[object_id] = st.[object_id]
    AND sc.name = 'CompanyId'
);
**/