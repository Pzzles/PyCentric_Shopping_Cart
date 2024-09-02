using PyCentric_Shopping_Cart.Models;

namespace PyCentric_Shopping_Cart.Service
{
    public class ShoppingCartService
    {
        public List<Product> Items { get; set; } = new List<Product>();

        public void AddItem(Product item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void RemoveItem(int itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

    }
}
