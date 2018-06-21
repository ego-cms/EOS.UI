using System.Linq;
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
    [Activity(Label = ControlNames.Input, Theme = "@style/Sandbox.Main")]
    public class InputActivity : BaseActivity, IOnCheckedChangeListener
    {
        private Input _inputTop;
        private Input _inputBottom;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _fontDropDown;
        private EOSSandboxDropDown _letterSpacingDropDown;
        private EOSSandboxDropDown _textSizeDropDown;
        private EOSSandboxDropDown _textColorDropDown;
        private EOSSandboxDropDown _textColorDisabledDropDown;
        private EOSSandboxDropDown _hintTextColorDropDown;
        private EOSSandboxDropDown _hintTextColorDisabledDropDown;
        private EOSSandboxDropDown _leftDrawableDropDown;
        private EOSSandboxDropDown _focusedColorDropDown;
        private EOSSandboxDropDown _disabledColorDropDown;
        private EOSSandboxDropDown _normalUnderlineColorDropDown;
        private EOSSandboxDropDown _normalIconColorDropDown;
        private EOSSandboxDropDown _populatedUnderlineColorDropDown;
        private EOSSandboxDropDown _populatedIconColorDropDown;
        private EOSSandboxDropDown _validatedRulesDropDown;
        private Switch _disabledSwitch;
        private int _validationKey;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InputLayout);

            _inputTop = FindViewById<Input>(Resource.Id.inputTop);
            _inputTop.UpdateAppearance();

            _inputTop.TextChanged += (s, e) =>
            {
                ProceedValidation(_validationKey);
            };

            _inputBottom = FindViewById<Input>(Resource.Id.inputBottom);
            _inputBottom.UpdateAppearance();

            _inputBottom.TextChanged += (s, e) =>
            {
                ProceedValidation(_validationKey);
            };

            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            _textColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textColorDropDown);
            _textColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledTextColorDropDown);
            _hintTextColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.hintTextColorDropDown);
            _hintTextColorDisabledDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledHintTextColorDropDown);
            _leftDrawableDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.iconDropDown);
            _focusedColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.focusedColorDropDown);
            _disabledColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledColorDropDown);
            _normalUnderlineColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.normalUnderlineColorDropDown);
            _normalIconColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.normalIconColorDropDown);
            _populatedUnderlineColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.populatedUnderlineColorDropDown);
            _populatedIconColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.populatedIconColorDropDown);
            _validatedRulesDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.validationRulesDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            _disabledSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

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
            _textSizeDropDown.ItemSelected += TextSizeView_ItemSelected;

            _textColorDropDown.Name = Fields.TextColor;
            _textColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDropDown.ItemSelected += TextColorItemSelected;

            _textColorDisabledDropDown.Name = Fields.DisabledTextColor;
            _textColorDisabledDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDisabledDropDown.ItemSelected += TextColorDisabledItemSelected;

            _hintTextColorDropDown.Name = Fields.HintTextColor;
            _hintTextColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _hintTextColorDropDown.ItemSelected += HintTextColorItemSelected;

            _hintTextColorDisabledDropDown.Name = Fields.HintTextColorDisabled;
            _hintTextColorDisabledDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _hintTextColorDisabledDropDown.ItemSelected += HintTextColorDisabledItemSelected;

            _leftDrawableDropDown.Name = Fields.IconFocused;
            _leftDrawableDropDown.SetupAdapter(Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableDropDown.ItemSelected += LeftDrawableItemSelected;

            _focusedColorDropDown.Name = Fields.FocusedColor;
            _focusedColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _focusedColorDropDown.ItemSelected += FocusedColorItemSelected;

            _disabledColorDropDown.Name = Fields.DisabledColor;
            _disabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _disabledColorDropDown.ItemSelected += DisabledColorItemSelected;

            _normalUnderlineColorDropDown.Name = Fields.NormalUnderlineColor;
            _normalUnderlineColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _normalUnderlineColorDropDown.ItemSelected += NormalUnderlineColorItemSelected;

            _normalIconColorDropDown.Name = Fields.NormalIconColor;
            _normalIconColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _normalIconColorDropDown.ItemSelected += NormalIconColorItemSelected;

            _populatedIconColorDropDown.Name = Fields.PopulatedIconColor;
            _populatedIconColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _populatedIconColorDropDown.ItemSelected += PopulatedIconColorItemSelected;

            _populatedUnderlineColorDropDown.Name = Fields.PopulatedUnderlineColor;
            _populatedUnderlineColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _populatedUnderlineColorDropDown.ItemSelected += PopulatedUnderlineColorItemSelected;

            _validatedRulesDropDown.Name = Fields.ValidationRules;
            _validatedRulesDropDown.SetupAdapter(Validation.ValidationCollection.Select(item => item.Key).ToList());
            _validatedRulesDropDown.ItemSelected += ValidatedRulesItemSelected;

            SetCurrenTheme(_inputTop.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            _disabledSwitch.SetOnCheckedChangeListener(this);

            Window.SetSoftInputMode(A.Views.SoftInput.StateAlwaysHidden);
        }

        private void ProceedValidation(int key)
        {
            switch(_validationKey)
            {
                case 0:
                case 1:
                    break;
                case 2:
                    _inputTop.IsValid = _inputTop.Text.Contains("@");
                    _inputBottom.IsValid = _inputBottom.Text.Contains("@");
                    break;
                case 3:
                    _inputTop.IsValid = !string.IsNullOrWhiteSpace(_inputTop.Text);
                    _inputBottom.IsValid = !string.IsNullOrWhiteSpace(_inputBottom.Text);
                    break;
            }
        }

        private void ValidatedRulesItemSelected(int position)
        {
            _validationKey = position;
            ProceedValidation(_validationKey);

            if(_validationKey == 1 || _validationKey == 0)
            {
                _inputTop.IsValid = true;
                _inputBottom.IsValid = true;
            }
        }

        private void PopulatedUnderlineColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.PopulatedUnderlineColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.PopulatedUnderlineColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void PopulatedIconColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.PopulatedIconColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.PopulatedIconColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void NormalIconColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.NormalIconColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.NormalIconColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void ThemeItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateApperaence();
            }
        }

        private void TextSizeView_ItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
                _inputBottom.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
            }
        }

        private void LetterSpacingItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
                _inputBottom.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
            }
        }

        private void FontItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
                _inputBottom.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
            }
        }

        private void TextColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void TextColorDisabledItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.TextColorDisabled = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.TextColorDisabled = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void HintTextColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.HintTextColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.HintTextColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void HintTextColorDisabledItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.HintTextColorDisabled = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.HintTextColorDisabled = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void LeftDrawableItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.LeftImage = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
                _inputBottom.LeftImage = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
            }
        }

        private void FocusedColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.FocusedColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.FocusedColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void NormalUnderlineColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.NormalUnderlineColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.NormalUnderlineColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void DisabledColorItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.DisabledColor = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.DisabledColor = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void ResetCustomValues()
        {
            _inputTop.ResetCustomization();
            _inputBottom.ResetCustomization();
            _fontDropDown.SetSpinnerSelection(0);
            _letterSpacingDropDown.SetSpinnerSelection(0);
            _textSizeDropDown.SetSpinnerSelection(0);
            _textColorDropDown.SetSpinnerSelection(0);
            _textColorDisabledDropDown.SetSpinnerSelection(0);
            _hintTextColorDropDown.SetSpinnerSelection(0);
            _hintTextColorDisabledDropDown.SetSpinnerSelection(0);
            _leftDrawableDropDown.SetSpinnerSelection(0);
            _focusedColorDropDown.SetSpinnerSelection(0);
            _disabledColorDropDown.SetSpinnerSelection(0);
            _normalUnderlineColorDropDown.SetSpinnerSelection(0);
            _normalIconColorDropDown.SetSpinnerSelection(0);
            _populatedUnderlineColorDropDown.SetSpinnerSelection(0);
            _populatedIconColorDropDown.SetSpinnerSelection(0);
            _validatedRulesDropDown.SetSpinnerSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _inputTop.Enabled = isChecked;
            _inputBottom.Enabled = isChecked;
        }
    }
}
