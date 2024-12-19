namespace WeatherApp
{
	public partial class WeatherForm : Form
	{
		public WeatherForm()
		{
			InitializeComponent();
		}

		// Carrega o formul�rio
		private async void WeatherForm_Load(object sender, EventArgs e)
		{
			// Cria uma inst�ncia da classe WeatherApiClient
			WeatherApiClient apiClient = new WeatherApiClient();
			WeatherData weatherData = await apiClient.GetWeatherDataAsync();

			// Exibe a cidade e a descri��o do clima atual
			lblCity.Text = $"Cidade: {weatherData.results.city}";
			lblDescription.Text = $"Descri��o: {weatherData.results.forecast[0].description}";
			lblTemperature.Text = $"Temperatura atual: {weatherData.results.temp} graus";

			// Preenche a DataGridView com os dados da previs�o
			dgvForecast.DataSource = weatherData.results.forecast;

			dgvForecast.Columns[0].HeaderText = "Data";
			dgvForecast.Columns[1].HeaderText = "Dia da Semana";
			dgvForecast.Columns[2].HeaderText = "M�xima (�C)";
			dgvForecast.Columns[3].HeaderText = "M�nima (�C)";
			dgvForecast.Columns[4].HeaderText = "Humidade (%)";
			dgvForecast.Columns[5].HeaderText = "Chuva (mm)";
			dgvForecast.Columns[6].HeaderText = "Descri��o";
			dgvForecast.Columns[7].HeaderText = "Mudan�a de Temperatura";
		
			// Salva os dados da previs�o do tempo no banco de dados
			WeatherDatabase database = new WeatherDatabase();
			database.SaveWeatherDataToDatabase(weatherData);
		}
	}
}