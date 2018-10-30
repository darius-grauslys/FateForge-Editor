using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge.Managers
{
    public static class EventFieldManager
    {
        private static List<EventField> _eventFields = new List<EventField>();

        public static List<EventField> EventFields { get => _eventFields.ToList(); private set => _eventFields = value; }
        private static List<EventField> PrivateEventFields { get => _eventFields; }

        public static void AddEvent(EventField _event)
        {
            PrivateEventFields.Add(_event);
            AssignNames();
        }

        public static void RemoveEvent(EventField _event)
        {
            PrivateEventFields.Remove(_event);
            AssignNames();
        }

        private static void AssignNames()
        {
            for (int i = 0; i < PrivateEventFields.Count; i++)
            {
                PrivateEventFields[i].UniqueIdentifier = "_e"+i;
            }
        }
    }
}
