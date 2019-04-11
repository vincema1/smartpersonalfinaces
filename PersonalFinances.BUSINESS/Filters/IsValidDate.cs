using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PersonalFinances.BUSINESS.Filters
{
    public class IsValidDate : ValidationAttribute
    {

        public override bool IsValid(object value)
        {

            string stringValue = value as string;
            if (stringValue == null) return false;

            //This regular expression checks the format dd/mm/yyyy. It does not check leap years, 
            //it takes wrong dates like 31/06/2017
            Match match = Regex.Match(stringValue, 
                                      @"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$", 
                                      RegexOptions.IgnoreCase);
            if (!match.Success) return false;
            
        
            //Further validation is needed.
            DateTime dtTmp;
            if (!DateTime.TryParse(stringValue, out dtTmp))
                return false;


            return true;
        
        }

    }
}