using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
   public class RoleBLL
    {
      public List<RolePermissions> GetRolePermissionAll()
        {
            return new RoleDLL().GetRolePermission();
        }

        public List<SecRoles> GetRoleAll(int pageIndex=0, int pageSize=0)
        {
            return new RoleDLL().GetRoles("",pageIndex,pageSize);
        }

        public List<RolePermissions> GetRolePermission(String roleID,String moduleID)
        {
            return new RoleDLL().GetRolePermission("", roleID, "", "", moduleID);
        }

        public List<SecRoles> GetRoleByID(String id)
        {
            return new RoleDLL().GetRoles(id);
        }

        public List<SecRoles> SearchRole(String roleName, int pageIndex = 0, int pageSize = 0)
        {
            return new RoleDLL().GetRoles("",pageIndex,pageSize,roleName);
        }

        public String  InsertRolePermission(RolePermissions rolePermission)
        {
            String msg = GLobalMessage.Global_Success_Message;
            new RoleDLL().DeleteRolePermission("" + rolePermission.rp_rl_ID, "" + rolePermission.rp_m_ID, "" + rolePermission.rp_companyID);
            try
            {
                new RoleDLL().InsertRolePermission("" + rolePermission.rp_ID, "" + rolePermission.rp_rl_ID, "" + rolePermission.rp_r_ID, "" + rolePermission.rp_add, "" + rolePermission.rp_ReadOnly, "" + rolePermission.rp_Edit, "" + rolePermission.rp_Delete, "" + rolePermission.rp_Print, "" + rolePermission.CreatedBy, "" + rolePermission.CreatedDate, "" + rolePermission.ModifiedBy, "" + rolePermission.ModifiedDate,""+rolePermission.rp_m_ID);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }

            return msg;
        }

        public String InsertRolePermissions(List<RolePermissions> rolePermissions)
        {
            String msg = GLobalMessage.Global_Success_Message;
            new RoleDLL().DeleteRolePermission(""+rolePermissions[0].rp_rl_ID, ""+rolePermissions[0].rp_m_ID, ""+rolePermissions[0].rp_companyID);
            foreach (RolePermissions rolePermission in rolePermissions)
            {
                try
                {
                    new RoleDLL().InsertRolePermission("" + rolePermission.rp_ID, "" + rolePermission.rp_rl_ID, "" + rolePermission.rp_r_ID, "" + rolePermission.rp_add, "" + rolePermission.rp_ReadOnly, "" + rolePermission.rp_Edit, "" + rolePermission.rp_Delete, "" + rolePermission.rp_Print, "" + rolePermission.CreatedBy, "" + rolePermission.CreatedDate, "" + rolePermission.ModifiedBy, "" + rolePermission.ModifiedDate,""+rolePermission.rp_m_ID);
                }
                catch
                {
                    msg = GLobalMessage.Global_Error_Message;
                }

            }
            return msg;
        }
    }
}
