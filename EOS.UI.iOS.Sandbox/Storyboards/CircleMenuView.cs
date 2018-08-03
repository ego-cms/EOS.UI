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

        public CircleMenuView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var circleMenu = new CircleMenu(View);
            circleMenu.LeftSwiped += (sender, e) => swipeLabel.Text = "Left swipe";
            circleMenu.RightSwiped += (sender, e) => swipeLabel.Text = "Right swipe";

            circleMenu.CircleMenuItems = CreateSource();
            circleMenu.Attach();
        }
        
        private List<CircleMenuItemModel> CreateSource()
        {
            var menuModels = new List<CircleMenuItemModel>();
            for (int i = 0; i < 6; ++i)
            {
                var menuModel = new CircleMenuItemModel(i, null);
                if(i % 2 == 0)
                {
                    for (int j = 0; j < 3; ++j)
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