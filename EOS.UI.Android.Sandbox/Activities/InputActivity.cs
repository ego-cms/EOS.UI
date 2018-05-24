using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Adapters;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static Android.Widget.CompoundButton;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;
using A = Android;
using EOS.UI.Android.Sandbox.Controls;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.Input)]
    public class InputActivity : BaseActivity, IOnCheckedChangeListener
    {
        private Input _inputTop;
        private Input _inputBottom;
        private DropDown _themeDropDown;
        private DropDown _fontDropDown;
        private DropDown _letterSpacingDropDown;
        private DropDown _textSizeDropDown;
        private DropDown _textColorDropDown;
        private DropDown _textColorDisabledDropDown;
        private DropDown _hintTextColorDropDown;
        private DropDown _hintTextColorDisabledDropDown;
        private DropDown _leftDrawableFocusedDropDown;
        private DropDown _leftDrawableUnfocusedDropDown;
        private DropDown _leftDrawableDisabledDropDown;
        private DropDown _underlineColorFocusedDropDown;
        private DropDown _underlineColorUnfocusedDropDown;
        private DropDown _underlineColorDisabledDropDown;
        private Switch _disabledSwitch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InputLayout);

            _inputTop = FindViewById<Input>(Resource.Id.inputTop);
            _inputTop.UpdateAppearance();
            _inputBottom = FindViewById<Input>(Resource.Id.inputBottom);
            _inputBottom.UpdateAppearance();

            _themeDropDown = FindViewById<DropDown>(Resource.Id.themeDropDown);
            _fontDropDown = FindViewById<DropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<DropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<DropDown>(Resource.Id.textSizeDropDown);
            _textColorDropDown = FindViewById<DropDown>(Resource.Id.textColorDropDown);
            _textColorDisabledDropDown = FindViewById<DropDown>(Resource.Id.disabledTextColorDropDown);
            _hintTextColorDropDown = FindViewById<DropDown>(Resource.Id.hintTextColorDropDown);
            _hintTextColorDisabledDropDown = FindViewById<DropDown>(Resource.Id.disabledHintTextColorDropDown);
            _leftDrawableFocusedDropDown = FindViewById<DropDown>(Resource.Id.focusedIconDropDown);
            _leftDrawableUnfocusedDropDown = FindViewById<DropDown>(Resource.Id.unfocusedIconDropDown);
            _leftDrawableDisabledDropDown = FindViewById<DropDown>(Resource.Id.disabledIconDropDown);
            _underlineColorFocusedDropDown = FindViewById<DropDown>(Resource.Id.focusedUnderlineColorDropDown);
            _underlineColorUnfocusedDropDown = FindViewById<DropDown>(Resource.Id.unfocusedUnderlineColorDropDown);
            _underlineColorDisabledDropDown = FindViewById<DropDown>(Resource.Id.disabledUnderlineColorDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            _disabledSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

            _textSizeDropDown.Name = Fields.TextSize;
            _textSizeDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeDropDown.ItemSelected += TextSizeView_ItemSelected;

            _textColorDropDown.Name = Fields.TextColor;
            _textColorDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDropDown.ItemSelected += TextColorItemSelected;

            _textColorDisabledDropDown.Name = Fields.DisabledTextColor;
            _textColorDisabledDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDisabledDropDown.ItemSelected += TextColorDisabledItemSelected;

            _hintTextColorDropDown.Name = Fields.HintTextColor;
            _hintTextColorDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _hintTextColorDropDown.ItemSelected += HintTextColorItemSelected;

            _hintTextColorDisabledDropDown.Name = Fields.HintTextColorDisabled;
            _hintTextColorDisabledDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _hintTextColorDisabledDropDown.ItemSelected += HintTextColorDisabledItemSelected;

            _leftDrawableFocusedDropDown.Name = Fields.IconFocused;
            _leftDrawableFocusedDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableFocusedDropDown.ItemSelected += LeftDrawableFocusedItemSelected;

            _leftDrawableUnfocusedDropDown.Name = Fields.IconUnocused;
            _leftDrawableUnfocusedDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableUnfocusedDropDown.ItemSelected += LeftDrawableUnfocusedItemSelected;

            _leftDrawableDisabledDropDown.Name = Fields.IconDisabled;
            _leftDrawableDisabledDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableDisabledDropDown.ItemSelected += LeftDrawableDisabledItemSelected;

            _underlineColorFocusedDropDown.Name = Fields.UnderlineColorFocused;
            _underlineColorFocusedDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _underlineColorFocusedDropDown.ItemSelected += UnderlineColorFocusedItemSelected;

            _underlineColorUnfocusedDropDown.Name = Fields.UnderlineColorUnocused;
            _underlineColorUnfocusedDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _underlineColorUnfocusedDropDown.ItemSelected += UnderlineColorUnfocusedItemSelected;

            _underlineColorDisabledDropDown.Name = Fields.UnderlineColorDisabled;
            _underlineColorDisabledDropDown.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _underlineColorDisabledDropDown.ItemSelected += UnderlineColorDisabledItemSelected;

            SetCurrenTheme(_inputTop.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            _disabledSwitch.SetOnCheckedChangeListener(this);

            Window.SetSoftInputMode(A.Views.SoftInput.StateAlwaysHidden);
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

        private void LeftDrawableUnfocusedItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.LeftImageUnfocused =  BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
                _inputBottom.LeftImageUnfocused = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
            }
        }

        private void LeftDrawableFocusedItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.LeftImageFocused = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
                _inputBottom.LeftImageFocused = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
            }
        }

        private void LeftDrawableDisabledItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.LeftImageDisabled = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
                _inputBottom.LeftImageDisabled = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(position).Value);
            }
        }

        private void UnderlineColorFocusedItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.UnderlineColorFocused = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.UnderlineColorFocused = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void UnderlineColorUnfocusedItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.UnderlineColorUnfocused = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.UnderlineColorUnfocused = Colors.ColorsCollection.ElementAt(position).Value;
            }
        }

        private void UnderlineColorDisabledItemSelected(int position)
        {
            if(position > 0)
            {
                _inputTop.UnderlineColorDisabled = Colors.ColorsCollection.ElementAt(position).Value;
                _inputBottom.UnderlineColorDisabled = Colors.ColorsCollection.ElementAt(position).Value;
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
            _leftDrawableFocusedDropDown.SetSpinnerSelection(0);
            _leftDrawableUnfocusedDropDown.SetSpinnerSelection(0);
            _leftDrawableDisabledDropDown.SetSpinnerSelection(0);
            _underlineColorFocusedDropDown.SetSpinnerSelection(0);
            _underlineColorUnfocusedDropDown.SetSpinnerSelection(0);
            _underlineColorDisabledDropDown.SetSpinnerSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _inputTop.Enabled = isChecked;
            _inputBottom.Enabled = isChecked;
        }
    }
}