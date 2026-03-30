using System;

namespace AspKnP231.Services.Time
{
    public class NationalDateTimeService : IDateTimeService
    {
        public string GetDate() => DateTime.Now.ToString("dd.MM.yyyy");

        public string GetTime() => DateTime.Now.ToString("HH:mm:ss");
    }
}