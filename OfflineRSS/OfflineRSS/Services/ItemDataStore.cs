using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfflineRSS.Models;

namespace OfflineRSS.Services
{
    public class ItemDataStore : IDataStore<Item>
    {
        public async Task<bool> AddItemAsync(Item item)
        {
            using (var ctx = new StorageContext())
            {
                await ctx.Items.AddAsync(item);
                await ctx.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            using (var ctx = new StorageContext())
            {
                var oldItem = ctx.Items.Where(arg => arg.Id == item.Id).FirstOrDefault();
                ctx.Items.Remove(oldItem);
                await ctx.Items.AddAsync(item);
                await ctx.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var ctx = new StorageContext())
            {
                var oldItem = ctx.Items.Where(arg => arg.Id == id).FirstOrDefault();
                ctx.Items.Remove(oldItem);
                await ctx.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            using (var ctx = new StorageContext())
            {
                return await Task.FromResult(ctx.Items.Where((Item arg) => arg.Id == id).FirstOrDefault());
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var ctx = new StorageContext())
            {
                return await Task.FromResult(ctx.Items.ToArray());
            }
        }
    }
}