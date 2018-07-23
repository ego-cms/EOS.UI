using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using EOS.UI.Android.Components;
using EOS.UI.Android.Sandbox.Controls;
using EOS.UI.Shared.Themes.DataModels;
using UIFrameworks.Shared.Themes.Helpers;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using A = Android;

namespace EOS.UI.Android.Sandbox.Activities
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleMenuLayout);

            _circleMenu = new CircleMenu(BaseContext);
            _circleMenu.Attach(Window.DecorView.FindViewById(A.Resource.Id.Content) as ViewGroup);

            _circleMenu.CircleMenuItems = GenerateSource(9);

            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _mainColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.mainColor);
            _focusedMainColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.focusedMainColor);
            _focusedButtonColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.focusedButtonColor);
            _unfocusedButtonColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.unfocusedButtonColor);
            _circleMenuItemsDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.circleMenuItems);
            var resetButton = FindViewById<A.Widget.Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _mainColorDropDown.Name = Fields.MainColor;
            _mainColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _mainColorDropDown.ItemSelected += MainColorItemSelected;

            _focusedMainColorDropDown.Name = Fields.FocusedMainColor;
            _focusedMainColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _focusedMainColorDropDown.ItemSelected += FocusedMainColorItemSelected;

            _focusedButtonColorDropDown.Name = Fields.FocusedButtonColor;
            _focusedButtonColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _focusedButtonColorDropDown.ItemSelected += FocusedButtonColorItemSelected;

            _unfocusedButtonColorDropDown.Name = Fields.UnfocusedButtonColor;
            _unfocusedButtonColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _unfocusedButtonColorDropDown.ItemSelected += UnfocusedButtonColorItemSelected;

            _circleMenuItemsDropDown.Name = Fields.CircleMenuItems;
            _circleMenuItemsDropDown.SetupAdapter(CircleMenuSource.SourceCollection.Select(item => item.Key).ToList());
            _circleMenuItemsDropDown.ItemSelected += CircleMenuItemsItemSelected;

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };
        }

        private void CircleMenuItemsItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.CircleMenuItems = GenerateSource(CircleMenuSource.SourceCollection.ElementAt(position).Value);
        }

        private void UnfocusedButtonColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.UnfocusedButtonColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void FocusedButtonColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.FocusedButtonColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void FocusedMainColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.FocusedMainColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void MainColorItemSelected(int position)
        {
            if(position > 0)
                _circleMenu.MainColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void ThemeItemSelected(int position)
        {
            if(position > 0)
            {
                _circleMenu.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateApperaence();
            }
        }

        private List<CircleMenuItemModel> GenerateSource(int items)
        {
            var menus = new List<CircleMenuItemModel>();
            var submenus = new List<CircleMenuItemModel>();

            submenus.Add(new CircleMenuItemModel(31, BaseContext.Resources.GetDrawable(Resource.Drawable.WidescreenIcon)));
            submenus.Add(new CircleMenuItemModel(32, BaseContext.Resources.GetDrawable(Resource.Drawable.OneToOneIcon)));
            submenus.Add(new CircleMenuItemModel(33, BaseContext.Resources.GetDrawable(Resource.Drawable.HDRIcon)));

            menus.Add(new CircleMenuItemModel(1, BaseContext.Resources.GetDrawable(Resource.Drawable.SwitchIcon)));
            menus.Add(new CircleMenuItemModel(2, BaseContext.Resources.GetDrawable(Resource.Drawable.CameraIcon)));
            menus.Add(new CircleMenuItemModel(3, BaseContext.Resources.GetDrawable(Resource.Drawable.ShutterIcon), submenus));

            if(items == 3)
                return menus;

            menus.Add(new CircleMenuItemModel(4, BaseContext.Resources.GetDrawable(Resource.Drawable.TimerIcon)));

            if(items == 4)
                return menus;

            menus.Add(new CircleMenuItemModel(5, BaseContext.Resources.GetDrawable(Resource.Drawable.BushIcon)));
            menus.Add(new CircleMenuItemModel(6, BaseContext.Resources.GetDrawable(Resource.Drawable.DurationIcon)));
            menus.Add(new CircleMenuItemModel(7, BaseContext.Resources.GetDrawable(Resource.Drawable.EffectsIcon)));
            menus.Add(new CircleMenuItemModel(8, BaseContext.Resources.GetDrawable(Resource.Drawable.HealIcon)));
            menus.Add(new CircleMenuItemModel(9, BaseContext.Resources.GetDrawable(Resource.Drawable.MasksIcon)));

            return menus;
        }

        private void ResetCustomValues()
        {
            _circleMenu.ResetCustomization();
            _mainColorDropDown.SetSpinnerSelection(0);
            _focusedMainColorDropDown.SetSpinnerSelection(0);
            _focusedButtonColorDropDown.SetSpinnerSelection(0);
            _unfocusedButtonColorDropDown.SetSpinnerSelection(0);
        }
    }
}
