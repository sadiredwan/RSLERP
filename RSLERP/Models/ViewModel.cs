using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RSLERP.Models
{
    public class ViewModel : BaseModel
    {
        
        public List<TreeMenu> VM_Tree { get; set; }

        public s_User VM_USER { get; set; }

        public List<s_User> VM_USERS { get; set; }

        public Menus VM_MENU { get; set; }

        public List<Menus> VM_MENUS { get; set; }

        public s_Modules VM_MODULE { get; set; }

        public List<s_Modules> VM_MODULES { get; set; }

        public s_Resource VM_RESOURCE { get; set; }

        public List<s_Resource> VM_RESOURCES { get; set; }

        public RolePermission VM_ROLEPERMISSION { get; set; }

        public List<RolePermission> VM_ROLEPERMISSIONS { get; set; }

        public SecRoles VM_SECROLE { get; set; }

        public List<SecRoles> VM_SECROLES { get; set; }

        public s_Company VM_COMPANY { get; set; }

        public List<s_Company> VM_COMPANYS { get; set; }

        public s_ApplicationState VM_APPLICATION { get; set; }

        public List<s_ApplicationState> VM_APPLICATIONS { get; set; }

        public CmnTransactionalYears VM_FINANCIALYEAR{ get; set; }

        public List<CmnTransactionalYears> VM_FINANCIALYEARS { get; set; }

        public List<Company> VM_COMPANIES { get; set; }

        public Company VM_COMPANE { get; set; }

        public Role VM_ROLE { get; set; }

        public List<Role> VM_ROLES { get; set; }


        public Module VM_MDULE { get; set; }

        public List<Module> VM_MDULES { get; set; }


        public Resource VM_MODULE_RESOURCE { get; set; }

        public List<Resource> VM_MODULE_RESOURCES { get; set; }

        public PriorityLevel VM_PRIORITY_LEVEL { get; set; }

        public List<PriorityLevel> VM_PRIORITY_LEVELS { get; set; }

        public CompanyUser VM_COMPANY_USER { get; set; }

        public List<CompanyUser> VM_COMPANY_USERS { get; set; }




    }


    public class CustomTest
    {
        public String Name { get; set; }

        public String Age { get; set; }

        public String ContactNo { get; set; }
    }
}