namespace ShopApp.Models
{
    public class ShopItem
    {
        public ShopItem()
        {
            Name = "BROWNIE HIP SUN GLASSES";
            Status = "ALMOST NEW";
            Price = "770.00 SAR";
            ImageSource = "item.png";
            Categorie = "Bags";
            IsVisible = true;
        }

        public ShopItem(string nm, string st, string pr, string ct, string img = "item.png", bool vis = true)
        {
            Name = nm;
            Status = st;
            Price = pr;
            ImageSource = img;
            Categorie = ct;
            IsVisible = vis;
        }

        public ShopItem(string ct)
        {
            Name = "BROWNIE HIP SUN GLASSES";
            Status = "ALMOST NEW";
            Price = "770.00 SAR";
            ImageSource = "item.png";
            Categorie = ct;
            IsVisible = true;
        }

        public string Name { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string ImageSource { get; set; }
        public string Categorie { get; set; }
        public bool IsVisible { get; set; }
    }
}