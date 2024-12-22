using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services;
using WeatherApp.Views;

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
		/// Inicializa os componentes do formulário e prepara o ambiente para exibição dos dados de previsão do tempo.
		/// </summary>
		public WeatherForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Manipulador de evento acionado quando o formulário é carregado.
		/// Este método obtém os dados de previsão do tempo da API, exibe as informações no formulário e salva os dados no banco de dados.
		/// 
		/// O fluxo é:
		/// - Obter os dados da API.
		/// - Se bem-sucedido, salvar os dados no banco de dados.
		/// - Exibir os dados da API ou do banco de dados (se os dados no banco forem mais antigos ou estiverem presentes).
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento, normalmente o próprio formulário.</param>
		/// <param name="e">Os dados do evento que contêm informações sobre o carregamento do formulário.</param>
		/// <returns>Retorna nada (void).</returns>
		private async void WeatherForm_Load(object sender, EventArgs e)
		{
			// Cria uma instância do repositório do banco de dados para armazenar os dados.
			WeatherDatabase database = new WeatherDatabase();

			// Cria uma instância da classe WeatherApiClient para buscar os dados da API.
			WeatherApiClient apiClient = new WeatherApiClient();

			WeatherData weatherData = null;

			try
			{
				// Tenta obter os dados de previsão do tempo da API.
				weatherData = await apiClient.GetWeatherDataAsync();
			}
			catch (Exception ex)
			{
				// Caso ocorra algum erro ao obter os dados, exibe uma mensagem de erro.
				MessageBox.Show($"Erro ao obter os dados da API: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Salva os dados da previsão do tempo no banco de dados.
			database.SaveWeatherDataToDatabase(weatherData);

			// Consulta os dados existentes no banco de dados (recupera as previsões já salvas).
			var databaseForecasts = database.GetAllWeatherDataFromDatabase();

			// Define a fonte de dados do DataGridView para exibir as previsões obtidas do banco de dados.
			dgvForecast.DataSource = databaseForecasts;

			// Exibe a cidade e a descrição do clima atual no painel do usuário.
			lblCity.Text = $"Cidade: {weatherData.results.city}";
			lblDescription.Text = $"Descrição: {weatherData.results.forecast[0].description}";
			lblTemperature.Text = $"Temperatura atual: {weatherData.results.temp} graus";

			// Configura os cabeçalhos das colunas da DataGridView para uma exibição mais amigável.
			ConfigureDataGridViewHeaders();
		}

		/// <summary>
		/// Configura os cabeçalhos das colunas da <see cref="DataGridView"/> para exibir as informações da previsão do tempo.
		/// 
		/// Este método é responsável por melhorar a legibilidade e proporcionar uma experiência mais clara ao usuário ao visualizar a previsão do tempo.
		/// </summary>
		/// <remarks>
		/// Este método altera os textos das colunas da DataGridView para títulos mais compreensíveis.
		/// </remarks>
		/// <returns>Retorna nada (void).</returns>
		private void ConfigureDataGridViewHeaders()
		{
			// Configura os cabeçalhos das colunas conforme a descrição dos dados.
			dgvForecast.Columns[0].HeaderText = "Data";
			dgvForecast.Columns[1].HeaderText = "Dia da Semana";
			dgvForecast.Columns[2].HeaderText = "Máxima (°C)";
			dgvForecast.Columns[3].HeaderText = "Mínima (°C)";
			dgvForecast.Columns[4].HeaderText = "Humidade (%)";
			dgvForecast.Columns[5].HeaderText = "Nebulosidade";
			dgvForecast.Columns[6].HeaderText = "Chuva (mm)";
			dgvForecast.Columns[7].HeaderText = "Probabilidade de Chuva (%)";
			dgvForecast.Columns[8].HeaderText = "Vento (km/h)";
			dgvForecast.Columns[9].HeaderText = "Nascer do Sol";
			dgvForecast.Columns[10].HeaderText = "Pôr do Sol";
			dgvForecast.Columns[11].HeaderText = "Fase Lunar";
			dgvForecast.Columns[12].HeaderText = "Descrição";
			dgvForecast.Columns[13].HeaderText = "Condição";
			dgvForecast.Columns[14].HeaderText = "Mudança de Temperatura";
		}

		/// <summary>
		/// Manipulador de evento para abrir uma nova janela de exibição aprimorada.
		/// Este método exibe uma nova tela (formulário) com uma visualização melhorada das informações do clima.
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento (o botão clicado).</param>
		/// <param name="e">Os dados do evento de clique.</param>
		private void btnOtherView_Click(object sender, EventArgs e)
		{
			// Cria e exibe o formulário ImprovedForm.
			ImprovedForm improvedForm = new ImprovedForm();
			improvedForm.ShowDialog();
		}
	}
}