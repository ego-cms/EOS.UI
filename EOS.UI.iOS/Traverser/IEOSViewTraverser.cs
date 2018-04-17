using System;
using UIKit;

namespace EOS.UI.iOS.Traverser
{
    public interface IEOSViewTraverser
    {
        void TraverseView<T>(UIViewController viewController, Action action);
    }
}
