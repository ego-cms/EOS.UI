using System.Collections;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Sandbox.Adapters
{
    public class EOSSandboxSpinnerAdapter : ArrayAdapter, IEOSThemeControl
    {
        private const string DefaultText = "default";
        private int _resourseId;
        private Color _backgroundColor;
        private Color _textColor;

        public EOSSandboxSpinnerAdapter(Context context, int resource, IList objects) : base(context, resource, objects)
        {
            _resourseId = resource;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            if(_textColor != Color.Transparent)
                (view as TextView).SetTextColor(_textColor);

            if(position == 0)
                (view as TextView).Text = DefaultText;

            return view;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView?? LayoutInflater.From(Context).Inflate(_resourseId, parent, false);
            var item = GetItem(position);
            var text = view as TextView;
            text.Gravity = GravityFlags.CenterVertical;
            text.SetText(item.ToString(), TextView.BufferType.Normal);
            var parameters = text.LayoutParameters;
            parameters.Height = position == 0 ? parameters.Height = 1 : parameters.Height = (int)(35 * Context.Resources.DisplayMetrics.Density);

            text.LayoutParameters = parameters;

            if(_backgroundColor != Color.Transparent)
                text.SetBackgroundColor(_backgroundColor);

            if(_textColor != Color.Transparent)
                text.SetTextColor(_textColor);

            return view;
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
                _textColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1);
                _backgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor5);
                NotifyDataSetChanged();
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
