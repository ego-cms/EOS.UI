using System;
using CoreGraphics;
using EOS.UI.iOS.CollectionViewSources;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Components
{
    [Register("WorkTimeCalendarCollection")]
    public class WorkTimeCalendarCollection : UICollectionView
    {
        public WorkTimeCalendarCollection(): this(CGRect.Empty, new WorkTimeCalendarFlowLayout())
        {

        }

        public WorkTimeCalendarCollection(CGRect frame) : this(frame, new WorkTimeCalendarFlowLayout())
        {

        }

        public WorkTimeCalendarCollection(NSCoder coder) : base(coder)
        {
            CollectionViewLayout = new WorkTimeCalendarFlowLayout();
            Initalize();
        }

        public WorkTimeCalendarCollection(CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
        {
            Initalize();
        }

        protected WorkTimeCalendarCollection(NSObjectFlag t) : base(t)
        {
            CollectionViewLayout = new WorkTimeCalendarFlowLayout();
            Initalize();
        }

        protected internal WorkTimeCalendarCollection(IntPtr handle) : base(handle)
        {
            CollectionViewLayout = new WorkTimeCalendarFlowLayout();
            Initalize();
        }
        
        private void Initalize()
        {
            Source = new WorkTimeCalendarCollectionSource(this);
            BackgroundColor = UIColor.Clear;
        }
    }
}
