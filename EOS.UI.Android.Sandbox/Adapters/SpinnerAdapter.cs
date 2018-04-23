using System.Collections;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using A = Android;

namespace EOS.UI.Android.Sandbox.Adapters
{
    public class SpinnerAdapter : ArrayAdapter
    {
        private int _resourseId;
        public SpinnerAdapter(Context context, int resource, IList objects) : base(context, resource, objects)
        {
            _resourseId = resource;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView?? LayoutInflater.From(Context).Inflate(_resourseId, parent, false);
            var item = GetItem(position);
            var text = view as TextView;
            text.Gravity = GravityFlags.CenterVertical;
            text.SetText(item.ToString(), TextView.BufferType.Normal);
            var parameters = text.LayoutParameters;
            parameters.Height = position == 0 ? parameters.Height = 1 : parameters.Height = 65;
            text.LayoutParameters = parameters;

            return view;
        }
    }
}