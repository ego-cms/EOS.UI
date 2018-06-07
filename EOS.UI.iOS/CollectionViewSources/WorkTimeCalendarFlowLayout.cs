using System;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using UIKit;

namespace EOS.UI.iOS.CollectionViewSources
{
    public class WorkTimeCalendarFlowLayout: UICollectionViewFlowLayout
    {
        public WorkTimeCalendarFlowLayout()
        {
            MinimumInteritemSpacing = 0;
            MinimumLineSpacing = 0;
            ScrollDirection = UICollectionViewScrollDirection.Horizontal;
        }
        
        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            var attrs  = base.LayoutAttributesForElementsInRect(rect);
            for (int i = 1; i < attrs.Length; ++i)
            {
                var currentAttribute = attrs[i];
                var prevAttribute = attrs[i - 1];
                var maxSpacing = 0;
                var origin = prevAttribute.Frame.GetMaxX();
                if(origin + maxSpacing + currentAttribute.Frame.Size.Width < CollectionViewContentSize.Width)
                {
                    var frame = currentAttribute.Frame.ResizeRect(x: origin + maxSpacing);
                    currentAttribute.Frame = frame;
                }
            }
            return attrs;
        }
    }
}
