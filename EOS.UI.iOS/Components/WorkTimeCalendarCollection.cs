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
        public WorkTimeCalendarCollection(NSCoder coder) : base(coder)
        {
            Initalize();
        }

        public WorkTimeCalendarCollection(CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
        {
            Initalize();
        }

        protected WorkTimeCalendarCollection(NSObjectFlag t) : base(t)
        {
            Initalize();
        }

        protected internal WorkTimeCalendarCollection(IntPtr handle) : base(handle)
        {
            Initalize();
        }
        
        private void Initalize()
        {
            Source = new WorkTimeCalendarCollectionSource(this);
        }
    }
}
