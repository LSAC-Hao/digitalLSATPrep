using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace digitalLSATPrep
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "digitalLSAT";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

#if __IOS__
			MessagingCenter.Subscribe<iOS.ItemNewViewController, Item>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as Item;
				Items.Add(_item);
				await DataStore.AddItemAsync(_item);
			});
#elif __ANDROID__
			MessagingCenter.Subscribe<Android.App.Activity, Item>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as Item;
				Items.Add(_item);
				await DataStore.AddItemAsync(_item);
			});
#else
            MessagingCenter.Subscribe<AddItems, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
#endif
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                // MessageDialog.SendMessage("Unable to load items.", "Error");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command<string> GoToDetailsCommand { get; }
        ItemDetailViewModel detailsViewModel;
    }
}
