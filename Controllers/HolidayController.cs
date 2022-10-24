using API_Challenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : ControllerBase
    {
        [HttpGet(Name = "GetIsHoliday")]
        public string Get(DateTime dayToCheck)
        {
            List<Holiday> holidays = PickListHolidays(dayToCheck);
            return checkIsHoliday(dayToCheck, holidays);
        }        

        private List<Holiday> PickListHolidays(DateTime dayToCheck)
        {
            int year = dayToCheck.Year;
            DateTime easter = PickEaster(year);
            bool isLeapYear = CheckisLeapYear(year);
            DateTime carnival = !isLeapYear ? easter.AddDays(-47) : easter.AddDays(-48);
            var holidays = ListHolidays(easter, carnival);
            return holidays;
        }                 

        private DateTime PickEaster(int easterYear)
        {
            int lunarCyclePosition = easterYear % 19;
            int century = easterYear / 100;
            int daysBetweenEquinoxAndFullmoon = (century - (int)(century / 4) - (int)((8 * century + 13) / 25) + 19 * lunarCyclePosition + 15) % 30;
            int daysBetweenFullmoonAndFirstSunday = daysBetweenEquinoxAndFullmoon - (int)(daysBetweenEquinoxAndFullmoon / 28) * (1 - (int)(daysBetweenEquinoxAndFullmoon / 28) * (int)(29 / (daysBetweenEquinoxAndFullmoon + 1)) * (int)((21 - lunarCyclePosition) / 11));

            int easterDay = daysBetweenFullmoonAndFirstSunday - ((easterYear + (int)(easterYear / 4) + daysBetweenFullmoonAndFirstSunday + 2 - century + (int)(century / 4)) % 7) + 28;
            int easterMonth = 3;

            if (easterDay > 31)
            {
                easterMonth++;
                easterDay -= 31;
            }

            DateTime easter = new DateTime(easterYear, easterMonth, easterDay);
            return easter;

            //https://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year
        }

        private string checkIsHoliday(DateTime dayToCheck, List<Holiday> holidays)
        {
            foreach (var holiday in holidays)
            {
                if (dayToCheck.Day == holiday.Day && dayToCheck.Month == holiday.Month)
                {
                    return "É um dia de feriado!";
                }
            }
            return "Não é um dia de feriado.";
        }

        private bool CheckisLeapYear(int year)
        {
            if ((year % 400) == 0)
            {
                return true;
            }
            else
            {
                if ((year % 100) == 0)
                {
                    return false;
                }
                else
                {
                    return (year % 4) == 0;
                }
            }
        }

        private static List<Holiday> ListHolidays(DateTime easter, DateTime carnival)
        {
            List<Holiday> holidays = new List<Holiday>
            {
                new Holiday(1,1,"New Year's Day"),
                new Holiday(carnival.Day,carnival.Month, "Carnival"),
                new Holiday(easter.AddDays(-2).Day,easter.AddDays(-2).Month,"Good Friday"),
                new Holiday(easter.Day,easter.Month,"Easter"),
                new Holiday(21,4,"Tiradentes Holiday"),
                new Holiday(1,5,"Workers Day"),
                new Holiday(easter.AddDays(60).Day,easter.AddDays(60).Month,"Corpus Christi"),
                new Holiday(7,9, "Independence of Brazil"),
                new Holiday(12,10,"Day of Our Lady"),
                new Holiday(2,11, "All Souls' Day"),
                new Holiday(15,11, "Proclamation of the Republic"),
                new Holiday(25,12, "Chrismas"),
            };
            return holidays;
        }
    }
}