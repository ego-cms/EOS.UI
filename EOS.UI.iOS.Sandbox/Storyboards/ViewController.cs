using EOS.UI.iOS.Sandbox.Controls;
using EOS.UI.iOS.Sandbox.TableSources;
using Foundation;
using System;
using System.Linq;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new ControlTableSource(controlsTableView);
            source.Selected += OnRowSelected;
            controlsTableView.Source = source;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(true, false);
        }

        void OnRowSelected(object sender, NSIndexPath indexPath)
        {
            var element = ControlsData.Instance.Names.ElementAt(indexPath.Row);
            var storyboard = UIStoryboard.FromName(element.Value, null);
            var viewController = storyboard.InstantiateViewController(element.Value);
            viewController.NavigationItem.Title = element.Key;
            this.NavigationController.PushViewController(viewController, true);
        }
    }
}