using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using EOS.UI.Android.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    internal class WorkTimeAdapter : RecyclerView.Adapter, IEOSThemeControl
    {
        #region fields

        private const string Dash = "�";
        private int _selectedWorkDay = -1;

        #endregion

        #region RecyclerView.Adapter implementation

        public override int ItemCount => 7;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var workTimeItem = holder as WorkTimeItem;
            var workDayModel = Items[position];

            workTimeItem.StartDayTimeLabel.IsEmpty = false;
            workTimeItem.EndDayTimeLabel.IsEmpty = false;
            workTimeItem.StartBreakTimeLabel.IsEmpty = false;
            workTimeItem.EndBreakTimeLabel.IsEmpty = false;

            workTimeItem.DayLabel.Text = workDayModel.ShortWeekDay;
            if(!workDayModel.IsDayOff)
            {
                workTimeItem.StartDayTimeLabel.Text = workDayModel.StartTime.GetString();
                workTimeItem.EndDayTimeLabel.Text = workDayModel.EndTime.GetString();
                if(workDayModel.HasBreak)
                {
                    workTimeItem.StartBreakTimeLabel.Text = workDayModel.BreakStartTime.GetString();
                    workTimeItem.EndBreakTimeLabel.Text = workDayModel.BreakEndTime.GetString();
                }
                else
                {
                    workTimeItem.StartBreakTimeLabel.Text = Dash;
                    workTimeItem.EndBreakTimeLabel.IsEmpty = true;
                }
            }
            else
            {
                workTimeItem.StartDayTimeLabel.Text = Dash;
                workTimeItem.EndDayTimeLabel.IsEmpty = true;
                workTimeItem.StartBreakTimeLabel.IsEmpty = true;
                workTimeItem.EndBreakTimeLabel.IsEmpty = true;
            }

            workTimeItem.Container.IsEven = position % 2 == 0;
            UpdateISelectableViews(workTimeItem.Container, _selectedWorkDay == position);

            if(TitleFont != null)
                workTimeItem.DayLabel.TitleFont = TitleFont;

            if(DayTextFont != null)
            {
                workTimeItem.StartDayTimeLabel.DayTextFont = DayTextFont;
                workTimeItem.EndDayTimeLabel.DayTextFont = DayTextFont;
                workTimeItem.StartBreakTimeLabel.DayTextFont = DayTextFont;
                workTimeItem.EndBreakTimeLabel.DayTextFont = DayTextFont;
            }

            if(TitleTextSize != 0)
                workTimeItem.DayLabel.TitleTextSize = TitleTextSize;

            if(DayTextSize != 0)
            {
                workTimeItem.StartDayTimeLabel.DayTextSize = DayTextSize;
                workTimeItem.EndDayTimeLabel.DayTextSize = DayTextSize;
                workTimeItem.StartBreakTimeLabel.DayTextSize = DayTextSize;
                workTimeItem.EndBreakTimeLabel.DayTextSize = DayTextSize;
            }

            if(TitleColor != Color.Transparent)
                workTimeItem.DayLabel.TitleColor = TitleColor;

            if(DayTextColor != Color.Transparent)
            {
                workTimeItem.StartDayTimeLabel.DayTextColor = DayTextColor;
                workTimeItem.EndDayTimeLabel.DayTextColor = DayTextColor;
                workTimeItem.StartBreakTimeLabel.DayTextColor = DayTextColor;
                workTimeItem.EndBreakTimeLabel.DayTextColor = DayTextColor;
            }

            if(CurrentDayBackgroundColor != Color.Transparent)
                workTimeItem.Container.CurrentDayBackgroundColor = CurrentDayBackgroundColor;

            if(DayEvenBackgroundColor != Color.Transparent)
                workTimeItem.Container.DayEvenBackgroundColor = DayEvenBackgroundColor;

            if(DividerColor != Color.Transparent)
            {
                workTimeItem.DayDivider.DividerColor = DividerColor;
                workTimeItem.CircleDivider.DividerColor = DividerColor;
            }

            if(CurrentDividerColor != Color.Transparent)
            {
                workTimeItem.DayDivider.CurrentDividerColor = CurrentDividerColor;
                workTimeItem.CircleDivider.CurrentDividerColor = CurrentDividerColor;
            }

            if(IsEOSCustomizationIgnored)
                UpdateAppearance(workTimeItem.Container);
            else
                ResetCustomization(workTimeItem.Container);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.WorkTimeItemLayout, parent, false);
            var viewHolder = new WorkTimeItem(itemView, SetSelectedDay);
            return viewHolder;
        }

        #endregion

        #region customization

        private WeekStartEnum _weekStart;
        public WeekStartEnum WeekStart
        {
            get => _weekStart;
            set
            {
                _weekStart = value;
                Items = Items.SetFirstDayOfWeek(_weekStart);
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Typeface _titleFont;
        public Typeface TitleFont
        {
            get => _titleFont;
            set
            {
                _titleFont = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Typeface _dayTextFont;
        public Typeface DayTextFont
        {
            get => _dayTextFont;
            set
            {
                _dayTextFont = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private int _titleTextSize;
        public int TitleTextSize
        {
            get => _titleTextSize;
            set
            {
                _titleTextSize = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private int _dayTextSize;
        public int DayTextSize
        {
            get => _dayTextSize;
            set
            {
                _dayTextSize = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _titleColor;
        public Color TitleColor
        {
            get => _titleColor;
            set
            {
                _titleColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _dayTextColor;
        public Color DayTextColor
        {
            get => _dayTextColor;
            set
            {
                _dayTextColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _currentDayBackgroundColor;
        public Color CurrentDayBackgroundColor
        {
            get => _currentDayBackgroundColor;
            set
            {
                _currentDayBackgroundColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _currentDayTextColor;
        public Color CurrentDayTextColor
        {
            get => _currentDayTextColor;
            set
            {
                _currentDayTextColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _dayEvenBackgroundColor;
        public Color DayEvenBackgroundColor
        {
            get => _dayEvenBackgroundColor;
            set
            {
                _dayEvenBackgroundColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _dividerColor;
        public Color DividerColor
        {
            get => _dividerColor;
            set
            {
                _dividerColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private Color _currentDividerColor;
        public Color CurrentDividerColor
        {
            get => _currentDividerColor;
            set
            {
                _currentDividerColor = value;
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        private List<WorkTimeCalendarItem> _items;
        public List<WorkTimeCalendarItem> Items
        {
            get => _items;
            set
            {
                if(value.Count != 7)
                    throw new ArgumentOutOfRangeException(nameof(Items), "Days of the week should be 7!");

                _items = value.SortByDayOfWeek(WeekStart);
                IsEOSCustomizationIgnored = false;
                NotifyDataSetChanged();
            }
        }

        #endregion

        #region utility methods

        private void SetSelectedDay(int position)
        {
            _selectedWorkDay = position;
            NotifyDataSetChanged();
        }

        private List<View> GetAllChildren(View view)
        {
            if(!(view is ViewGroup))
                return new List<View> { view };

            var result = new List<View>() { view };

            var viewGroup = (ViewGroup)view;
            for(int i = 0; i < viewGroup.ChildCount; i++)
                result.AddRange(GetAllChildren(viewGroup.GetChildAt(i)));

            return result;
        }

        private void UpdateISelectableViews(View parent, bool isSelected)
        {
            var views = GetAllChildren(parent);
            foreach(var view in views)
                if(view is ISelectable selectable)
                    selectable.IsSelected = isSelected;
        }

        private void UpdateAppearance(View parent)
        {
            var views = GetAllChildren(parent);
            foreach(var view in views)
                if(view is IEOSThemeControl themeControl)
                    themeControl.UpdateAppearance();
        }

        private void ResetCustomization(View parent)
        {
            var views = GetAllChildren(parent);
            foreach(var view in views)
                if(view is IEOSThemeControl themeControl)
                    themeControl.ResetCustomization();
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            NotifyDataSetChanged();
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        #endregion
    }
}
