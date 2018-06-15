using System;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public partial class WorkTimeCalendarCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("WorkTimeCalendarCell");
        public static readonly UINib Nib;

        public UIFont TitleFont
        {
            get => dayLabel.Font;
            set => dayLabel.Font = value.WithSize(TitleTextSize);
        }

        public UIFont DayTextFont
        {
            get => startWorkLabel.Font;
            set
            {
                startWorkLabel.Font = value.WithSize(DayTextSize);
                stopWorkLabel.Font = value.WithSize(DayTextSize);
                startBreakLabel.Font = value.WithSize(DayTextSize);
                stopBreakLabel.Font = value.WithSize(DayTextSize);
            }
        }

        public UIColor TitleColor
        {
            get => dayLabel.TextColor;
            set => dayLabel.TextColor = value;
        }

        public UIColor DayTextColor
        {
            get => startWorkLabel.TextColor;
            set
            {
                startWorkLabel.TextColor = value;
                stopWorkLabel.TextColor = value;
                startBreakLabel.TextColor = value;
                stopBreakLabel.TextColor = value;
                dayOffDevider.BackgroundColor = value;
                breakDevider.BackgroundColor = value;
            }
        }

        public int TitleTextSize
        {
            get => (int)dayLabel.Font.PointSize;
            set => dayLabel.Font = dayLabel.Font.WithSize(value);
        }

        public int DayTextSize
        {
            get => (int)startWorkLabel.Font.PointSize;
            set
            {
                startWorkLabel.Font = DayTextFont.WithSize(value);
                stopWorkLabel.Font = DayTextFont.WithSize(value);
                startBreakLabel.Font = DayTextFont.WithSize(value);
                stopBreakLabel.Font = DayTextFont.WithSize(value);
            }
        }

        private bool WorkTimeVisible
        {
            set
            {
                startWorkLabel.Hidden = value;
                stopWorkLabel.Hidden = value;
                dayOffDevider.Hidden = !value;
            }
        }

        private bool BreakTimeVisible
        {
            set
            {
                startBreakLabel.Hidden = !value;
                stopBreakLabel.Hidden = !value;
                breakDevider.Hidden = value;
            }
        }

        public UIColor CellBackgroundColor
        {
            get => cellContentView.BackgroundColor;
            set => cellContentView.BackgroundColor = value;
        }

        private UIColor _dividersColor;
        public UIColor DividersColor
        {
            get => _dividersColor;
            set
            {
                _dividersColor = value;
                weekDayDevider.BackgroundColor = value;
                circleDevider.BackgroundColor = value;
            }
        }

        static WorkTimeCalendarCell()
        {
            Nib = UINib.FromName("WorkTimeCalendarCell", NSBundle.MainBundle);
        }

        protected WorkTimeCalendarCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void Init(WorkTimeCalendarItem day)
        {
            dayLabel.Text = day.ShortWeekDay;
            WorkTimeVisible = day.IsDayOff;
            if (day.IsDayOff)
            {
                startBreakLabel.Hidden = true;
                stopBreakLabel.Hidden = true;
                breakDevider.Hidden = true;
            }
            else
            {
                BreakTimeVisible = day.HasBreak;
            }

            startWorkLabel.Text = day.StartTime.ToShortString();
            stopWorkLabel.Text = day.EndTime.ToShortString();
            startBreakLabel.Text = day.BreakStartTime.ToShortString();
            stopBreakLabel.Text = day.BreakEndTime.ToShortString();
        }
    }
}