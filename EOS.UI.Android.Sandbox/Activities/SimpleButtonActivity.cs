﻿using System.Linq;
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

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.SimpleButton, Theme = "@style/Sandbox.Main")]
    public class SimpleButtonActivity : BaseActivity, IOnCheckedChangeListener
    {
        private SimpleButton _simpleButton;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleButtonLayout);

            _simpleButton = FindViewById<SimpleButton>(Resource.Id.simpleButton);
            _simpleButton.UpdateAppearance();

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
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontSpinner_ItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingView_ItemSelected;

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
            _cornerRadiusDropDown.ItemSelected += CornerRadiurSpinner_ItemSelected;

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            disableSwitch.SetOnCheckedChangeListener(this);

            SetCurrenTheme(_simpleButton.GetThemeProvider().GetCurrentTheme());
        }

        private void ThemeItemSelected(int position)
        {
            if(position > 0)
            {
                _simpleButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateApperaence();
            }
        }

        private void FontSpinner_ItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
        }

        private void LetterSpacingView_ItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
        }

        private void TextSizeItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
        }

        private void TextColorEnabledItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void TextColorDisabledItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void TextColorPressedItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.PressedTextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorEnabledItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorDisabledItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorPressedItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void CornerRadiurSpinner_ItemSelected(int position)
        {
            if(position > 0)
                _simpleButton.CornerRadius = Sizes.CornerRadusCollection.ElementAt(position).Value;
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
            _simpleButton.ResetCustomization();
            _fontDropDown.SetSpinnerSelection(0);
            _letterSpacingDropDown.SetSpinnerSelection(0);
            _textSizeDropDown.SetSpinnerSelection(0);
            _textColorEnabledDropDown.SetSpinnerSelection(0);
            _textColorDisabledDropDown.SetSpinnerSelection(0);
            _textColorPressedDropDown.SetSpinnerSelection(0);
            _backgroundColorEnabledDropDown.SetSpinnerSelection(0);
            _backgroundColorDisabledDropDown.SetSpinnerSelection(0);
            _backgroundColorPressedDropDown.SetSpinnerSelection(0);
            _cornerRadiusDropDown.SetSpinnerSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _simpleButton.Enabled = isChecked;
        }
    }
}