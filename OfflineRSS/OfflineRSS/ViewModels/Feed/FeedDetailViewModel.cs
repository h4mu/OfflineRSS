using System;

using OfflineRSS.Models;

namespace OfflineRSS.ViewModels
{
    public class FeedDetailViewModel : BaseViewModel
    {
        public Feed Item { get; set; }
        public FeedDetailViewModel(Feed item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
