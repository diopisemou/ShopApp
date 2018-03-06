using System;
using System.Collections.Generic;
using System.Linq;
using ShopApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShopApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //Create mock data
            ShopItems = new List<ItemPair>
            {
                new ItemPair(new ShopItem("Bags"), new ShopItem("Accesories")),
                new ItemPair(new ShopItem("Accesories"), new ShopItem("Travel")),
                new ItemPair(new ShopItem("Travel"), new ShopItem("Bags"))
            };

            
            FilterList = new List<Filter>
            {
                new Filter("Bags", 0),
                new Filter("Accesories", 1),
                new Filter("Shoes", 2),
                new Filter("Travel", 3),
                new Filter("Other", 4),
                new Filter("Earings", 5),
                new Filter("Dress", 6)

            };

            //ProductFilter.ItemsSource = FilterList;
            ProductList.ItemsSource = ShopItems; // Binding ListView to Products List
            foreach (var filter in FilterList)
            {
                Label elem = new Label();
                elem.Text = filter.FilterName;
                ClickGestureRecognizer  clickRecognizer =  new ClickGestureRecognizer();
                clickRecognizer.Clicked += ClickGestureRecognizer_OnClicked;
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += ClickGestureRecognizer_OnTapped;
                elem.GestureRecognizers.Add(clickRecognizer);
                elem.GestureRecognizers.Add(tapGesture);

                Object res;
                this.Resources.TryGetValue("CarouselLabelStyle", out res);
                Style labsStyle = (Style) res;
                elem.Style = labsStyle ;
                FilterIem.Children.Add(elem);
            }
        }

        public List<ItemPair> ShopItems { get; set; }
        public List<Filter> FilterList { get; set; }

        private void TapGestureRecognizer_OnTappedHome(object sender, EventArgs e)
        {
            DisplayAlert("Home", "Home Button Tapped", "Ok");
        }

        private void TapGestureRecognizer_OnTappedSearch(object sender, EventArgs e)
        {
            DisplayAlert("Search", "Search Button Tapped", "Ok");
        }

        private void TapGestureRecognizer_OnTappedMenu(object sender, EventArgs e)
        {
            DisplayAlert("Menu", "Menu Button Tapped", "Ok");
        }

        private void TapGestureRecognizer_OnTappedCart(object sender, EventArgs e)
        {
            DisplayAlert("Cart", "Cart Button Tapped", "Ok");
        }

        private void TapGestureRecognizer_OnTappedAccount(object sender, EventArgs e)
        {
            DisplayAlert("Account", "Account Button Tapped", "Ok");
        }

        private void TapGestureRecognizer_OnTappedFilter(object sender, EventArgs e)
        {
            DisplayAlert("Filter", "Filter Button Tapped", "Ok");
        }

       

        private async void ProductFilter_OnItemTappedAsync(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            (sender as ListView).SelectedItem = null;

            if (e.Item != null)
            {
                var fil = e.Item as Filter;
                List<ItemPair> results = ShopItems.FindAll(
                    delegate(ItemPair bk) { return bk.Item1.Categorie.Contains(fil.FilterName); }
                );
                if (results.Count != 0)
                {
                    ProductList.ItemsSource = results;
                }
                else
                {
                    List<ItemPair> noresults = new List<ItemPair> {
                        new ItemPair(new ShopItem {Name = "No Results Found" , Status =" " , Price = " "  , ImageSource = " " },null)
                    };

                    ProductList.ItemsSource = noresults;

                }

                //await DisplayAlert("Item Tapped", "Le Filtre : " + fil.FilterName, "OK");
            }

            //await DisplayAlert("Item Tapped", "An item was tapped."+e.ToString(), "OK");

            //Deselect Item
            ((ListView) sender).SelectedItem = null;
        }

        private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(SearchBar_Text.Text))
            {
                ProductList.ItemsSource = ShopItems;
            }
            else
            {
                var fil = new Filter(SearchBar_Text.Text, 0);
                List<ItemPair> results = ShopItems.FindAll(
                    delegate (ItemPair bk) { return bk.Item1.Categorie.Contains(fil.FilterName) || bk.Item2.Categorie.Contains(fil.FilterName); }
                );
                if (results.Count != 0)
                {
                    ProductList.ItemsSource = results;
                }
                else
                {
                    List<ItemPair> noresults = new List<ItemPair> {
                        new ItemPair(new ShopItem {Name = "No Results Found" , Status =" " , Price = " "  , ImageSource = " " },null)
                    };

                    ProductList.ItemsSource = noresults;

                }
            }
        }

        private void SearchBar_Text_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBar_Text.Text))
            {
                ProductList.ItemsSource = ShopItems;
            }
            else
            {
                var fil = new Filter(SearchBar_Text.Text, 0);
                List<ItemPair> results = ShopItems.FindAll(
                    delegate (ItemPair bk) { return bk.Item1.Categorie.Contains(fil.FilterName) || bk.Item2.Categorie.Contains(fil.FilterName); }
                );
                if (results.Count != 0)
                {
                    ProductList.ItemsSource = results;
                }
                else
                {
                    List<ItemPair> noresults = new List<ItemPair> {
                        new ItemPair(new ShopItem {Name = "No Results Found" , Status =" " , Price = " "  , ImageSource = " " },null)
                    };

                    ProductList.ItemsSource = noresults;

                }
            }
        }

        public class Filter
        {
            public Filter()
            {
            }

            public Filter(string ft, int cid)
            {
                FilterName = ft;
                ColumnID = cid;
            }

            public string FilterName { get; set; }
            public int ColumnID { get; set; }
        }

        private void ClickGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            
            

            Label senderLabel = (Label) sender ;

            if (senderLabel!= null)
            {
                foreach (Label VARIABLE in FilterIem.Children)
                {
                    if (senderLabel.Text == VARIABLE.Text)
                    {
                        VARIABLE.FontSize = 0.1*(VARIABLE.FontSize) + VARIABLE.FontSize;
                        VARIABLE.Opacity = 1;
                        VARIABLE.TextColor = Color.FromHex("#F9FAFC");
                        VARIABLE.FontAttributes = FontAttributes.Bold;
                        VARIABLE.Margin = new Thickness(0,-4,0,0);
                    }
                    else
                    {
                        VARIABLE.FontAttributes = FontAttributes.None;
                        VARIABLE.FontSize = 15;
                        VARIABLE.Opacity = 0.7;
                        VARIABLE.Margin = new Thickness(5);
                    }
                }

                var fil = new Filter(senderLabel.Text,0);
                List<ItemPair> results = ShopItems.FindAll(
                    delegate (ItemPair bk) { return bk.Item1.Categorie.Contains(fil.FilterName); }
                );
                if (results.Count != 0)
                {
                    ProductList.ItemsSource = results;
                }
                else
                {
                    List<ItemPair> noresults = new List<ItemPair> {
                        new ItemPair(new ShopItem {Name = "No Results Found" , Status =" " , Price = " "  , ImageSource = " " },null)
                    };

                    ProductList.ItemsSource = noresults;

                }

                //await DisplayAlert("Item Tapped", "Le Filtre : " + fil.FilterName, "OK");
            }
            else
            {
                DisplayAlert("Label", "Label Tapped", "Ok");
            }

            //await DisplayAlert("Item Tapped", "An item was tapped."+e.ToString(), "OK");

            //Deselect Item
            //((ListView)sender).SelectedItem = null;

            //DisplayAlert("Label", "Label Tapped", "Ok");
        }

        private void ClickGestureRecognizer_OnClicked(object sender, EventArgs e)
        {
            DisplayAlert("Label", "Label Clicked", "Ok");
        }

       

        private void FrameTapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Frame senderFrame = (Frame)sender;
            ShopItem shopi = new ShopItem();
            //ItemPair bk = senderFrame.BindingContext as ItemPair;
            shopi = senderFrame.BindingContext as ShopItem;
            DisplayAlert("Frame Tapped ", "Product Name : " + shopi.Name + " Product Category : " + shopi.Categorie, "Ok");
        }
    }
}