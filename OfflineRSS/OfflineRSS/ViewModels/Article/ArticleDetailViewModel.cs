using System;

using OfflineRSS.Models;

namespace OfflineRSS.ViewModels
{
    public class ArticleDetailViewModel : BaseViewModel
    {
        public Article Item { get; set; }
        public ArticleDetailViewModel(Article item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
