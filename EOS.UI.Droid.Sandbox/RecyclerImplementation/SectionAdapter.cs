using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using EOS.UI.Droid.Components;
using EOS.UI.Droid.Models;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Droid.Sandbox.RecyclerImplementation
{
    public class SectionAdapter : RecyclerView.Adapter
    {
        private List<object> _itemSource = new List<object>();
        public List<object> Headers => _itemSource.FindAll(item => item is SectionModel);

        public void ResetCustomizatin(SectionModel defaultModel)
        {
            if(Headers.FirstOrDefault() is SectionModel section)
            {
                section.CopyData(defaultModel);
                section.ResetCustomization = true;
                NotifyDataSetChanged();
            }
        }

        public SectionAdapter(List<object> itemSource)
        {
            if(itemSource != null)
                _itemSource = itemSource;
        }

        public override int ItemCount => _itemSource.Count;

        public override int GetItemViewType(int position)
        {
            return _itemSource[position] is SectionModel ? 0 : 1;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is Section section)
            {
                var sectionModel = _itemSource[position] as SectionModel;

                if(sectionModel.ResetCustomization)
                {
                    section.ResetCustomization();
                    sectionModel.ResetCustomization = false;
                }
                else
                {
                    section.HasBorder = sectionModel.HasBorder;
                    section.HasButton = sectionModel.HasButton;
                    section.SectionAction = sectionModel.SectionAction;

                    if(!string.IsNullOrEmpty(sectionModel.SectionName))
                        section.SectionTitleText = sectionModel.SectionName;

                    if(!string.IsNullOrEmpty(sectionModel.ButtonText))
                        section.ButtonText = sectionModel.ButtonText;

                    if(sectionModel.SectionNameTextSize != 0)
                        section.SectionTextSize = sectionModel.SectionNameTextSize;

                    if(sectionModel.ButtonTextTextSize != 0)
                        section.ButtonTextSize = sectionModel.ButtonTextTextSize;

                    if(sectionModel.SectionTextLetterSpacing != 0)
                        section.SectionTextLetterSpacing = sectionModel.SectionTextLetterSpacing;

                    if(sectionModel.ButtonTextLetterSpacing != 0)
                        section.ButtonTextLetterSpacing = sectionModel.ButtonTextLetterSpacing;

                    if(sectionModel.SectionNameFont != null)
                        section.SectionTextFont = sectionModel.SectionNameFont;

                    if(sectionModel.ButtonNameFont != null)
                        section.ButtonTextFont = sectionModel.ButtonNameFont;

                    if(sectionModel.BackgroundColor != Color.Transparent)
                        section.BackgroundColor = sectionModel.BackgroundColor;

                    if(sectionModel.BorderWidth != 0)
                        section.BorderWidth = sectionModel.BorderWidth;

                    if(sectionModel.BorderColor != Color.Transparent)
                        section.BorderColor = sectionModel.BorderColor;

                    if(sectionModel.SectionNameColor != Color.Transparent)
                        section.SectionTextColor = sectionModel.SectionNameColor;

                    if(sectionModel.ButtonNameColor != Color.Transparent)
                        section.ButtonTextColor = sectionModel.ButtonNameColor;

                    section.SetPaddings(sectionModel.LeftPadding, sectionModel.TopPadding, sectionModel.RightPadding, sectionModel.BottonPadding);
                }
            }
            else
            {
                (holder as SimpleViewHolder).TitleView.Text = _itemSource[position] as string;
                (holder as SimpleViewHolder).Layout.SetBackgroundColor(EOSThemeProvider.Instance.GetEOSProperty<Color>(EOSConstants.NeutralColor6));
                (holder as SimpleViewHolder).TitleView.SetTextColor(EOSThemeProvider.Instance.GetEOSProperty<Color>(EOSConstants.NeutralColor1));
                (holder as SimpleViewHolder).Devider.Visibility = position < 3 ? ViewStates.Visible : ViewStates.Invisible;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if(viewType == 0)
            {
                return Section.Create(parent);
            }
            else
            {
                var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SimpleCell, parent, false);
                var viewHolder = new SimpleViewHolder(itemView);
                return viewHolder;
            }
        }
    }
}
