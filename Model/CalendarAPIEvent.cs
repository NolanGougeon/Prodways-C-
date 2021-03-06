﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrication.UserControls.Model
{
    public class CalendarAPIEvent : EventDescriptor
    {
        ///////////////////////////////////////////////////////////////////
        // Member
        ///////////////////////////////////////////////////////////////////
        public new string Type { get; set; }
        public string Description { get; set; }
        ///////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        ///////////////////////////////////////////////////////////////////
        public CalendarAPIEvent(string type,string title, DateTime start, DateTime end,bool realStart,bool realEnd, string description) : base(type,title,start,end,realStart,realEnd)
        {
            Type = "Calendar Event";
            Description = description;
        }
        ///////////////////////////////////////////////////////////////////
        // PROPERTIES
        ///////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////
        // METHODS
        ///////////////////////////////////////////////////////////////////
    }
}
