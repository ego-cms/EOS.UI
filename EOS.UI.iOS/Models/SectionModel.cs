using System;
using UIKit;

namespace EOS.UI.iOS.Models
{
    public class SectionModel
    {
        public EventHandler OnPropertyChanged { get; set; }
        public Action SectionAction { get; set; }

        private bool _resetCustomization;
        public bool ResetCustomization
        {
            get => _resetCustomization;
            set
            {
                _resetCustomization = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private string _sectionName;
        public string SectionName
        {
            get => _sectionName;
            set
            {
                _sectionName = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private string _buttonText;
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool _hasButton;
        public bool HasButton
        {
            get => _hasButton;
            set
            {
                _hasButton = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool _hasBorder;
        public bool HasBorder
        {
            get => _hasBorder;
            set
            {
                _hasBorder = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private float _sectionNameTextSize;
        public float SectionNameTextSize
        {
            get => _sectionNameTextSize;
            set
            {
                _sectionNameTextSize = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private float _buttonTextSize;
        public float ButtonTextSize
        {
            get => _buttonTextSize;
            set
            {
                _buttonTextSize = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private float _sectionTextLetterSpacing;
        public float SectionTextLetterSpacing
        {
            get => _sectionTextLetterSpacing;
            set
            {
                _sectionTextLetterSpacing = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private float _buttonTextLetterSpacing;
        public float ButtonTextLetterSpacing
        {
            get => _buttonTextLetterSpacing;
            set
            {
                _buttonTextLetterSpacing = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIFont _sectionNameFont;
        public UIFont SectionNameFont
        {
            get => _sectionNameFont;
            set
            {
                _sectionNameFont = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIFont _buttonNameFont;
        public UIFont ButtonNameFont
        {
            get => _buttonNameFont;
            set
            {
                _buttonNameFont = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _sectionNameColor;
        public UIColor SectionNameColor
        {
            get => _sectionNameColor;
            set
            {
                _sectionNameColor = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _buttonNameColor;
        public UIColor ButtonNameColor
        {
            get => _buttonNameColor;
            set
            {
                _buttonNameColor = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private UIColor _backgroundColor;
        public UIColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        private UIColor _borderColor;
        public UIColor BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _borderWidth;
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _topPadding = 10;
        public int TopPadding
        {
            get => _topPadding;
            set
            {
                _topPadding = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _bottomPadding = 10;
        public int BottonPadding
        {
            get => _bottomPadding;
            set
            {
                _bottomPadding = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _rightPadding = 16;
        public int RightPadding
        {
            get => _rightPadding;
            set
            {
                _rightPadding = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _leftPadding = 16;
        public int LeftPadding
        {
            get => _leftPadding;
            set
            {
                _leftPadding = value;
                OnPropertyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void CopyData(SectionModel model)
        {
            ResetCustomization = model.ResetCustomization;
            SectionName = model.SectionName;
            ButtonText = model.ButtonText;
            HasButton = model.HasButton;
            SectionAction = model.SectionAction;
            SectionNameTextSize = model.SectionNameTextSize;
            ButtonTextSize = model.ButtonTextSize;
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