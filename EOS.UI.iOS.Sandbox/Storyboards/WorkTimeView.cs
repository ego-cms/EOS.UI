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

        public WorkTimeView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var dataSource = CreateSchedule();
            var source = (WorkTimeCalendarCollectionSource)workTimeCollection.Source;
            source.CalendarModel.Items = dataSource;
            workTimeCollection.Source = source;

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

            themesDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) =>
                {
                    source.CalendarModel.GetThemeProvider().SetCurrentTheme(theme);
                    source.CalendarModel.ResetCustomization();
                    _dropDowns.Except(new[] { themesDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateAppearance();
                },
                Fields.Theme,
                rect);
            themesDropDown.SetTextFieldText(source.CalendarModel.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");

            titleFontDropDown.InitSource(
                WorkTimeConstants.TitleFonts,
                font => source.CalendarModel.TitleFont = font,
                Fields.TitleFont,
                rect);

            dayFontDropDown.InitSource(
                WorkTimeConstants.DayFonts,
                font => source.CalendarModel.DayTextFont = font,
                Fields.DayTextFont,
                rect);

            titleSizeDropDown.InitSource(
                WorkTimeConstants.TitleTextSizes,
                size => source.CalendarModel.TitleTextSize = size,
                Fields.TitleTextSize,
               rect);

            dayTextSizeDropDown.InitSource(
                WorkTimeConstants.DayTextSizes,
                size => source.CalendarModel.DayTextSize = size,
                Fields.DayTextSize,
               rect);

            dayTextColorDropDown.InitSource(
                WorkTimeConstants.DayColors,
                color => source.CalendarModel.DayTextColor = color,
                Fields.DayTextColor,
                rect);

            currentDayTextColorDropDown.InitSource(
                WorkTimeConstants.CurrentDayColors,
               color => source.CalendarModel.CurrentDayTextColor = color,
               Fields.CurrentDayTextColor,
               rect);

            titleColorDropDown.InitSource(
                WorkTimeConstants.TitleColors,
              color => source.CalendarModel.TitleColor = color,
              Fields.TitleColor,
              rect);

            currentDayBackgroundColorDropDown.InitSource(
                WorkTimeConstants.CurrentDayBackgroundColors,
                color => source.CalendarModel.CurrentDayBackgroundColor = color,
                Fields.CurrentDayBackgroundColor,
               rect);

            devidersColor.InitSource(
                WorkTimeConstants.DevidersColors,
                color => source.CalendarModel.ColorDividers = color,
                Fields.ColorDividers,
                rect);

            currentDayDevidersColor.InitSource(
                WorkTimeConstants.CurrentDayDeviderColors,
                color => source.CalendarModel.CurrentColorDividers = color,
                Fields.CurrentColorDividers,
                rect);

            dayEvenBackgroundColor.InitSource(
                WorkTimeConstants.DayEvenBackgroundColors,
                color => source.CalendarModel.DayEvenBackgroundColor = color,
                Fields.DayEvenBackgroundColor,
                rect);

            weekStartDropdown.InitSource(
                Days.DaysCollection,
                weekStart => source.CalendarModel.WeekStart = weekStart,
                Fields.WeekStartDay,
                rect);

            resetButton.TouchUpInside += (sender, e) =>
            {
                _dropDowns.Except(new[] { themesDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                source.CalendarModel.ResetCustomization();
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
    }
}
