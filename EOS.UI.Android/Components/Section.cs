using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    public class Section : RecyclerView.ViewHolder, IEOSThemeControl
    {
        #region fields

        private SimpleLabel _titleLabel;
        private Button _actionButton;
        private RelativeLayout _containerLayout;
        private Action _action;

        #endregion

        #region constructors

        public Section(View itemView, Action action = null) : base(itemView)
        {
            _action = action;
            _containerLayout = itemView.FindViewById<RelativeLayout>(Resource.Id.sectionContainer);
            _titleLabel = itemView.FindViewById<SimpleLabel>(Resource.Id.sectionTitle);
            _actionButton = itemView.FindViewById<Button>(Resource.Id.sectionButton);

            _actionButton.Click += delegate
            {
                _action?.Invoke();
            };
        }

        #endregion

        #region customization

        public string SectionName
        {
            get => _titleLabel.Text;
            set
            {
                _titleLabel.Text = value;
                IsEOSCustomizationIgnored = true;
            }
        }
        
        public string ButtonText
        {
            get => _actionButton.Text;
            set
            {
                _actionButton.Text = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float SectionTextSize
        {
            get => _titleLabel.TextSize;
            set
            {
                _titleLabel.TextSize = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float ButtonTextSize
        {
            get => _actionButton.TextSize;
            set
            {
                _actionButton.TextSize = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float SectionTextLetterSpacing
        {
            get => _titleLabel.LetterSpacing;
            set
            {
                _titleLabel.LetterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float ButtonTextLetterSpacing
        {
            get => _actionButton.LetterSpacing;
            set
            {
                _actionButton.LetterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public Typeface SectionNameFont
        {
            get => _titleLabel.Typeface;
            set
            {
                _titleLabel.Typeface = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public Typeface ButtonNameFont
        {
            get => _titleLabel.Typeface;
            set
            {
                _titleLabel.Typeface = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _sectionNameColor;
        public Color SectionNameColor
        {
            get => _sectionNameColor;
            set
            {
                _sectionNameColor = value;
                if(!HasBorder)
                    _titleLabel.TextColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _buttonNameColor;
        public Color ButtonNameColor
        {
            get => _buttonNameColor;
            set
            {
                _buttonNameColor = value;
                if(!HasBorder)
                    _actionButton.SetTextColor(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        public bool HasBorder { get; set; }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                _containerLayout.Background = CreateDrawableBackground();
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _borderWidth;
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                HasBorder = true;
                _containerLayout.Background = CreateDrawableBackground();
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _borderColor;
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                HasBorder = true;
                _containerLayout.Background = CreateDrawableBackground();
                IsEOSCustomizationIgnored = true;
            }
        }

        public void SetPaddings(int left, int top, int right, int bottom)
        {
            _containerLayout.SetPadding(left, top, right, bottom);
        }

        private bool _hasAction;
        public bool HasAction
        {
            get => _hasAction;
            set
            {
                _hasAction = value;
                _actionButton.Visibility = value ? ViewStates.Visible : ViewStates.Gone;
            }
        }

        #endregion

        #region utility methods

        private Drawable CreateDrawableBackground()
        {
            if(HasBorder)
            {
                var borderedBackground = new GradientDrawable();
                borderedBackground.SetColor(BackgroundColor);
                borderedBackground.SetStroke(BorderWidth, BorderColor);
                return borderedBackground;
            }
            else
            {
                return new ColorDrawable(BackgroundColor);
            }
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
                HasBorder = GetThemeProvider().GetEOSProperty<bool>(this, EOSConstants.HasSectionBorder);
                HasAction = GetThemeProvider().GetEOSProperty<bool>(this, EOSConstants.HasSectionAction);
                SectionName = GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SectionTitle);
                ButtonText = GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SectionActionTitle);
                SectionTextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                ButtonTextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.SecondaryTextSize);
                SectionTextLetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                ButtonTextLetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.SecondaryLetterSpacing);
                SectionNameFont = Typeface.CreateFromAsset(ItemView.Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font));
                ButtonNameFont = Typeface.CreateFromAsset(ItemView.Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SecondaryFont));
                BackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.PrimaryColor);
                SectionNameColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.SecondaryColor);
                ButtonNameColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.TertiaryColor);
                BorderColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.QuaternaryColor);
                BorderWidth = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.BorderWidth);
                var leftPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftPadding);
                var topPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.TopPadding);
                var rightPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.RightPadding);
                var bottomPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.BottomPadding);
                SetPaddings(leftPadding, topPadding, rightPadding, bottomPadding);
                IsEOSCustomizationIgnored = false;
            }
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