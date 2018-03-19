using CoreCS.UI;
using Fabrication.UserControls.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Fabrication.UserControls.ViewModel
{
    class JobMiniatureViewModel : ProjectViewModelBase, IDisposable
    {
        ///////////////////////////////////////////////////////////////////
        // Member
        ///////////////////////////////////////////////////////////////////

        public JobDescriptor _jobDescriptor = null;
        ///////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        ///////////////////////////////////////////////////////////////////
        public JobMiniatureViewModel() {
            PartsListVisibility = Visibility.Collapsed;
        }


        ///////////////////////////////////////////////////////////////////
        // PROPERTIES
        ///////////////////////////////////////////////////////////////////
        private ICommand _displayLog;
        public ICommand DisplayLog
        {
            get
            {
                if (_displayLog == null)
                {
                    _displayLog = new RelayCommand(param => DisplayLog_Execute());
                }
                return _displayLog;
            }
        }
        private Visibility _partsListVisibility;
        public Visibility PartsListVisibility
        {
            get { return _partsListVisibility; }
            set
            {
                if (_partsListVisibility != value)
                {
                    _partsListVisibility = value;
                    OnPropertyChanged("PartsListVisibility");
                }
            }
        }

        public string ScreenShotPreview
        {
            get
            {
                if(_jobDescriptor != null)
                {
                    if (!String.IsNullOrEmpty(_jobDescriptor.ScreenShot))
                        return _jobDescriptor.ScreenShot;
                }
                return @"pack://application:,,,/Fabrication;component/Resources/MenuIcons/Help.png";
            }
        }

        
        public string TitlePreview
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    string title = _jobDescriptor.Title;
                    title = title.Replace(" ", "_");
                    if (title.Length > 33)
                    {
                        title = title.Remove(33);
                        title = string.Format(title + "...");
                    }
                    return title;
                }
                return "";
            }
        }
        
        public string StatePreview
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    return string.Format("{0} on {1}  at {2}", _jobDescriptor.State, _jobDescriptor.End.Date.ToShortDateString(), _jobDescriptor.End.ToShortTimeString());
                }
                return "";
            }
        }
        
        public string LayersPreview
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    return string.Format("{0}/{1} layers", Convert.ToInt32(_jobDescriptor.CurrentLayer), Convert.ToInt32(_jobDescriptor.Layers));
                }
                return "";
            }
        }
        
        public string MetaDataRight
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    return DisplayMetaData(false);
                }
                return "";
            }
        }
        
        public string MetaDataLeft
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    return DisplayMetaData(true);
                }
                return "";
            }
        }

        public List<string> PartsListPreview
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    List<string> NamePartList = new List<string>();
                    foreach (var part in _jobDescriptor.PartsList)
                    {
                        FileInfo NamePart = new FileInfo(part);
                        NamePartList.Add(NamePart.Name);
                    }
                    
                    return NamePartList;
                }
                return null;
            }
        }

        public string PercentProgressBar
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    if (_jobDescriptor.State == "Completed")
                    {
                        return "100";
                    }
                    else if (_jobDescriptor.State == "Failed")
                    {
                        double percentLayers = Convert.ToDouble((_jobDescriptor.CurrentLayer * 100) / _jobDescriptor.Layers);
                        return string.Format(Convert.ToString(percentLayers));
                    }
                    else if (_jobDescriptor.State == "in progress")
                    {
                        double percentLayers = Convert.ToDouble((_jobDescriptor.CurrentLayer * 100) / _jobDescriptor.Layers);
                        return string.Format(Convert.ToString(percentLayers));
                    }
                }
                return "0";
            }
        }

        public string ColorBackgroundProgressBar
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    if (_jobDescriptor.State == "Completed" || _jobDescriptor.State == "in progress")
                    {
                        return "#3000AA00";
                    }
                    else if (_jobDescriptor.State == "Failed" )
                    {
                        return "#30FF0000";
                    }
                    
                }
                return "White";
            }
        }
        public string ColorProgressBar
        {
            get
            {
                if (_jobDescriptor != null)
                {
                    if (_jobDescriptor.State == "Completed" || _jobDescriptor.State == "in progress")
                    {
                        return "#2000AA00";
                    }
                    else if (_jobDescriptor.State == "Failed" )
                    {
                        return "#20FF0000";
                    }
                    
                }
                return "White";
            }
        }
        ///////////////////////////////////////////////////////////////////
        // METHODS
        ///////////////////////////////////////////////////////////////////
        private void DisplayLog_Execute()
        {
            if (_jobDescriptor.FileLogUrl != null)
            {
                System.Diagnostics.Process.Start(_jobDescriptor.FileLogUrl);
            }
            
        }
        internal void InitData(JobDescriptor job)
        {
            _jobDescriptor = job;
            OnPropertyChanged("ScreenShotPreview");
            OnPropertyChanged("TitlePreview");
            OnPropertyChanged("StatePreview");
            OnPropertyChanged("LayersPreview");
            OnPropertyChanged("MetaDataLeft");
            OnPropertyChanged("PartsListPreview");
            OnPropertyChanged("PercentProgressBar");
            OnPropertyChanged("NoteValue");
            OnPropertyChanged("NoteValueResult");
            OnPropertyChanged("ColorBackgroundProgressBar");
            OnPropertyChanged("ColorProgressBar");
            OnPropertyChanged("MetaDataRight");
        }
        /// <summary>
        /// Display all the meta of the metadata list in the jobDescriptor
        /// </summary>
        /// <param name="Left"></param>
        /// <returns></returns>
        public string DisplayMetaData(bool Left)
        {
            JobDescriptor Job = _jobDescriptor;
            int num = 0;
            string metaDataTextLeft = "";
            string metaDataTextRight = "";
            if (Job.MetaData != null && Job.MetaData.Count > 0)
            {
                foreach (var meta in Job.MetaData)
                {
                    
                    if (num % 2 == 0 && num < Job.MetaData.Count - 2)
                    {
                        metaDataTextLeft += meta.Value + "\n";
                    }
                    else if (num % 2 == 1 && num < Job.MetaData.Count - 2)
                    {
                        metaDataTextRight += meta.Value + "\n";
                    }
                    else if (num % 2 == 0 && num < Job.MetaData.Count - 1)
                    {
                        metaDataTextLeft += meta.Value;
                    }
                    else if (num % 2 == 1 && num < Job.MetaData.Count - 1)
                    {
                        metaDataTextRight += meta.Value ;
                    }
                    else if(num % 2 == 0 && num < Job.MetaData.Count)
                    {
                        metaDataTextLeft += meta.Value;
                    }
                    else if (num % 2 == 1 && num < Job.MetaData.Count)
                    {
                        metaDataTextRight += meta.Value;
                    }
                    num += 1;
                }
            }
            if (Left)
            {
                return metaDataTextLeft;
            }
            else
            {
                return metaDataTextRight;
            }
        }
        public void Dispose()
        {
        }
    }
}

