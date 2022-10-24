namespace API_Challenge.Models
{
    public class Holiday
    {
        public Holiday(int day, int month, string holiday)
        {
            Day = day;
            Month = month;
            HolidayName = holiday;
        }

        public int Day { get; set; }
        public int Month { get; set; }
        public string HolidayName { get; set; }
    }
}
