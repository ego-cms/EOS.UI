using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class PassthroughToWindowView : UIView
    {
        public PassthroughToWindowView(CGRect frame) : base(frame)
        {
        }

        public PassthroughToWindowView()
        {
        }

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            var view = base.HitTest(point, uievent);
            return view == this ? null : view;
        }
    }
}
