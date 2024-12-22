using WeatherApp.Models;
using WeatherApp.Repositories;
using WeatherApp.Services;
using WeatherApp.Views;

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
		/// Inicializa os componentes do formul�rio e prepara o ambiente para exibi��o dos dados de previs�o do tempo.
		/// </summary>
		public WeatherForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Manipulador de evento acionado quando o formul�rio � carregado.
		/// Este m�todo obt�m os dados de previs�o do tempo da API, exibe as informa��es no formul�rio e salva os dados no banco de dados.
		/// 
		/// O fluxo �:
		/// - Obter os dados da API.
		/// - Se bem-sucedido, salvar os dados no banco de dados.
		/// - Exibir os dados da API ou do banco de dados (se os dados no banco forem mais antigos ou estiverem presentes).
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento, normalmente o pr�prio formul�rio.</param>
		/// <param name="e">Os dados do evento que cont�m informa��es sobre o carregamento do formul�rio.</param>
		/// <returns>Retorna nada (void).</returns>
		private async void WeatherForm_Load(object sender, EventArgs e)
		{
			// Cria uma inst�ncia do reposit�rio do banco de dados para armazenar os dados.
			WeatherDatabase database = new WeatherDatabase();

			// Cria uma inst�ncia da classe WeatherApiClient para buscar os dados da API.
			WeatherApiClient apiClient = new WeatherApiClient();

			WeatherData weatherData = null;

			try
			{
				// Tenta obter os dados de previs�o do tempo da API.
				weatherData = await apiClient.GetWeatherDataAsync();
			}
			catch (Exception ex)
			{
				// Caso ocorra algum erro ao obter os dados, exibe uma mensagem de erro.
				MessageBox.Show($"Erro ao obter os dados da API: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Salva os dados da previs�o do tempo no banco de dados.
			database.SaveWeatherDataToDatabase(weatherData);

			// Consulta os dados existentes no banco de dados (recupera as previs�es j� salvas).
			var databaseForecasts = database.GetAllWeatherDataFromDatabase();

			// Define a fonte de dados do DataGridView para exibir as previs�es obtidas do banco de dados.
			dgvForecast.DataSource = databaseForecasts;

			// Exibe a cidade e a descri��o do clima atual no painel do usu�rio.
			lblCity.Text = $"Cidade: {weatherData.results.city}";
			lblDescription.Text = $"Descri��o: {weatherData.results.forecast[0].description}";
			lblTemperature.Text = $"Temperatura atual: {weatherData.results.temp} graus";

			// Configura os cabe�alhos das colunas da DataGridView para uma exibi��o mais amig�vel.
			ConfigureDataGridViewHeaders();
		}

		/// <summary>
		/// Configura os cabe�alhos das colunas da <see cref="DataGridView"/> para exibir as informa��es da previs�o do tempo.
		/// 
		/// Este m�todo � respons�vel por melhorar a legibilidade e proporcionar uma experi�ncia mais clara ao usu�rio ao visualizar a previs�o do tempo.
		/// </summary>
		/// <remarks>
		/// Este m�todo altera os textos das colunas da DataGridView para t�tulos mais compreens�veis.
		/// </remarks>
		/// <returns>Retorna nada (void).</returns>
		private void ConfigureDataGridViewHeaders()
		{
			// Configura os cabe�alhos das colunas conforme a descri��o dos dados.
			dgvForecast.Columns[0].HeaderText = "Data";
			dgvForecast.Columns[1].HeaderText = "Dia da Semana";
			dgvForecast.Columns[2].HeaderText = "M�xima (�C)";
			dgvForecast.Columns[3].HeaderText = "M�nima (�C)";
			dgvForecast.Columns[4].HeaderText = "Humidade (%)";
			dgvForecast.Columns[5].HeaderText = "Nebulosidade";
			dgvForecast.Columns[6].HeaderText = "Chuva (mm)";
			dgvForecast.Columns[7].HeaderText = "Probabilidade de Chuva (%)";
			dgvForecast.Columns[8].HeaderText = "Vento (km/h)";
			dgvForecast.Columns[9].HeaderText = "Nascer do Sol";
			dgvForecast.Columns[10].HeaderText = "P�r do Sol";
			dgvForecast.Columns[11].HeaderText = "Fase Lunar";
			dgvForecast.Columns[12].HeaderText = "Descri��o";
			dgvForecast.Columns[13].HeaderText = "Condi��o";
			dgvForecast.Columns[14].HeaderText = "Mudan�a de Temperatura";
		}

		/// <summary>
		/// Manipulador de evento para abrir uma nova janela de exibi��o aprimorada.
		/// Este m�todo exibe uma nova tela (formul�rio) com uma visualiza��o melhorada das informa��es do clima.
		/// </summary>
		/// <param name="sender">O objeto que gerou o evento (o bot�o clicado).</param>
		/// <param name="e">Os dados do evento de clique.</param>
		private void btnOtherView_Click(object sender, EventArgs e)
		{
			// Cria e exibe o formul�rio ImprovedForm.
			ImprovedForm improvedForm = new ImprovedForm();
			improvedForm.ShowDialog();
		}
	}
}