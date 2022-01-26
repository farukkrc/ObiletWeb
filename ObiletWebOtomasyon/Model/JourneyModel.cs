using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletWebOtomasyon.Model
{
    public class JourneyModel
    {
        // 2+1, 2+2 vs
        public string SeatCategory { get; set; }

        // 10:30, 12:45 vs
        public DateTime DepartureHour { get; set; }
    }
}
