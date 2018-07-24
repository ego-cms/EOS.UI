using System;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Sandbox.RecyclerImplementation
{
    public class EOSSandboxControlsViewHolder : RecyclerView.ViewHolder, IEOSThemeControl, View.IOnClickListener
    {
        private LinearLayout _container;
        private ImageView _arrowImage;
        private EOSSandboxDivider _divider;
        private Action<int> _clickAction;

        public TextView ControlTitle { get; private set; }
             
        public EOSSandboxControlsViewHolder(View itemView, Action<int> clickAction) : base(itemView)
        {
            itemView.Clickable = true;
            itemView.SetOnClickListener(this);
            _clickAction = clickAction;
            _container = itemView.FindViewById<LinearLayout>(Resource.Id.holderContainer);
            ControlTitle = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _arrowImage = itemView.FindViewById<ImageView>(Resource.Id.imageArrow);
            _divider = itemView.FindViewById<EOSSandboxDivider>(Resource.Id.dropDownDivider);
        }

        public void OnClick(View v)
        {
            _clickAction?.Invoke(Position);
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
