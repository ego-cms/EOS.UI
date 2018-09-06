using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.ControlConstants.Android;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static Android.Widget.CompoundButton;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.SimpleButton, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SimpleButtonActivity : BaseActivity, IOnCheckedChangeListener
    {
        private Size _cachedSize;
        private SimpleButton _simpleButton;
        private EOSSandboxDropDown _buttonTypeDropDown;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _fontDropDown;
        private EOSSandboxDropDown _letterSpacingDropDown;
        private EOSSandboxDropDown _textSizeDropDown;
        private EOSSandboxDropDown _textColorEnabledDropDown;
        private EOSSandboxDropDown _textColorDisabledDropDown;
        private EOSSandboxDropDown _backgroundColorEnabledDropDown;
        private EOSSandboxDropDown _backgroundColorDisabledDropDown;
        private EOSSandboxDropDown _backgroundColorPressedDropDown;
        private EOSSandboxDropDown _cornerRadiusDropDown;
        private EOSSandboxDropDown _rippleColorDropDown;
        private EOSSandboxDropDown _shadowRadiusDropDown;
        private SimpleButtonTypeEnum _buttonType;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleButtonLayout);

            _simpleButton = FindViewById<SimpleButton>(Resource.Id.simpleButton);
            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            _textColorEnabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.enabledTextColorDropDown);
            _textColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledTextColorDropDown);
            _backgroundColorEnabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.enabledBackgroundDropDown);
            _backgroundColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledBackgroundDropDown);
            _backgroundColorPressedDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.pressedBackgroundDropDown);
            _cornerRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.cornerRadiusDropDown);
            _rippleColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.rippleColorDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);
            _buttonTypeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.buttonTypeDropDown);
            _shadowRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowRadiusDropDown);

            _shadowRadiusDropDown.Name = Fields.ShadowRadius;
            _shadowRadiusDropDown.SetupAdapter(SimpleButtonConstants.ShadowRadiusCollection.Select(item => item.Key).ToList());
            _shadowRadiusDropDown.ItemSelected += ShadowRadiusItemSelected;

            _buttonTypeDropDown.Visibility = ViewStates.Visible;
            _buttonTypeDropDown.Name = Fields.ButtonType;
            _buttonTypeDropDown.SetupAdapter(Buttons.SimpleButtonTypeCollection.Select(item => item.Key).ToList());
            _buttonTypeDropDown.ItemSelected += ButtonTypeItemSelected;

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(SimpleButtonConstants.SimpleButtonFonts.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontSpinner_ItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(SimpleButtonConstants.LetterSpacings.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingView_ItemSelected;

            _textSizeDropDown.Name = Fields.TextSize;
            _textSizeDropDown.SetupAdapter(SimpleButtonConstants.TextSizes.Select(item => item.Key).ToList());
            _textSizeDropDown.ItemSelected += TextSizeItemSelected;

            _textColorEnabledDropDown.Name = Fields.EnabledTextColor;
            _textColorEnabledDropDown.SetupAdapter(SimpleButtonConstants.FontColors.Select(item => item.Key).ToList());
            _textColorEnabledDropDown.ItemSelected += TextColorEnabledItemSelected;

            _textColorDisabledDropDown.Name = Fields.DisabledTextColor;
            _textColorDisabledDropDown.SetupAdapter(SimpleButtonConstants.DisabledFontColors.Select(item => item.Key).ToList());
            _textColorDisabledDropDown.ItemSelected += TextColorDisabledItemSelected;

            _backgroundColorEnabledDropDown.Name = Fields.EnabledBackground;
            _backgroundColorEnabledDropDown.SetupAdapter(SimpleButtonConstants.BackgroundColors.Select(item => item.Key).ToList());
            _backgroundColorEnabledDropDown.ItemSelected += BackgroundColorEnabledItemSelected;

            _backgroundColorDisabledDropDown.Name = Fields.DisabledBackground;
            _backgroundColorDisabledDropDown.SetupAdapter(SimpleButtonConstants.DisabledBackgroundColors.Select(item => item.Key).ToList());
            _backgroundColorDisabledDropDown.ItemSelected += BackgroundColorDisabledItemSelected;

            _backgroundColorPressedDropDown.Name = Fields.PressedBackground;
            _backgroundColorPressedDropDown.SetupAdapter(SimpleButtonConstants.PressedBackgroundColors.Select(item => item.Key).ToList());
            _backgroundColorPressedDropDown.ItemSelected += BackgroundColorPressedItemSelected;

            _cornerRadiusDropDown.Name = Fields.ConerRadius;
            _cornerRadiusDropDown.SetupAdapter(SimpleButtonConstants.CornerRadiusCollection.Select(item => item.Key).ToList());
            _cornerRadiusDropDown.ItemSelected += CornerRadiurSpinner_ItemSelected;

            _rippleColorDropDown.Name = Fields.RippleColor;
            _rippleColorDropDown.SetupAdapter(SimpleButtonConstants.RippleColors.Select(item => item.Key).ToList());
            _rippleColorDropDown.ItemSelected += RippleColorItemSelected;

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            disableSwitch.SetOnCheckedChangeListener(this);

            SetCurrenTheme(_simpleButton.GetThemeProvider().GetCurrentTheme());
        }

        private void ShadowRadiusItemSelected(int position)
        {
            if (_buttonType == SimpleButtonTypeEnum.FullBleed)
                return;
            var config = _simpleButton.ShadowConfig;
            config.Blur = SimpleButtonConstants.ShadowRadiusCollection.ElementAt(position).Value;
            _simpleButton.ShadowConfig = config;
        }

        private void ButtonTypeItemSelected(int position)
        {
            _buttonType = Buttons.SimpleButtonTypeCollection.ElementAt(position).Value;
            ResetCustomValues(true);
            switch (_buttonType)
            {
                case SimpleButtonTypeEnum.Simple:
                    SetupSimpleButtonStyle();
                    EnableSimpleButtonFields();
                    break;
                case SimpleButtonTypeEnum.FullBleed:
                    SetupFullBleedButtonStyle();
                    DisableSimpleButtonFields();
                    break;
            }
        }

        private void DisableSimpleButtonFields()
        {
            _cornerRadiusDropDown.Enabled = false;
            _shadowRadiusDropDown.Enabled = false;
        }

        private void EnableSimpleButtonFields()
        {
            _cornerRadiusDropDown.Enabled = true;
            _shadowRadiusDropDown.Enabled = true;
        }

        private void SetupFullBleedButtonStyle()
        {
            _simpleButton.ShadowConfig = null;
            _simpleButton.CornerRadius = 0;
            _simpleButton.SetPadding(0, 0, 0, 0);
            _simpleButton.LayoutParameters = GetFullBleedButtonLayoutParameters();
            _simpleButton.Text = Buttons.FullBleed;
        }

        private void SetupSimpleButtonStyle()
        {
            var layoutParameters = GetSimpleButtonLayoutParameters();
            layoutParameters.Gravity = GravityFlags.Center;
            _simpleButton.LayoutParameters = layoutParameters;
            var denisty = Resources.DisplayMetrics.Density;
            _simpleButton.SetPadding(
                (int)(SimpleButtonConstants.LeftPadding * denisty),
                (int)(SimpleButtonConstants.TopPadding * denisty),
                (int)(SimpleButtonConstants.RightPadding * denisty),
                (int)(SimpleButtonConstants.BottomPadding * denisty));
            _simpleButton.ResetCustomization();
            _simpleButton.Text = Buttons.Simple;
        }

        private LinearLayout.LayoutParams GetSimpleButtonLayoutParameters()
        {
            if (_cachedSize != null)
                return new LinearLayout.LayoutParams(_cachedSize.Width, _cachedSize.Height);

            return _simpleButton.LayoutParameters as LinearLayout.LayoutParams;
        }

        private LinearLayout.LayoutParams GetFullBleedButtonLayoutParameters()
        {
            if (_cachedSize == null)
                _cachedSize = new Size(_simpleButton.Width, _simpleButton.Height);

            return new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, _cachedSize.Height);
        }

        private void ThemeItemSelected(int position)
        {
            if (position > 0)
            {
                _simpleButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateAppearance();
            }
        }

        private void FontSpinner_ItemSelected(int position)
        {
            _simpleButton.Typeface = Typeface.CreateFromAsset(Assets, SimpleButtonConstants.SimpleButtonFonts.ElementAt(position).Value);
        }

        private void LetterSpacingView_ItemSelected(int position)
        {
            _simpleButton.LetterSpacing = SimpleButtonConstants.LetterSpacings.ElementAt(position).Value;
        }

        private void TextSizeItemSelected(int position)
        {
            _simpleButton.TextSize = SimpleButtonConstants.TextSizes.ElementAt(position).Value;
        }

        private void TextColorEnabledItemSelected(int position)
        {
            _simpleButton.TextColor = SimpleButtonConstants.FontColors.ElementAt(position).Value;
        }

        private void TextColorDisabledItemSelected(int position)
        {
            _simpleButton.DisabledTextColor = SimpleButtonConstants.DisabledFontColors.ElementAt(position).Value;
        }

        private void BackgroundColorEnabledItemSelected(int position)
        {
            _simpleButton.BackgroundColor = SimpleButtonConstants.BackgroundColors.ElementAt(position).Value;
        }

        private void BackgroundColorDisabledItemSelected(int position)
        {
            _simpleButton.DisabledBackgroundColor = SimpleButtonConstants.DisabledBackgroundColors.ElementAt(position).Value;
        }

        private void BackgroundColorPressedItemSelected(int position)
        {
            _simpleButton.PressedBackgroundColor = SimpleButtonConstants.PressedBackgroundColors.ElementAt(position).Value;
        }

        private void CornerRadiurSpinner_ItemSelected(int position)
        {
            if (_buttonType == SimpleButtonTypeEnum.FullBleed)
                return;
            _simpleButton.CornerRadius = SimpleButtonConstants.CornerRadiusCollection.ElementAt(position).Value * Resources.DisplayMetrics.Density;
        }

        private void RippleColorItemSelected(int position)
        {
            _simpleButton.RippleColor = SimpleButtonConstants.RippleColors.ElementAt(position).Value;
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if (iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if (iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void ResetCustomValues(bool ignogeButtonType = false)
        {
            _simpleButton.ResetCustomization();
            _fontDropDown.SetSpinnerSelection(0);
            _letterSpacingDropDown.SetSpinnerSelection(0);
            _textSizeDropDown.SetSpinnerSelection(0);
            _textColorEnabledDropDown.SetSpinnerSelection(0);
            _textColorDisabledDropDown.SetSpinnerSelection(0);
            _backgroundColorEnabledDropDown.SetSpinnerSelection(0);
            _backgroundColorDisabledDropDown.SetSpinnerSelection(0);
            _backgroundColorPressedDropDown.SetSpinnerSelection(0);
            _cornerRadiusDropDown.SetSpinnerSelection(0);
            _rippleColorDropDown.SetSpinnerSelection(0);
            _shadowRadiusDropDown.SetSpinnerSelection(0);
            if (!ignogeButtonType)
                _buttonTypeDropDown.SetSpinnerSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _simpleButton.Enabled = isChecked;
        }
    }
}
