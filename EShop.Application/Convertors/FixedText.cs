using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Convertors
{
    public class FixedText
    {
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }

        public static string FixDateToShamsi(DateTime? date)
        {
            PersianCalendar pc = new PersianCalendar();
            if (date.HasValue)
            {

                return $"{pc.GetYear(date.Value)}/{pc.GetMonth(date.Value)}/{pc.GetDayOfMonth(date.Value)}";
            }
            return "";
        }

        public static DateTime FixShamsiDateToAdDate(DateTime? date)
        {
            PersianCalendar pc = new PersianCalendar();
            if (date.HasValue)
            {
                DateTime adDateTime = pc.ToDateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0, 0);
                return adDateTime;
            }
            return DateTime.Now;
        }


















        public static string MoenyFormatter(int moneny)
        {
           
            string formattedNumber = moneny.ToString("N0");

            string[] groups = formattedNumber.Split(',');

            string result = string.Join(",", groups);
            return result;
        } 
        public static string MoenyFormatter(long moneny)
        {
           
            string formattedNumber = moneny.ToString("N0");

            string[] groups = formattedNumber.Split(',');

            string result = string.Join(",", groups);
            return result;
        }
    }
}
