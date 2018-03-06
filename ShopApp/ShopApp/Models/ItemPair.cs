using System;

namespace ShopApp.Models
{
    public class ItemPair : Tuple<ShopItem, ShopItem>
    {
        public ItemPair(ShopItem item1, ShopItem item2)
            : base(item1, item2 ?? CreateEmptyModel())
        {
        }

        private static ShopItem CreateEmptyModel()
        {
            return new ShopItem {IsVisible = false};
        }
    }
}