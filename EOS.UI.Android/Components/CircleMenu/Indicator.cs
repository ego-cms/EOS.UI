using System;
using System.Drawing;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;

namespace EOS.UI.Android.Components
{
    public class Indicator : View
    {
        #region .ctor

        public Indicator(Context context) : base(context)
        {
        }

        public Indicator(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public Indicator(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public Indicator(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected Indicator(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        #endregion

        #region overrides

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                Alpha = Enabled ? 1f : 0.5f;
            }
        }

        #endregion
    }
}
