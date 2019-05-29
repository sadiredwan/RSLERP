using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace demo.Models
{
    public class user
    {
        [Required]
		[UserValidation(ErrorMessage = "{0} is invaid")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("([0-9]+)",ErrorMessage ="{0} Must Be Numeric")]  
        public string phoneNo { get; set; }

        public string WillAttend { get; set; }

      
        public List<checkbox> checkBoxLst { get; set; }
    }

    public class dropdown
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class checkbox
    {
        public string name { get; set; }
        public string value { get; set; }
    }
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class UserValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (!(value is string)) return false;

        var val = (string) value;
            //var bytes = Enumerable
            //    .Range(0, val.Length / 2)
            //    .Select(x => Byte.Parse(val.Substring(2 * x, 2), NumberStyles.HexNumber))
            //    .ToArray();
            return val.Length == 5;
    }

    public override string FormatErrorMessage(string name)
    {
        return base.FormatErrorMessage(name);
    }
}

    public static class errorstate
    {
        public static string errors(ModelStateDictionary modelstate,int rows=0)
        {
            StringBuilder sb = new StringBuilder();
            var errors = modelstate.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();
            int i = 1;
            if(errors.Count>0)
            {
                sb.Append("<ul style='font-color:red'>");
                foreach (ModelErrorCollection mdl in errors)
                {
                    sb.Append("<li>");
                    sb.Append(mdl[0].ErrorMessage);
                    sb.Append("</li>");
                    if(i==rows)
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