using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    private object p;

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {

        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.Getall();

            tmp.ForEach(i => lista.Add(i));
        }

        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "Ok");

        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)

    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {

            DisplayAlert("Ops", ex.Message, "Ok");

        }
    }

    private async void txt_seach_TextChanged(object sender, TextChangedEventArgs e)
    {

        try
        { 

        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));

        }

        catch (Exception ex)
        {

         await DisplayAlert("Ops", ex.Message, "Ok");

        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is MenuItem selecionado && selecionado.BindingContext is Produto p)
            {
                bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remver {p.descricao}?", "Sim", "Não");
                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }
            }
        }

        catch (Exception ex)
        {

            await DisplayAlert("Ops", ex.Message, "Ok");

        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {

            DisplayAlert("Ops", ex.Message, "Ok");

        }
    }
}