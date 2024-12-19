namespace WeatherApp
{
	public partial class WeatherForm : Form
	{
		public WeatherForm()
		{
			InitializeComponent();
		}

		// Carrega o formulário
		private async void WeatherForm_Load(object sender, EventArgs e)
		{
			// Cria uma instância da classe WeatherApiClient
			WeatherApiClient apiClient = new WeatherApiClient();
			WeatherData weatherData = await apiClient.GetWeatherDataAsync();

			// Exibe a cidade e a descrição do clima atual
			lblCity.Text = $"Cidade: {weatherData.results.city}";
			lblDescription.Text = $"Descrição: {weatherData.results.forecast[0].description}";
			lblTemperature.Text = $"Temperatura atual: {weatherData.results.temp} graus";

			// Preenche a DataGridView com os dados da previsão
			dgvForecast.DataSource = weatherData.results.forecast;

			dgvForecast.Columns[0].HeaderText = "Data";
			dgvForecast.Columns[1].HeaderText = "Dia da Semana";
			dgvForecast.Columns[2].HeaderText = "Máxima (°C)";
			dgvForecast.Columns[3].HeaderText = "Mínima (°C)";
			dgvForecast.Columns[4].HeaderText = "Humidade (%)";
			dgvForecast.Columns[5].HeaderText = "Chuva (mm)";
			dgvForecast.Columns[6].HeaderText = "Descrição";
			dgvForecast.Columns[7].HeaderText = "Mudança de Temperatura";
		
			// Salva os dados da previsão do tempo no banco de dados
			WeatherDatabase database = new WeatherDatabase();
			database.SaveWeatherDataToDatabase(weatherData);
		}
	}
}