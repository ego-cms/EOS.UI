using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static Android.Widget.CompoundButton;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.CTAButton, Theme = "@style/Sandbox.Main")]
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleButtonLayout);

            _CTAButton = FindViewById<SimpleButton>(Resource.Id.simpleButton);
            _CTAButton.UpdateAppearance();
            _CTAButton.Text = "CTA button";

            _CTAButton.Click += async (s, e) =>
            {
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
            _shadowRadiusDropDown.SetupAdapter(Shadow.RadiusCollection.Select(item => item.Key).ToList());
            _shadowRadiusDropDown.ItemSelected += ShadowRadiusItemSelected;

            _buttonTypeDropDown.Visibility = ViewStates.Visible;
            _buttonTypeDropDown.Name = Fields.ButtonType;
            _buttonTypeDropDown.SetupAdapter(Buttons.CTAButtonTypeCollection.Select(item => item.Key).ToList());
            _buttonTypeDropDown.ItemSelected += ButtonTypeItemSelected;

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(Fonts.GetGhostButtonSimpleLabelFonts().Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

            _textSizeDropDown.Name = Fields.TextSize;
            _textSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeDropDown.ItemSelected += TextSizeItemSelected;

            _textColorEnabledDropDown.Name = Fields.EnabledTextColor;
            _textColorEnabledDropDown.SetupAdapter(Colors.FontColorsCollection.Select(item => item.Key).ToList());
            _textColorEnabledDropDown.ItemSelected += TextColorEnabledItemSelected;

            _textColorDisabledDropDown.Name = Fields.DisabledTextColor;
            _textColorDisabledDropDown.SetupAdapter(Colors.FontColorsCollection.Select(item => item.Key).ToList());
            _textColorDisabledDropDown.ItemSelected += TextColorDisabledItemSelected;

            _backgroundColorEnabledDropDown.Name = Fields.EnabledBackground;
            _backgroundColorEnabledDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorEnabledDropDown.ItemSelected += BackgroundColorEnabledItemSelected;

            _backgroundColorDisabledDropDown.Name = Fields.DisabledBackground;
            _backgroundColorDisabledDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorDisabledDropDown.ItemSelected += BackgroundColorDisabledItemSelected;

            _backgroundColorPressedDropDown.Name = Fields.PressedBackground;
            _backgroundColorPressedDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorPressedDropDown.ItemSelected += BackgroundColorPressedItemSelected;

            _cornerRadiusDropDown.Name = Fields.ConerRadius;
            _cornerRadiusDropDown.SetupAdapter(Sizes.CornerRadiusCollection.Select(item => item.Key).ToList());
            _cornerRadiusDropDown.ItemSelected += CornerRadiusItemSelected;

            _rippleColorDropDown.Name = Fields.RippleColor;
            _rippleColorDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
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
            if(position > 0)
            {
                var config = _CTAButton.ShadowConfig;
                config.Blur = Shadow.RadiusCollection.ElementAt(position).Value;
                _CTAButton.ShadowConfig = config;
            }
        }

        private void ToggleEnableState()
        {
            _dropDowns.ForEach(dropDown => dropDown.Enabled = !_CTAButton.InProgress);
            _resetButton.Enabled = !_CTAButton.InProgress;
            _disableSwitch.Enabled = !_CTAButton.InProgress;
        }

        private void ThemeItemSelected(int position)
        {
            if (position > 0)
            {
                _CTAButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateApperaence();
            }
        }

        private void FontItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.GetButtonBadgeFonts().ElementAt(position).Value);
        }

        private void LetterSpacingItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
        }

        private void TextSizeItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
        }

        private void TextColorEnabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.TextColor = Colors.FontColorsCollection.ElementAt(position).Value;
        }

        private void TextColorDisabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.DisabledTextColor = Colors.FontColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorEnabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.BackgroundColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorDisabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.DisabledBackgroundColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorPressedItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.PressedBackgroundColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void CornerRadiusItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.CornerRadius = Sizes.CornerRadiusCollection.ElementAt(position).Value;
        }

        private void RippleColorItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.RippleColor = Colors.MainColorsCollection.ElementAt(position).Value;
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
            if(!ignogeButtonType)
                _buttonTypeDropDown.SetSpinnerSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _CTAButton.Enabled = isChecked;
        }

        private void ButtonTypeItemSelected(int position)
        {
            var buttonType = Buttons.CTAButtonTypeCollection.ElementAt(position).Value;
            switch(buttonType)
            {
                case SimpleButtonTypeEnum.Simple:
                    ResetCustomValues(true);
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
                    break;
                case SimpleButtonTypeEnum.FullBleed:
                    ResetCustomValues(true);
                    _CTAButton.ShadowConfig = null;
                    _CTAButton.CornerRadius = 0;
                    _CTAButton.SetPadding(0, 0, 0, 0);
                    _CTAButton.LayoutParameters = GetFullBleedButtonLayoutParameters();
                    _CTAButton.Text = Buttons.FullBleed;
                    break;
            }
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
