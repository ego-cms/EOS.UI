using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Components;
using EOS.UI.iOS.Models;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.CollectionViewSources
{
    public class WorkTimeCalendarCollectionSource : UICollectionViewSource
    {
        //by default longest string fills 70% of the cell width
        private readonly nfloat _cellWidthRatio = 1.4f;
        private readonly UICollectionView _collectionView;

        public WorkTimeCalendarModel CalendarModel { get; }

        public WorkTimeCalendarCollectionSource(UICollectionView collectionView)
        {
            _collectionView = collectionView;
            _collectionView.RegisterNibForCell(WorkTimeCalendarCell.Nib, WorkTimeCalendarCell.Key);
            CalendarModel = new WorkTimeCalendarModel();
            CalendarModel.UpdateAppearance();
            CalendarModel.ItemsChanged += (sender, e) => InitFlowLayout();
        }


        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return CalendarModel.Items.Count();
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (WorkTimeCalendarCell) collectionView.DequeueReusableCell(WorkTimeCalendarCell.Key, indexPath);
            InitCell(ref cell, CalendarModel.Items.ElementAt(indexPath.Row));
            return cell;
        }

        private void InitFlowLayout()
        {
            var layout = new WorkTimeCalendarFlowLayout();
            layout.ItemSize = CalculateCellSize();
            _collectionView.CollectionViewLayout = layout;
        }

        private CGSize CalculateCellSize()
        {
            nfloat requiredWidth = 0;
            foreach (var item in CalendarModel.Items)
            {
                var endWorkTime = new NSString(item.EndTime.ToShortString());
                var dayTitle = new NSString(item.ShortWeekDay);

                var endWorkWidth = endWorkTime.StringSize(CalendarModel.DayTextFont.WithSize(CalendarModel.DayTextSize)).Width;
                var dayTitleWidth = dayTitle.StringSize(CalendarModel.TitleFont.WithSize(CalendarModel.TitleTextSize)).Width;
                var biggerWidth = Math.Max(endWorkWidth, dayTitleWidth);
                if (biggerWidth > requiredWidth)
                    requiredWidth = (nfloat) biggerWidth;
            }
            requiredWidth *= _cellWidthRatio;
            return new CGSize(requiredWidth, _collectionView.Frame.Height);
        }

        private void InitCell(ref WorkTimeCalendarCell cell, WorkTimeCalendarItem item)
        {
            cell.Init(item);
            cell.DayTextSize = CalendarModel.DayTextSize;
            cell.TitleTextSize = CalendarModel.TitleTextSize;
            cell.DayTextColor = CalendarModel.DayTextColor;
            cell.TitleColor = CalendarModel.TitleColor;
        }
    }
}