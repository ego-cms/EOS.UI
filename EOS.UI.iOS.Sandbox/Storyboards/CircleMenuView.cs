using CoreGraphics;
using EOS.UI.iOS.Components;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleMenuView : BaseViewController
    {
        public const string Identifier = "CircleMenuView";
        private Dictionary<string, UIImage> _icons;
        private bool _navigationBarEnabled = true;
        private List<EOSSandboxDropDown> _dropDowns;
        private CircleMenu _circleMenu;

        public CircleMenuView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _icons = new Dictionary<string, UIImage>()
            {
                {"icImage",UIImage.FromBundle("icImage")},
                {"icPanorama", UIImage.FromBundle("icPanorama")},
                {"icVideo",UIImage.FromBundle("icVideo")},
                {"icPhoto", UIImage.FromBundle("icPhoto")},
                {"icTimelapse", UIImage.FromBundle("icTimelapse")},
                {"icMacro", UIImage.FromBundle("icMacro")},
                {"icPortrait", UIImage.FromBundle("icPortrait")},
                {"icSeries", UIImage.FromBundle("icSeries")},
                {"icTimer", UIImage.FromBundle("icTimer")},
                {"icSixteenToNine", UIImage.FromBundle("icSixteenToNine")},
                {"icOneToOne", UIImage.FromBundle("icOneToOne")},
                {"icHDR", UIImage.FromBundle("icHDR")}
            };

            _circleMenu = new CircleMenu();
            _circleMenu.Clicked += (object sender, int id) =>
            {
                if (id == 100)
                {
                    _navigationBarEnabled = !_navigationBarEnabled;
                    ToggleNavigationBar(_navigationBarEnabled);
                }
                else
                {
                    if (id != 2 && id != 3)
                        ShowItemController(_icons.ElementAt(id).Key);
                }
            };

            _circleMenu.CircleMenuItems = CreateSource(9);
            _circleMenu.Attach(this);

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                unfocusedButtonColorDropDown,
                unfocusedIconColorDropDown,
                focusedButtonColorDropDown,
                focusedIconColorDropDown,
                itemsCountDropDown
            };

            var rect = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(rect);
            InitItemsCountDropDown(rect);
            InitSources(rect);
            InitResetButton();
        }

        private void InitSources(CGRect rect)
        {
            InitFocusedIconColorDropDown(rect);
            InitFocusedBackgroundColorDropDown(rect);
            InitUnfocusedIconColorDropDown(rect);
            InitUnfocusedButtonColorDropDown(rect);
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) =>
                {
                    _circleMenu.GetThemeProvider().SetCurrentTheme(theme);
                    _circleMenu.ResetCustomization();
                    ResetFields();
                    InitSources(rect);
                    UpdateAppearance();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_circleMenu.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFocusedBackgroundColorDropDown(CGRect rect)
        {
            focusedButtonColorDropDown.InitSource(
                CircleMenuConstants.FocusedBackgroundColors,
                color => _circleMenu.FocusedBackgroundColor = color,
                Fields.FocusedBackgroundColor,
               rect);
        }

        private void InitFocusedIconColorDropDown(CGRect rect)
        {
            focusedIconColorDropDown.InitSource(
                CircleMenuConstants.FocusedIconColors,
                color => _circleMenu.FocusedIconColor = color,
                Fields.FocusedIconColor,
               rect);
        }

        private void InitUnfocusedButtonColorDropDown(CGRect rect)
        {
            unfocusedButtonColorDropDown.InitSource(
                CircleMenuConstants.UnfocusedBackgroundColors,
                color => _circleMenu.UnfocusedBackgroundColor = color,
                Fields.UnfocusedBackgroundColor,
              rect);
        }

        private void InitUnfocusedIconColorDropDown(CGRect rect)
        {
            unfocusedIconColorDropDown.InitSource(
                CircleMenuConstants.UnfocusedIconColors,
                color => _circleMenu.UnfocusedIconColor = color,
                Fields.UnfocusedIconColor,
              rect);
        }

        private void InitItemsCountDropDown(CGRect rect)
        {
            itemsCountDropDown.InitSource(
                CircleMenuSource.SourceCollection.Select(item => item.Key).ToList(),
                items =>
                {
                    _circleMenu.CircleMenuItems = CreateSource(Convert.ToInt32(items));
                },
                Fields.CircleMenuItems,
              rect);
        }

        private void InitResetButton()
        {
            resetButton.TouchUpInside += (sender, e) =>
            {
                _circleMenu.ResetCustomization();
                ResetFields();
            };
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
            _circleMenu.CircleMenuItems = CreateSource(Convert.ToInt32(9));
        }

        private List<CircleMenuItemModel> CreateSource(int count)
        {
            var menuModels = new List<CircleMenuItemModel>();
            for (int i = 0; i < count; ++i)
            {
                var menuModel = new CircleMenuItemModel(i, _icons.ElementAt(i).Value);
                if (i == 2 || i == 3)
                {
                    for (int j = 9; j < 12; ++j)
                    {
                        var subMenuModel = new CircleMenuItemModel(j, _icons.ElementAt(j).Value);
                        menuModel.Children.Add(subMenuModel);
                    }
                }
                menuModels.Add(menuModel);
            }
            return menuModels;
        }

        void ShowItemController(string hdImageName)
        {
            var storyboard = UIStoryboard.FromName("CircleMenuItemView", null);
            var viewController = (CircleMenuItemView)storyboard.InstantiateViewController("CircleMenuItemView");
            viewController.NavigationItem.Title = "CircleMenuItemView";
            viewController.MenuItemImage = UIImage.FromBundle(hdImageName + "_HD").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            NavigationController.PushViewController(viewController, true);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ToggleNavigationBar(_navigationBarEnabled);
        }
    }
}