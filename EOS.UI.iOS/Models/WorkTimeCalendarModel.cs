using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Models
{
    public class WorkTimeCalendarModel : IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

        public EventHandler ItemsChanged;

        private IEnumerable<WorkTimeCalendarItem> _items;
        public IEnumerable<WorkTimeCalendarItem> Items
        {
            get => _items;
            set
            {
                if (value.Count() != 7)
                    throw new Exception("datasource must contain 7 week days");
                _items = value;
                ItemsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public DayOfWeek WeekStart { get; set; }
        
        private UIFont _titleFont;
        public UIFont TitleFont
        {
            get => _titleFont;
            set
            {
                _titleFont = value;
                IsEOSCustomizationIgnored = true;
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
            }
        }

        private UIColor _dayUnevenBackgroundColor;
        public UIColor DayUnevenBackgroundColor
        {
            get => _dayUnevenBackgroundColor;
            set
            {
                _dayUnevenBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
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
            if (!IsEOSCustomizationIgnored)
            {
                var provider = EOSThemeProvider.Instance;
                TitleColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                TitleFont = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                DayTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor5);
                DayTextFont = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                CurrentDayBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                
                    
                
            }
        }
        
    }
}