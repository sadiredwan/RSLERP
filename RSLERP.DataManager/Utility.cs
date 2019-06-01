using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
namespace RSLERP.DataManager
{
    public class Utility
    {
        public List<T> produceList<T>(IDataReader dataReader)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            var properties = typeof(T).GetProperties();

            List<T> lst = new List<T>();
            while (dataReader.Read())
            {

                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    try
                    {
                        if (dataReader[pro.Name] != null && !dataReader[pro.Name].ToString().Equals(""))
                        {
                            if (pro.PropertyType == typeof(System.DateTime))
                            {
                                pro.SetValue(objT, dataReader[pro.Name], null);
                            }
                            else if (pro.PropertyType == typeof(Nullable<System.DateTime>))
                            {
                                pro.SetValue(objT, dataReader[pro.Name], null);
                            }
                            else if (pro.PropertyType == typeof(System.Byte[]))
                            {
                                pro.SetValue(objT, (Byte[])dataReader[pro.Name], null);
                            }
                            else if (pro.PropertyType == typeof(Nullable<System.Int32>))
                            {
                                pro.SetValue(objT, Convert.ToInt32(dataReader[pro.Name].ToString()), null);
                            }
                            else if (pro.PropertyType == typeof(System.Int32))
                            {
                                pro.SetValue(objT, Convert.ToInt32(dataReader[pro.Name].ToString()), null);
                            }
                            else if (pro.PropertyType == typeof(System.Double))
                            {
                                pro.SetValue(objT, Convert.ToDouble(dataReader[pro.Name].ToString()), null);
                            }
                            else if (pro.PropertyType == typeof(Nullable<System.Double>))
                            {
                                pro.SetValue(objT, Convert.ToDouble(dataReader[pro.Name].ToString()), null);
                            }
                            else if (pro.PropertyType == typeof(System.Boolean))
                            {
                                pro.SetValue(objT, (Convert.ToBoolean(dataReader[pro.Name])), null);
                            }
                            else if (pro.PropertyType == typeof(System.Decimal))
                            {
                                pro.SetValue(objT, (Convert.ToDecimal(dataReader[pro.Name])), null);
                            }
                            else if (pro.PropertyType == typeof(Nullable<System.Decimal>))
                            {
                                pro.SetValue(objT, (Convert.ToDecimal(dataReader[pro.Name])), null);
                            }
                            else if (pro.PropertyType == typeof(Nullable<System.Boolean>))
                            {
                                pro.SetValue(objT, Convert.ToBoolean(dataReader[pro.Name]), null);
                            }
                            else if (pro.PropertyType == typeof(System.Guid))
                            {
                                pro.SetValue(objT, (new Guid(dataReader[pro.Name].ToString())), null);
                            }
                            else if (pro.PropertyType == typeof(Nullable<System.Guid>))
                            {
                                pro.SetValue(objT, (new Guid(dataReader[pro.Name].ToString())), null);
                            }
                            else
                            {
                                pro.SetValue(objT, dataReader[pro.Name].ToString(), null);
                            }
                        }
                        else
                            pro.SetValue(objT, "", null);
                    }
                    catch (Exception ex)
                    {
                        //pro.SetValue(objT, "", null);
                    }
                }

                lst.Add(objT);

            }
            return lst;
        }

        public List<DynamicClass> MakeDynamicList(DataTable dtresult)
        {
            List<DynamicClass> lst = new List<DynamicClass>();
            var fields = new List<Field>();
            foreach (DataColumn dc in dtresult.Columns)
            {
                fields.Add(new Field(dc.ColumnName, dc.DataType));

            }


            foreach (DataRow drin in dtresult.Rows)
            {
                dynamic obj = new DynamicClass(fields);
                var properties = obj.GetType().GetProperties();

                foreach (var field in fields)
                {
                    obj.setMember(field.FieldName, drin[field.FieldName]);
                }
                lst.Add(obj);
            }

            return lst;
        }

        public string Encode(string original)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(original);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static class Application
        {
            public static ApplicationState CurrentState()
            {
                int app_id = Convert.ToInt32(HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID]);
                return new DBContext().ApplicationStates.Find(app_id);
            }
        }

        public static class errorstate
        {
            public static string errors(ModelStateDictionary modelstate, int rows = 0)
            {
                StringBuilder sb = new StringBuilder();
                var errors = modelstate.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .ToList();
                int i = 1;
                if (errors.Count > 0)
                {
                    sb.Append("<ul style='font-color:red'>");
                    foreach (ModelErrorCollection mdl in errors)
                    {
                        sb.Append("<li>");
                        sb.Append(mdl[0].ErrorMessage);
                        sb.Append("</li>");
                        if (i == rows)
                        {
                            break;
                        }
                        i++;
                    }
                    sb.Append("</ul>");
                }

                return sb.ToString();
            }



        }

    }

    public static class CurentApplication
    {
        public static ApplicationState CurrentState()
        {
            int app_id = Convert.ToInt32(HttpContext.Current.Application[GLobalSessionName.GLOBAL_APPLICATION_ID]);
            return new DBContext().ApplicationStates.Find(app_id);
        }
    }
}
