using EOS.UI.iOS.Components;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleMenuView : BaseViewController
    {
        public const string Identifier = "CircleMenuView";
        private List<UIImage> _icons;
        private bool _navigationBarEnabled = true;

        public CircleMenuView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            _icons = new List<UIImage>()
            {
                UIImage.FromBundle("icImage"),
                UIImage.FromBundle("icPanorama"),
                UIImage.FromBundle("icVideo"),
                UIImage.FromBundle("icPhoto"),
                
                UIImage.FromBundle("icTimelapse"),
                UIImage.FromBundle("icMacro"),
                UIImage.FromBundle("icPortrait"),
                UIImage.FromBundle("icSeries"),
                UIImage.FromBundle("icTimer"),
                
                UIImage.FromBundle("icSixteenToNine"),
                UIImage.FromBundle("icOneToOne"),
                UIImage.FromBundle("icHDR"),
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
                        ShowItemController(_icons[id]);
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
                var menuModel = new CircleMenuItemModel(i, _icons[i]);
                if (i == 2 || i == 3)
                {
                    for (int j = 9; j < 12; ++j)
                    {
                        var subMenuModel = new CircleMenuItemModel(j, _icons[j]);
                        menuModel.Children.Add(subMenuModel);
                    }
                }
                menuModels.Add(menuModel);
            }
            return menuModels;
        }

        void ShowItemController(UIImage image)
        {
            var storyboard = UIStoryboard.FromName("CircleMenuItemView", null);
            var viewController = (CircleMenuItemView)storyboard.InstantiateViewController("CircleMenuItemView");
            viewController.NavigationItem.Title = "CircleMenuItemView";
            viewController.MenuItemImage = image;
            NavigationController.PushViewController(viewController, true);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ToggleNavigationBar(_navigationBarEnabled);
        }
    }
}