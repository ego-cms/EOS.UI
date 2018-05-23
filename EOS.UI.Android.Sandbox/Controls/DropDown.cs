using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Android.Sandbox.Controls
{
    public delegate void ItemSelectedEventHandler(int position);
    public class DropDown : LinearLayout
    {
        #region fields

        private TextView _nameTextView;
        private Spinner _spinner;

        #endregion

        #region properties

        public string Name
        {
            get => _nameTextView.Text;
            set => _nameTextView.Text = value;
        }

        public ISpinnerAdapter Adapter
        {
            get => _spinner.Adapter;
            set => _spinner.Adapter = value;
        }

        #endregion

        #region events

        public event ItemSelectedEventHandler ItemSelected;

        #endregion

        #region constructors

        public DropDown(Context context) : base(context)
        {
            Initialize();
        }

        public DropDown(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public DropDown(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public DropDown(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected DropDown(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region utility methods

        public void SetSpinnerSelection(int position)
        {
            _spinner?.SetSelection(position);
        }

        private void Initialize(IAttributeSet attrs = null)
        {
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