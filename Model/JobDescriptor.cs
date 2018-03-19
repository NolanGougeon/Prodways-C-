using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Fabrication.UserControls.Model
{

    public class MetaDataProperty
    {
        [XmlElement("key")]
        public string Key { get; set; }
        [XmlElement("value")]
        public string Value { get; set; }
    }
    [XmlRoot("cereal")]
    public class JobDescriptor : EventDescriptor
    {
        ///////////////////////////////////////////////////////////////////
        // Member
        ///////////////////////////////////////////////////////////////////
        public enum Note
        {
            NonAssigned,
            VeryBad,
            Bad,
            Normal,
            Good,
            VeryGood
        }
        [XmlElement("number_of_layers")]
        public int Layers { get; set; }
        public string LayersThickness {
            get
            {
                MetaDataProperty property = new MetaDataProperty();
                if (MetaData != null)
                {
                    property = MetaData.FirstOrDefault(e => e.Key == "build style");
                }
                if(property!=null) return property.Value;
                return "";
            }
            set
            {
            }
        }

        public string Quality {
            get
            {
                MetaDataProperty property = new MetaDataProperty();
                if (MetaData != null)
                {
                    property = MetaData.FirstOrDefault(e => e.Key == "build mode");
                }
                if (property != null) return property.Value;
                return "";
            }
            set
            {
            }
        }

        public string Resin
        {
            get
            {
                MetaDataProperty property = new MetaDataProperty();
                if (MetaData != null)
                {
                    property = MetaData.FirstOrDefault(e => e.Key == "resin");
                }
                if (property != null) return property.Value;
                return "";
            }
            set
            {
            }
        }

        public string Seamline
        {
            get
            {
                MetaDataProperty property = new MetaDataProperty();
                if (MetaData != null)
                {
                    property = MetaData.FirstOrDefault(e => e.Key == "seamline");
                }
                if (property != null) return property.Value;
                return "";
            }
            set{}
        }
        private bool _isCompleted;
        [XmlElement("completed")]
        public bool IsCompleted
        {
            get
            {
                return IsCompleted;
            }
            set
            {
                _isCompleted = value;
                if (_isCompleted)
                {
                    State = "Completed";
                }
                else
                {
                    State = "Failed";
                }
            }
        }
        public string State{ get; set; }
        public String ScreenShot { get; set; }
        [XmlElement("current_layer")]
        public uint CurrentLayer { get; set; }
        public Note NoteMember { get; set; }
        [XmlArray("parts")]
        [XmlArrayItem("part")]
        public List<string> PartsList { get; set; }
        public string FileLogUrl { get; set; }
        [XmlArray("metadata")]
        [XmlArrayItem("data")]
        public List<MetaDataProperty> MetaData { get; set; }
        public bool AlreadyInsert { get; set; }
        ///////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        ///////////////////////////////////////////////////////////////////
        public JobDescriptor(string type,string title, DateTime start, DateTime end,bool realStart, bool realEnd, int layers, string layersThickness, string quality, string state, List<string> partsList) : base(type,title, start, end,realStart,realEnd)
        {
            Layers = layers;
            State = state;
            PartsList = partsList;
            MetaData = new List<MetaDataProperty>();
        }

        public JobDescriptor()
        {
            MetaData = new List<MetaDataProperty>();
            Layers = 0;
            Quality = Quality;
            State = State;
            ScreenShot = String.Empty;
            CurrentLayer = 0;
            NoteMember = Note.NonAssigned;
            PartsList = new List<string>();
            State = "Failed";
            AlreadyInsert = false;
        }

        ///////////////////////////////////////////////////////////////////
        // PROPERTIES
        ///////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////
        // METHODS
        ///////////////////////////////////////////////////////////////////
    }
}
