using System;
using System.Xml.Serialization;

namespace Fabrication.UserControls.Model
{
    public class EventDescriptor
    {
        ///////////////////////////////////////////////////////////////////
        // Member
        ///////////////////////////////////////////////////////////////////
        public string Type { get; set; }
        [XmlElement("date_start")]
        public DateTime Start { get; set; }
        [XmlElement("date_end")]
        public DateTime End { get; set; }
        [XmlElement("name")]
        public string Title { get; set; }
        [XmlIgnore]
        public bool RealStart { get; set; }
        [XmlIgnore]
        public bool RealEnd { get; set; }
        ///////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        ///////////////////////////////////////////////////////////////////
        public EventDescriptor(string type,string title, DateTime start, DateTime end,bool realStart,bool realEnd)
        {
            Type = type;
            Title = title;
            Start = start;
            End = end;
            RealStart = realStart;
            RealEnd = realEnd;
        }
        public EventDescriptor()
        {
            Type = "Fabrication";
            Title = "";
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            RealEnd = true;
            RealStart = true;
        }

        ///////////////////////////////////////////////////////////////////
        // PROPERTIES
        ///////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////
        // METHODS
        ///////////////////////////////////////////////////////////////////
    }
}
