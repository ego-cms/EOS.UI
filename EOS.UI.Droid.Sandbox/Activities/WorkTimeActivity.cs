using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using EOS.UI.Droid.Components;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.ControlConstants.Android;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.WorkTimeCalendar, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class WorkTimeActivity : BaseActivity
    {
        private WorkTime _workTime;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _titleFontDropDown;
        private EOSSandboxDropDown _dayTextFontDropDown;
        private EOSSandboxDropDown _titleTextSizeDropDown;
        private EOSSandboxDropDown _dayTextSizeDropDown;
        private EOSSandboxDropDown _titleColorDropDown;
        private EOSSandboxDropDown _dayTextColorDropDown;
        private EOSSandboxDropDown _currentDayBackgroundColorDropDown;
        private EOSSandboxDropDown _currentDayTextColorDropDown;
        private EOSSandboxDropDown _dayEvenBackgroundColorDropDown;
        private EOSSandboxDropDown _dividerColorDropDown;
        private EOSSandboxDropDown _currentDividerColorDropDown;
        private EOSSandboxDropDown _firstDayOfWeekDropDown;
        private List<EOSSandboxDropDown> _customFields;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WorkTimeActivity);

            _workTime = FindViewById<WorkTime>(Resource.Id.workTime);
            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _titleFontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.titleFontDropDown);
            _dayTextFontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.dayTextFontDropDown);
            _titleTextSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.titleTextSizeDropDown);
            _dayTextSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.dayTextSizeDropDown);
            _titleColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.titleColorDropDown);
            _dayTextColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.dayTextColorDropDown);
            _currentDayBackgroundColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.currentDayBackgroundColorDropDown);
            _currentDayTextColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.currentDayTextColorDropDown);
            _dayEvenBackgroundColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.dayEvenBackgroundColorDropDown);
            _dividerColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.dividerColorDropDown);
            _currentDividerColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.currentDividerColorDropDown);
            _firstDayOfWeekDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.firstDayOfWeek);
            var resetButton = FindViewById<EOSSandboxButton>(Resource.Id.buttonResetCustomization);

            _customFields = new List<EOSSandboxDropDown>
            {
                _titleFontDropDown,
                _dayTextFontDropDown,
                _titleTextSizeDropDown,
                _dayTextSizeDropDown,
                _titleColorDropDown,
                _dayTextColorDropDown,
                _currentDayBackgroundColorDropDown,
                _currentDayTextColorDropDown,
                _dayEvenBackgroundColorDropDown,
                _dividerColorDropDown,
                _currentDividerColorDropDown,
                _firstDayOfWeekDropDown
            };

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _titleFontDropDown.Name = Fields.TitleFont;
            _titleFontDropDown.SetupAdapter(WorkTimeConstants.TitleFonts.Select(item => item.Key).ToList());
            _titleFontDropDown.ItemSelected += TitleFontItemSelected;

            _dayTextFontDropDown.Name = Fields.DayTextFont;
            _dayTextFontDropDown.SetupAdapter(WorkTimeConstants.DayFonts.Select(item => item.Key).ToList());
            _dayTextFontDropDown.ItemSelected += DayTextFonItemSelected;

            _titleTextSizeDropDown.Name = Fields.TitleTextSize;
            _titleTextSizeDropDown.SetupAdapter(WorkTimeConstants.TitleTextSizes.Select(item => item.Key).ToList());
            _titleTextSizeDropDown.ItemSelected += TitleTextSizeItemSelected;

            _dayTextSizeDropDown.Name = Fields.DayTextSize;
            _dayTextSizeDropDown.SetupAdapter(WorkTimeConstants.DayTextSizes.Select(item => item.Key).ToList());
            _dayTextSizeDropDown.ItemSelected += DayTextSizeItemSelected;

            _titleColorDropDown.Name = Fields.TitleColor;
            _titleColorDropDown.SetupAdapter(WorkTimeConstants.TitleColors.Select(item => item.Key).ToList());
            _titleColorDropDown.ItemSelected += TitleColorItemSelected;

            _dayTextColorDropDown.Name = Fields.DayTextColor;
            _dayTextColorDropDown.SetupAdapter(WorkTimeConstants.DayColors.Select(item => item.Key).ToList());
            _dayTextColorDropDown.ItemSelected += DayTextColorItemSelected;

            _currentDayBackgroundColorDropDown.Name = Fields.CurrentDayBackgroundColor;
            _currentDayBackgroundColorDropDown.SetupAdapter(WorkTimeConstants.CurrentDayBackgroundColors.Select(item => item.Key).ToList());
            _currentDayBackgroundColorDropDown.ItemSelected += CurrentDayBackgroundColorItemSelected;

            _currentDayTextColorDropDown.Name = Fields.CurrentDayTextColor;
            _currentDayTextColorDropDown.SetupAdapter(WorkTimeConstants.CurrentDayColors.Select(item => item.Key).ToList());
            _currentDayTextColorDropDown.ItemSelected += CurrentDayTextColorItemSelected;

            _dayEvenBackgroundColorDropDown.Name = Fields.DayEvenBackgroundColor;
            _dayEvenBackgroundColorDropDown.SetupAdapter(WorkTimeConstants.DayEvenBackgroundColors.Select(item => item.Key).ToList());
            _dayEvenBackgroundColorDropDown.ItemSelected += DayEvenBackgroundColorItemSelected;

            _dividerColorDropDown.Name = Fields.ColorDividers;
            _dividerColorDropDown.SetupAdapter(WorkTimeConstants.DividersColors.Select(item => item.Key).ToList());
            _dividerColorDropDown.ItemSelected += DividerColorItemSelected;

            _currentDividerColorDropDown.Name = Fields.CurrentColorDividers;
            _currentDividerColorDropDown.SetupAdapter(WorkTimeConstants.CurrentDayDividerColors.Select(item => item.Key).ToList());
            _currentDividerColorDropDown.ItemSelected += CurrentDividerColorItemSelected;

            _firstDayOfWeekDropDown.Name = Fields.WeekStartDay;
            _firstDayOfWeekDropDown.SetupAdapter(Days.DaysCollection.Select(item => item.Key).ToList());
            _firstDayOfWeekDropDown.ItemSelected += FirstDayOfWeekItemSelected;

            _workTime.Items = GenerateDaysOfWeekSource();

            SetCurrenTheme(_workTime.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            ResetViewWhenActivityRecreated(savedInstanceState);
        }

        private void ResetViewWhenActivityRecreated(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
                ResetAndUpdateView();
        }

        private void FirstDayOfWeekItemSelected(int position)
        {
            if (position > 0)
                _workTime.WeekStart = Days.DaysCollection.ElementAt(position).Value;
        }

        private void CurrentDividerColorItemSelected(int position)
        {
            _workTime.CurrentDividerColor = WorkTimeConstants.CurrentDayDividerColors.ElementAt(position).Value;
        }

        private void DividerColorItemSelected(int position)
        {
            _workTime.DividerColor = WorkTimeConstants.DividersColors.ElementAt(position).Value;
        }

        private void DayEvenBackgroundColorItemSelected(int position)
        {
            _workTime.DayEvenBackgroundColor = WorkTimeConstants.DayEvenBackgroundColors.ElementAt(position).Value;
        }

        private void CurrentDayTextColorItemSelected(int position)
        {
            _workTime.CurrentDayTextColor = WorkTimeConstants.CurrentDayColors.ElementAt(position).Value;
        }

        private void CurrentDayBackgroundColorItemSelected(int position)
        {
            _workTime.CurrentDayBackgroundColor = WorkTimeConstants.CurrentDayBackgroundColors.ElementAt(position).Value;
        }

        private void DayTextColorItemSelected(int position)
        {
            _workTime.DayTextColor = WorkTimeConstants.DayColors.ElementAt(position).Value;
        }

        private void TitleColorItemSelected(int position)
        {
            _workTime.TitleColor = WorkTimeConstants.TitleColors.ElementAt(position).Value;
        }

        private void DayTextSizeItemSelected(int position)
        {
            _workTime.DayTextSize = (int) WorkTimeConstants.DayTextSizes.ElementAt(position).Value;
        }

        private void TitleTextSizeItemSelected(int position)
        {
            _workTime.TitleTextSize = (int)WorkTimeConstants.TitleTextSizes.ElementAt(position).Value;
        }

        private void DayTextFonItemSelected(int position)
        {
            _workTime.DayTextFont = Typeface.CreateFromAsset(Assets, WorkTimeConstants.DayFonts.ElementAt(position).Value);
        }

        private void TitleFontItemSelected(int position)
        {
            _workTime.TitleFont = Typeface.CreateFromAsset(Assets, WorkTimeConstants.TitleFonts.ElementAt(position).Value);
        }

        private void ThemeItemSelected(int position)
        {
            if (position > 0)
            {
                _workTime.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                if(_workTime.GetThemeProvider().GetCurrentTheme() is LightEOSTheme)
                {
                    _workTime.SetBackgroundColor(Color.White);
                }
                else
                {
                    _workTime.SetBackgroundColor(Color.Transparent);
                }
                ResetCustomValues();
                UpdateAppearance();
            }
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if (iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if (iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void ResetCustomValues()
        {
            _workTime.ResetCustomization();
            _customFields.ForEach(item => item.SetSpinnerSelection(0));
        }

        private List<WorkTimeCalendarItem> GenerateDaysOfWeekSource()
        {
            return new List<WorkTimeCalendarItem>()
            {
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Sunday,
                    IsDayOff = true
                },
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Monday,
                    IsDayOff = false,
                    HasBreak = true,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    BreakStartTime = new TimeSpan(13, 0, 0),
                    BreakEndTime = new TimeSpan(14, 0, 0)
                },
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Tuesday,
                    IsDayOff = false,
                    HasBreak = true,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    BreakStartTime = new TimeSpan(13, 0, 0),
                    BreakEndTime = new TimeSpan(14, 0, 0)
                },
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Wednesday,
                    IsDayOff = false,
                    HasBreak = true,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    BreakStartTime = new TimeSpan(13, 0, 0),
                    BreakEndTime = new TimeSpan(14, 0, 0)
                },
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Thursday,
                    IsDayOff = false,
                    HasBreak = true,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                    BreakStartTime = new TimeSpan(13, 0, 0),
                    BreakEndTime = new TimeSpan(14, 0, 0)
                },
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Friday,
                    IsDayOff = false,
                    HasBreak = false,
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0),
                },
                new WorkTimeCalendarItem()
                {
                    WeekDay = DayOfWeek.Saturday,
                    IsDayOff = true,
                }
            };
        }
    }
}
