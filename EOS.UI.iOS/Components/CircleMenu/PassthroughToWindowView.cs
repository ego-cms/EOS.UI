using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class PassthroughToWindowView : UIView
    {
        public PassthroughToWindowView(CGRect frame): base(frame)
        {
        }
        
        public PassthroughToWindowView()
        {
        }

        public override bool PointInside(CGPoint point, UIEvent uievent)
        {
            foreach(var subview in Subviews)
            {
                if(!subview.Hidden && subview.UserInteractionEnabled && subview.PointInside(ConvertPointToView(point, subview), uievent))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
