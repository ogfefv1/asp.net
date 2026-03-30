using System;

namespace AspKnP231.Services.Time
{
    public class SqlDateTimeService : IDateTimeService
    {
        public string GetDate() => DateTime.Now.ToString("yyyy-MM-dd");

        public string GetTime() => DateTime.Now.ToString("HH:mm:ss.fff");
    }
}