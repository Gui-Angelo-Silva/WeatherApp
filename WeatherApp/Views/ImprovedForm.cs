﻿using Svg;
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
	/// <summary>
	/// Representa o formulário melhorado do aplicativo, que exibe informações detalhadas sobre a previsão do tempo.
	/// Este formulário carrega dados da API e do banco de dados, e exibe cards de previsão do tempo com base nas informações obtidas.
	/// </summary>
	public partial class ImprovedForm : Form
	{
		/// <summary>
		/// Construtor da classe <see cref="ImprovedForm"/>.
		/// Inicializa os componentes do formulário.
		/// </summary>
		public ImprovedForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Manipulador de evento acionado quando o formulário é carregado.
		/// Este método é responsável por obter os dados da API, filtrar os dados da previsão do tempo com base na data e exibir os resultados no formulário.
		/// Ele também salva os dados obtidos no banco de dados e atualiza os cards exibidos.
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento (neste caso, o formulário).</param>
		/// <param name="e">Os dados do evento de carregamento do formulário.</param>
		/// <remarks>
		/// Este método realiza várias ações:
		/// 1. Cria instâncias de objetos para acessar a API e o banco de dados.
		/// 2. Obtém os dados da previsão do tempo da API.
		/// 3. Exibe as informações básicas da previsão do tempo no formulário, como cidade, temperatura, umidade e máxima/mínima.
		/// 4. Filtra os dados de acordo com a data atual e exibe cards com informações atualizadas.
		/// 5. Atualiza a interface para garantir que o card do dia atual seja exibido com a palavra "Hoje" no campo weekday.
		/// 6. Exibe as informações de previsão do tempo filtradas no formulário e salva os dados no banco de dados.
		/// </remarks>
		private async void ImprovedForm_Load(object sender, EventArgs e)
		{
			// Cria uma instância do repositório do banco de dados para armazenar os dados
			WeatherDatabase database = new WeatherDatabase();

			// Cria uma instância da classe WeatherApiClient para buscar os dados da API
			WeatherApiClient apiClient = new WeatherApiClient();

			WeatherData weatherData = null;

			try
			{
				// Tenta obter os dados de previsão do tempo da API
				weatherData = await apiClient.GetWeatherDataAsync();
			}
			catch (Exception ex)
			{
				// Caso ocorra algum erro ao obter os dados, exibe uma mensagem de erro
				MessageBox.Show($"Erro ao obter os dados da API: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Exibe informações básicas da API (máxima, mínima e umidade do dia atual)
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

			// Se não houver dados no banco de dados, use os dados da API diretamente
			if (forecasts.Count == 0)
				forecasts = weatherData.results.forecast.ToList();

			// Data atual do sistema (formato dd/MM)
			string currentDate = DateTime.Now.ToString("dd/MM");

			// Lista para armazenar os forecasts filtrados do banco de dados
			List<Forecast> filteredForecasts = new List<Forecast>();

			// Comparação da data atual com os dados do banco
			foreach (var forecast in forecasts)
			{
				string dbDate = forecast.date.Substring(0, 5); // Extrai apenas dd/MM da data do banco

				// Verifica se a data do banco é igual ou posterior ao dia atual
				if (string.Compare(dbDate, currentDate) >= 0)
				{
					filteredForecasts.Add(forecast);
				}
			}



			// Procura e exibe as informações da previsão do dia atual (max, min, humidade)
			var todayForecast = filteredForecasts.FirstOrDefault(f => f.date.Substring(0, 5) == currentDate);

			if (todayForecast != null)
			{
				lblMax.Text = $"{todayForecast.max} °C";
				lblMin.Text = $"{todayForecast.min} °C";
				lblHumidity.Text = $"Umidade: {todayForecast.humidity}%";
			}
			else
			{
				// Caso não tenha dados do dia atual no banco, utiliza os dados da API
				lblMax.Text = $"{weatherData.results.forecast[0].max} °C";
				lblMin.Text = $"{weatherData.results.forecast[0].min} °C";
				lblHumidity.Text = $"Umidade: {weatherData.results.forecast[0].humidity}%";
			}

			// Atualiza o campo "weekday" para "Hoje" caso o card seja para o dia atual
			foreach (var forecast in filteredForecasts)
			{
				string dbDate = forecast.date.Substring(0, 5); // Extrai apenas dd/MM da data do banco

				if (dbDate == currentDate)
				{
					forecast.weekday = "Hoje";
				}
			}

			// Exibe os cards filtrados com as previsões
			DisplayWeatherCards(filteredForecasts);

			// Salva os dados mais recentes no banco de dados
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

		/// <summary>
		/// Manipulador de evento acionado quando o usuário clica no controle de saída (lblExit).
		/// Este método oculta o formulário atual e abre uma nova instância do formulário <see cref="WeatherForm"/> de forma não modal.
		/// A interação com o formulário atual é suspensa enquanto o novo formulário está aberto.
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento (neste caso, o controle de saída <see cref="lblExit"/>).</param>
		/// <param name="e">Os dados do evento de clique no controle de saída.</param>
		/// <remarks>
		/// Este método executa as seguintes ações:
		/// 1. Cria uma nova instância do formulário <see cref="WeatherForm"/>.
		/// 2. Oculta o formulário atual, mas não o fecha.
		/// 3. Abre o novo formulário <see cref="WeatherForm"/> usando o método <see cref="Form.Show"/>.
		/// </remarks>
		private void lblExit_Click(object sender, EventArgs e)
		{
			// Cria uma nova instância do WeatherForm
			WeatherForm weatherForm = new WeatherForm();

			// Oculta o formulário atual
			this.Hide();

			// Abre o WeatherForm de maneira não modal (não bloqueia o fluxo do código)
			weatherForm.Show();
		}
	}
}