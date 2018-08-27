using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EOS.UI.Droid.Components;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.CircleMenu, Theme = "@style/Sandbox.Main")]
    public class CircleMenuActivity : BaseActivity
    {
        private CircleMenu _circleMenu;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _mainColorDropDown;
        private EOSSandboxDropDown _focusedMainColorDropDown;
        private EOSSandboxDropDown _focusedButtonColorDropDown;
        private EOSSandboxDropDown _unfocusedButtonColorDropDown;
        private EOSSandboxDropDown _circleMenuItemsDropDown;
        private Dictionary<int, int> _iconsDictionary;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleMenuLayout);

            _circleMenu = new CircleMenu(BaseContext);
            _circleMenu.Attach(Window.DecorView.FindViewById(Android.Resource.Id.Content) as ViewGroup);

            _iconsDictionary = new Dictionary<int, int>()
            {
                { 1,  Resource.Drawable.SwitchIcon },
                { 2,  Resource.Drawable.CameraIcon },
                { 3,  Resource.Drawable.ShutterIcon },
                { 4,  Resource.Drawable.TimerIcon },
                { 5,  Resource.Drawable.BushIcon },
                { 6,  Resource.Drawable.DurationIcon },
                { 7,  Resource.Drawable.EffectsIcon },
                { 8,  Resource.Drawable.HealIcon },
                { 9,  Resource.Drawable.MasksIcon },
                { 101,  Resource.Drawable.WidescreenIcon },
                { 102,  Resource.Drawable.OneToOneIcon },
                { 103,  Resource.Drawable.HDRIcon },
            };

            _circleMenu.CircleMenuItems = GenerateSource(9);

            _circleMenu.Clicked += (s, index) =>
            {
                var intent = new Intent(this, typeof(CircleMenuItemActivity));
                intent.PutExtra("logoId", _iconsDictionary.GetValueOrDefault(index));
                StartActivity(intent);
            };

            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _mainColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.mainColor);
            _focusedMainColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.focusedMainColor);
            _focusedButtonColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.focusedButtonColor);
            _unfocusedButtonColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.unfocusedButtonColor);
            _circleMenuItemsDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.circleMenuItems);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _mainColorDropDown.Name = Fields.UnfocusedBackgroundColor;
            _mainColorDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _mainColorDropDown.ItemSelected += MainColorItemSelected;

            _focusedMainColorDropDown.Name = Fields.FocusedBackgroundColor;
            _focusedMainColorDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _focusedMainColorDropDown.ItemSelected += FocusedMainColorItemSelected;

            _focusedButtonColorDropDown.Name = Fields.FocusedIconColor;
            _focusedButtonColorDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _focusedButtonColorDropDown.ItemSelected += FocusedButtonColorItemSelected;

            _unfocusedButtonColorDropDown.Name = Fields.UnfocusedIconColor;
            _unfocusedButtonColorDropDown.SetupAdapter(Colors.MainColorsCollection.Select(item => item.Key).ToList());
            _unfocusedButtonColorDropDown.ItemSelected += UnfocusedButtonColorItemSelected;

            _circleMenuItemsDropDown.Name = Fields.CircleMenuItems;
            _circleMenuItemsDropDown.SetupAdapter(CircleMenuSource.SourceCollection.Select(item => item.Key).ToList());
            _circleMenuItemsDropDown.ItemSelected += CircleMenuItemsItemSelected;

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            SetCurrenTheme(_circleMenu.GetThemeProvider().GetCurrentTheme());
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void CircleMenuItemsItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.CircleMenuItems = GenerateSource(CircleMenuSource.SourceCollection.ElementAt(position).Value);
        }

        private void UnfocusedButtonColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.UnfocusedIconColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void FocusedButtonColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.FocusedIconColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void FocusedMainColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.FocusedBackgroundColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void MainColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.UnfocusedBackgroundColor = Colors.MainColorsCollection.ElementAt(position).Value;
        }

        private void ThemeItemSelected(int position)
        {
            if(position > 0)
            {
                _circleMenu.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateAppearance();
            }
        }

        private List<CircleMenuItemModel> GenerateSource(int items)
        {
            var menus = new List<CircleMenuItemModel>();
            var submenus = new List<CircleMenuItemModel>();
            for(int i = 101; i <= 103; i++)
                submenus.Add(new CircleMenuItemModel(i, BaseContext.Resources.GetDrawable(_iconsDictionary.GetValueOrDefault(i))));

            for(int i = 1; i <= items; i++)
            {
                if(i == 3 || i == 4)
                    menus.Add(new CircleMenuItemModel(i, BaseContext.Resources.GetDrawable(_iconsDictionary.GetValueOrDefault(i)), submenus));
                else
                    menus.Add(new CircleMenuItemModel(i, BaseContext.Resources.GetDrawable(_iconsDictionary.GetValueOrDefault(i))));
            }

            return menus;
        }

        private void ResetCustomValues()
        {
            _circleMenu.ResetCustomization();
            _mainColorDropDown.SetSpinnerSelection(0);
            _focusedMainColorDropDown.SetSpinnerSelection(0);
            _focusedButtonColorDropDown.SetSpinnerSelection(0);
            _unfocusedButtonColorDropDown.SetSpinnerSelection(0);
            _circleMenuItemsDropDown.SetSpinnerSelection(0);
            _circleMenu.CircleMenuItems = GenerateSource(9);
        }
    }
}
