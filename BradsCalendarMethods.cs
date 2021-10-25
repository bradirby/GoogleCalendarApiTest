using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using NUnit.Framework;

namespace iCalTest
{
    public class BradsCalendarMethods
    {
        public CalendarEvent CreateEvent()
        {
            var now = DateTime.Now;
            var later = now.AddHours(1);
            var e = new CalendarEvent
            {
                Start = new CalDateTime(now),
                End = new CalDateTime(later),
            };

            
            return e;
        }

        public CalendarService GetService()
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("GoogleCalendarApiTest_ApiKey");

                var svcInitializer = new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                    ApplicationName = string.Format("{0} API key example", "CalendarDesktopTestApp"),
                };
                return new CalendarService(svcInitializer);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create new Calendar Service", ex);
            }
        }
    }

    public class CalendarMethodsTest
    {

        [Test]
        public void CreateCalSvc()
        {
            var m = new BradsCalendarMethods();
            var svc = m.GetService();
            var calId = Environment.GetEnvironmentVariable("GoogleCalendarApiTest_CalendarId");
            svc.CalendarList.Get(calId).Execute();
        }

        [Test]
        public void CreateEventWithAttendee()
        {
            var c = new BradsCalendarMethods();
            var e = c.CreateEvent();
            var attendee = new Attendee
            {
                CommonName = "Rian Stockbower",
                Rsvp = true,
                Value = new Uri("mailto:rstockbower@gmail.com")
            };
            e.Attendees = new List<Attendee> {attendee};
            Assert.IsNotNull(e);
            Assert.AreEqual(1, e.Attendees.Count);
        }

        [Test]
        public void CreateEvent()
        {
            var c = new BradsCalendarMethods();
            var e = c.CreateEvent();
            Assert.IsNotNull(e);
        }

        [Test]
        public void CreateCalendar()
        {
            var c = new BradsCalendarMethods();
            var e = c.CreateEvent();
            var cal = new Calendar();
            cal.Events.Add(e);

        }
    }
}
