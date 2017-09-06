using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeGeneratorService
    {
        private static readonly string Consonant = "bcdfghjklmnpqrstvwxyz";
        private static readonly string Vowels = "aeiou";
        
        public string Generate(HttpContext context)
        {
            string resStr = String.Empty;
            
            string conCode = GetShortStr(Int32.Parse(context.Request.Query["id"]), Consonant);
            string vowCode = GetShortStr(Int32.Parse(context.Request.Query["id"]), Vowels);

            for (int i = 0; i < 3; i++)
            {
                resStr += conCode[i];
                resStr += vowCode[i];
            }

            return resStr.ToUpper();
        }

        private string GetShortStr(int id, string str, int strSize = 3)
        {
            string result = String.Empty;
            int addNum = id;

            for (int i = 0; i < strSize; i++)
            {
                if (!(addNum >= 0 && addNum < str.Length))
                {
                    int num = addNum % str.Length;
                    result += str[num];
                }
                else
                    result += str[addNum];

                addNum += id;
            }

            return result;
        }
    }
}
