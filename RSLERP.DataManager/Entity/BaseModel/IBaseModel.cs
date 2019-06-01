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
        int? created_by { get; set; }
        DateTime? modified_at { get; set; }
        int? modified_by { get; set; }
        int? CompanyId { get; set; }
        int? app_id { get; set; }

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

    EXEC sp_RENAME 's_User.CreatedBy', 'created_by', 'COLUMN'

     try
                    {
                        //Add new Company
                        using (var contxt = new DBContext())
                        {

                            contxt.CompanyUsers.Add(vmdl.VM_COMPANY_USER);
                            contxt.SaveChanges();

                        }
                    }
                    catch(DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }

**/
