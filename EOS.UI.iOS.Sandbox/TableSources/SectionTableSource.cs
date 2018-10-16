using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Components;
using EOS.UI.iOS.Models;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Extensions;
using Foundation;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox.TableSources
{
    public class SectionTableSource : UITableViewSource
    {
        private UITableView _tableView;
        private const string _cellIdentifier = "SectionTableViewCell";
        private List<string> _source;
        private readonly List<string> _cellsText = new List<string>() { "Terms of Use", "Privacy Policy", "About Us" };
        public SectionModel SectionModel { get; set; }

        public SectionTableSource(UITableView table, List<object> dataSource)
        {
            SectionModel = dataSource.FirstOrDefault(item => item is SectionModel) as SectionModel;
            SectionModel.OnPropertyChanged += (sender, e) => _tableView.ReloadData();
            _source = dataSource.FindAll(item => item is string).Select(item => (string)item).ToList();
            _tableView = table;
            _tableView.RegisterNibForHeaderFooterViewReuse(Section.Nib, Section.Key);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(_cellIdentifier, indexPath);
            cell.Accessory = indexPath.Row == 0 ? UITableViewCellAccessory.DisclosureIndicator : UITableViewCellAccessory.None;
            cell.TextLabel.Text = _cellsText[indexPath.Row];
            cell.TextLabel.TextColor = EOSThemeProvider.Instance.GetEOSProperty<UIColor>(EOSConstants.NeutralColor1);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _source.Count;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            var header = tableView.DequeueReusableHeaderFooterView(Section.Key);
            if (header is Section sectionHeader)
            {
                return sectionHeader.IntrinsicContentSize.Height;
            }
            return UITableView.AutomaticDimension;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 44;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            _tableView.BackgroundColor = EOSThemeProvider.Instance.GetEOSProperty<UIColor>(EOSConstants.NeutralColor6);
            var header = _tableView.DequeueReusableHeaderFooterView(Section.Key);
            var customSection = (header as Section);

            customSection.Initialize();
            customSection.SectionAction = SectionModel.SectionAction;

            if(SectionModel.ResetCustomization)
            {
                customSection.ResetCustomization();
                SectionModel.ResetCustomization = false;
            }
            else
            {
                customSection.HasBorder = SectionModel.HasBorder;
                customSection.HasButton = SectionModel.HasButton;

                if(!string.IsNullOrEmpty(SectionModel.SectionName))
                    customSection.SectionName = SectionModel.SectionName;

                if(!string.IsNullOrEmpty(SectionModel.ButtonText))
                    customSection.ButtonText = SectionModel.ButtonText;

                if(SectionModel.SectionNameTextSize != 0)
                    customSection.SectionTextSize = SectionModel.SectionNameTextSize;

                if(SectionModel.ButtonTextSize != 0)
                    customSection.ButtonTextSize = SectionModel.ButtonTextSize;

                if(SectionModel.SectionTextLetterSpacing != 0)
                    customSection.SectionTextLetterSpacing = SectionModel.SectionTextLetterSpacing;

                if(SectionModel.ButtonTextLetterSpacing != 0)
                    customSection.ButtonTextLetterSpacing = SectionModel.ButtonTextLetterSpacing;

                if(SectionModel.SectionNameFont != null)
                    customSection.SectionNameFont = SectionModel.SectionNameFont;

                if(SectionModel.ButtonNameFont != null)
                    customSection.ButtonNameFont = SectionModel.ButtonNameFont;

                if(SectionModel.BackgroundColor != null)
                    customSection.BackgroundColor = SectionModel.BackgroundColor;

                if(SectionModel.BorderWidth != 0)
                    customSection.BorderWidth = SectionModel.BorderWidth;

                if(SectionModel.BorderColor != null)
                    customSection.BorderColor = SectionModel.BorderColor;

                if(SectionModel.SectionNameColor != null)
                    customSection.SectionNameColor = SectionModel.SectionNameColor;

                if(SectionModel.ButtonNameColor != null)
                    customSection.ButtonNameColor = SectionModel.ButtonNameColor;

                customSection.SetPaddings(SectionModel.LeftPadding, SectionModel.TopPadding, SectionModel.RightPadding, SectionModel.BottonPadding);
            }
            return header;
        }
    }
}
