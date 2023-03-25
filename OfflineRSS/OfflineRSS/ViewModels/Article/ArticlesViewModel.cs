using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using OfflineRSS.Models;
using OfflineRSS.Views;

namespace OfflineRSS.ViewModels
{
    public class ArticlesViewModel : BaseViewModel
    {
        public ObservableCollection<Article> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ArticlesViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Article>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Article>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Article;
                Items.Add(newItem);
                await ArticleDataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ArticleDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}