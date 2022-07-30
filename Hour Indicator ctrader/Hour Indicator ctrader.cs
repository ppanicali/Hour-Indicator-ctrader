// TIME TO TRADE INDICATOR by Paolo Panicali 20

using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
using cAlgo.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.CentralEuropeStandardTime, AccessRights = AccessRights.None)]
    public class HourIndicatorctrader : Indicator
    {
        [Parameter("Start Hour", Group = "Time To Trade", DefaultValue = 0, MinValue = 0, MaxValue = 23)]
        public int StartHour { get; set; }

        [Parameter("End Hour", Group = "Time To Trade", DefaultValue = 23, MinValue = 0, MaxValue = 23)]
        public int EndHour { get; set; }

        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }

        DateTime StartTime, EndTime;
        bool HourOk;

        public override void Calculate(int index)
        {
            DateTime DT, DC;
            DC = Bars.OpenTimes[index];
            HourOk = false;

            //IF START AND END HOUR ARE EQUAL THE RESULT IS ALWAYS TRUE
            if (StartHour == DC.Hour || EndHour == DC.Hour || StartHour == EndHour)
            {
                HourOk = true;
            }
            else
            {
                for (int i = 0; i <= 24; i++)
                {
                    DT = DC.AddHours(-i);

                    if (DT.Hour == EndHour)
                    {
                        break;
                    }


                    if (DT.Hour == StartHour)
                    {
                        StartTime = DT;
                        break;
                    }
                }

                for (int i = 0; i <= 24; i++)
                {
                    DT = DC.AddHours(i);

                    if (DT.Hour == StartHour)
                    {
                        break;
                    }

                    if (DT.Hour == EndHour)
                    {
                        EndTime = DT;
                        break;
                    }
                }
                HourOk = Bars.OpenTimes[index] >= StartTime && Bars.OpenTimes[index] <= EndTime ? true : false;
            }

            Result[index] = HourOk == true ? 1 : 0;
        }
    }
}
