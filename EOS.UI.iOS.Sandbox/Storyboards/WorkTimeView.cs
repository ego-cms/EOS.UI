using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.CollectionViewSources;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class WorkTimeView : BaseViewController
    {
        public const string Identifier = "WorkTimeView";
        private List<EOSSandboxDropDown> _dropDowns;
        private WorkTimeCalendarCollectionSource _source;

        public WorkTimeView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var dataSource = CreateSchedule();
            _source = (WorkTimeCalendarCollectionSource)workTimeCollection.Source;
            _source.CalendarModel.Items = dataSource;
            workTimeCollection.Source = _source;

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themesDropDown,
                titleFontDropDown,
                titleSizeDropDown,
                dayFontDropDown,
                dayTextSizeDropDown,
                dayTextColorDropDown,
                currentDayBackgroundColorDropDown,
                currentDayTextColorDropDown,
                weekStartDropdown,
                dayEvenBackgroundColor,
                devidersColor,
                currentDayDevidersColor,
                titleColorDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            var rect = new CGRect(0, 0, 100, 100);
            InitThemeDropDown(rect);
            themesDropDown.SetTextFieldText(_source.CalendarModel.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
            InitSources(rect);
            InitWeekStartDropDown(rect);
            resetButton.TouchUpInside += (sender, e) =>
            {
                _dropDowns.Except(new[] { themesDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                _source.CalendarModel.ResetCustomization();
            };
        }

        private List<WorkTimeCalendarItem> CreateSchedule()
        {
            var schedule = new List<WorkTimeCalendarItem>();
            for (int i = 0; i < 7; ++i)
            {
                var day = new WorkTimeCalendarItem();
                day.WeekDay = (DayOfWeek)i;
                if (i == 0 || i == 6)
                {
                    day.IsDayOff = true;
                    schedule.Add(day);
                    continue;
                }

                day.StartTime = TimeSpan.FromHours(8);
                day.EndTime = TimeSpan.FromHours(18);
                day.BreakStartTime = TimeSpan.FromHours(13);
                day.BreakEndTime = TimeSpan.FromHours(14);
                day.HasBreak = day.WeekDay != DayOfWeek.Friday;
                schedule.Add(day);
            }

            return schedule;
        }

        private void InitSources(CGRect rect)
        {
            InitTitleFontDropDown(rect);
            InitDayFontDropDown(rect);
            InitTitleSizeDropDown(rect);
            InitDayTextSizeDropDown(rect);
            InitDayTextColorDropDown(rect);
            InitCurrentDayTextColorDropDown(rect);
            InitTitleColorDropDown(rect);
            InitCurrentDayBackgroundColorDropDown(rect);
            InitDevidersColorDropDown(rect);
            InitCurrentDayDevidersColorDropDown(rect);
            InitDayEvenBackgroundColor(rect);
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themesDropDown.InitSource(
               ThemeTypes.ThemeCollection,
               (theme) =>
               {
                   _source.CalendarModel.GetThemeProvider().SetCurrentTheme(theme);
                   _source.CalendarModel.ResetCustomization();
                   _dropDowns.Except(new[] { themesDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                   InitSources(rect);
                   UpdateAppearance();
               },
               Fields.Theme,
               rect);
        }

        private void InitTitleFontDropDown(CGRect rect)
        {
            titleFontDropDown.InitSource(
                WorkTimeConstants.TitleFonts,
                font => _source.CalendarModel.TitleFont = font,
                Fields.TitleFont,
                rect);
        }

        private void InitDayFontDropDown(CGRect rect)
        {
            dayFontDropDown.InitSource(
                WorkTimeConstants.DayFonts,
                font => _source.CalendarModel.DayTextFont = font,
                Fields.DayTextFont,
                rect);
        }

        private void InitTitleSizeDropDown(CGRect rect)
        {
            titleSizeDropDown.InitSource(
               WorkTimeConstants.TitleTextSizes,
               size => _source.CalendarModel.TitleTextSize = size,
               Fields.TitleTextSize,
              rect);
        }

        private void InitDayTextSizeDropDown(CGRect rect)
        {
            dayTextSizeDropDown.InitSource(
                WorkTimeConstants.DayTextSizes,
                size => _source.CalendarModel.DayTextSize = size,
                Fields.DayTextSize,
               rect);
        }

        private void InitDayTextColorDropDown(CGRect rect)
        {
            dayTextColorDropDown.InitSource(
               WorkTimeConstants.DayColors,
               color => _source.CalendarModel.DayTextColor = color,
               Fields.DayTextColor,
               rect);
        }

        private void InitCurrentDayTextColorDropDown(CGRect rect)
        {
            currentDayTextColorDropDown.InitSource(
                WorkTimeConstants.CurrentDayColors,
               color => _source.CalendarModel.CurrentDayTextColor = color,
               Fields.CurrentDayTextColor,
               rect);
        }

        private void InitTitleColorDropDown(CGRect rect)
        {
            titleColorDropDown.InitSource(
               WorkTimeConstants.TitleColors,
             color => _source.CalendarModel.TitleColor = color,
             Fields.TitleColor,
             rect);
        }

        private void InitCurrentDayBackgroundColorDropDown(CGRect rect)
        {
            currentDayBackgroundColorDropDown.InitSource(
               WorkTimeConstants.CurrentDayBackgroundColors,
               color => _source.CalendarModel.CurrentDayBackgroundColor = color,
               Fields.CurrentDayBackgroundColor,
              rect);
        }

        private void InitDevidersColorDropDown(CGRect rect)
        {
            devidersColor.InitSource(
                WorkTimeConstants.DividersColors,
                color => _source.CalendarModel.ColorDividers = color,
                Fields.ColorDividers,
                rect);
        }

        private void InitCurrentDayDevidersColorDropDown(CGRect rect)
        {
            currentDayDevidersColor.InitSource(
               WorkTimeConstants.CurrentDayDividerColors,
               color => _source.CalendarModel.CurrentColorDividers = color,
               Fields.CurrentColorDividers,
               rect);
        }

        private void InitDayEvenBackgroundColor(CGRect rect)
        {
            dayEvenBackgroundColor.InitSource(
               WorkTimeConstants.DayEvenBackgroundColors,
               color => _source.CalendarModel.DayEvenBackgroundColor = color,
               Fields.DayEvenBackgroundColor,
               rect);
        }

        private void InitWeekStartDropDown(CGRect rect)
        {
            weekStartDropdown.InitSource(
               Days.DaysCollection,
               weekStart => _source.CalendarModel.WeekStart = weekStart,
               Fields.WeekStartDay,
               rect);
        }
    }
}
