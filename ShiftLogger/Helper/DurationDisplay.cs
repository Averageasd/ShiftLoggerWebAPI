namespace ShiftLogger.Helper
{
    public class DurationDisplay
    {
        public static string DurationString(DateTime t1, DateTime t2)
        {
            TimeSpan timeSpan = t2 - t1;
            string res = "";
            if (timeSpan.Hours > 0)
            {
                res += timeSpan.Hours + " hours, ";
            }
            if (timeSpan.Minutes > 0)
            {
                res += timeSpan.Minutes + " minutes";
            }

            if (res.Length == 0)
            {
                return "0 seconds";
            }

            return res;
        }
    }
}
