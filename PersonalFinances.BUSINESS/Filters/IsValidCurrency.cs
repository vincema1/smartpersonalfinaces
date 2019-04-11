using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PersonalFinances.BUSINESS.Filters
{
    public class IsValidCurrency : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            string stringValue = value as string;
            if (stringValue == null) return false;

            //checking characters
            for (int i = 0; i < stringValue.Length; i++)
                if (("1234567890,.").IndexOf(stringValue[i].ToString()) == -1) return false;

            //Only one comma, neither first nor last position
            string[] arrTmp= stringValue.Split(',');
            if (arrTmp.Count() != 2)
                return false;

            //only two figures after the comma
            if (arrTmp[1].Length != 2)
                return false;

            //only numbers after the comma
            for (int i = 0; i < arrTmp[1].Length; i++)
                if (("1234567890").IndexOf(arrTmp[1][i].ToString()) == -1) return false;
         

            //checking the presence of . in the integer part
            string[] ThousandBlocks = arrTmp[0].Split('.');
            
            //The first block of thousands carnot be > 3
            //From the second on,their length must be 3
            if (ThousandBlocks.Count()>1)
            {
                if (ThousandBlocks[0].Length > 3)
                    return false;

                for (int i = 1; i < ThousandBlocks.Count(); i++)
                    if (ThousandBlocks[i].Length != 3)
                        return false;
            }

            return true;

        }

    }
}
