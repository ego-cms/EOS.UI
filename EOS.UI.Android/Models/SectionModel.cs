using System;
using Android.Graphics;

namespace EOS.UI.Android.Models
{
    public class SectionModel
    {
        public bool ResetCustomization { get; set; }
        public string SectionName { get; set; }
        public string ButtonText { get; set; }
        public bool HasButton { get; set; }
        public Action SectionAction { get; set; }
        public float SectionNameTextSize { get; set; }
        public float ButtonTextTextSize { get; set; }
        public float SectionTextLetterSpacing { get; set; }
        public float ButtonTextLetterSpacing { get; set; }
        public Typeface SectionNameFont { get; set; }
        public Typeface ButtonNameFont { get; set; }
        public Color SectionNameColor { get; set; }
        public Color ButtonNameColor { get; set; }
        public Color BackgroundColor { get; set; }
        public bool HasBorder { get; set; }
        public Color BorderColor { get; set; }
        public int BorderWidth { get; set; }
        public int TopPadding { get; set; }
        public int BottonPadding { get; set; }
        public int RightPadding { get; set; }
        public int LeftPadding { get; set; }

        public void CopyData(SectionModel model)
        {
            ResetCustomization = model.ResetCustomization;
            SectionName = model.SectionName;
            ButtonText = model.ButtonText;
            HasButton = model.HasButton;
            SectionAction = model.SectionAction;
            SectionNameTextSize = model.SectionNameTextSize;
            ButtonTextTextSize = model.ButtonTextTextSize;
            SectionTextLetterSpacing = model.SectionTextLetterSpacing;
            ButtonTextLetterSpacing = model.ButtonTextLetterSpacing;
            SectionNameFont = model.SectionNameFont;
            ButtonNameFont = model.ButtonNameFont;
            SectionNameColor = model.SectionNameColor;
            ButtonNameColor = model.ButtonNameColor;
            BackgroundColor = model.BackgroundColor;
            HasBorder = model.HasBorder;
            BorderColor = model.BorderColor;
            BorderWidth = model.BorderWidth;
            TopPadding = model.TopPadding;
            BottonPadding = model.BottonPadding;
            RightPadding = model.RightPadding;
            LeftPadding = model.LeftPadding;
        }
    }
}