using Newtonsoft.Json;

namespace WeatherApp
{
	public class WeatherData
	{
		public Results results { get; set; }
	}

	public class Results
	{
		public string city { get; set; }
		public Forecast[] forecast { get; set; }
		public string timezone { get; set; }
		public double temp { get; set; }
	}

	public class Forecast
	{
		public string date { get; set; }
		public string weekday { get; set; }
		public double max { get; set; }
		public double min { get; set; }
		public double humidity { get; set; }
		public double rain { get; set; }
		public string description { get; set; }
		public string TemperatureChange { get; set; }
	}

	public class WeatherApiClient
	{
		public async Task<WeatherData> GetWeatherDataAsync()
		{
			string url = "https://api.hgbrasil.com/weather?woeid=457398";
			using (HttpClient client = new HttpClient())
			{
				var response = await client.GetStringAsync(url);
				var weatherData = JsonConvert.DeserializeObject<WeatherData>(response);

				// Lógica para comparar as temperaturas dos dias consecutivos
				for (int i = 1; i < weatherData.results.forecast.Length; i++)  // Alteração de .Count para .Length
				{
					var currentForecast = weatherData.results.forecast[i];
					var previousForecast = weatherData.results.forecast[i - 1];

					// Comparando temperaturas
					double currentTemp = currentForecast.max;  // Não precisa de Convert.ToDouble, pois já é double
					double previousTemp = previousForecast.max;

					if (currentTemp > previousTemp)
					{
						currentForecast.TemperatureChange = "Aumentou";
					}
					else if (currentTemp < previousTemp)
					{
						currentForecast.TemperatureChange = "Diminuíu";
					}
					else
					{
						currentForecast.TemperatureChange = "Sem mudança";
					}
				}

				return weatherData;
			}
		}
	}
}