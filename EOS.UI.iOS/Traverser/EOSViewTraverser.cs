using System;
using UIKit;

namespace EOS.UI.iOS.Traverser
{
    public class EOSViewTraverser : IEOSViewTraverser
    {
        public void TraverseView<T>(UIViewController viewController, Action action)
        {
            foreach(var controller in viewController.ChildViewControllers)
                if(controller is T)
                    action?.Invoke();
        }
    }
}
