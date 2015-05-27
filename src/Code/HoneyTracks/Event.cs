using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoneyTracks
{
    public class TrackingEvent
    {
        public string Action;
        public Dictionary<string, string> EventData;

        public TrackingEvent(string action)
        {
            Action = action;
            EventData = new Dictionary<string, string>();
        }

        public TrackingEvent SetData(List<KeyValuePair<string, string>> data)
        {
            for (int i = 0; i < data.Count; ++i)
            {
                EventData[data[i].Key] = data[i].Value;
            }
            return this;
        }

        public TrackingEvent SetData(string key, string value)
        {
            EventData[key] = value;
            return this;
        }

        public string ToJson()
        {
            Dictionary<string, object> o = new Dictionary<string, object>();
            o["action"] = Action;
            o["eventData"] = EventData;
            return MiniJSON.Json.Serialize(o);
        }

        public override string ToString()
        {
            return ToJson();
        }

        public static TrackingEvent FromJson(string json)
        {
            try
            {
                Dictionary<string, object> o = MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
                var e = new TrackingEvent(o["action"].ToString());
                Dictionary<string, object> ed = o["eventData"] as Dictionary<string, object>;
                if (ed != null)
                {
                    foreach (var pair in ed)
                    {
                        e.SetData(pair.Key, pair.Value.ToString());
                    }
                }
                return e;
            }
            catch
            {
                throw new System.Exception("Failed parsing event json: " + json);
            }
        }
    }
    
}

