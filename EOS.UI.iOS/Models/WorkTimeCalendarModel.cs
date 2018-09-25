using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Models
{
    public class WorkTimeCalendarModel : IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

        public EventHandler PropertyChanged { get; set; }

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

        private FontStyleItem _dayFontStyle;
        public FontStyleItem DayFontStyle
        {
            get => _dayFontStyle;
            set => _dayFontStyle = value;
        }
        
        private FontStyleItem _titleFontStyle;
        public FontStyleItem TitleFontStyle
        {
            get => _titleFontStyle;
            set => _titleFontStyle = value;
        }
        
        private FontStyleItem _currentDayFontStyle;
        public FontStyleItem CurrentDayFontStyle
        {
            get => _currentDayFontStyle;
            set => _currentDayFontStyle = value;
        }

        public UIFont TitleFont
        {
            get => TitleFontStyle.Font;
            set
            {
                TitleFontStyle.Font = value.WithSize(TitleTextSize);
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public UIFont DayTextFont
        {
            get => DayFontStyle.Font;
            set
            {
                DayFontStyle.Font = value.WithSize(DayTextSize);
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public UIColor TitleColor
        {
            get => TitleFontStyle.Color;
            set
            {
                TitleFontStyle.Color = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public UIColor DayTextColor
        {
            get => DayFontStyle.Color;
            set
            {
                DayFontStyle.Color = value;
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

        public UIColor CurrentDayTextColor
        {
            get => CurrentDayFontStyle.Color;
            set
            {
                CurrentDayFontStyle.Color = value;
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

        public float TitleTextSize
        {
            get => TitleFontStyle.Size;
            set
            {
                TitleFontStyle.Size = value;
                IsEOSCustomizationIgnored = true;
                PropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public float DayTextSize
        {
            get => DayFontStyle.Size;
            set
            {
                DayFontStyle.Size = value;
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

        private UIColor _currentColorDividers;
        public UIColor CurrentColorDividers
        {
            get => _currentColorDividers;
            set
            {
                _currentColorDividers = value;
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
                TitleFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C2);
                DayFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R1C3);
                CurrentDayFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R1C6S);
                CurrentDayBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                DayEvenBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor5);
                ColorDividers = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
                CurrentColorDividers = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
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