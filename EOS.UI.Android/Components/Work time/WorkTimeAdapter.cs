using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static EOS.UI.Android.Helpers.Constants;

namespace EOS.UI.Android.Components
{
    internal class WorkTimeAdapter : RecyclerView.Adapter, IEOSThemeControl
    {
        #region fields

        private const string Dash = "----";
        private const float DashLetterSpacing = -0.2f; 
        private const string EmptyTime = "00:00";
        private int _sectionWidth;
        private Context _context;
        private bool _isEven;
        private bool _isCurrentDay;
        private DayOfWeek _startDayOfweek;

        #endregion

        #region conastructors

        public WorkTimeAdapter(Context context, int sectionWidth)
        {
            _context = context;
            _sectionWidth = sectionWidth;
            _startDayOfweek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }

        #endregion

        #region RecyclerView.Adapter implementation

        public override int ItemCount => WorkTimeConstants.DaysCount;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var workTimeItem = holder as WorkTimeItem;

            if(Items == null)
                _items = GenerateDefaultItems();

            SetDataFromModel(workTimeItem, Items[position]);

            _isEven = position % 2 == 0;
            _isCurrentDay = IsThisCurrentDay(Items[position].WeekDay);

            SetTitleFont(workTimeItem);
            SetDayTextFont(workTimeItem);
            SetTitleTextSize(workTimeItem);
            SetDayTextSize(workTimeItem);
            SetTitleColor(workTimeItem);
            SetDayTextColor(workTimeItem);
            SetCurrentDayBackgroundColor(workTimeItem);
            SetCurrentDayTextColor(workTimeItem);
            SetDayEvenBackgroundColor(workTimeItem);
            SetDividerColor(workTimeItem);
            SetCurrentDividerColor(workTimeItem);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.WorkTimeItemLayout, parent, false);
            var viewHolder = new WorkTimeItem(itemView);
            itemView.LayoutParameters.Width = _sectionWidth;
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
                Items = Items.SortWeekByFirstDay(_weekStart).ToList();
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
                TitleFontStyle.Typeface = value; 
                IsEOSCustomizationIgnored = true;
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
                DayFontStyle.Typeface = value;
                IsEOSCustomizationIgnored = true;
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
                TitleFontStyle.Size = value;
                IsEOSCustomizationIgnored = true;
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
                DayFontStyle.Size = value;
                IsEOSCustomizationIgnored = true;
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
                TitleFontStyle.Color = value;
                IsEOSCustomizationIgnored = true;
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
                DayFontStyle.Color = value;
                IsEOSCustomizationIgnored = true;
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
                IsEOSCustomizationIgnored = true;
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
                IsEOSCustomizationIgnored = true;
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
                IsEOSCustomizationIgnored = true;
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
                IsEOSCustomizationIgnored = true;
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
                IsEOSCustomizationIgnored = true;
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

                _items = value.SortWeekByFirstDay(WeekStart).ToList();
                IsEOSCustomizationIgnored = true;
                NotifyDataSetChanged();
            }
        }

        private FontStyleItem _titleFontStyle;
        public FontStyleItem TitleFontStyle
        {
            get => _titleFontStyle;
            set
            {
                _titleFontStyle = value;
                SetTitleFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private FontStyleItem _dayFontStyle;
        public FontStyleItem DayFontStyle
        {
            get => _dayFontStyle;
            set
            {
                _dayFontStyle = value;
                SetDayFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private void SetTitleFontStyle()
        {
            _titleFont = TitleFontStyle.Typeface;
            _titleTextSize = (int)TitleFontStyle.Size;
            _titleColor = TitleFontStyle.Color;
        }

        private void SetDayFontStyle()
        {
            _dayTextFont = DayFontStyle.Typeface;
            _dayTextSize = (int)DayFontStyle.Size;
            _dayTextColor = DayFontStyle.Color;
        }

        #endregion

        #region utility methods

        private void SetDataFromModel(WorkTimeItem workTimeItem, WorkTimeCalendarItem workDayModel)
        {
            workTimeItem.StartDayTimeLabel.Text = EmptyTime;
            workTimeItem.EndDayTimeLabel.Text = EmptyTime;
            workTimeItem.StartBreakTimeLabel.Text = EmptyTime;
            workTimeItem.EndBreakTimeLabel.Text = EmptyTime;

            workTimeItem.StartDayTimeLabel.LetterSpacing = 0;
            workTimeItem.StartBreakTimeLabel.LetterSpacing = 0;

            workTimeItem.DayLabel.Text = workDayModel.ShortWeekDay;
            if(!workDayModel.IsDayOff)
            {
                workTimeItem.StartDayTimeLabel.Text = workDayModel.StartTime.ToShortString();
                workTimeItem.EndDayTimeLabel.Text = workDayModel.EndTime.ToShortString();
                if(workDayModel.HasBreak)
                {
                    workTimeItem.StartBreakTimeLabel.Text = workDayModel.BreakStartTime.ToShortString();
                    workTimeItem.EndBreakTimeLabel.Text = workDayModel.BreakEndTime.ToShortString();
                }
                else
                {
                    workTimeItem.StartBreakTimeLabel.Text = Dash;
                    workTimeItem.StartBreakTimeLabel.LetterSpacing = DashLetterSpacing;
                    workTimeItem.EndBreakTimeLabel.Text = EmptyTime;
                }
            }
            else
            {
                workTimeItem.StartDayTimeLabel.Text = Dash;
                workTimeItem.StartDayTimeLabel.LetterSpacing = DashLetterSpacing;
                workTimeItem.EndDayTimeLabel.Text = EmptyTime;
                workTimeItem.StartBreakTimeLabel.Text = EmptyTime;
                workTimeItem.EndBreakTimeLabel.Text = EmptyTime;
            }
        }

        private void SetDayTextFont(WorkTimeItem workTimeItem)
        {
            if(DayTextFont != null)
            {
                workTimeItem.StartDayTimeLabel.Typeface = DayTextFont;
                workTimeItem.EndDayTimeLabel.Typeface = DayTextFont;
                workTimeItem.StartBreakTimeLabel.Typeface = DayTextFont;
                workTimeItem.EndBreakTimeLabel.Typeface = DayTextFont;
            }
        }

        private void SetTitleFont(WorkTimeItem workTimeItem)
        {
            if(TitleFont != null)
                workTimeItem.DayLabel.Typeface = TitleFont;
        }

        private void SetTitleTextSize(WorkTimeItem workTimeItem)
        {
            if(TitleTextSize != 0)
                workTimeItem.DayLabel.TextSize = TitleTextSize;
        }

        private void SetDayTextSize(WorkTimeItem workTimeItem)
        {
            if(DayTextSize != 0)
            {
                workTimeItem.StartDayTimeLabel.TextSize = DayTextSize;
                workTimeItem.EndDayTimeLabel.TextSize = DayTextSize;
                workTimeItem.StartBreakTimeLabel.TextSize = DayTextSize;
                workTimeItem.EndBreakTimeLabel.TextSize = DayTextSize;
            }
        }

        private void SetTitleColor(WorkTimeItem workTimeItem)
        {
            if(TitleColor != default(Color))
                workTimeItem.DayLabel.SetTextColor(TitleColor);
        }

        private void SetDayTextColor(WorkTimeItem workTimeItem)
        {
            if(DayTextColor != default(Color))
            {
                workTimeItem.StartDayTimeLabel.SetTextColor(workTimeItem.StartDayTimeLabel.Text == EmptyTime ? Color.Transparent : DayTextColor);
                workTimeItem.EndDayTimeLabel.SetTextColor(workTimeItem.EndDayTimeLabel.Text == EmptyTime ? Color.Transparent : DayTextColor);
                workTimeItem.StartBreakTimeLabel.SetTextColor(workTimeItem.StartBreakTimeLabel.Text == EmptyTime ? Color.Transparent : DayTextColor);
                workTimeItem.EndBreakTimeLabel.SetTextColor(workTimeItem.EndBreakTimeLabel.Text == EmptyTime ? Color.Transparent : DayTextColor);
            }
        }

        private void SetCurrentDayBackgroundColor(WorkTimeItem workTimeItem)
        {
            if(CurrentDayBackgroundColor != default(Color) && _isCurrentDay)
                workTimeItem.Container.Background = CreateGradientDrawable(CurrentDayBackgroundColor);
        }

        private void SetCurrentDayTextColor(WorkTimeItem workTimeItem)
        {
            if(CurrentDayTextColor != default(Color) && _isCurrentDay)
            {
                workTimeItem.DayLabel.SetTextColor(CurrentDayTextColor);
                workTimeItem.StartDayTimeLabel.SetTextColor(workTimeItem.StartDayTimeLabel.Text == EmptyTime ? Color.Transparent : CurrentDayTextColor);
                workTimeItem.EndDayTimeLabel.SetTextColor(workTimeItem.EndDayTimeLabel.Text == EmptyTime ? Color.Transparent : CurrentDayTextColor);
                workTimeItem.StartBreakTimeLabel.SetTextColor(workTimeItem.StartBreakTimeLabel.Text == EmptyTime ? Color.Transparent : CurrentDayTextColor);
                workTimeItem.EndBreakTimeLabel.SetTextColor(workTimeItem.EndBreakTimeLabel.Text == EmptyTime ? Color.Transparent : CurrentDayTextColor);
            }
        }

        private void SetDayEvenBackgroundColor(WorkTimeItem workTimeItem)
        {
            if(DayEvenBackgroundColor != default(Color) && !_isCurrentDay)
                workTimeItem.Container.Background = CreateGradientDrawable(_isEven ? Color.Transparent : DayEvenBackgroundColor);
        }

        private void SetDividerColor(WorkTimeItem workTimeItem)
        {
            if(DividerColor != default(Color) && !_isCurrentDay)
            {
                workTimeItem.DayDivider.Background = new ColorDrawable(DividerColor);
                workTimeItem.CircleDivider.Background = CreateGradientDrawable(DividerColor);
            }
        }

        private void SetCurrentDividerColor(WorkTimeItem workTimeItem)
        {
            if(CurrentDividerColor != default(Color) && _isCurrentDay)
            {
                workTimeItem.DayDivider.Background = new ColorDrawable(CurrentDividerColor);
                workTimeItem.CircleDivider.Background = CreateGradientDrawable(CurrentDividerColor);
            }
        }

        private bool IsThisCurrentDay(DayOfWeek day)
        {
            return day == CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(DateTime.Now);
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

        private List<WorkTimeCalendarItem> GenerateDefaultItems()
        {
            var list = new List<WorkTimeCalendarItem>();
            for(int i = 0; i < 7; i++)
                list.Add(new WorkTimeCalendarItem() { WeekDay = (DayOfWeek)i });

            return list;
        }

        private GradientDrawable CreateGradientDrawable(Color color)
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetColor(color);
            drawable.SetCornerRadius(7f);
            return drawable;
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
            if(!IsEOSCustomizationIgnored)
            {
                TitleFontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C2);
                DayFontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R1C3);

                CurrentDayTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);

                CurrentDayBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DayEvenBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);

                DividerColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                CurrentDividerColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
            }
            NotifyDataSetChanged();
        }

        public void ResetCustomization()
        {
            if(Items == null)
                Items = GenerateDefaultItems();
            WeekStart = _startDayOfweek == DayOfWeek.Sunday ? (WeekStartEnum)0 : (WeekStartEnum)1;
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
