using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using EOS.UI.Android.Models;
using EOS.UI.Android.Sandbox.Adapters;
using EOS.UI.Android.Sandbox.RecyclerImplementation;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.Section)]
    public class SectionActivity : BaseActivity
    {
        private RecyclerView _sectionRecyclerView;
        private List<object> _dataSource;

        private Spinner _themeSpinner;
        private Spinner _sectionNameSpinner;
        private Spinner _buttonTextSpinner;
        private Spinner _sectionFontSpinner;
        private Spinner _buttonFontSpinner;
        private Spinner _sectionNameLetterSpacingSpinner;
        private Spinner _buttonTextLetterSpacingSpinner;
        private Spinner _sectionTextSizeSpinner;
        private Spinner _buttonTextSizeSpinner;
        private Spinner _sectionTextColorSpinner;
        private Spinner _buttonTextColorSpinner;
        private Spinner _backgroundColorSpinner;
        private Spinner _borderColorSpinner;
        private Spinner _borderWidthSpinner;
        private Spinner _paddingTopSpinner;
        private Spinner _paddingButtonSpinner;
        private Spinner _paddingLeftSpinner;
        private Spinner _paddingRightSpinner;
        private Switch _hasBorderSwitch;
        private Switch _hasButtonSwitch;
        private Button _resetCustomizationSwich;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SectionLayout);

            _sectionRecyclerView = FindViewById<RecyclerView>(Resource.Id.sectionRecyclerView);
            var layoutManager = new LinearLayoutManager(BaseContext);
            _sectionRecyclerView.SetLayoutManager(layoutManager);

            _dataSource = new List<object>()
            {
                new SectionModel()
                {
                    SectionAction = () => { Toast.MakeText(BaseContext, "Action invoked", ToastLength.Short).Show(); },
                    HasBorder= (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder],
                    HasButton = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction]
                },
                "First item",
                "Second item",
                "Third item"
            };

            var adapter = new SectionAdapter(_dataSource);
            _sectionRecyclerView.SetAdapter(adapter);

            _themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            _sectionNameSpinner = FindViewById<Spinner>(Resource.Id.spinnerSectionName);
            _buttonTextSpinner = FindViewById<Spinner>(Resource.Id.spinnerButtonText);
            _sectionFontSpinner = FindViewById<Spinner>(Resource.Id.spinnerSectionNameFont);
            _buttonFontSpinner = FindViewById<Spinner>(Resource.Id.spinnerButtonTextFont);
            _sectionNameLetterSpacingSpinner = FindViewById<Spinner>(Resource.Id.spinnerSectionNameLetterSpacing);
            _buttonTextLetterSpacingSpinner = FindViewById<Spinner>(Resource.Id.spinnerButtonTextLetterSpacing);
            _sectionTextSizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerSectionTextSize);
            _buttonTextSizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerButtonTextSize);
            _sectionTextColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerSectionTextColor);
            _buttonTextColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerButtonTextColor);
            _backgroundColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerSectionBackgroundColor);
            _borderColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerBorderColor);
            _borderWidthSpinner = FindViewById<Spinner>(Resource.Id.spinnerBorderWidth);
            _paddingTopSpinner = FindViewById<Spinner>(Resource.Id.spinnerPaddingTop);
            _paddingButtonSpinner = FindViewById<Spinner>(Resource.Id.spinnerPaddingBottom);
            _paddingLeftSpinner = FindViewById<Spinner>(Resource.Id.spinnerPaddingLeft);
            _paddingRightSpinner = FindViewById<Spinner>(Resource.Id.spinnerPaddingRight);
            _hasBorderSwitch = FindViewById<Switch>(Resource.Id.switchHasBorder);
            _hasButtonSwitch = FindViewById<Switch>(Resource.Id.switchHasButton);
            _resetCustomizationSwich = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeSpinner.ItemSelected += ThemeSpinner_ItemSelected;

            _sectionNameSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Titles.TitleCollection.Select(item => item.Key).ToList());
            _sectionNameSpinner.ItemSelected += SectionNameSpinner_ItemSelected;

            _buttonTextSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Titles.TitleCollection.Select(item => item.Key).ToList());
            _buttonTextSpinner.ItemSelected += ButtonTextSpinner_ItemSelected;

            _sectionNameLetterSpacingSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _sectionNameLetterSpacingSpinner.ItemSelected += SectionNameLetterSpacingSpinner_ItemSelected;

            _buttonTextLetterSpacingSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _buttonTextLetterSpacingSpinner.ItemSelected += ButtonTextLetterSpacingSpinner_ItemSelected;

            _sectionTextSizeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _sectionTextSizeSpinner.ItemSelected += SectionTextSizeSpinner_ItemSelected;

            _buttonTextSizeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _buttonTextSizeSpinner.ItemSelected += ButtonTextSizeSpinner_ItemSelected;

            _sectionTextColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _sectionTextColorSpinner.ItemSelected += SectionTextColorSpinner_ItemSelected;

            _buttonTextColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _buttonTextColorSpinner.ItemSelected += ButtonTextColorSpinner_ItemSelected;

            _backgroundColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorSpinner.ItemSelected += BackgroundColorSpinner_ItemSelected;

            _borderColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _borderColorSpinner.ItemSelected += BorderColorSpinner_ItemSelected;

            _borderWidthSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.BorderWidthCollection.Select(item => item.Key).ToList());
            _borderWidthSpinner.ItemSelected += BorderWidthSpinner_ItemSelected;

            _paddingTopSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingTopSpinner.ItemSelected += PaddingTopSpinner_ItemSelected;

            _paddingButtonSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingButtonSpinner.ItemSelected += PaddingButtonSpinner_ItemSelected;

            _paddingLeftSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingLeftSpinner.ItemSelected += PaddingLeftSpinner_ItemSelected;

            _paddingRightSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingRightSpinner.ItemSelected += PaddingRightSpinner_ItemSelected;

            _sectionFontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _sectionFontSpinner.ItemSelected += SectionFontSpinner_ItemSelected;

            _buttonFontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _buttonFontSpinner.ItemSelected += ButtonFontSpinner_ItemSelected;

            _hasBorderSwitch.CheckedChange += HasBorderSwitch_CheckedChange;

            _hasButtonSwitch.CheckedChange += HasButtonSwitch_CheckedChange;

            _resetCustomizationSwich.Click += ResetCustomizationSwich_Click;

            SetCurrenTheme(EOSThemeProvider.Instance.GetCurrentTheme());
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeSpinner.SetSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeSpinner.SetSelection(2);

            _hasBorderSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder];
            _hasButtonSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction];
        }

        private void ResetCustomValues()
        {
            (_sectionRecyclerView.GetAdapter() as SectionAdapter).ResetCustomizatin();
            _sectionFontSpinner.SetSelection(0);
            _buttonFontSpinner.SetSelection(0);
            _sectionNameSpinner.SetSelection(0);
            _buttonTextSpinner.SetSelection(0);
            _sectionNameLetterSpacingSpinner.SetSelection(0);
            _buttonTextLetterSpacingSpinner.SetSelection(0);
            _sectionTextSizeSpinner.SetSelection(0);
            _buttonTextSizeSpinner.SetSelection(0);
            _sectionTextColorSpinner.SetSelection(0);
            _buttonTextColorSpinner.SetSelection(0);
            _backgroundColorSpinner.SetSelection(0);
            _borderColorSpinner.SetSelection(0);
            _borderWidthSpinner.SetSelection(0);
            _paddingTopSpinner.SetSelection(0);
            _paddingButtonSpinner.SetSelection(0);
            _paddingLeftSpinner.SetSelection(0);
            _paddingRightSpinner.SetSelection(0);
            _hasBorderSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder];
            _hasButtonSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction];
        }

        private void ResetCustomizationSwich_Click(object sender, EventArgs e)
        {
            ResetCustomValues();
        }

        private void ButtonFontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonNameFont = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionFontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionNameFont = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void HasButtonSwitch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
            if(adapter.Headers.FirstOrDefault() is SectionModel section)
            {
                section.HasButton = e.IsChecked;
                adapter.NotifyDataSetChanged();
            }
        }

        private void HasBorderSwitch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
            if(adapter.Headers.FirstOrDefault() is SectionModel section)
            {
                section.HasBorder = e.IsChecked;
                adapter.NotifyDataSetChanged();
            }
        }

        private void PaddingRightSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.RightPadding = Sizes.PaddingsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void PaddingLeftSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.LeftPadding = Sizes.PaddingsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void PaddingButtonSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BottonPadding = Sizes.PaddingsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }

        }

        private void PaddingTopSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.TopPadding = Sizes.PaddingsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void BorderWidthSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BorderWidth = Sizes.BorderWidthCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void BorderColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BorderColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void BackgroundColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonNameColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionTextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionNameColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextSizeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonTextTextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionTextSizeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionNameTextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextLetterSpacingSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonTextLetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionNameLetterSpacingSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionTextLetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonText = Titles.TitleCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }


        private void SectionNameSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionName = Titles.TitleCollection.ElementAt(e.Position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ThemeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                EOSThemeProvider.Instance.SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                ResetCustomValues();
            }
        }
    }
}