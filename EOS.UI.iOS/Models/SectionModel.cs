using System;
using UIKit;

namespace EOS.UI.iOS.Models
{
    public class SectionModel
    {
        public bool ResetCustomization { get; set; }
        public string SectionName { get; set; }
        public string ButtonText { get; set; }
        public bool HasButton { get; set; }
        public Action SectionAction { get; set; }
        public int SectionNameTextSize { get; set; }
        public int ButtonTextTextSize { get; set; }
        public int SectionTextLetterSpacing { get; set; }
        public int ButtonTextLetterSpacing { get; set; }
        public UIFont SectionNameFont { get; set; }
        public UIFont ButtonNameFont { get; set; }
        public UIColor SectionNameColor { get; set; }
        public UIColor ButtonNameColor { get; set; }
        public UIColor BackgroundColor { get; set; }
        public bool HasBorder { get; set; }
        public UIColor BorderColor { get; set; }
        public int BorderWidth { get; set; }
        public int TopPadding { get; set; }
        public int BottonPadding { get; set; }
        public int RightPadding { get; set; }
        public int LeftPadding { get; set; }
    }
}