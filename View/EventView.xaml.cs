using Fabrication.UserControls.Model;
using Fabrication.UserControls.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fabrication.UserControls.View
{
    /// <summary>
    /// Interaction logic for EventControlsView.xaml
    /// </summary>
    public partial class EventsView : UserControl
    {
        public EventsView()
        {
            InitializeComponent();
        }
        private readonly List<Border> _jobBorderBlock = new List<Border>();
        private readonly List<Line> _nowLine = new List<Line>();
        /// <summary>
        /// This method display the jobs of the week of the selectedDate
        /// </summary>
        /// <param name="Jobs">List in wich there is the jobs </param>
        /// <returns></returns>
        public void AddJobCtrlToGrid(List<EventDescriptor> Jobs)
        {
            foreach (Border border in _jobBorderBlock) WeekGrid.Children.Remove(border);
            foreach (Line now in _nowLine) WeekGrid.Children.Remove(now);
            _jobBorderBlock.Clear();
            _nowLine.Clear();
            List<EventDescriptor> jobsOfTheWeek = sortWeekday(developJobList(Jobs));
            double height = WeekGrid.ActualHeight;
            double twoHours = height / 13;
            double day = height - twoHours;
            double oneHour = twoHours / 2;
            double oneMinute = oneHour / 60;
            foreach (var job in jobsOfTheWeek)
            {
                StackPanel jobPanel = new StackPanel();
                Border newBorder = new Border();
                TextBlock Title = new TextBlock();
                TextBlock Layers = new TextBlock();
                TextBlock LayersThickness = new TextBlock();
                TextBlock Quality = new TextBlock();
                TextBlock State = new TextBlock();
                TextBlock Resin = new TextBlock();
                TextBlock Seamline = new TextBlock();
                TextBlock Description= new TextBlock();
                newBorder.Background = new SolidColorBrush(Color.FromArgb(130,128,128,128));
                if (job.Type == "Fabrication" || job.Type == "Current_Job")
                {
                    newBorder.Effect = Fabrication.Resources.ColorEffectLibrary.Instance.GreenEffect;
                    
                }
                else if (job.Type == "Calendar Event")
                {
                    newBorder.Effect = Fabrication.Resources.ColorEffectLibrary.Instance.GrayEffect;
                }
                
                if (job.GetType() == typeof(JobDescriptor))
                {
                    if (((JobDescriptor)job).State == "Failed")
                    {
                        newBorder.Effect = Fabrication.Resources.ColorEffectLibrary.Instance.RedEffect;
                    }
                }
                newBorder.Margin = new Thickness(2.5, (job.Start.Hour * oneHour)+(job.Start.Minute*oneMinute), 2.5, (day)-((oneHour*job.End.Hour) + (job.End.Minute * oneMinute)));
                Grid.SetRow(newBorder, 1);
                Grid.SetRowSpan(newBorder, 13);
                newBorder.Tag = job;
                newBorder.TouchDown += displayEventPopupOnTouchDown;
                newBorder.MouseDown += displayEventPopupOnMouseDown;
                if (job.RealStart== false && job.RealEnd== false)
                {
                    newBorder.CornerRadius = new CornerRadius(0);
                }
                else if (job.RealStart == true && job.RealEnd== false)
                {
                    newBorder.CornerRadius = new CornerRadius(5,5,0,0);
                }
                else if (job.RealStart == false && job.RealEnd == true)
                {
                    newBorder.CornerRadius = new CornerRadius(0, 0, 5, 5);
                }
                else if (job.RealStart == true && job.RealEnd == true)
                {
                    newBorder.CornerRadius = new CornerRadius(5);
                }
                WeekGrid.Children.Add(newBorder);
                _jobBorderBlock.Add(newBorder);
                //// JobPanel

                jobPanel.Children.Add(Title);
                if (job.GetType() == typeof(JobDescriptor))
                {
                    jobPanel.Children.Add(Layers);
                    jobPanel.Children.Add(State);
                    jobPanel.Children.Add(Resin);
                    jobPanel.Children.Add(LayersThickness);
                    jobPanel.Children.Add(Quality);
                    jobPanel.Children.Add(Seamline);
                }
                else
                {
                    jobPanel.Children.Add(Description);
                }
                Grid.SetRow(jobPanel,1);
                Grid.SetRowSpan(jobPanel, 13);
                newBorder.Child = jobPanel;
                //////////////////
                /// Text Block
                /////////////////
                //// Title
                Title.Text = job.Title;
                Title.Text = Title.Text.Replace(" ", "_");
                if (Title.Text.Length > 33)
                {
                    Title.Text =Title.Text.Remove(33);
                    Title.Text = string.Format(Title.Text + "...");
                }
                Title.TextWrapping = TextWrapping.Wrap;
                Title.VerticalAlignment = VerticalAlignment.Center;
                Title.Margin = new Thickness(0, 0, 0, 5);
                if (job.GetType() == typeof(JobDescriptor))
                {
                    //// Layers
                    Layers.Text = string.Format("{0}/{1} layers", ((JobDescriptor)job).CurrentLayer, ((JobDescriptor)job).Layers);
                    Layers.TextWrapping = TextWrapping.Wrap;
                    Layers.VerticalAlignment = VerticalAlignment.Center;

                    //// State
                    State.Text = ((JobDescriptor)job).State;
                    State.TextWrapping = TextWrapping.Wrap;
                    State.VerticalAlignment = VerticalAlignment.Center;
                    State.Margin = new Thickness(0, 5, 0, 5);
                    //// Layer Thickness
                    LayersThickness.Text = string.Format("{0}", ((JobDescriptor)job).LayersThickness);
                    LayersThickness.TextWrapping = TextWrapping.Wrap;
                    LayersThickness.VerticalAlignment = VerticalAlignment.Center;
                    //// Quality
                    Quality.Text = ((JobDescriptor)job).Quality;
                    Quality.TextWrapping = TextWrapping.Wrap;
                    Quality.VerticalAlignment = VerticalAlignment.Center;

                    //// Resin
                    Resin.Text = ((JobDescriptor)job).Resin;
                    Resin.VerticalAlignment = VerticalAlignment.Center;

                    //// Seamline
                    Seamline.Text = ((JobDescriptor)job).Seamline;
                    Seamline.VerticalAlignment = VerticalAlignment.Center;
                }
                else
                {
                    Description.Text = ((CalendarAPIEvent)job).Description;
                    Description.VerticalAlignment = VerticalAlignment.Center;
                }

                //// This switch set the right column for the border
                DateTime selectedDate = (DateTime)jobCalendar.SelectedDate;
                switch (job.Start.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        Grid.SetColumn(newBorder, 0);
                        break;
                    case DayOfWeek.Tuesday:
                        Grid.SetColumn(newBorder, 1);
                        break;
                    case DayOfWeek.Wednesday:
                        Grid.SetColumn(newBorder, 2);
                        break;
                    case DayOfWeek.Thursday:
                        Grid.SetColumn(newBorder, 3);
                        break;
                    case DayOfWeek.Friday:
                        Grid.SetColumn(newBorder, 4);
                        break;
                    case DayOfWeek.Saturday:
                        Grid.SetColumn(newBorder, 5);
                        break;
                    case DayOfWeek.Sunday:
                        Grid.SetColumn(newBorder, 6);
                        break;
                }
                /// Now: red line indicator of the current time
                Line Now = new Line();
                Now.Stroke = Brushes.Red;
                Now.Stretch = Stretch.Fill;
                Now.X2 = 10;
                Now.VerticalAlignment = VerticalAlignment.Top;
                Now.Margin = new Thickness(0, (DateTime.Now.Hour * oneHour) + (DateTime.Now.Minute * oneMinute), 0, 0);
                Now.StrokeThickness = 2.5;
                WeekGrid.Children.Add(Now);
                bool belongToThisWeek =nowColumn(Now);
                if (belongToThisWeek == false)
                {
                    Now.Visibility = Visibility.Hidden;
                }
                _nowLine.Add(Now);
                Grid.SetRow(Now, 1);
                Grid.SetRowSpan(Now, 13);
            }
        }

        /// <summary>
        /// This method is use for build the popup of each border on the mousedown and touchdown event
        /// </summary>
        /// <param name="sender"></param>
        private void displayEventPopup(object sender)
        {
            Popup BorderInformations = new Popup();
            BorderInformations.StaysOpen = false;
            Border PopupBorder = new Border();
            PopupBorder.Background = Brushes.LightGray;
            PopupBorder.Margin = new Thickness(2);
            TextBlock PopupText = new TextBlock();
            PopupText.Margin = new Thickness(5,0,5,0);
            PopupText.VerticalAlignment = VerticalAlignment.Center;
            PopupText.FontSize = 20.0;
            PopupBorder.Child = PopupText;
            if (((Border)sender).Tag.GetType() == typeof(JobDescriptor))
            {
                JobDescriptor Job = ((Border)sender).Tag as JobDescriptor;
                string popupText = Job.Title + "\nBuild " + Job.State.ToLower() + "\n" + Job.CurrentLayer + "/" + Job.Layers + " layers";
                if (Job.MetaData != null && Job.MetaData.Count > 0)
                {
                    popupText += "\n";
                    foreach (var meta in Job.MetaData)
                    {
                        string metaKey = meta.Key;
                        if (metaKey.Count() > 1)
                        {
                            metaKey = metaKey.ToUpper()[0] + metaKey.Substring(1);
                        }
                        popupText += "\n" + metaKey + ": " + meta.Value;
                    }
                }

                PopupText.Text = popupText;
            }
            else
            {
                CalendarAPIEvent Job = ((Border)sender).Tag as CalendarAPIEvent;
                PopupText.Text = string.Format(Job.Title + "\n" + Job.Description);
            }
            BorderInformations.PlacementTarget = sender as Border;
            BorderInformations.Child = PopupBorder;
            BorderInformations.Placement = PlacementMode.Top;
            BorderInformations.IsOpen = true;
        }
        /// <summary>
        /// display the popup when the mouse down is activate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayEventPopupOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            displayEventPopup(sender);
        }
        /// <summary>
        /// display the popup when the touch down is activate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayEventPopupOnTouchDown(object sender, TouchEventArgs e)
        {
            displayEventPopup(sender);
        }
        /// <summary>
        /// This method allow to use the selectedDate of the Calendar in the ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jobCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventCtrl.IsLoaded)
            {
                PrinterDataModel.Instance.UpdateEventList(Convert.ToDateTime(jobCalendar.SelectedDate));
                List<DateTime> DaysOfTheWeek = PrinterDataModel.Instance.DayOfTheWeek((DateTime)jobCalendar.SelectedDate);
                DateTime SelectedDay = (DateTime)jobCalendar.SelectedDate;
                foreach (var Day in DaysOfTheWeek)
                {
                    if (SelectedDay.Date == Day.Date)
                    {
                        AddJobCtrlToGrid(PrinterDataModel.Instance.ListEventDescriptor);
                    }
                }
                ((EventViewModel)DataContext).SelectedDate_Changed(e);
                int n = 0;
                foreach (var item in sortWeekday(PrinterDataModel.Instance.ListEventDescriptor))
                {
                    if (item.GetType() == typeof(JobDescriptor))
                    {
                        n += 1;
                    }
                }
                JobWeekCount.Text = Convert.ToString(n);
                int nbPartsTotal = 0;
                foreach (EventDescriptor job in sortWeekday(PrinterDataModel.Instance.ListEventDescriptor))
                {
                    if (job.GetType() == typeof(JobDescriptor))
                    {
                        if (((JobDescriptor)job).State == "Completed")
                        {
                            nbPartsTotal = nbPartsTotal + ((JobDescriptor)job).PartsList.Count;
                        }
                    }
                }
                PartsWeekCount.Text = Convert.ToString(nbPartsTotal);
            }
        }

        private void WeekGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AddJobCtrlToGrid(PrinterDataModel.Instance.ListEventDescriptor);
        }

        /// <summary>
        /// This method sort the JobsList and only return the days of the week of the selectedDate of the calendar
        /// </summary>
        /// <param name="notSortList"></param>
        /// <returns></returns>
        private List<EventDescriptor> sortWeekday(List<EventDescriptor> notSortList)
        {
            List<EventDescriptor> jobsOfTheWeek = new List<EventDescriptor>();
            List<DateTime> dayOfTheWeek = PrinterDataModel.Instance.DayOfTheWeek((DateTime)jobCalendar.SelectedDate);
            for (int i = 0; i < notSortList.Count; i++)
            {
                for (int n = 0; n < dayOfTheWeek.Count; n++)
                {
                    if (notSortList[i].Start.Date == dayOfTheWeek[n].Date)
                    {
                        jobsOfTheWeek.Add(notSortList[i]);
                        break;
                    }
                }
            }
            return jobsOfTheWeek;
        }

        /// <summary>
        /// divise a job when his start and his end are not in the same day
        /// </summary>
        /// <param name="Jobs"></param>
        /// <returns></returns>
        private List<EventDescriptor> developJobList(List<EventDescriptor> Jobs)
        {
            List<EventDescriptor> developJobList = new List<EventDescriptor>();
            foreach (var job in Jobs)
            {
                if (job.Start.Date != job.End.Date)                    
                {
                    DateTime EndMidnight = new DateTime(job.Start.Year, job.Start.Month, job.Start.Day, 23, 59, 59);
                    DateTime StartMidnight = new DateTime(job.End.Year, job.End.Month, job.End.Day, 0, 0, 0);
                    DateTime dayTemp = job.Start;
                    int numberOfJobDescriptor = 1;
                    for (int i = 1; job.End.Date != dayTemp.Date; i++)
                    {
                        dayTemp = job.Start.AddDays(i);
                        numberOfJobDescriptor += 1;
                    }
                    EventDescriptor jobStart;
                    EventDescriptor jobEnd;
                    if (job.GetType() == typeof(JobDescriptor))
                    {
                        jobStart = new JobDescriptor(job.Type, job.Title, job.Start, EndMidnight, true, false, ((JobDescriptor)job).Layers, ((JobDescriptor)job).LayersThickness, ((JobDescriptor)job).Quality, ((JobDescriptor)job).State, ((JobDescriptor)job).PartsList);
                        jobEnd = new JobDescriptor(job.Type, job.Title, StartMidnight, job.End, false, true, ((JobDescriptor)job).Layers, ((JobDescriptor)job).LayersThickness, ((JobDescriptor)job).Quality, ((JobDescriptor)job).State, ((JobDescriptor)job).PartsList);
                    }
                    else
                    {
                        jobStart = new CalendarAPIEvent(job.Type ,job.Title, job.Start, EndMidnight, true, false,((CalendarAPIEvent)job).Description);
                        jobEnd = new CalendarAPIEvent(job.Type, job.Title, StartMidnight, job.End, false, true, ((CalendarAPIEvent)job).Description);
                    }
                    developJobList.Add(jobStart);
                    developJobList.Add(jobEnd);
                    if (numberOfJobDescriptor>2)
                    {
                        for (int i = 1; i <= numberOfJobDescriptor-2; i++)
                        {
                            DateTime wholeDayStartMidnight = new DateTime(job.Start.AddDays(i).Year, job.Start.AddDays(i).Month, job.Start.AddDays(i).Day, 0, 0, 0);
                            DateTime wholeDayEndMidnight = new DateTime(job.Start.AddDays(i).Year, job.Start.AddDays(i).Month, job.Start.AddDays(i).Day, 23, 59, 59);
                            EventDescriptor wholeDay;
                            if (job.GetType() == typeof(JobDescriptor))
                            {
                                wholeDay = new JobDescriptor(job.Type, job.Title, wholeDayStartMidnight, wholeDayEndMidnight, false, false, ((JobDescriptor)job).Layers, ((JobDescriptor)job).LayersThickness, ((JobDescriptor)job).Quality, ((JobDescriptor)job).State, ((JobDescriptor)job).PartsList);
                            }
                            else
                            {
                                wholeDay = new CalendarAPIEvent(job.Type, job.Title, wholeDayStartMidnight, wholeDayEndMidnight, false, false, ((CalendarAPIEvent)job).Description);
                            }
                            developJobList.Add(wholeDay);
                        }
                    }
                }
                else
                {
                    developJobList.Add(job);
                }
            }
            return developJobList;
        }

        /// <summary>
        /// Check if the week selected is the current week and set the right column
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool nowColumn(Line line)
        {
            bool belongToThisWeek = false;
            List<DateTime> DayOfTheCurrentWeek = PrinterDataModel.Instance.DayOfTheWeek((DateTime)jobCalendar.SelectedDate);
            foreach (DateTime Day in DayOfTheCurrentWeek)
            {
                if (DateTime.Now.Date == Day.Date)
                {
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            Grid.SetColumn(line, 0);
                            break;
                        case DayOfWeek.Tuesday:
                            Grid.SetColumn(line, 1);
                            break;
                        case DayOfWeek.Wednesday:
                            Grid.SetColumn(line, 2);
                            break;
                        case DayOfWeek.Thursday:
                            Grid.SetColumn(line, 3);
                            break;
                        case DayOfWeek.Friday:
                            Grid.SetColumn(line, 4);
                            break;
                        case DayOfWeek.Saturday:
                            Grid.SetColumn(line, 5);
                            break;
                        case DayOfWeek.Sunday:
                            Grid.SetColumn(line, 6);
                            break;
                    }
                    belongToThisWeek = true;
                }
            }
            return belongToThisWeek;
        }

        private void EventCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            jobCalendar.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Disable the mouse focus on the calendar when you select a date.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

    }
}
