using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Shared.Themes.DataModels;
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
        private LinearLayout _containerLayout;

        #endregion

        #region constructors

        public Section(View itemView) : base(itemView)
        {
            _containerLayout = itemView.FindViewById<LinearLayout>(Resource.Id.sectionContainer);
            _titleLabel = itemView.FindViewById<SimpleLabel>(Resource.Id.sectionTitle);
            _actionButton = itemView.FindViewById<Button>(Resource.Id.sectionButton);

            _actionButton.Click += delegate
            {
                SectionAction?.Invoke();
            };

            UpdateAppearance();
        }

        #endregion

        #region customization

        public Action SectionAction { get; set; }

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
                TitleFontStyle.Size = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float ButtonTextSize
        {
            get => _actionButton.TextSize;
            set
            {
                _actionButton.TextSize = value;
                ButtonFontStyle.Size = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float SectionTextLetterSpacing
        {
            get => _titleLabel.LetterSpacing;
            set
            {
                _titleLabel.LetterSpacing = value;
                TitleFontStyle.LetterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public float ButtonTextLetterSpacing
        {
            get => _actionButton.LetterSpacing;
            set
            {
                _actionButton.LetterSpacing = value;
                ButtonFontStyle.LetterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public Typeface SectionNameFont
        {
            get => _titleLabel.Typeface;
            set
            {
                _titleLabel.Typeface = value;
                TitleFontStyle.Typeface = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public Typeface ButtonNameFont
        {
            get => _actionButton.Typeface;
            set
            {
                _actionButton.Typeface = value;
                ButtonFontStyle.Typeface = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public Color SectionNameColor
        {
            get => TitleFontStyle.Color;
            set
            {
                TitleFontStyle.Color = value;
                _titleLabel.TextColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public Color ButtonNameColor
        {
            get => ButtonFontStyle.Color;
            set
            {
                ButtonFontStyle.Color = value;
                _actionButton.SetTextColor(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private bool _hasBorder;
        public bool HasBorder
        {
            get => _hasBorder;
            set
            {
                _hasBorder = value;
                _containerLayout.Background = CreateDrawableBackground();
            }
        }

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
                _containerLayout.Background = CreateDrawableBackground();
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _leftPadding;
        private int _rightPadding;
        private int _topPadding;
        private int _bottomPadding;

        public void SetPaddings(int left, int top, int right, int bottom)
        {
            _leftPadding = left == -1 ? _leftPadding : left;
            _rightPadding = right == -1 ? _rightPadding : right;
            _topPadding = top == -1 ? _topPadding : top;
            _bottomPadding = bottom == -1 ? _bottomPadding : bottom;
            UpdatePaddings();
        }

        private void UpdatePaddings()
        {
            _containerLayout.SetPadding(_leftPadding, _topPadding, _rightPadding, _bottomPadding);
        }

        private bool _hasButton;
        public bool HasButton
        {
            get => _hasButton;
            set
            {
                _hasButton = value;
                _actionButton.Visibility = value ? ViewStates.Visible : ViewStates.Gone;
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

        private FontStyleItem _buttonFontStyle;
        public FontStyleItem ButtonFontStyle
        {
            get => _buttonFontStyle;
            set
            {
                _buttonFontStyle = value;
                SetButtonFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private void SetTitleFontStyle()
        {
            _titleLabel.Typeface = TitleFontStyle.Typeface;
            _titleLabel.TextSize = TitleFontStyle.Size;
            _titleLabel.SetTextColor(TitleFontStyle.Color);
            _titleLabel.LetterSpacing = TitleFontStyle.LetterSpacing;
        }

        private void SetButtonFontStyle()
        {
            _actionButton.Typeface = ButtonFontStyle.Typeface;
            _actionButton.TextSize = ButtonFontStyle.Size;
            _actionButton.SetTextColor(ButtonFontStyle.Color);
            _actionButton.LetterSpacing = ButtonFontStyle.LetterSpacing;
        }

        #endregion

        #region utility methods

        private Drawable CreateDrawableBackground()
        {
            if(HasBorder)
            {
                var border = new GradientDrawable();
                border.SetStroke(BorderWidth, BorderColor);
                var background = new ColorDrawable(BackgroundColor);
                Drawable[] layers = { background, border };
                var layerDrawable = new LayerDrawable(layers);
                layerDrawable.SetLayerInset(1, -BorderWidth, 0, -BorderWidth, -BorderWidth);
                return layerDrawable;
            }
            else
            {
                return new ColorDrawable(BackgroundColor);
            }
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
                HasBorder = GetThemeProvider().GetEOSProperty<bool>(this, EOSConstants.HasSectionBorder);
                HasButton = GetThemeProvider().GetEOSProperty<bool>(this, EOSConstants.HasSectionAction);
                SectionName = GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SectionTitle);
                ButtonText = GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SectionActionTitle);
                TitleFontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C3);
                ButtonFontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C1);
                BackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor5);
                BorderColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
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
