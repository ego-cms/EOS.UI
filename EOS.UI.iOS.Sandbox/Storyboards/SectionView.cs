
using System;
using System.Drawing;
using EOS.UI.iOS.Sandbox.TableSources;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public partial class SectionView : BaseViewController
    {
        public const string Identifier = "SectionView";

        public SectionView(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            sectionTableView.Source = new SectionTableSource(sectionTableView);
        }

        #endregion
    }
}