using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfflineRSS.Models;

namespace OfflineRSS.Services
{
    public sealed class FeedDataStore : IDataStore<Feed>
    {
        public async Task<bool> AddItemAsync(Feed item)
        {
            using (var ctx = new StorageContext())
            {
                await ctx.Feeds.AddAsync(item);
                await ctx.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Feed item)
        {
            using (var ctx = new StorageContext())
            {
                var oldItem = ctx.Feeds.Where(arg => arg.Id == item.Id).FirstOrDefault();
                ctx.Feeds.Remove(oldItem);
                await ctx.Feeds.AddAsync(item);
                await ctx.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var ctx = new StorageContext())
            {
                var oldItem = ctx.Feeds.Where(arg => arg.Id == id).FirstOrDefault();
                ctx.Feeds.Remove(oldItem);
                await ctx.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<Feed> GetItemAsync(int id)
        {
            using (var ctx = new StorageContext())
            {
                return await Task.FromResult(ctx.Feeds.Where(arg => arg.Id == id).FirstOrDefault());
            }
        }

        public async Task<IEnumerable<Feed>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var ctx = new StorageContext())
            {
                return await Task.FromResult(ctx.Feeds.ToArray());
            }
        }
    }
}