using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32.SafeHandles;
using ShopApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Timer = System.Timers.Timer;

namespace ShopApp
{
    public partial class MainPage : ContentPage
    {
        private int elemindex;
        private Grid elemgrids;
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
                tapGesture.Tapped += ClickGestureRecognizer_OnTappedAsync;
                elem.GestureRecognizers.Add(clickRecognizer);
                elem.GestureRecognizers.Add(tapGesture);

                Object res;
                this.Resources.TryGetValue("CarouselLabelStyle", out res);
                Style labsStyle = (Style) res;
                elem.Style = labsStyle ;
                elem.FontAttributes = FontAttributes.None;
                elem.FontSize = 15;
                elem.Opacity = 0.7;
                elem.Margin = new Thickness(5);
                FilterIem.Children.Add(elem);
            }

            elemindex = 0;
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
                    delegate (ItemPair bk) { return bk.Item1.Name.Contains(fil.FilterName) || bk.Item2.Name.Contains(fil.FilterName) || bk.Item2.Categorie.Contains(fil.FilterName) || bk.Item2.Categorie.Contains(fil.FilterName); }
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
                    delegate (ItemPair bk) { return bk.Item1.Name.Contains(fil.FilterName) || bk.Item2.Name.Contains(fil.FilterName) || bk.Item2.Categorie.Contains(fil.FilterName) || bk.Item2.Categorie.Contains(fil.FilterName); }
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

        private async void ClickGestureRecognizer_OnTappedAsync(object sender, EventArgs e)
        {
            
            Label senderLabel = (Label) sender ;

            ProductList.IsRefreshing = true;
            if (senderLabel!= null)
            {
                foreach (Label VARIABLE in FilterIem.Children)
                {
                    if (senderLabel.Text == VARIABLE.Text)
                    {
                        int nbrelement = FilterIem.Children.Count;
                        double xmilieu = FilterIem.Children.ElementAt(nbrelement/2).X;
                        double ymilieux = FilterIem.Children.ElementAt(nbrelement / 2).Y;
                        VARIABLE.ScaleTo(1.1, 1500);
                        //VARIABLE.TranslateTo(xmilieu, VARIABLE.Y, 1000);
                        //FilterIem.Children.ElementAt(nbrelement / 2).TranslateTo(-VARIABLE.X, ymilieux, 1000);
                        //VARIABLE.FontSize = 0.1*(VARIABLE.FontSize) + VARIABLE.FontSize;
                        VARIABLE.Opacity = 1;
                        VARIABLE.TextColor = Color.FromHex("#F9FAFC");
                        VARIABLE.FontAttributes = FontAttributes.Bold;
                        VARIABLE.Margin = new Thickness(5,-4,5,0);
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
                //AddProgressDisplay(this, this.ActIndicator);
                //await DisplayAlert("Item Tapped", "Le Filtre : " + fil.FilterName, "OK");

            }
            else
            {
                DisplayAlert("Label", "Label Tapped", "Ok");
            }


            //await DisplayAlert("Item Tapped", "An item was tapped."+e.ToString(), "OK");

            //Deselect Item
            //((ListView)sender).SelectedItem = null;

            Timer a = new Timer(500);
            a.AutoReset = false;
            a.Start();
            a.Elapsed += A_Elapsed;
        }

        private void A_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProductList.IsRefreshing = false;
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

        public void AddProgressDisplay(ContentPage page,ActivityIndicator activ)
        {
            var content = page.Content;
            var grid = new Grid();
            grid.Children.Add(content);
            var gridProgress = new Grid { BackgroundColor = Color.FromHex("#64FFE0B2"), Padding = new Thickness(50) };
            gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridProgress.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //gridProgress.SetBinding(VisualElement.IsVisibleProperty, "IsWorking");
            activ = new ActivityIndicator
            {
                IsEnabled = true,
                IsVisible = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsRunning = true
            };
            gridProgress.Children.Add(activ ,0, 1);
            grid.Children.Add(gridProgress);
            this.elemindex = grid.Children.IndexOf(gridProgress);
            page.Content = grid;
        }
    }
}