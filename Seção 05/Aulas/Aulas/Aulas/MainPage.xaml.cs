using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Aulas.Servico.Modelo;
using Aulas.Servico;

namespace Aulas
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {

            //To do - Validações

            string cep = CEP.Text.Trim();
            if(Validacao(cep))
            {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                try
                {
                    RESULTADO.Text = string.Format("Endereço: {0}, {1}, {2}, {3}", end.logradouro, end.bairro, end.localidade, end.uf);
                }
                catch
                {
                    DisplayAlert("ERRO CRÍTICO!", "Verifique sua conexao com a internet e tente novamente.", "Ok");
                }
            }
        }

        private bool Validacao(string cep)
        {
            bool validade = true;
            if (cep.Length != 8)
            {
                DisplayAlert("ERRO!", "O CEP deve conter 8 digitos!", "Ok");
                validade = false;
            }
            int novoCep = 0;
            if (!int.TryParse(cep, out novoCep))
            {
                DisplayAlert("ERRO!", "O CEP deve conter apenas números", "Ok");
                validade = false;
            }
            if(ViaCEPServico.BuscarEnderecoViaCEP(cep).logradouro == null)
            {
                DisplayAlert("ERRO!", "CEP não encontrado", "Ok");
                validade = false;
            }


            return validade;
        }
	}
}
