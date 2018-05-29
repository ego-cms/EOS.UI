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
    }
}