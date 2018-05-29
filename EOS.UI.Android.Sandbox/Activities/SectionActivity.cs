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
using EOS.UI.Android.Sandbox.Controls;
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

        private DropDown _themeDropDown;
        private DropDown _sectionNameDropDown;
        private DropDown _buttonTextDropDown;
        private DropDown _sectionFontDropDown;
        private DropDown _buttonFontDropDown;
        private DropDown _sectionNameLetterSpacingDropDown;
        private DropDown _buttonTextLetterSpacingDropDown;
        private DropDown _sectionTextSizeDropDown;
        private DropDown _buttonTextSizeDropDown;
        private DropDown _sectionTextColorDropDown;
        private DropDown _buttonTextColorDropDown;
        private DropDown _backgroundColorDropDown;
        private DropDown _borderColorDropDown;
        private DropDown _borderWidthDropDown;
        private DropDown _paddingTopDropDown;
        private DropDown _paddingBottomDropDown;
        private DropDown _paddingLeftDropDown;
        private DropDown _paddingRightDropDown;
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

            _themeDropDown = FindViewById<DropDown>(Resource.Id.themeDropDown);
            _sectionNameDropDown = FindViewById<DropDown>(Resource.Id.sectionNameDropDown);
            _buttonTextDropDown = FindViewById<DropDown>(Resource.Id.buttonTextDropDown);
            _sectionFontDropDown = FindViewById<DropDown>(Resource.Id.sectionNameFontDropDown);
            _buttonFontDropDown = FindViewById<DropDown>(Resource.Id.buttonTextFontDropDown);
            _sectionNameLetterSpacingDropDown = FindViewById<DropDown>(Resource.Id.sectionNameLetterSpacingDropDown);
            _buttonTextLetterSpacingDropDown = FindViewById<DropDown>(Resource.Id.buttonTextLetterSpacingDropDown);
            _sectionTextSizeDropDown = FindViewById<DropDown>(Resource.Id.sectionTextSizeDropDown);
            _buttonTextSizeDropDown = FindViewById<DropDown>(Resource.Id.buttonTextSizeDropDown);
            _sectionTextColorDropDown = FindViewById<DropDown>(Resource.Id.sectionTextColorDropDown);
            _buttonTextColorDropDown = FindViewById<DropDown>(Resource.Id.buttonTextColorDropDown);
            _backgroundColorDropDown = FindViewById<DropDown>(Resource.Id.backgroundColorDropDown);
            _borderColorDropDown = FindViewById<DropDown>(Resource.Id.borderColorDropDown);
            _borderWidthDropDown = FindViewById<DropDown>(Resource.Id.borderWidthDropDown);
            _paddingTopDropDown = FindViewById<DropDown>(Resource.Id.paddingTopDropDown);
            _paddingBottomDropDown = FindViewById<DropDown>(Resource.Id.paddingBottomDropDown);
            _paddingLeftDropDown = FindViewById<DropDown>(Resource.Id.paddingLeftDropDown);
            _paddingRightDropDown = FindViewById<DropDown>(Resource.Id.paddingRightDropDown);
            _hasBorderSwitch = FindViewById<Switch>(Resource.Id.switchHasBorder);
            _hasButtonSwitch = FindViewById<Switch>(Resource.Id.switchHasButton);
            _resetCustomizationSwich = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _sectionNameDropDown.Name = Fields.SectionName;
            _sectionNameDropDown.SetupAdapter(Titles.TitleCollection.Select(item => item.Key).ToList());
            _sectionNameDropDown.ItemSelected += SectionNameItemSelected;

            _buttonTextDropDown.Name = Fields.ButtonText;
            _buttonTextDropDown.SetupAdapter(Titles.TitleCollection.Select(item => item.Key).ToList());
            _buttonTextDropDown.ItemSelected += ButtonTextItemSelected;

            _sectionNameLetterSpacingDropDown.Name = Fields.SectionNameLetterSpacing;
            _sectionNameLetterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _sectionNameLetterSpacingDropDown.ItemSelected += SectionNameLetterSpacingItemSelected;

            _buttonTextLetterSpacingDropDown.Name = Fields.ButtonTextLetterSpacing;
            _buttonTextLetterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _buttonTextLetterSpacingDropDown.ItemSelected += ButtonTextLetterSpacingItemSelected;

            _sectionTextSizeDropDown.Name = Fields.SectionTextSize;
            _sectionTextSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _sectionTextSizeDropDown.ItemSelected += SectionTextSizeItemSelected;

            _buttonTextSizeDropDown.Name = Fields.ButtonTextSize;
            _buttonTextSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _buttonTextSizeDropDown.ItemSelected += ButtonTextSizeItemSelected;

            _sectionTextColorDropDown.Name = Fields.SectionTextSize;
            _sectionTextColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _sectionTextColorDropDown.ItemSelected += SectionTextColorItemSelected;

            _buttonTextColorDropDown.Name = Fields.ButtonTextColor;
            _buttonTextColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _buttonTextColorDropDown.ItemSelected += ButtonTextColorItemSelected;

            _backgroundColorDropDown.Name = Fields.BackgroundColor;
            _backgroundColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorDropDown.ItemSelected += BackgroundColorItemSelected;

            _borderColorDropDown.Name = Fields.BorderColor;
            _borderColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _borderColorDropDown.ItemSelected += BorderColorItemSelected;

            _borderWidthDropDown.Name = Fields.BorderWidth;
            _borderWidthDropDown.SetupAdapter(Sizes.BorderWidthCollection.Select(item => item.Key).ToList());
            _borderWidthDropDown.ItemSelected += BorderWidthItemSelected;

            _paddingTopDropDown.Name = Fields.PaddingTop;
            _paddingTopDropDown.SetupAdapter(Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingTopDropDown.ItemSelected += PaddingTopItemSelected;

            _paddingBottomDropDown.Name = Fields.PaddingBottom;
            _paddingBottomDropDown.SetupAdapter(Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingBottomDropDown.ItemSelected += PaddingButtonItemSelected;

            _paddingLeftDropDown.Name = Fields.PaddingLeft;
            _paddingLeftDropDown.SetupAdapter(Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingLeftDropDown.ItemSelected += PaddingLeftItemSelected;

            _paddingRightDropDown.Name = Fields.PaddingRight;
            _paddingRightDropDown.SetupAdapter(Sizes.PaddingsCollection.Select(item => item.Key).ToList());
            _paddingRightDropDown.ItemSelected += PaddingRightItemSelected;

            _sectionFontDropDown.Name = Fields.SectionNameFont;
            _sectionFontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            _sectionFontDropDown.ItemSelected += SectionFontItemSelected;

            _buttonFontDropDown.Name = Fields.ButtonTextFont;
            _buttonFontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            _buttonFontDropDown.ItemSelected += ButtonFontItemSelected;

            _hasBorderSwitch.CheckedChange += HasBorderSwitch_CheckedChange;

            _hasButtonSwitch.CheckedChange += HasButtonSwitch_CheckedChange;

            _resetCustomizationSwich.Click += ResetCustomizationSwich_Click;

            SetCurrenTheme(EOSThemeProvider.Instance.GetCurrentTheme());
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);

            _hasBorderSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder];
            _hasButtonSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction];
        }

        private void ResetCustomValues()
        {
            (_sectionRecyclerView.GetAdapter() as SectionAdapter).ResetCustomizatin();
            _sectionFontDropDown.SetSpinnerSelection(0);
            _buttonFontDropDown.SetSpinnerSelection(0);
            _sectionNameDropDown.SetSpinnerSelection(0);
            _buttonTextDropDown.SetSpinnerSelection(0);
            _sectionNameLetterSpacingDropDown.SetSpinnerSelection(0);
            _buttonTextLetterSpacingDropDown.SetSpinnerSelection(0);
            _sectionTextSizeDropDown.SetSpinnerSelection(0);
            _buttonTextSizeDropDown.SetSpinnerSelection(0);
            _sectionTextColorDropDown.SetSpinnerSelection(0);
            _buttonTextColorDropDown.SetSpinnerSelection(0);
            _backgroundColorDropDown.SetSpinnerSelection(0);
            _borderColorDropDown.SetSpinnerSelection(0);
            _borderWidthDropDown.SetSpinnerSelection(0);
            _paddingTopDropDown.SetSpinnerSelection(0);
            _paddingBottomDropDown.SetSpinnerSelection(0);
            _paddingLeftDropDown.SetSpinnerSelection(0);
            _paddingRightDropDown.SetSpinnerSelection(0);
            _hasBorderSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder];
            _hasButtonSwitch.Checked = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction];
        }

        private void ResetCustomizationSwich_Click(object sender, EventArgs e)
        {
            ResetCustomValues();
        }

        private void ButtonFontItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonNameFont = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionFontItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionNameFont = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
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

        private void PaddingRightItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.RightPadding = Sizes.PaddingsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void PaddingLeftItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.LeftPadding = Sizes.PaddingsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void PaddingButtonItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BottonPadding = Sizes.PaddingsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }

        }

        private void PaddingTopItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.TopPadding = Sizes.PaddingsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void BorderWidthItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BorderWidth = Sizes.BorderWidthCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void BorderColorItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BorderColor = Colors.ColorsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void BackgroundColorItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextColorItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonNameColor = Colors.ColorsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionTextColorItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionNameColor = Colors.ColorsCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextSizeItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonTextTextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionTextSizeItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionNameTextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextLetterSpacingItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonTextLetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void SectionNameLetterSpacingItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionTextLetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ButtonTextItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.ButtonText = Titles.TitleCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }


        private void SectionNameItemSelected(int position)
        {
            if(position > 0)
            {
                var adapter = _sectionRecyclerView.GetAdapter() as SectionAdapter;
                if(adapter.Headers.FirstOrDefault() is SectionModel section)
                {
                    section.SectionName = Titles.TitleCollection.ElementAt(position).Value;
                    adapter.NotifyDataSetChanged();
                }
            }
        }

        private void ThemeItemSelected(int position)
        {
            if(position > 0)
            {
                EOSThemeProvider.Instance.SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
            }
        }
    }
}