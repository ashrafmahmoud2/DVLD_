using System;
using System.Diagnostics;

namespace DVlD.GenrelClass
{
    public class clsEventLog
    {
        private static string sourceName = "DVLD2";

        public static void EventLogInformation(string Message)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            EventLog.WriteEntry(sourceName, Message, EventLogEntryType.Information);
        }

        public static void EventLogError(string Message)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            EventLog.WriteEntry(sourceName, Message, EventLogEntryType.Error);
        }

        public static void EventLogWarning(string Message)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            EventLog.WriteEntry(sourceName, Message, EventLogEntryType.Warning);
        }
    }
}
