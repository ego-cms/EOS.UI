using System.Linq;
using System.Globalization;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using C = UIFrameworks.Shared.Themes.Helpers.Controls;
using R = Android.Resource;


namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = C.SimpleLabel)]
    public class SimpleLabelActivity : BaseActivity
    {
        private SimpleLabel _simpleLabel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleLabelLayout);

            _simpleLabel = FindViewById<SimpleLabel>(Resource.Id.simpleLabel);
            _simpleLabel.UpdateAppearance();

            var textColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColor);
            var fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            var letterSpacingView = FindViewById<EditText>(Resource.Id.editLetterSpacing);
            var textSizeView = FindViewById<EditText>(Resource.Id.editTextSize);

            textColorSpinner.Adapter = new ArrayAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            textColorSpinner.ItemSelected += TextColorSpinner_ItemSelected;
            fontSpinner.Adapter = new ArrayAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontSpinner.ItemSelected += FontSpinner_ItemSelected;

            letterSpacingView.AfterTextChanged += LetterSpacingView_AfterTextChanged;
            textSizeView.AfterTextChanged += TextSizeView_AfterTextChanged;
        }

        private void TextSizeView_AfterTextChanged(object sender, global::Android.Text.AfterTextChangedEventArgs e)
        {
            if(float.TryParse((sender as EditText).Text, out float result))
                if(result > 0f)
                    _simpleLabel.SetCustomTextSize(result);
        }

        private void LetterSpacingView_AfterTextChanged(object sender, global::Android.Text.AfterTextChangedEventArgs e)
        {
            if(float.TryParse((sender as EditText).Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
                if(result > 0)
                    _simpleLabel.SetCustomLetterSpacing(result);
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleLabel.SetCustomFont(Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value));
        }

        private void TextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleLabel.SetCustomTextColor(Colors.ColorsCollection.ElementAt(e.Position).Value);
        }

    }
}