using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoneyTracks.Helper;
using HoneyTracks;

namespace HoneyTracks
{
    public class Transport 
    {
        /// <summary>
        /// this is a reference to the manager's list
        /// </summary>
        private List<TrackingEvent> undeliveredEvents;
        
        /// <summary>
        /// maximum number of undelivered events
        /// </summary>
        private int maxUndeliveredEvents;

        public Transport(List<TrackingEvent> undeliveredEvents, int maxUndeliveredEvents)
        {
            this.undeliveredEvents = undeliveredEvents;
            this.maxUndeliveredEvents = maxUndeliveredEvents;
        }

        public void Queue(TrackingEvent ev)
        {
            //Debug.Log(string.Format("enqueued event {0}", ev));
            undeliveredEvents.Add(ev);

            // enforce max number of undelivered events
            int droppedEvents = 0;
            while (undeliveredEvents.Count > maxUndeliveredEvents)
            {
                ++droppedEvents;
                undeliveredEvents.RemoveAt(0);
            }

            if (droppedEvents > 0)
            {
                Debug.LogWarning(string.Format("HONEYTRACKS: dropped {0} events because there are more than {1} undelivered events", droppedEvents, maxUndeliveredEvents));
            }
        }
    }
}
