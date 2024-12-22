using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services;

namespace WeatherApp.Views
{
	public partial class ImprovedForm : Form
	{
		public ImprovedForm()
		{
			InitializeComponent();
		}

		private async void ImprovedForm_Load(object sender, EventArgs e)
		{
			// Cria uma instância do repositório do banco de dados para armazenar os dados
			WeatherDatabase database = new WeatherDatabase();

			// Cria uma instância da classe WeatherApiClient para buscar os dados da API
			WeatherApiClient apiClient = new WeatherApiClient();

			WeatherData weatherData = null;

			try
			{
				// Tenta obter os dados de previsão do tempo
				weatherData = await apiClient.GetWeatherDataAsync();
			}
			catch (Exception ex)
			{
				// Caso ocorra algum erro ao obter os dados, exibe uma mensagem de erro
				MessageBox.Show($"Erro ao obter os dados da API: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Exibe informações básicas
			lblCity.Text = weatherData.results.city;
			lblTemp.Text = $"{weatherData.results.temp} °C";
			lblDescription.Text = weatherData.results.description;

			// Exibe o ícone correspondente
			UpdateWeatherIcon(weatherData.results.condition_slug);

			// Consulta os dados existentes no banco de dados
			var databaseForecasts = database.GetAllWeatherDataFromDatabase();
		}

		/// <summary>
		/// Atualiza o ícone do estado do tempo com base no atributo condition_slug.
		/// </summary>
		/// <param name="conditionSlug">O valor do atributo condition_slug da API.</param>
		private void UpdateWeatherIcon(string conditionSlug)
		{
			// Obtém o caminho absoluto da pasta raiz do projeto
			string iconsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons");

			// Forma o caminho completo para o ícone baseado no condition_slug
			string iconFileName = $"{conditionSlug}.svg";
			string iconFilePath = Path.Combine(iconsPath, iconFileName);

			// Verifica se o arquivo existe e carrega o ícone
			if (File.Exists(iconFilePath))
			{
				try
				{
					// Carrega o documento SVG
					SvgDocument svgDocument = SvgDocument.Open(iconFilePath);

					// Converte para Bitmap e exibe no lblIcon
					Bitmap bitmap = svgDocument.Draw();
					lblIcon.Image = bitmap;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Erro ao carregar o ícone '{iconFileName}': {ex.Message}",
									"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show($"O ícone para o estado '{conditionSlug}' não foi encontrado no caminho: {iconFilePath}",
								"Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}