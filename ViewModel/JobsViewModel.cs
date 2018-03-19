using CoreCS.UI;
using Fabrication.Resources;
using Fabrication.UserControls.Model;
using Fabrication.UserControls.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Fabrication.UserControls.ViewModel
{
    public class JobsViewModel : ProjectViewModelBase, IDisposable
    {
        public enum FilterJobsEnum
        {
            ByDate,
            BySuccess,
            ByRating,
        }
        ///////////////////////////////////////////////////////////////////
        // Member
        ///////////////////////////////////////////////////////////////////
        private PrinterDataModel _printerDataModel;
        /// <summary>
        /// List filter order by style Ascending or Descending
        /// </summary>
        private bool _filterOrdreByAscending = false;

        private const string FILTER_ASC_IMG = @"pack://application:,,,/Fabrication;component/Resources/filter_ascending.png";
        private const string FILTER_DESC_IMG = @"pack://application:,,,/Fabrication;component/Resources/filter_descending.png";
        private const string FILTER_ASC_S_IMG = @"pack://application:,,,/Fabrication;component/Resources/filter_ascending_selected.png";
        private const string FILTER_DESC_S_IMG = @"pack://application:,,,/Fabrication;component/Resources/filter_descending_selected.png";

        //The View wrap panel container
        private WrapPanel _jobWrapPanel = null;

        private bool _filterOrdreDateByAscending = true;
        private bool _filterOrdreSuccessByAscending = true;

        private readonly List<JobDescriptor> _actualJobUse = new List<JobDescriptor>();

        private DateTime _start;
        private DateTime _end;
        ///////////////////////////////////////////////////////////////////
        // CONSTRUCTOR
        ///////////////////////////////////////////////////////////////////
        public JobsViewModel()
        {
            _printerDataModel = PrinterDataModel.Instance;
            FilterDateImg = FILTER_ASC_IMG;

            FilterSuccessImg = FILTER_ASC_IMG;
            FilterDateSImg = FILTER_ASC_S_IMG;

            FilterSuccessSImg = FILTER_ASC_S_IMG;

            
        }
        ///////////////////////////////////////////////////////////////////
        // PROPERTIES
        ///////////////////////////////////////////////////////////////////
        public System.Windows.Media.Effects.ShaderEffect BackgroundEffect
        {
            get { return Resources.ColorEffectLibrary.Instance.BackgroundEffect; }
        }
        private ICommand _filterList;
        public ICommand FilterList
        {
            get
            {
                if (_filterList == null)
                {
                    _filterList = new RelayCommand(param => ListFilter_Execute((FilterJobsEnum)param));
                }
                return _filterList;
            }
        }
        private ICommand _3dViewClick;
        public ICommand ThreeDViewClick
        {
            get
            {
                if (_3dViewClick == null)
                {
                    _3dViewClick = new RelayCommand(param=> Button3DView_Click());
                }
                return _3dViewClick;
            }
        }
        private ICommand _partsListClick;
        public ICommand PartsListClick
        {
            get
            {
                if (_partsListClick == null)
                {
                    _partsListClick = new RelayCommand(param => PartsListButton_Click());
                }
                return _partsListClick;
            }
        }
        private string _filterDateImg;
        public string FilterDateImg
        {
            get { return _filterDateImg; }
            set
            {
                if (_filterDateImg != value)
                {
                    _filterDateImg = value;
                    OnPropertyChanged("FilterDateImg");
                }
            }
        }

        private string _filterSuccessImg;
        public string FilterSuccessImg
        {
            get { return _filterSuccessImg; }
            set
            {
                if (_filterSuccessImg != value)
                {
                    _filterSuccessImg = value;
                    OnPropertyChanged("FilterSuccessImg");
                }
            }
        }
        private string _filterDateSImg;
        public string FilterDateSImg
        {
            get { return _filterDateSImg; }
            set
            {
                if (_filterDateSImg != value)
                {
                    _filterDateSImg = value;
                    OnPropertyChanged("FilterDateSImg");
                }
            }
        }
        private string _filterSuccessSImg;
        public string FilterSuccessSImg
        {
            get { return _filterSuccessSImg; }
            set
            {
                if (_filterSuccessSImg != value)
                {
                    _filterSuccessSImg = value;
                    OnPropertyChanged("FilterSuccessSImg");
                }
            }
        }
        public ShaderEffect BtnFilterColorEffect
        {
            get { return ColorEffectLibrary.Instance.BlueEffect; }
        }
        private Visibility _selectorFilterDate;
        public Visibility SelectorFilterDate
        {
            get { return _selectorFilterDate; }
            set
            {
                if (_selectorFilterDate != value)
                {
                    _selectorFilterDate = value;
                    OnPropertyChanged("SelectorFilterDate");
                }
            }
        }

        private Visibility _selectorFilterSuccess;
        public Visibility SelectorFilterSuccess
        {
            get { return _selectorFilterSuccess; }
            set
            {
                if (_selectorFilterSuccess != value)
                {
                    _selectorFilterSuccess = value;
                    OnPropertyChanged("SelectorFilterSuccess");
                }
            }
        }

        

        ///////////////////////////////////////////////////////////////////
        // METHODS
        ///////////////////////////////////////////////////////////////////
        //Init the wrap panel view container 
        public void Init(WrapPanel jobContainer)
        {
            _jobWrapPanel = jobContainer;
        }
        private FilterJobsEnum _filterType = FilterJobsEnum.ByDate;
        /// <summary>
        /// Set the right filter image if it's ascendent or descendent or if the filtr is selected or not
        /// </summary>
        /// <param name="filterType"></param>
        private void ListFilter_Execute(FilterJobsEnum filterType)
        {
            FilterDateImg = FILTER_ASC_IMG;

            FilterSuccessImg = FILTER_ASC_IMG;
            FilterDateSImg = FILTER_ASC_S_IMG;

            FilterSuccessSImg = FILTER_ASC_S_IMG;
            SelectorFilterDate = Visibility.Collapsed;
            SelectorFilterSuccess = Visibility.Collapsed;
            if (filterType == FilterJobsEnum.ByDate)
            {
                if (_filterType == FilterJobsEnum.ByDate)
                    _filterOrdreByAscending = !_filterOrdreByAscending;
                else
                {
                    _filterType = FilterJobsEnum.ByDate;
                    _filterOrdreByAscending = true;
                }
                FilterDateImg = _filterOrdreByAscending ? FILTER_ASC_IMG : FILTER_DESC_IMG;
                FilterDateSImg = _filterOrdreByAscending ? FILTER_ASC_S_IMG : FILTER_DESC_S_IMG;
                SelectorFilterDate = Visibility.Visible;
            }
            else if (filterType == FilterJobsEnum.BySuccess)
            {
                if (_filterType == FilterJobsEnum.BySuccess)
                    _filterOrdreByAscending = !_filterOrdreByAscending;
                else
                {
                    _filterType = FilterJobsEnum.BySuccess;
                    _filterOrdreByAscending = true;
                }
                FilterSuccessImg = _filterOrdreByAscending ? FILTER_ASC_IMG : FILTER_DESC_IMG;
                FilterSuccessSImg = _filterOrdreByAscending ? FILTER_ASC_S_IMG : FILTER_DESC_S_IMG;
                SelectorFilterSuccess = Visibility.Visible;
            }
            UpdateJobViews();
        }
        /// <summary>
        /// Command for the event click on the parts PartsList who will put the visibility of the PartsList button in visible
        /// </summary>
        private void PartsListButton_Click()
        {
            foreach (JobMiniatureView oneMiniature in _jobWrapPanel.Children)
            {
                var miniatureViewModel = (JobMiniatureViewModel)oneMiniature.DataContext;
                miniatureViewModel.PartsListVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Command for the event click on the 3DViewButton who will put the visibility of the PartsList button in collapsed
        /// </summary>
        private void Button3DView_Click()
        {
            foreach (JobMiniatureView oneMiniature in _jobWrapPanel.Children)
            {
                var miniatureViewModel = (JobMiniatureViewModel)oneMiniature.DataContext;
                miniatureViewModel.PartsListVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// return the list of the jobs use sort by Date or Success, ascendent or descendent
        /// </summary>
        /// <returns></returns>
        private List<JobDescriptor> OrderCurrentJobList()
        {
            List<JobDescriptor> ret = null;
            switch (_filterType)
            {
                case FilterJobsEnum.ByDate:
                    {
                        if (_filterOrdreDateByAscending)
                        {
                            _filterOrdreDateByAscending = false;
                            _filterOrdreSuccessByAscending = true;
                            ret = _actualJobUse.OrderBy(job => job.Start).ToList();
                        }
                        else
                        {
                            _filterOrdreDateByAscending = true;
                            _filterOrdreSuccessByAscending = true;
                            ret = _actualJobUse.OrderByDescending(job => job.Start).ToList();
                        }
                        break;
                    }
                case FilterJobsEnum.BySuccess:
                    if (_filterOrdreSuccessByAscending)
                    {
                        _filterOrdreSuccessByAscending = false;
                        _filterOrdreDateByAscending = true;
                        ret = _actualJobUse.OrderBy(job => (job.State)).ToList();
                    }
                    else
                    {
                        _filterOrdreSuccessByAscending = true;
                        _filterOrdreDateByAscending = true;
                        ret = _actualJobUse.OrderByDescending(job => (job.State)).ToList();
                    }
                    break;
            }
            if (ret == null) return new List<JobDescriptor>();
            return ret;
        }

        /// <summary>
        /// Update the jobMiniature in the view with those who are in the desired time interval of the datespickers
        /// </summary>
        /// <param name="start">DateTime of the StartDatePicker</param>
        /// <param name="end">DateTime of the EndDatePicker</param>
        public void DatePickerFilterUpdate(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
            _actualJobUse.Clear();
            if (start <= end)
            {
                _actualJobUse.AddRange(PrinterDataModel.Instance.ListJobs.Where(date => date.Start >= start && date.Start <= end.AddDays(1)));
            }
            UpdateJobViews();
        }

        /// <summary>
        /// This method is use for update the view and keep the right filter activated
        /// </summary>
        private void UpdateJobViews()
        {
            Visibility PartListVisibility = new Visibility();
            foreach (JobMiniatureView oneMiniature in _jobWrapPanel.Children)
            {
                var miniatureViewModel = (JobMiniatureViewModel)oneMiniature.DataContext;
                PartListVisibility = miniatureViewModel.PartsListVisibility;
            }
            _jobWrapPanel.Children.Clear();
            foreach (var job in OrderCurrentJobList())
            {
                JobMiniatureView oneJobMiniature = new JobMiniatureView();
                ((JobMiniatureViewModel)oneJobMiniature.DataContext).InitData(job);
                _jobWrapPanel.Children.Add(oneJobMiniature);
                if (PartListVisibility == Visibility.Collapsed)
                {
                    Button3DView_Click();
                }
                else
                {
                    PartsListButton_Click();
                }
            }
        }

        /// <summary>
        /// Called when user click on the tab menu, to display the history view
        /// </summary>
        public override void OnShowChanged(bool isVisible)
        {
            ListFilter_Execute(FilterJobsEnum.ByDate);
            DatePickerFilterUpdate(_start, _end);
        }

        public void Dispose()
        {
        }
    }
}
