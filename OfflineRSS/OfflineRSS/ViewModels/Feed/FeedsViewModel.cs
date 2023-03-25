using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using OfflineRSS.Models;
using OfflineRSS.Views;

namespace OfflineRSS.ViewModels
{
    public class FeedsViewModel : BaseViewModel
    {
        public ObservableCollection<Feed> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public FeedsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Feed>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Feed>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Feed;
                Items.Add(newItem);
                await FeedDataStore.AddItemAsync(newItem);
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
                var items = await FeedDataStore.GetItemsAsync(true);
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