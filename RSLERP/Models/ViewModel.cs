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

        public List<Company> VM_COMPANIES { get; set; }

        public Company VM_COMPANE { get; set; }

        public List<Department> VM_DEPARTMENTS { get; set; }

        public Department VM_DEPARTMENT { get; set; }

        public List<Financialyear> VM_FINANCIALYEARS { get; set; }

        public Financialyear VM_FINANCIALYEAR { get; set; }

        public List<Country> VM_COUNTRIES { get; set; }

        public Country VM_COUNTRY { get; set; }

        public List<Currency> VM_CURRENCIES { get; set; }

        public Currency VM_CURRENCY { get; set; }

        public List<BusinessSector> VM_BUSINESS_SECTORS { get; set; }

        public BusinessSector VM_BUSINESS_SECTOR { get; set; }

        public List<ProjectSegment> VM_PROJECT_SEGMENTS { get; set; }

        public ProjectSegment VM_PROJECT_SEGMENT { get; set; }

        public List<BankInfo> VM_BANK_INFOS { get; set; }

        public BankInfo VM_BANK_INFO { get; set; }

        public List<BankInfoType> VM_BANK_INFO_TYPES { get; set; }

        public BankInfoType VM_BANK_INFO_TYPE { get; set; }

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

        public UserRole VM_USER_ROLE { get; set; }

        public List<UserRole> VM_USER_ROLES { get; set; }

        public CompanyModule VM_COMPANY_MODULE { get; set; }

        public List<CompanyModule> VM_COMPANY_MODULES { get; set; }

        public CompanyUserMap VM_COMPANY_USER_MAP { get; set; }

        public List<CompanyUserMap> VM_COMPANY_USER_MAPS { get; set; }

        public List<TreeMenu> VM_TREE_MENUS { get; set; }

        public InvManufacturer VM_INVMANUFACTURER { get; set; }

        public List<InvManufacturer> VM_INVMANUFACTURERS { get; set; }

        public InvUnitMeasurement VM_INVUNIT_MEASUREMENT { get; set; }

        public List<InvUnitMeasurement> VM_INVUNIT_MEASUREMENTS { get; set; }

        public InvSupplier VM_INVSUPPLIER { get; set; }

        public List<InvSupplier> VM_INVSUPPLIERS { get; set; }

        public InvSupplierType VM_INVSUPPLIER_TYPE { get; set; }

        public List<InvSupplierType> VM_INVSUPPLIER_TYPES { get; set; }

        public AnFCOA VM_ANFCOA { get; set; }

        public List<AnFCOA> VM_ANFCOAS { get; set; }

        public InvWarehouseType VM_INVWAREHOUSE_TYPE { get; set; }

        public List<InvWarehouseType> VM_INVWAREHOUSE_TYPES { get; set; }

        public InvWarehouse VM_INVWAREHOUSE { get; set; }

        public List<InvWarehouse> VM_INVWAREHOUSES { get; set; }

        public InvRackType VM_INVRACK_TYPE { get; set; }

        public List<InvRackType> VM_INVRACK_TYPES { get; set; }

        public InvRack VM_INVRACK { get; set; }

        public List<InvRack> VM_INVRACKS { get; set; }

        public InvDamageType VM_INVDAMAGE_TYPE { get; set; }

        public List<InvDamageType> VM_INVDAMAGE_TYPES { get; set; }

        public InvReturnType VM_INVRETURN_TYPE { get; set; }

        public List<InvReturnType> VM_INVRETURN_TYPES { get; set; }

        public SlsSDPType VM_SLSSDP_TYPE { get; set; }

        public List<SlsSDPType> VM_SLSSDP_TYPES { get; set; }

        public SlsSDP VM_SLSSDP { get; set; }

        public List<SlsSDP> VM_SLSSDPS { get; set; }

        public BnkBranch VM_BNK_BRANCH { get; set; }

        public List<BnkBranch> VM_BNK_BRANCHES { get; set; }

        public BnkAccountType VM_BNK_ACCOUNT_TYPE { get; set; }

        public List<BnkAccountType> VM_BNK_ACCOUNT_TYPES { get; set; }

        public HrmDesignation VM_HRM_DESIGNATION { get; set; }

        public List<HrmDesignation> VM_HRM_DESIGNATIONS { get; set; }

        public HrmSection VM_HRM_SECTION { get; set; }

        public List<HrmSection> VM_HRM_SECTIONS { get; set; }

        public HrmSubSection VM_HRM_SUB_SECTION { get; set; }

        public List<HrmSubSection> VM_HRM_SUB_SECTIONS { get; set; }

        public HrmEmployeeType VM_HRM_EMPLOYEE_TYPE { get; set; }

        public List<HrmEmployeeType> VM_HRM_EMPLOYEE_TYPES { get; set; }

        public HrmEmploymentType VM_HRM_EMPLOYMENT_TYPE { get; set; }

        public List<HrmEmploymentType> VM_HRM_EMPLOYMENT_TYPES { get; set; }

        public HrmShift VM_HRM_SHIFT { get; set; }

        public List<HrmShift> VM_HRM_SHIFTS { get; set; }

        public HrmEmployeeOfficial VM_HRM_EMPLOYEE_OFFICIAL { get; set; }

        public List<HrmEmployeeOfficial> VM_HRM_EMPLOYEE_OFFICIALS { get; set; }

        public List<HrmEmployeeAcademicInfo> VM_HRM_EMPLOYEE_ACADEMIC_INFOS { get; set; }

        public HrmEmployeeAcademicInfo VM_HRM_EMPLOYEE_ACADEMIC_INFO { get; set; }

        public List<HrmEducationLevel> VM_HRM_EDUCATIONS_LEVELS { get; set; }

        public HrmEducationLevel VM_HRM_EDUCATIONS_LEVEL { get; set; }

        public HrmEmployeePersonalInfo VM_HRM_EMPLOYEE_PERSONAL_INFO { get; set; }

        public List<HrmEmployeePersonalInfo> VM_HRM_EMPLOYEE_PERSONAL_INFOS { get; set; }

        public HrmEmployeeRelation VM_HRM_EMPLOYEE_RELATION { get; set; }

        public List<HrmEmployeeRelation> VM_HRM_EMPLOYEE_RELATIONS { get; set; }


        public Group VM_GROUP { get; set; }

        public List<Group> VM_GROUPS { get; set; }

    }

   
}