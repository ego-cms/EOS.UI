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

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.Input)]
    public class InputActivity : BaseActivity, IOnCheckedChangeListener
    {
        private Input _inputTop;
        private Input _inputBottom;
        private Spinner _themeSpinner;
        private Spinner _fontSpinner;
        private Spinner _letterSpacingSpinner;
        private Spinner _textSizeSpinner;
        private Spinner _textColorSpinner;
        private Spinner _textColorSpinnerDisabled;
        private Spinner _hintTextColorSpinner;
        private Spinner _hintTextColorSpinnerDisabled;
        private Spinner _leftDrawableFocusedSpinner;
        private Spinner _leftDrawableUnfocusedSpinner;
        private Spinner _leftDrawableDisabledSpinner;
        private Spinner _underlineColorFocusedSpinner;
        private Spinner _underlineColorUnfocusedSpinner;
        private Spinner _underlineColorDisabledSpinner;
        private Switch _disabledSwitch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InputLayout);

            _inputTop = FindViewById<Input>(Resource.Id.inputTop);
            _inputTop.UpdateAppearance();
            _inputBottom = FindViewById<Input>(Resource.Id.inputBottom);
            _inputBottom.UpdateAppearance();

            _themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            _fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            _letterSpacingSpinner = FindViewById<Spinner>(Resource.Id.spinnerLetterSpacing);
            _textSizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            _textColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColor);
            _textColorSpinnerDisabled = FindViewById<Spinner>(Resource.Id.spinnerTextColorDisabled);
            _hintTextColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerHintTextColor);
            _hintTextColorSpinnerDisabled = FindViewById<Spinner>(Resource.Id.spinnerHintTextColorDisabled);
            _leftDrawableFocusedSpinner = FindViewById<Spinner>(Resource.Id.spinnerDrawableFocused);
            _leftDrawableUnfocusedSpinner = FindViewById<Spinner>(Resource.Id.spinnerDrawableUnfocused);
            _leftDrawableDisabledSpinner = FindViewById<Spinner>(Resource.Id.spinnerDrawableDisabled);
            _underlineColorFocusedSpinner = FindViewById<Spinner>(Resource.Id.spinnerUnderlineColorFocused);
            _underlineColorUnfocusedSpinner = FindViewById<Spinner>(Resource.Id.spinnerUnderlineColorUnfocused);
            _underlineColorDisabledSpinner = FindViewById<Spinner>(Resource.Id.spinnerUnderlineColorDisabled);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

            _themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeSpinner.ItemSelected += ThemeSpinner_ItemSelected;

            _fontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontSpinner.ItemSelected += FontSpinner_ItemSelected;

            _letterSpacingSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingSpinner.ItemSelected += LetterSpacingView_ItemSelected;

            _textSizeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeSpinner.ItemSelected += TextSizeView_ItemSelected;

            _textColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorSpinner.ItemSelected += TextColorSpinner_ItemSelected;

            _textColorSpinnerDisabled.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorSpinnerDisabled.ItemSelected += TextColorSpinnerDisabled_ItemSelected;

            _hintTextColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _hintTextColorSpinner.ItemSelected += HintTextColor_ItemSelected;

            _hintTextColorSpinnerDisabled.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _hintTextColorSpinnerDisabled.ItemSelected += HintTextColorSpinnerDisabled_ItemSelected;

            _leftDrawableFocusedSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableFocusedSpinner.ItemSelected += LeftDrawableFocused_ItemSelected;

            _leftDrawableUnfocusedSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableUnfocusedSpinner.ItemSelected += LeftDrawableUnfocused_ItemSelected;

            _leftDrawableDisabledSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Icons.DrawableCollection.Select(item => item.Key).ToList());
            _leftDrawableDisabledSpinner.ItemSelected += LeftDrawableDisabledSpinner_ItemSelected;

            _underlineColorFocusedSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _underlineColorFocusedSpinner.ItemSelected += UnderlineColorFocusedSpinner_ItemSelected;

            _underlineColorUnfocusedSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _underlineColorUnfocusedSpinner.ItemSelected += UnderlineColorUnfocusedSpinner_ItemSelected;

            _underlineColorDisabledSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _underlineColorDisabledSpinner.ItemSelected += UnderlineColorDisabledSpinner_ItemSelected;

            SetCurrenTheme(_inputTop.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            disableSwitch.SetOnCheckedChangeListener(this);
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeSpinner.SetSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeSpinner.SetSelection(2);
        }

        private void ThemeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                ResetCustomValues();
            }
        }

        private void TextSizeView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
                _inputBottom.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
            }
        }

        private void LetterSpacingView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
                _inputBottom.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
            }
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
                _inputBottom.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
            }
        }

        private void TextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.TextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.TextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void TextColorSpinnerDisabled_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.TextColorDisabled = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.TextColorDisabled = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void HintTextColor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.HintTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.HintTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void HintTextColorSpinnerDisabled_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.HintTextColorDisabled = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.HintTextColorDisabled = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void LeftDrawableUnfocused_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.LeftImageUnfocused =  BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(e.Position).Value);
                _inputBottom.LeftImageUnfocused = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(e.Position).Value);
            }
        }

        private void LeftDrawableFocused_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.LeftImageFocused = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(e.Position).Value);
                _inputBottom.LeftImageFocused = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(e.Position).Value);
            }
        }

        private void LeftDrawableDisabledSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.LeftImageDisabled = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(e.Position).Value);
                _inputBottom.LeftImageDisabled = BaseContext.GetDrawable(Icons.DrawableCollection.ElementAt(e.Position).Value);
            }
        }

        private void UnderlineColorFocusedSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.UnderlineColorFocused = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.UnderlineColorFocused = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void UnderlineColorUnfocusedSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.UnderlineColorUnfocused = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.UnderlineColorUnfocused = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void UnderlineColorDisabledSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _inputTop.UnderlineColorDisabled = Colors.ColorsCollection.ElementAt(e.Position).Value;
                _inputBottom.UnderlineColorDisabled = Colors.ColorsCollection.ElementAt(e.Position).Value;
            }
        }

        private void ResetCustomValues()
        {
            _inputTop.ResetCustomization();
            _inputBottom.ResetCustomization();
            _fontSpinner.SetSelection(0);
            _letterSpacingSpinner.SetSelection(0);
            _textSizeSpinner.SetSelection(0);
            _textColorSpinner.SetSelection(0);
            _textColorSpinnerDisabled.SetSelection(0);
            _hintTextColorSpinner.SetSelection(0);
            _hintTextColorSpinnerDisabled.SetSelection(0);
            _leftDrawableFocusedSpinner.SetSelection(0);
            _leftDrawableUnfocusedSpinner.SetSelection(0);
            _leftDrawableDisabledSpinner.SetSelection(0);
            _underlineColorFocusedSpinner.SetSelection(0);
            _underlineColorUnfocusedSpinner.SetSelection(0);
            _underlineColorDisabledSpinner.SetSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _inputTop.Enabled = !isChecked;
            _inputBottom.Enabled = !isChecked;
        }
    }
}