using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digitalLSATPrep
{
    public class MockDataStore : IDataStore<Item>
    {
        bool isInitialized;
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var _items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Getting familiar with", Description="Step by step demostration"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Practice runs", Description="Live digitalLSAT simulator"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Prep tests", Description="Practic prep test in digitalLSAT"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Register", Description="Register a digitalLSAT test"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "My tests", Description="Manage you digitalLSAT tests"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "My scores", Description="Your past LSAT scores"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "About", Description="Information about this app"},
            };

            foreach (Item item in _items)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
