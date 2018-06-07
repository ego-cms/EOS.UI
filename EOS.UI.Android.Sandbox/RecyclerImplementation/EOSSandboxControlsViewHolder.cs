using System;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Sandbox.Controls;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Sandbox.RecyclerImplementation
{
    public class EOSSandboxControlsViewHolder : RecyclerView.ViewHolder, View.IOnTouchListener, IEOSThemeControl
    {
        private Color _normalBackground = Color.Transparent;
        private Color _selectedBackground = Color.Gray;
        private LinearLayout _container;
        private ImageView _arrowImage;
        private EOSSandboxDivider _divider;
        private Action<int> _clickAction;

        public TextView ControlTitle { get; private set; }
             
        public EOSSandboxControlsViewHolder(View itemView, Action<int> clickAction) : base(itemView)
        {
            _clickAction = clickAction;
            _container = itemView.FindViewById<LinearLayout>(Resource.Id.holderContainer);
            ControlTitle = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _arrowImage = itemView.FindViewById<ImageView>(Resource.Id.imageArrow);
            _divider = itemView.FindViewById<EOSSandboxDivider>(Resource.Id.dropDownDivider);
            _container.SetOnTouchListener(this);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down)
            {
                _container.SetBackgroundColor(_selectedBackground);
            }
            else if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
            {
                _container.SetBackgroundColor(_normalBackground);
                _clickAction?.Invoke(Position);
            }
            return true;
        }

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
                _selectedBackground = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                ControlTitle.SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1));
                _arrowImage.Drawable.SetColorFilter(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3), PorterDuff.Mode.SrcIn);
                _divider.UpdateAppearance();
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        #endregion

    }
}