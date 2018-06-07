using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Components;
using EOS.UI.Shared.Themes.DataModels;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.CollectionViewSources
{
    public class WorkTimeCalendarCollectionSource: UICollectionViewSource
    {
        //by default longest string fills 70% of the cell width
        private readonly nfloat cellWidthRatio = 1.4f;
        private IEnumerable<WorkTimeCalendarItem> _dataSource;
        private UICollectionView _collectionView;
        
        public WorkTimeCalendarCollectionSource(IEnumerable<WorkTimeCalendarItem> dataSource, UICollectionView collectionView)
        {
            if (dataSource.Count() != 7)
                throw new Exception("datasource must contain 7 week days");

            _dataSource = dataSource;
            _collectionView = collectionView;
            _collectionView.RegisterNibForCell(WorkTimeCalendarCell.Nib, WorkTimeCalendarCell.Key);
            //_collectionView.PagingEnabled = true;
            InitFlowLayout();
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _dataSource.Count();
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (WorkTimeCalendarCell) collectionView.DequeueReusableCell(WorkTimeCalendarCell.Key, indexPath);
            cell.SetFont(UIFont.SystemFontOfSize(37));
            return cell;
        }
        
        private void InitFlowLayout()
        {
            var layout = new WorkTimeCalendarFlowLayout();
            layout.ItemSize = CalculateCellSize();
            //layout.ItemSize = new CGSize(_collectionView.Frame.Width/_dataSource.Count(), _collectionView.Frame.Height);
            _collectionView.CollectionViewLayout = layout;
        }
        
        private CGSize CalculateCellSize()
        {
            nfloat requiredWidth = 0;
            foreach(WorkTimeCalendarItem item in _dataSource)
            {
                var timeString = item.StartTime.ToString(@"hh\:mm");
                var nsString = new NSString(timeString);
                var size = nsString.StringSize(UIFont.SystemFontOfSize(37));
                if (size.Width > requiredWidth)
                    requiredWidth = size.Width;
            }
            requiredWidth *= cellWidthRatio;
            return new CGSize(requiredWidth, _collectionView.Frame.Height);
        }
    }
}
