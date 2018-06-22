using System;
using System.Collections;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Sandbox.Adapters;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Controls
{
    public delegate void ItemSelectedEventHandler(int position);
    public class EOSSandboxDropDown : LinearLayout, IEOSThemeControl
    {
        #region fields

        private TextView _nameTextView;
        private Spinner _spinner;
        private Context _context;
        private EOSSandboxDivider _divider;

        #endregion

        #region properties

        public string Name
        {
            get => _nameTextView.Text;
            set => _nameTextView.Text = value;
        }

        public override bool Enabled
        {
            get => _spinner.Enabled;
            set => _spinner.Enabled = value;
        }

        #endregion

        #region events

        public event ItemSelectedEventHandler ItemSelected;

        #endregion

        #region constructors

        public EOSSandboxDropDown(Context context) : base(context)
        {
            Initialize(context);
        }

        public EOSSandboxDropDown(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public EOSSandboxDropDown(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context, attrs);
        }

        public EOSSandboxDropDown(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(context, attrs);
        }

        protected EOSSandboxDropDown(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region utility methods

        public void SetSpinnerSelection(int position)
        {
            _spinner?.SetSelection(position);
        }

        public void SetupAdapter(IList source)
        {
            _spinner.Adapter = new EOSSandboxSpinnerAdapter(_context, R.Layout.SimpleSpinnerItem, source);
            _spinner.SetSelection(0);
        }

        private void Initialize(Context context = null, IAttributeSet attrs = null)
        {
            _context = context;
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.DropDownLayout, this);

            _nameTextView = view.FindViewById<TextView>(Resource.Id.dropDownName);
            _spinner = view.FindViewById<Spinner>(Resource.Id.dropDownSpinner);
            _divider = view.FindViewById<EOSSandboxDivider>(Resource.Id.dropDownDivider);

            _spinner.ItemSelected += (sender, e) =>
            {
                ItemSelected?.Invoke(e.Position);
            };

            if(attrs != null)
                SetAttributes(attrs);
        }

        private void SetAttributes(IAttributeSet attrs)
        {
            
        }

        #endregion

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
                _nameTextView.SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1));
                (_spinner.Adapter as IEOSThemeControl)?.UpdateAppearance();
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
