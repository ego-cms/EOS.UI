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
    [Activity(Label = ControlNames.CTAButton)]
    public class CTAActivity : BaseActivity, IOnCheckedChangeListener
    {
        private SimpleButton _CTAButton;
        private SandboxDropDown _themeDropDown;
        private SandboxDropDown _fontDropDown;
        private SandboxDropDown _letterSpacingDropDown;
        private SandboxDropDown _textSizeDropDown;
        private SandboxDropDown _textColorEnabledDropDown;
        private SandboxDropDown _textColorDisabledDropDown;
        private SandboxDropDown _textColorPressedDropDown;
        private SandboxDropDown _backgroundColorEnabledDropDown;
        private SandboxDropDown _backgroundColorDisabledDropDown;
        private SandboxDropDown _backgroundColorPressedDropDown;
        private SandboxDropDown _cornerRadiusDropDown;
        private Button _resetButton;
        private Switch _disableSwitch;
        private List<SandboxDropDown> _dropDowns;

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

            _themeDropDown = FindViewById<SandboxDropDown>(Resource.Id.themeDropDown);
            _fontDropDown = FindViewById<SandboxDropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<SandboxDropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<SandboxDropDown>(Resource.Id.textSizeDropDown);
            _textColorEnabledDropDown = FindViewById<SandboxDropDown>(Resource.Id.enabledTextColorDropDown);
            _textColorDisabledDropDown = FindViewById<SandboxDropDown>(Resource.Id.disabledTextColorDropDown);
            _textColorPressedDropDown = FindViewById<SandboxDropDown>(Resource.Id.pressedTextColorDropDown);
            _backgroundColorEnabledDropDown = FindViewById<SandboxDropDown>(Resource.Id.enabledBackgroundDropDown);
            _backgroundColorDisabledDropDown = FindViewById<SandboxDropDown>(Resource.Id.disabledBackgroundDropDown);
            _backgroundColorPressedDropDown = FindViewById<SandboxDropDown>(Resource.Id.pressedBackgroundDropDown);
            _cornerRadiusDropDown = FindViewById<SandboxDropDown>(Resource.Id.cornerRadiusDropDown);
            _resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            _disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

            _dropDowns = new List<SandboxDropDown>
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
            if(position > 0)
            {
                _CTAButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
            }
        }

        private void FontItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
        }

        private void LetterSpacingItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
        }

        private void TextSizeItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
        }

        private void TextColorEnabledItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void TextColorDisabledItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void TextColorPressedItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.PressedTextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorEnabledItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorDisabledItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorPressedItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void CornerRadiusItemSelected(int position)
        {
            if(position > 0)
                _CTAButton.CornerRadius = Sizes.CornerRadusCollection.ElementAt(position).Value;
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if(iEOSTheme is DarkEOSTheme)
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