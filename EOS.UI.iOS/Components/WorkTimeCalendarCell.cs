using System;

using Foundation;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public partial class WorkTimeCalendarCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("WorkTimeCalendarCell");
        public static readonly UINib Nib;

        static WorkTimeCalendarCell()
        {
            Nib = UINib.FromName("WorkTimeCalendarCell", NSBundle.MainBundle);
        }

        protected WorkTimeCalendarCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        
        public void SetFont(UIFont font)
        {
            dayLabel.Font = font;
            startWorkLabel.Font = font;
            stopWorkLabel.Font = font;
            startBreakLabel.Font = font;
            stopBreakLabel.Font = font;
        }
    }
}
