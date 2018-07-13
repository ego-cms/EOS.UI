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

            var source = new List<CircleMenuItemModel>()
            {
                new CircleMenuItemModel()
                {
                    Id = 0,
                    ImageSource = UIImage.FromBundle("ic_backup")
                },
                new CircleMenuItemModel()
                {
                    Id = 1,
                    ImageSource = UIImage.FromBundle("ic_build")
                },
                new CircleMenuItemModel()
                {
                    Id = 2,
                    ImageSource = UIImage.FromBundle("ic_camera_enhance")
                },
                new CircleMenuItemModel()
                {
                    Id = 3,
                    ImageSource = UIImage.FromBundle("ic_backup")
                },
                new CircleMenuItemModel()
                {
                    Id = 4,
                    ImageSource = UIImage.FromBundle("ic_build")
                },
                new CircleMenuItemModel()
                {
                    Id = 5,
                    ImageSource = UIImage.FromBundle("ic_camera_enhance")
                },
                new CircleMenuItemModel()
                {
                    Id = 6,
                    ImageSource = UIImage.FromBundle("ic_build")
                },
                new CircleMenuItemModel()
                {
                    Id = 7,
                    ImageSource = UIImage.FromBundle("ic_camera_enhance")
                },
            };
            circleMenu.Source = source;
            circleMenu.Attach();
        }
    }
}