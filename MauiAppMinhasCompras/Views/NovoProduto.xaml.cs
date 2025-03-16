using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
			descricao = txt_descricao.Text,
			Quantidade = Convert.ToDouble(txt_quantidade.Text),
			Preco = Convert.ToDouble(txt_preco.Text)
			};

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "Ok");

		} catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
		}

    }
}