using Android.App;
using Android.Content;
using Android.Views.InputMethods;

namespace EOS.UI.Droid.Sandbox.Helpers
{
    public static class ContextHelpers
    {
        public static void HideKeyBoard(this Context _context)
        {
            if(_context is Activity activity)
            {
                if(activity.CurrentFocus != null)
                {
                    var inputMethodManager = _context.GetSystemService(Context.InputMethodService) as InputMethodManager;
                    inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
                }
            }
            else
            {
                //Check type of context
                //If context is ContextWrapper type we must use BaseContext to get Activity and hide keyboard
                if(_context is ContextWrapper context)
                {
                    HideKeyBoard(context.BaseContext);
                }
            }
        }
    }
}