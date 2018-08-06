using EOS.UI.iOS.Components;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.DataModels;
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

        public CircleMenuView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _icons = new List<UIImage>()
            {
                UIImage.FromBundle("icBrush"),
                UIImage.FromBundle("icDuration"),
                UIImage.FromBundle("icEffects"),
                UIImage.FromBundle("icHDR"),
                UIImage.FromBundle("icHeal"),
                UIImage.FromBundle("icMasks"),
                UIImage.FromBundle("icOneToOne"),
                UIImage.FromBundle("icReplay"),
                UIImage.FromBundle("icShutter"),
                UIImage.FromBundle("icSixteenToNine"),
                UIImage.FromBundle("icTimer"),
                UIImage.FromBundle("icVideo"),
            };
            
            var circleMenu = new CircleMenu(View);
            circleMenu.LeftSwiped += (sender, e) => swipeLabel.Text = "Left swipe";
            circleMenu.RightSwiped += (sender, e) => swipeLabel.Text = "Right swipe";
            circleMenu.Clicked += (object sender, int e) =>
            {
                swipeLabel.Text = $"{e.ToString()} clicked";
            };

            circleMenu.CircleMenuItems = CreateSource();
            circleMenu.Attach();
        }
        
        private List<CircleMenuItemModel> CreateSource()
        {
            var menuModels = new List<CircleMenuItemModel>();
            for (int i = 0; i < 9; ++i)
            {
                var menuModel = new CircleMenuItemModel(i, null);
                menuModel.ImageSource = _icons[i];
                if(i % 2 == 0)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        var subMenuModel = new CircleMenuItemModel(j, null);
                        menuModel.Children.Add(subMenuModel);
                    }
                }
                menuModels.Add(menuModel);
            }
            return menuModels;
        }
    }
}