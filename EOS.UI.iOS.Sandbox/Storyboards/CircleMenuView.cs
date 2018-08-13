using EOS.UI.iOS.Components;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleMenuView : BaseViewController
    {
        public const string Identifier = "CircleMenuView";
        private Dictionary<string, UIImage> _icons;
        private bool _navigationBarEnabled = true;

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

            var circleMenu = new CircleMenu();
            circleMenu.LeftSwiped += (sender, e) => swipeLabel.Text = "Left swipe";
            circleMenu.RightSwiped += (sender, e) => swipeLabel.Text = "Right swipe";
            circleMenu.Clicked += (object sender, int id) =>
            {
                swipeLabel.Text = $"{id.ToString()}id clicked";
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

            circleMenu.CircleMenuItems = CreateSource();
            circleMenu.Attach(this);
        }

        private List<CircleMenuItemModel> CreateSource()
        {
            var menuModels = new List<CircleMenuItemModel>();
            for (int i = 0; i < 9; ++i)
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
            viewController.MenuItemImage = UIImage.FromBundle(hdImageName+"_HD");
            NavigationController.PushViewController(viewController, true);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ToggleNavigationBar(_navigationBarEnabled);
        }
    }
}