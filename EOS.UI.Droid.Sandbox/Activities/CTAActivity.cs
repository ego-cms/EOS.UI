using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static Android.Widget.CompoundButton;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.CTAButton, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CTAActivity : BaseActivity, IOnCheckedChangeListener
    {
        private Size _cachedSize;
        private SimpleButton _CTAButton;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _cornerRadiusDropDown;
        private Button _resetButton;
        private Switch _disableSwitch;
        private List<EOSSandboxDropDown> _dropDowns;
        private EOSSandboxDropDown _buttonTypeDropDown;
        private EOSSandboxDropDown _shadowRadiusDropDown;
        private SimpleButtonTypeEnum _buttonType;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleButtonLayout);

            _CTAButton = FindViewById<SimpleButton>(Resource.Id.simpleButton);
            _CTAButton.UpdateAppearance();
            _CTAButton.Text = "CTA button";

            _CTAButton.Click += async (s, e) =>
            {
                if (_CTAButton.InProgress)
                    return;
                _CTAButton.StartProgressAnimation();
                ToggleEnableState();
                await Task.Delay(5000);
                _CTAButton.StopProgressAnimation();
                ToggleEnableState();
            };

            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _cornerRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.cornerRadiusDropDown);
            var fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            var letterSpacingDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.letterSpacingDropDown);
            var textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            var textColorEnabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.enabledTextColorDropDown);
            var textColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledTextColorDropDown);
            var backgroundColorEnabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.enabledBackgroundDropDown);
            var backgroundColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledBackgroundDropDown);
            var backgroundColorPressedDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.pressedBackgroundDropDown);
            var rippleColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.rippleColorDropDown);
            _resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            _disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);
            _buttonTypeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.buttonTypeDropDown);
            _shadowRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowRadiusDropDown);

            _dropDowns = new List<EOSSandboxDropDown>
            {
                _themeDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown,
                textColorEnabledDropDown,
                textColorDisabledDropDown,
                backgroundColorEnabledDropDown,
                backgroundColorDisabledDropDown,
                backgroundColorPressedDropDown,
                _cornerRadiusDropDown,
                rippleColorDropDown,
                _shadowRadiusDropDown,
                _buttonTypeDropDown
            };

            _shadowRadiusDropDown.Name = Fields.ShadowRadius;
            _shadowRadiusDropDown.SetupAdapter(SimpleButtonConstants.ShadowRadiusCollection.Select(item => item.Key).ToList());
            _shadowRadiusDropDown.ItemSelected += ShadowRadiusItemSelected;

            _buttonTypeDropDown.Visibility = ViewStates.Visible;
            _buttonTypeDropDown.Name = Fields.ButtonType;
            _buttonTypeDropDown.SetupAdapter(Buttons.CTAButtonTypeCollection.Select(item => item.Key).ToList());
            _buttonTypeDropDown.ItemSelected += ButtonTypeItemSelected;

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            fontDropDown.Name = Fields.Font;
            fontDropDown.SetupAdapter(SimpleButtonConstants.SimpleButtonFonts.Select(item => item.Key).ToList());
            fontDropDown.ItemSelected += FontItemSelected;

            letterSpacingDropDown.Name = Fields.LetterSpacing;
            letterSpacingDropDown.SetupAdapter(SimpleButtonConstants.LetterSpacings.Select(item => item.Key).ToList());
            letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

            textSizeDropDown.Name = Fields.TextSize;
            textSizeDropDown.SetupAdapter(SimpleButtonConstants.TextSizes.Select(item => item.Key).ToList());
            textSizeDropDown.ItemSelected += TextSizeItemSelected;

            textColorEnabledDropDown.Name = Fields.EnabledTextColor;
            textColorEnabledDropDown.SetupAdapter(SimpleButtonConstants.FontColors.Select(item => item.Key).ToList());
            textColorEnabledDropDown.ItemSelected += TextColorEnabledItemSelected;

            textColorDisabledDropDown.Name = Fields.DisabledTextColor;
            textColorDisabledDropDown.SetupAdapter(SimpleButtonConstants.DisabledFontColors.Select(item => item.Key).ToList());
            textColorDisabledDropDown.ItemSelected += TextColorDisabledItemSelected;

            backgroundColorEnabledDropDown.Name = Fields.EnabledBackground;
            backgroundColorEnabledDropDown.SetupAdapter(SimpleButtonConstants.BackgroundColors.Select(item => item.Key).ToList());
            backgroundColorEnabledDropDown.ItemSelected += BackgroundColorEnabledItemSelected;

            backgroundColorDisabledDropDown.Name = Fields.DisabledBackground;
            backgroundColorDisabledDropDown.SetupAdapter(SimpleButtonConstants.DisabledBackgroundColors.Select(item => item.Key).ToList());
            backgroundColorDisabledDropDown.ItemSelected += BackgroundColorDisabledItemSelected;

            backgroundColorPressedDropDown.Name = Fields.PressedBackground;
            backgroundColorPressedDropDown.SetupAdapter(SimpleButtonConstants.PressedBackgroundColors.Select(item => item.Key).ToList());
            backgroundColorPressedDropDown.ItemSelected += BackgroundColorPressedItemSelected;

            _cornerRadiusDropDown.Name = Fields.ConerRadius;
            _cornerRadiusDropDown.SetupAdapter(SimpleButtonConstants.CornerRadiusCollection.Select(item => item.Key).ToList());
            _cornerRadiusDropDown.ItemSelected += CornerRadiusItemSelected;

            rippleColorDropDown.Name = Fields.RippleColor;
            rippleColorDropDown.SetupAdapter(SimpleButtonConstants.RippleColors.Select(item => item.Key).ToList());
            rippleColorDropDown.ItemSelected += RippleColorItemSelected;

            _resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            _disableSwitch.SetOnCheckedChangeListener(this);

            SetCurrenTheme(_CTAButton.GetThemeProvider().GetCurrentTheme());
        }

        private void ShadowRadiusItemSelected(int position)
        {
            if (_buttonType == SimpleButtonTypeEnum.FullBleed)
                return;
            var config = _CTAButton.ShadowConfig;
            config.Blur = SimpleButtonConstants.ShadowRadiusCollection.ElementAt(position).Value;
            _CTAButton.ShadowConfig = config;
        }

        private void ToggleEnableState()
        {
            _dropDowns.ForEach(dropDown => dropDown.Enabled = !_CTAButton.InProgress);
            _resetButton.Enabled = !_CTAButton.InProgress;
            _disableSwitch.Enabled = !_CTAButton.InProgress;
            if (_buttonType == SimpleButtonTypeEnum.FullBleed)
            {
                DisableSimpleButtonFields();
            }
        }

        private void ThemeItemSelected(int position)
        {
            if (position > 0)
            {
                _CTAButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateAppearance();
            }
        }

        private void FontItemSelected(int position)
        {
            _CTAButton.Typeface = Typeface.CreateFromAsset(Assets, SimpleButtonConstants.SimpleButtonFonts.ElementAt(position).Value);
        }

        private void LetterSpacingItemSelected(int position)
        {
            _CTAButton.LetterSpacing = SimpleButtonConstants.LetterSpacings.ElementAt(position).Value;
        }

        private void TextSizeItemSelected(int position)
        {
            _CTAButton.TextSize = SimpleButtonConstants.TextSizes.ElementAt(position).Value;
        }

        private void TextColorEnabledItemSelected(int position)
        {
            _CTAButton.TextColor = SimpleButtonConstants.FontColors.ElementAt(position).Value;
        }

        private void TextColorDisabledItemSelected(int position)
        {
            _CTAButton.DisabledTextColor = SimpleButtonConstants.DisabledFontColors.ElementAt(position).Value;
        }

        private void BackgroundColorEnabledItemSelected(int position)
        {
            _CTAButton.BackgroundColor = SimpleButtonConstants.BackgroundColors.ElementAt(position).Value;
        }

        private void BackgroundColorDisabledItemSelected(int position)
        {
            _CTAButton.DisabledBackgroundColor = SimpleButtonConstants.DisabledBackgroundColors.ElementAt(position).Value;
        }

        private void BackgroundColorPressedItemSelected(int position)
        {
            _CTAButton.PressedBackgroundColor = SimpleButtonConstants.PressedBackgroundColors.ElementAt(position).Value;
        }

        private void CornerRadiusItemSelected(int position)
        {
            if (_buttonType == SimpleButtonTypeEnum.FullBleed)
                return;
            _CTAButton.CornerRadius = SimpleButtonConstants.CornerRadiusCollection.ElementAt(position).Value * Resources.DisplayMetrics.Density;
        }

        private void RippleColorItemSelected(int position)
        {
            _CTAButton.RippleColor = SimpleButtonConstants.RippleColors.ElementAt(position).Value;
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
            _CTAButton.ResetCustomization();
            _dropDowns.Except(new[] { _themeDropDown, _buttonTypeDropDown }).ToList().ForEach(dropDown => dropDown.SetSpinnerSelection(0));
            if (!ignogeButtonType)
                _buttonTypeDropDown.SetSpinnerSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _CTAButton.Enabled = isChecked;
        }

        private void ButtonTypeItemSelected(int position)
        {
            _buttonType = Buttons.CTAButtonTypeCollection.ElementAt(position).Value;
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
            _CTAButton.ShadowConfig = null;
            _CTAButton.CornerRadius = 0;
            _CTAButton.SetPadding(0, 0, 0, 0);
            _CTAButton.LayoutParameters = GetFullBleedButtonLayoutParameters();
            _CTAButton.Text = Buttons.FullBleed;
        }

        private void SetupSimpleButtonStyle()
        {
            var layoutParameters = GetSimpleButtonLayoutParameters();
            layoutParameters.Gravity = GravityFlags.Center;
            _CTAButton.LayoutParameters = layoutParameters;
            var denisty = Resources.DisplayMetrics.Density;
            _CTAButton.SetPadding(
                (int)(SimpleButtonConstants.LeftPadding * denisty),
                (int)(SimpleButtonConstants.TopPadding * denisty),
                (int)(SimpleButtonConstants.RightPadding * denisty),
                (int)(SimpleButtonConstants.BottomPadding * denisty));
            _CTAButton.ResetCustomization();
            _CTAButton.Text = Buttons.CTA;
        }

        private LinearLayout.LayoutParams GetSimpleButtonLayoutParameters()
        {
            if (_cachedSize != null)
                return new LinearLayout.LayoutParams(_cachedSize.Width, _cachedSize.Height);

            return _CTAButton.LayoutParameters as LinearLayout.LayoutParams;
        }

        private LinearLayout.LayoutParams GetFullBleedButtonLayoutParameters()
        {
            if (_cachedSize == null)
                _cachedSize = new Size(_CTAButton.Width, _CTAButton.Height);

            return new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, _cachedSize.Height);
        }
    }
}
