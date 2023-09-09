using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Generator
{
    public class NameGenerator
    {

        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GenerateUnipNDigitCode(int randomDigit)
        {
            // Number of digits for random number to generate

            int _max = (int)Math.Pow(10, randomDigit);
            Random _rdm = new Random();
            int _out = _rdm.Next(0, _max);

            while (randomDigit != _out.ToString().ToArray().Distinct().Count())
            {
                _out = _rdm.Next(0, _max);
            }
            return _out.ToString();
        }
    }
}
