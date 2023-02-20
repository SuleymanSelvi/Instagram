using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Ultitites
    {
        public static string RelativeDate(DateTime theDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - theDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 saniye önce" : ts.Seconds + " saniye önce";

            if (delta < 2 * MINUTE)
                return " dakika önce";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " dakika önce";

            if (delta < 90 * MINUTE)
                return " saat önce";

            if (delta < 24 * HOUR)
                return ts.Hours + " saat önce";

            if (delta < 48 * HOUR)
                return "dün";

            if (delta < 30 * DAY)
                return ts.Days + " gün önce";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 ay önce" : months + " ay önce";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 yıl önce" : years + " yıl önce";
            }
        }
    }
}
