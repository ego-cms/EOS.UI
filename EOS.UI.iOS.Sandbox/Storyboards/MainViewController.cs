using EOS.UI.iOS.Sandbox.Controls;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.iOS.Sandbox.TableSources;
using Foundation;
using System;
using System.Linq;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class MainViewController : BaseViewController
    {
        public MainViewController(IntPtr handle) : base(handle)
        {
            NavigationItem.Title = ControlsData.Title;
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
            UpdateApperaence();

        }

        private void OnRowSelected(object sender, NSIndexPath indexPath)
        {
            var element = ControlsData.Instance.Names.ElementAt(indexPath.Row);
            var storyboard = UIStoryboard.FromName(element.Value, null);
            var viewController = storyboard.InstantiateViewController(element.Value);
            viewController.NavigationItem.Title = element.Key;
            NavigationController.PushViewController(viewController, true);
        }
    }
}