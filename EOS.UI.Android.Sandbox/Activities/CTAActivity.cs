using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Controls;
using EOS.UI.Shared.Themes.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static Android.Widget.CompoundButton;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using A = Android;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.CTAButton, Theme = "@style/Sandbox.Main")]
    public class CTAActivity : BaseActivity, IOnCheckedChangeListener
    {
        private SimpleButton _CTAButton;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _fontDropDown;
        private EOSSandboxDropDown _letterSpacingDropDown;
        private EOSSandboxDropDown _textSizeDropDown;
        private EOSSandboxDropDown _textColorEnabledDropDown;
        private EOSSandboxDropDown _textColorDisabledDropDown;
        private EOSSandboxDropDown _textColorPressedDropDown;
        private EOSSandboxDropDown _backgroundColorEnabledDropDown;
        private EOSSandboxDropDown _backgroundColorDisabledDropDown;
        private EOSSandboxDropDown _backgroundColorPressedDropDown;
        private EOSSandboxDropDown _cornerRadiusDropDown;
        private EOSSandboxDropDown _rippleColorDropDown;
        private Button _resetButton;
        private Switch _disableSwitch;
        private List<EOSSandboxDropDown> _dropDowns;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleButtonLayout);

            _CTAButton = FindViewById<SimpleButton>(Resource.Id.simpleButton);
            _CTAButton.UpdateAppearance();
            _CTAButton.Text = "CTA button";
            FindViewById<SimpleButton>(Resource.Id.simpleButton1).Text = "CTA button";
            FindViewById<SimpleButton>(Resource.Id.simpleButton2).Text = "CTA button";
            FindViewById<SimpleButton>(Resource.Id.simpleButton3).Text = "CTA button";

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
            _textColorPressedDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.pressedTextColorDropDown);
            _backgroundColorEnabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.enabledBackgroundDropDown);
            _backgroundColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledBackgroundDropDown);
            _backgroundColorPressedDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.pressedBackgroundDropDown);
            _cornerRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.cornerRadiusDropDown);
            _rippleColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.rippleColorDropDown);
            _resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            _disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

            _dropDowns = new List<EOSSandboxDropDown>
            {
                _themeDropDown,
                _fontDropDown,
                _letterSpacingDropDown,
                _textSizeDropDown,
                _textColorEnabledDropDown,
                _textColorDisabledDropDown,
                _textColorPressedDropDown,
                _backgroundColorEnabledDropDown,
                _backgroundColorDisabledDropDown,
                _backgroundColorPressedDropDown,
                _cornerRadiusDropDown,
                _rippleColorDropDown
            };

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

            _textSizeDropDown.Name = Fields.TextSize;
            _textSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeDropDown.ItemSelected += TextSizeItemSelected;

            _textColorEnabledDropDown.Name = Fields.EnabledTextColor;
            _textColorEnabledDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorEnabledDropDown.ItemSelected += TextColorEnabledItemSelected;

            _textColorDisabledDropDown.Name = Fields.DisabledTextColor;
            _textColorDisabledDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDisabledDropDown.ItemSelected += TextColorDisabledItemSelected;

            _textColorPressedDropDown.Name = Fields.PressedTextColor;
            _textColorPressedDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorPressedDropDown.ItemSelected += TextColorPressedItemSelected;

            _backgroundColorEnabledDropDown.Name = Fields.EnabledBackground;
            _backgroundColorEnabledDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorEnabledDropDown.ItemSelected += BackgroundColorEnabledItemSelected;

            _backgroundColorDisabledDropDown.Name = Fields.DisabledBackground;
            _backgroundColorDisabledDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorDisabledDropDown.ItemSelected += BackgroundColorDisabledItemSelected;

            _backgroundColorPressedDropDown.Name = Fields.PressedBackground;
            _backgroundColorPressedDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorPressedDropDown.ItemSelected += BackgroundColorPressedItemSelected;

            _cornerRadiusDropDown.Name = Fields.ConerRadius;
            _cornerRadiusDropDown.SetupAdapter(Sizes.CornerRadusCollection.Select(item => item.Key).ToList());
            _cornerRadiusDropDown.ItemSelected += CornerRadiusItemSelected;

            _rippleColorDropDown.Name = Fields.RippleColor;
            _rippleColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _rippleColorDropDown.ItemSelected += RippleColorItemSelected;

            _resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            _disableSwitch.SetOnCheckedChangeListener(this);

            SetCurrenTheme(_CTAButton.GetThemeProvider().GetCurrentTheme());
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
                _CTAButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
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
                _CTAButton.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void TextColorDisabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void TextColorPressedItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.PressedTextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorEnabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorDisabledItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorPressedItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void CornerRadiusItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.CornerRadius = Sizes.CornerRadusCollection.ElementAt(position).Value;
        }

        private void RippleColorItemSelected(int position)
        {
            if (position > 0)
                _CTAButton.RippleColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if (iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if (iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void ResetCustomValues()
        {
            _CTAButton.ResetCustomization();
            _dropDowns.Except(new[] { _themeDropDown }).ToList().ForEach(dropDown => dropDown.SetSpinnerSelection(0));
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _CTAButton.Enabled = isChecked;
        }
    }
}
