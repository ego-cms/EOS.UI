using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Models
{
    public class WorkTimeCalendarModel : IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

        public EventHandler PropertyChanged;

        private IEnumerable<WorkTimeCalendarItem> _items;
        public IEnumerable<WorkTimeCalendarItem> Items
        {
            get => _items;
            set
            {
                if (value.Count() != 7)
                    throw new Exception("datasource must contain 7 week days");
                _items = value.SortWeekByFirstDay(WeekStart);
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private WeekStartEnum _weekStart;
        public WeekStartEnum WeekStart
        {
            get => _weekStart;
            set
            {
                _weekStart = value;
                _items = _items?.SortWeekByFirstDay(value);
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIFont _titleFont;
        public UIFont TitleFont
        {
            get => _titleFont;
            set
            {
                _titleFont = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIFont _dayTextFont;
        public UIFont DayTextFont
        {
            get => _dayTextFont;
            set
            {
                _dayTextFont = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _titleColor;
        public UIColor TitleColor
        {
            get => _titleColor;
            set
            {
                _titleColor = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _dayTextColor;
        public UIColor DayTextColor
        {
            get => _dayTextColor;
            set
            {
                _dayTextColor = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _currentDayBackgroundColor;
        public UIColor CurrentDayBackgroundColor
        {
            get => _currentDayBackgroundColor;
            set
            {
                _currentDayBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _currentDayTextColor;
        public UIColor CurrentDayTextColor
        {
            get => _currentDayTextColor;
            set
            {
                _currentDayTextColor = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _dayEvenBackgroundColor;
        public UIColor DayEvenBackgroundColor
        {
            get => _dayEvenBackgroundColor;
            set
            {
                _dayEvenBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _titleTextSize;
        public int TitleTextSize
        {
            get => _titleTextSize;
            set
            {
                _titleTextSize = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _dayTextSize;
        public int DayTextSize
        {
            get => _dayTextSize;
            set
            {
                _dayTextSize = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _colorDividers;
        public UIColor ColorDividers
        {
            get => _colorDividers;
            set
            {
                _colorDividers = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _currentColorDeviders;
        public UIColor CurrentColorDeviders
        {
            get => _currentColorDeviders;
            set
            {
                _currentColorDeviders = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
            throw new NotImplementedException();
        }

        public void UpdateAppearance()
        {
            WeekStart = GetDefaultWeekStartDay();
            if (!IsEOSCustomizationIgnored)
            {
                var provider = EOSThemeProvider.Instance;
                TitleColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1);
                TitleFont = provider.GetEOSProperty<UIFont>(this, EOSConstants.WorkTimeTitleFont);
                DayTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor2);
                DayTextFont = provider.GetEOSProperty<UIFont>(this, EOSConstants.WorkTimeDayTextFont);
                CurrentDayBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                CurrentDayTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                TitleTextSize = provider.GetEOSProperty<int>(this, EOSConstants.WorkTimeTitleSize);
                DayTextSize = provider.GetEOSProperty<int>(this, EOSConstants.WorkTimeDayTextSize);
                DayEvenBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor5);
                ColorDividers = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
                CurrentColorDeviders = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
            }
        }

        private WeekStartEnum GetDefaultWeekStartDay()
        {
            var calendar = NSCalendar.CurrentCalendar;
            double intervel = 0.0;
            NSDate startOfWeek = null;
            calendar.Range(NSCalendarUnit.WeekOfMonth, out startOfWeek, out intervel, new NSDate());
            var weekDay = startOfWeek.ToDateTime().DayOfWeek;
            return (WeekStartEnum)weekDay;
        }
    }
}