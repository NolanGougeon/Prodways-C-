using CoreCS.UI;
using Fabrication.UserControls.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fabrication.UserControls.ViewModel
{
    public class EventViewModel : ProjectViewModelBase, IDisposable
    {
        
        
        ///////////////////////////////////////////////////////////////////
        // Member
        ///////////////////////////////////////////////////////////////////
        private PrinterDataModel _printerDataModel;
        ///////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        ///////////////////////////////////////////////////////////////////
        public EventViewModel()
        {
            _printerDataModel = PrinterDataModel.Instance;
        }
        ///////////////////////////////////////////////////////////////////
        // PROPERTIES
        ///////////////////////////////////////////////////////////////////
        public System.Windows.Media.Effects.ShaderEffect BackgroundEffect
        {
            get { return Resources.ColorEffectLibrary.Instance.BackgroundEffect; }
        }

        #region statisticsBinding
        //Binding for  the number of Job in Statistics
        private int _jobNbText;
        public int JobNbText
        {
            get { return _jobNbText; }
            set
            {
                if (_jobNbText!=value)
                {
                    _jobNbText = value;
                    OnPropertyChanged("JobNbText");
                }
            }
        }
        //Binding for  the number of parts printed in Statistics
        private int _partsPrintedNbText;
        public int PartsPrintedNbText
        {
            get { return _partsPrintedNbText; }
            set
            {
                if (_partsPrintedNbText!=value)
                {
                    _partsPrintedNbText = value;
                    OnPropertyChanged("PartsPrintedNbText");
                }
            }
        }
        //Binding for  the number of parts printed in Statistics
        private int _resinConsumptionNbText;
        public int ResinConsumptionNbText
        {
            get { return _resinConsumptionNbText; }
            set
            {
                if (_resinConsumptionNbText != value)
                {
                    _resinConsumptionNbText = value;
                    OnPropertyChanged("ResinConsumptionNbText");
                }
            }
        }
        //Binding for  the number of parts printed in Statistics
        private DateTime _lastPinsAdjsutementNbText = DateTime.MinValue;
        public DateTime LastPinsAdjsutementNbText
        {
            get { return _lastPinsAdjsutementNbText; }
            set
            {
                if (_lastPinsAdjsutementNbText != value)
                {
                    _lastPinsAdjsutementNbText = value;
                    OnPropertyChanged("LastPinsAdjsutementNbText");
                }
            }
        }
        #endregion

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }
        #region DayAndWeekBinding
        private string _monday;
        public string Monday
        {
            get { return _monday; }
            set
            {
                if (_monday != value)
                {
                    _monday = value;
                    OnPropertyChanged("Monday");
                }
            }
        }

        private string _tuesday;
        public string Tuesday
        {
            get { return _tuesday; }
            set
            {
                if (_tuesday != value)
                {
                    _tuesday = value;
                    OnPropertyChanged("Tuesday");
                }
            }
        }
        private string _wednesday;
        public string Wednesday
        {
            get { return _wednesday; }
            set
            {
                if (_wednesday != value)
                {
                    _wednesday = value;
                    OnPropertyChanged("Wednesday");
                }
            }
        }
        private string _thursday;
        public string Thursday
        {
            get { return _thursday; }
            set
            {
                if (_thursday != value)
                {
                    _thursday = value;
                    OnPropertyChanged("Thursday");
                }
            }
        }
        private string _friday;
        public string Friday
        {
            get { return _friday; }
            set
            {
                if (_friday != value)
                {
                    _friday = value;
                    OnPropertyChanged("Friday");
                }
            }
        }
        private string _saturday;
        public string Saturday
        {
            get { return _saturday; }
            set
            {
                if (_saturday != value)
                {
                    _saturday = value;
                    OnPropertyChanged("Saturday");
                }
            }
        }

        private string _sunday;
        public string Sunday
        {
            get { return _sunday; }
            set
            {
                if (_sunday != value)
                {
                    _sunday = value;
                    OnPropertyChanged("Sunday");
                }
            }
        }

        private string _week;
        public string Week
        {
            get { return _week; }
            set
            {
                if (_week != value)
                {
                    _week = value;
                    OnPropertyChanged("Week");
                }
            }
        }
        #endregion
        #region DaySelectedBinding

        
        private string _colorMonday;
        public string ColorMonday
        {
            get { return _colorMonday; }
            set
            {
                if (_colorMonday != value)
                {
                    _colorMonday = value;
                    OnPropertyChanged("ColorMonday");
                }
            }
        }

        private string _colorTuesday;
        public string ColorTuesday
        {
            get { return _colorTuesday; }
            set
            {
                if (_colorTuesday != value)
                {
                    _colorTuesday = value;
                    OnPropertyChanged("ColorTuesday");
                }
            }
        }
        private string _colorWednesday;
        public string ColorWednesday
        {
            get { return _colorWednesday; }
            set
            {
                if (_colorWednesday != value)
                {
                    _colorWednesday = value;
                    OnPropertyChanged("ColorWednesday");
                }
            }
        }
        private string _colorThursday;
        public string ColorThursday
        {
            get { return _colorThursday; }
            set
            {
                if (_colorThursday != value)
                {
                    _colorThursday = value;
                    OnPropertyChanged("ColorThursday");
                }
            }
        }
        private string _colorFriday;
        public string ColorFriday
        {
            get { return _colorFriday; }
            set
            {
                if (_colorFriday != value)
                {
                    _colorFriday = value;
                    OnPropertyChanged("ColorFriday");
                }
            }
        }
        private string _colorSaturday;
        public string ColorSaturday
        {
            get { return _colorSaturday; }
            set
            {
                if (_colorSaturday != value)
                {
                    _colorSaturday = value;
                    OnPropertyChanged("ColorSaturday");
                }
            }
        }

        private string _colorSunday;
        public string ColorSunday
        {
            get { return _colorSunday; }
            set
            {
                if (_colorSunday != value)
                {
                    _colorSunday = value;
                    OnPropertyChanged("ColorSunday");
                }
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////
        // METHODS
        ///////////////////////////////////////////////////////////////////
        public void SelectedDate_Changed(SelectionChangedEventArgs param)
        {
            SelectedDate = ((Calendar)param.Source).SelectedDate != null ? (DateTime)((Calendar)param.Source).SelectedDate: DateTime.MinValue;
            UpdatePlaningDates();
        }

        /// <summary>
        /// update the days of the week and the color of the column
        /// </summary>
        private void UpdatePlaningDates()
        {
            DateTime firstDayOfYear = new DateTime(SelectedDate.Year, 01,01);
            DateTime monday = DateTime.MinValue;
            ColorMonday = "#00FFFFFF";
            ColorTuesday = "#00FFFFFF";
            ColorWednesday = "#00FFFFFF";
            ColorThursday = "#00FFFFFF";
            ColorFriday = "#00FFFFFF";
            ColorSaturday = "#00FFFFFF";
            ColorSunday = "#00FFFFFF";
            switch (SelectedDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    monday = SelectedDate;
                    ColorMonday = "#60FFFFFF";
                    break;
                case DayOfWeek.Tuesday:
                    monday = SelectedDate.AddDays(-1);
                    ColorTuesday = "#60FFFFFF";
                    break;
                case DayOfWeek.Wednesday:
                    monday = SelectedDate.AddDays(-2);
                    ColorWednesday = "#60FFFFFF";
                    break;
                case DayOfWeek.Thursday:
                    monday = SelectedDate.AddDays(-3);
                    ColorThursday = "#60FFFFFF";
                    break;
                case DayOfWeek.Friday:
                    monday = SelectedDate.AddDays(-4);
                    ColorFriday = "#60FFFFFF";
                    break;
                case DayOfWeek.Saturday:
                    monday = SelectedDate.AddDays(-5);
                    ColorSaturday = "#60FFFFFF";
                    break;
                case DayOfWeek.Sunday:
                    monday = SelectedDate.AddDays(-6);
                    ColorSunday = "#60FFFFFF";
                    break;
            }
            Monday = String.Format("Mon {0}", monday.Day);
            Tuesday = String.Format("Tue {0}", monday.AddDays(1).Day);
            Wednesday = String.Format("Wed {0}", monday.AddDays(2).Day);
            Thursday = String.Format("Thu {0}", monday.AddDays(3).Day);
            Friday = String.Format("Fri {0}", monday.AddDays(4).Day);
            Saturday = String.Format("Sat {0}", monday.AddDays(5).Day);
            Sunday = String.Format("Sun {0}", monday.AddDays(6).Day);
            TimeSpan difference = monday-firstDayOfYear;
            Week = String.Format("Week {0}", (difference.Days / 7)+1);

        }

        public void Dispose()
        {

        }
    }
}
