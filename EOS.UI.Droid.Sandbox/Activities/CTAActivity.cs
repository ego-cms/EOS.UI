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
            _resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            _disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);
            _buttonTypeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.buttonTypeDropDown);
            _shadowRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.shadowRadiusDropDown);

            _dropDowns = new List<EOSSandboxDropDown>
            {
                _themeDropDown,
                _fontDropDown,
                _letterSpacingDropDown,
                _textSizeDropDown,
                _textColorEnabledDropDown,
                _textColorDisabledDropDown,
                _backgroundColorEnabledDropDown,
                _backgroundColorDisabledDropDown,
                _backgroundColorPressedDropDown,
                _cornerRadiusDropDown,
                _rippleColorDropDown,
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

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(SimpleButtonConstants.SimpleButtonFonts.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(SimpleButtonConstants.LetterSpacings.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

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
            _cornerRadiusDropDown.ItemSelected += CornerRadiusItemSelected;

            _rippleColorDropDown.Name = Fields.RippleColor;
            _rippleColorDropDown.SetupAdapter(SimpleButtonConstants.RippleColors.Select(item => item.Key).ToList());
            _rippleColorDropDown.ItemSelected += RippleColorItemSelected;

            _resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            _disableSwitch.SetOnCheckedChangeListener(this);

            SetCurrenTheme(_CTAButton.GetThemeProvider().GetCurrentTheme());
        }

        private void ShadowRadiusItemSelected(int position)
        {
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
