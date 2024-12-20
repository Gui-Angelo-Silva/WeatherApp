using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services;

namespace WeatherApp
{
	/// <summary>
	/// Representa o formul�rio principal do aplicativo, que exibe informa��es sobre a previs�o do tempo,
	/// interage com a API para obter dados clim�ticos e gerencia a persist�ncia desses dados no banco de dados.
	/// </summary>
	public partial class WeatherForm : Form
	{
		/// <summary>
		/// Construtor da classe <see cref="WeatherForm"/>.
		/// Inicializa os componentes do formul�rio.
		/// </summary>
		public WeatherForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Manipulador de evento acionado quando o formul�rio � carregado.
		/// Este m�todo obt�m os dados de previs�o do tempo da API, exibe as informa��es no formul�rio e salva os dados no banco de dados.
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento.</param>
		/// <param name="e">Os dados do evento.</param>
		/// <returns>Retorna nada (void).</returns>
		private async void WeatherForm_Load(object sender, EventArgs e)
		{
			// Cria uma inst�ncia do reposit�rio do banco de dados para armazenar os dados
			WeatherDatabase database = new WeatherDatabase();

			// Cria uma inst�ncia da classe WeatherApiClient para buscar os dados da API
			WeatherApiClient apiClient = new WeatherApiClient();

			WeatherData weatherData = null;

			try
			{
				// Tenta obter os dados de previs�o do tempo
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

			// Verifica se j� existem dados no banco de dados para exibir.
			// Se houver dados no banco, exibe-os na DataGridView.
			// Caso contr�rio, exibe os dados obtidos da API, que podem ser mais recentes.
			if (databaseForecasts.Count > 0)
            {
				dgvForecast.DataSource = databaseForecasts;
            }
			else
			{
				dgvForecast.DataSource = weatherData.results.forecast;
			}

			// Exibe a cidade e a descri��o do clima atual
			lblCity.Text = $"Cidade: {weatherData.results.city}";
			lblDescription.Text = $"Descri��o: {weatherData.results.forecast[0].description}";
			lblTemperature.Text = $"Temperatura atual: {weatherData.results.temp} graus";

			// Configura os cabe�alhos das colunas da DataGridView
			ConfigureDataGridViewHeaders();

			// Salva os dados da previs�o do tempo no banco de dados
			database.SaveWeatherDataToDatabase(weatherData);
		}

		/// <summary>
		/// Configura os cabe�alhos das colunas da <see cref="DataGridView"/> para exibir as informa��es da previs�o do tempo.
		/// </summary>
		/// <remarks>
		/// Este m�todo define os t�tulos das colunas para melhor legibilidade na interface do usu�rio.
		/// </remarks>
		/// <returns>Retorna nada (void).</returns>
		private void ConfigureDataGridViewHeaders()
		{
			dgvForecast.Columns[0].HeaderText = "Data";
			dgvForecast.Columns[1].HeaderText = "Dia da Semana";
			dgvForecast.Columns[2].HeaderText = "M�xima (�C)";
			dgvForecast.Columns[3].HeaderText = "M�nima (�C)";
			dgvForecast.Columns[4].HeaderText = "Humidade (%)";
			dgvForecast.Columns[5].HeaderText = "Chuva (mm)";
			dgvForecast.Columns[6].HeaderText = "Descri��o";
			dgvForecast.Columns[7].HeaderText = "Mudan�a de Temperatura";
		}
	}
}