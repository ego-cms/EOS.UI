using System.Globalization;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using UIFrameworks.Shared.Themes.Interfaces;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using C = UIFrameworks.Shared.Themes.Helpers.Controls;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = C.BadgeLabel)]
    public class BadgeLabelActivity : BaseActivity
    {
        private BadgeLabel _badge;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BadgeLabelLayout);

            _badge = FindViewById<BadgeLabel>(Resource.Id.badgeLabel);
            _badge.UpdateAppearance();

            var backgroundColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerBackgroundColor);
            var textColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColor);
            var fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            var letterSpacingView = FindViewById<EditText>(Resource.Id.editLetterSpacing);
            var textSizeView = FindViewById<EditText>(Resource.Id.editTextSize);
            var cornerRadiusView = FindViewById<EditText>(Resource.Id.editCornerRadius);

            backgroundColorSpinner.Adapter = new ArrayAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            backgroundColorSpinner.ItemSelected += BackgroundColorSpinner_ItemSelected;
            textColorSpinner.Adapter = new ArrayAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            textColorSpinner.ItemSelected += TextColorSpinner_ItemSelected;
            fontSpinner.Adapter = new ArrayAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontSpinner.ItemSelected += FontSpinner_ItemSelected;

            letterSpacingView.AfterTextChanged += LetterSpacingView_AfterTextChanged;
            textSizeView.AfterTextChanged += TextSizeView_AfterTextChanged;
            cornerRadiusView.AfterTextChanged += CornerRadiusView_AfterTextChanged;
        }

        private void CornerRadiusView_AfterTextChanged(object sender, global::Android.Text.AfterTextChangedEventArgs e)
        {
            if(float.TryParse((sender as EditText).Text, out float result))
                if(result > 0f)
                    _badge.SetCornerRadius(result);
        }

        private void TextSizeView_AfterTextChanged(object sender, global::Android.Text.AfterTextChangedEventArgs e)
        {
            if(float.TryParse((sender as EditText).Text, out float result))
                if(result > 0f)
                    _badge.SetCustomTextSize(result);
        }

        private void LetterSpacingView_AfterTextChanged(object sender, global::Android.Text.AfterTextChangedEventArgs e)
        {
            if(float.TryParse((sender as EditText).Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
                if(result > 0)
                    _badge.SetCustomLetterSpacing(result);
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.SetCustomFont(Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value));
        }

        private void TextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.SetCustomTextColor(Colors.ColorsCollection.ElementAt(e.Position).Value);
        }

        private void BackgroundColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.SetCustomBackgroundColor(Colors.ColorsCollection.ElementAt(e.Position).Value);
        }
    }
}