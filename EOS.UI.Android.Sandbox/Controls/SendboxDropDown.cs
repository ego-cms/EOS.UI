using System;
using System.Collections;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Sandbox.Adapters;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Controls
{
    public delegate void ItemSelectedEventHandler(int position);
    public class SendboxDropDown : LinearLayout
    {
        #region fields

        private TextView _nameTextView;
        private Spinner _spinner;
        private Context _context;

        #endregion

        #region properties

        public string Name
        {
            get => _nameTextView.Text;
            set => _nameTextView.Text = value;
        }

        #endregion

        #region events

        public event ItemSelectedEventHandler ItemSelected;

        #endregion

        #region constructors

        public SendboxDropDown(Context context) : base(context)
        {
            Initialize(context);
        }

        public SendboxDropDown(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public SendboxDropDown(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context, attrs);
        }

        public SendboxDropDown(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(context, attrs);
        }

        protected SendboxDropDown(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
            _spinner.Adapter = new SpinnerAdapter(_context, R.Layout.SimpleSpinnerItem, source);
        }

        private void Initialize(Context context = null, IAttributeSet attrs = null)
        {
            _context = context;
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.DropDownLayout, this);

            _nameTextView = view.FindViewById<TextView>(Resource.Id.dropDownName);
            _spinner = view.FindViewById<Spinner>(Resource.Id.dropDownSpinner);

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
    }
}