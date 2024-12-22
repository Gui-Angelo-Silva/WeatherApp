using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
			lblHumidity.Text = $"Umidade: {weatherData.results.forecast[0].humidity}%";
			lblMax.Text = $"{weatherData.results.forecast[0].max} °C";
			lblMin.Text = $"{weatherData.results.forecast[0].min} °C";

			// Exibe o ícone correspondente
			UpdateWeatherIcon(weatherData.results.condition_slug);

			// Consulta os dados existentes no banco de dados
			var forecasts = database.GetAllWeatherDataFromDatabase();

			if (forecasts.Count == 0)
				forecasts = weatherData.results.forecast.ToList();

			// Data da API no formato dd/MM/yyyy
			string apiDate = weatherData.results.date.Substring(0, 5); // Extrai apenas dd/MM

			// Lista para armazenar os cards filtrados
			List<Forecast> filteredForecasts = new List<Forecast>();

			// Comparação da data
			foreach (var forecast in forecasts)
			{
				string dbDate = forecast.date.Substring(0, 5); // Extrai apenas dd/MM da data do banco

				// Compara a data da API com a data do banco
				if (string.Compare(apiDate, dbDate) <= 0)
				{
					filteredForecasts.Add(forecast);
				}
			}

			// Exibe os cards filtrados
			DisplayWeatherCards(filteredForecasts);

			// Salva os dados mais recentes no banco
			database.SaveWeatherDataToDatabase(weatherData);
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

		private void DisplayWeatherCards(List<Forecast> forecasts)
		{
			FlowLayoutPanel flowPanel = new FlowLayoutPanel
			{
				AutoScroll = true,
				Padding = new Padding(10),
				BackColor = Color.White,
				Location = new Point(
					(this.ClientSize.Width - (this.ClientSize.Width - 20)) / 2, 
					250
				),
				Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 250),
				FlowDirection = FlowDirection.LeftToRight,
				WrapContents = false
			};

			flowPanel.HorizontalScroll.Visible = false;
			flowPanel.VerticalScroll.Visible = false;

			this.Controls.Add(flowPanel);

			foreach (var forecast in forecasts)
			{
				var card = CreateWeatherCard(forecast);
				flowPanel.Controls.Add(card);
			}
		}

		private Panel CreateWeatherCard(Forecast forecast)
		{
			Panel card = new Panel
			{
				Size = new Size(175, 100),
				Margin = new Padding(10),
				BackColor = Color.FromArgb(241, 241, 241), 
			};

			Label lblDay = new Label
			{
				Text = forecast.weekday,
				Font = new Font("Segoe UI", 12F, FontStyle.Bold),
				Dock = DockStyle.Top,
				TextAlign = ContentAlignment.MiddleCenter
			};

			Label lblMax = new Label
			{
				Text = $"Máx: {forecast.max}°C",
				Font = new Font("Segoe UI", 10F),
				Dock = DockStyle.Top,
				TextAlign = ContentAlignment.MiddleCenter
			};

			Label lblMin = new Label
			{
				Text = $"Mín: {forecast.min}°C",
				Font = new Font("Segoe UI", 10F),
				Dock = DockStyle.Top,
				TextAlign = ContentAlignment.MiddleCenter
			};

			Label lblTempChange = new Label
			{
				Text = forecast.TemperatureChange == "Sem mudança" || forecast.TemperatureChange == ""
					? $"{forecast.TemperatureChange}"
					: $"{forecast.TemperatureChange} a temperatura",
				Font = new Font("Segoe UI", 10F, FontStyle.Italic),
				Dock = DockStyle.Top,
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(105, 105, 105)
			};

			// Definindo bordas arredondadas
			int borderRadius = 15; // Tamanho do arredondamento
			GraphicsPath path = new GraphicsPath();
			path.AddArc(0, 0, borderRadius, borderRadius, 180, 90); // Canto superior esquerdo
			path.AddArc(card.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90); // Canto superior direito
			path.AddArc(card.Width - borderRadius, card.Height - borderRadius, borderRadius, borderRadius, 0, 90); // Canto inferior direito
			path.AddArc(0, card.Height - borderRadius, borderRadius, borderRadius, 90, 90); // Canto inferior esquerdo
			path.CloseAllFigures();

			// Aplica a região com bordas arredondadas
			card.Region = new Region(path);

			card.Controls.Add(lblTempChange);
			card.Controls.Add(lblMin);
			card.Controls.Add(lblMax);
			card.Controls.Add(lblDay);

			return card;
		}
	}
}