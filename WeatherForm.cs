using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services;

namespace WeatherApp
{
	/// <summary>
	/// Representa o formulário principal do aplicativo, que exibe informações sobre a previsão do tempo,
	/// interage com a API para obter dados climáticos e gerencia a persistência desses dados no banco de dados.
	/// </summary>
	public partial class WeatherForm : Form
	{
		/// <summary>
		/// Construtor da classe <see cref="WeatherForm"/>.
		/// Inicializa os componentes do formulário.
		/// </summary>
		public WeatherForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Manipulador de evento acionado quando o formulário é carregado.
		/// Este método obtém os dados de previsão do tempo da API, exibe as informações no formulário e salva os dados no banco de dados.
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento.</param>
		/// <param name="e">Os dados do evento.</param>
		/// <returns>Retorna nada (void).</returns>
		private async void WeatherForm_Load(object sender, EventArgs e)
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
			
			// Consulta os dados existentes no banco de dados
			var databaseForecasts = database.GetAllWeatherDataFromDatabase();

			// Verifica se já existem dados no banco de dados para exibir.
			// Se houver dados no banco, exibe-os na DataGridView.
			// Caso contrário, exibe os dados obtidos da API, que podem ser mais recentes.
			if (databaseForecasts.Count > 0)
            {
				dgvForecast.DataSource = databaseForecasts;
            }
			else
			{
				dgvForecast.DataSource = weatherData.results.forecast;
			}

			// Exibe a cidade e a descrição do clima atual
			lblCity.Text = $"Cidade: {weatherData.results.city}";
			lblDescription.Text = $"Descrição: {weatherData.results.forecast[0].description}";
			lblTemperature.Text = $"Temperatura atual: {weatherData.results.temp} graus";

			// Configura os cabeçalhos das colunas da DataGridView
			ConfigureDataGridViewHeaders();

			// Salva os dados da previsão do tempo no banco de dados
			database.SaveWeatherDataToDatabase(weatherData);
		}

		/// <summary>
		/// Configura os cabeçalhos das colunas da <see cref="DataGridView"/> para exibir as informações da previsão do tempo.
		/// </summary>
		/// <remarks>
		/// Este método define os títulos das colunas para melhor legibilidade na interface do usuário.
		/// </remarks>
		/// <returns>Retorna nada (void).</returns>
		private void ConfigureDataGridViewHeaders()
		{
			dgvForecast.Columns[0].HeaderText = "Data";
			dgvForecast.Columns[1].HeaderText = "Dia da Semana";
			dgvForecast.Columns[2].HeaderText = "Máxima (°C)";
			dgvForecast.Columns[3].HeaderText = "Mínima (°C)";
			dgvForecast.Columns[4].HeaderText = "Humidade (%)";
			dgvForecast.Columns[5].HeaderText = "Chuva (mm)";
			dgvForecast.Columns[6].HeaderText = "Descrição";
			dgvForecast.Columns[7].HeaderText = "Mudança de Temperatura";
		}
	}
}