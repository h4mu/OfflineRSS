using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfflineRSS.Models;

namespace OfflineRSS.Services
{
    public sealed class ArticleDataStore : IDataStore<Article>
    {
        public async Task<bool> AddItemAsync(Article item)
        {
            using (var ctx = new StorageContext())
            {
                await ctx.Articles.AddAsync(item);
                await ctx.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Article item)
        {
            using (var ctx = new StorageContext())
            {
                var oldItem = ctx.Articles.Where(arg => arg.Id == item.Id).FirstOrDefault();
                ctx.Articles.Remove(oldItem);
                await ctx.Articles.AddAsync(item);
                await ctx.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var ctx = new StorageContext())
            {
                var oldItem = ctx.Articles.Where(arg => arg.Id == id).FirstOrDefault();
                ctx.Articles.Remove(oldItem);
                await ctx.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<Article> GetItemAsync(int id)
        {
            using (var ctx = new StorageContext())
            {
                return await Task.FromResult(ctx.Articles.Where(arg => arg.Id == id).FirstOrDefault());
            }
        }

        public async Task<IEnumerable<Article>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var ctx = new StorageContext())
            {
                return await Task.FromResult(ctx.Articles.ToArray());
            }
        }
    }
}