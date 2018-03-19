using Fabrication.UserControls.Model;
using Fabrication.UserControls.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using static Fabrication.UserControls.Model.JobDescriptor;

namespace Fabrication.UserControls.View
{
    /// <summary>
    /// Interaction logic for JobControlsView.xaml
    /// </summary>
    public partial class JobsView : UserControl
    {
        public JobsView()
        {
            InitializeComponent();
            ((JobsViewModel)DataContext).Init(JobWrapPanel);
            EndFilter.SelectedDate = DateTime.Now;
            StartFilter.SelectedDate = DateTime.Now.AddDays(-7);
        }
        /// <summary>
        /// Activate filters when the selected date of the datePickers change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StartFilter.SelectedDate != null && EndFilter.SelectedDate != null)
                ((JobsViewModel)DataContext).DatePickerFilterUpdate(StartFilter.SelectedDate.Value, EndFilter.SelectedDate.Value);
        }
        /// <summary>
        /// Activate filters when the selected date of the datePickers change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartFilter.SelectedDate != null && EndFilter.SelectedDate != null)
                ((JobsViewModel)DataContext).DatePickerFilterUpdate(StartFilter.SelectedDate.Value, EndFilter.SelectedDate.Value);
        }
    }
}
