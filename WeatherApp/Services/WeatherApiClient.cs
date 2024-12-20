using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
	public class WeatherApiClient
	{
		/// <summary>
		/// Compara as temperaturas máximas de dois dias consecutivos e determina se a temperatura aumentou,
		/// diminuiu ou não houve mudança.
		/// </summary>
		/// <param name="previousTemp">A temperatura máxima do dia anterior.</param>
		/// <param name="currentTemp">A temperatura máxima do dia atual.</param>
		/// <returns>Retorna uma string indicando a mudança de temperatura: "Aumentou", "Diminuíu" ou "Sem mudança".</returns>
		/// <remarks>
		/// Este método é usado para comparar as temperaturas máximas de dois dias consecutivos e 
		/// determinar a mudança de temperatura entre eles. Ele ajuda a informar o status de variação
		/// de temperatura para cada previsão de clima.
		/// </remarks>
		public string CompareTemperatures(double previousTemp, double currentTemp)
		{
			if (currentTemp > previousTemp)
				return "Aumentou";
			if (currentTemp < previousTemp)
				return "Diminuíu";
			return "Sem mudança";
		}

		/// <summary>
		/// Obtém os dados da previsão do tempo por meio de API externa.
		/// Compara as temperaturas máximas dos dias consecutivos e adiciona o status de mudança.
		/// </summary>
		/// <returns>Retorna um objeto WeatherData contendo as previsões do clima.</returns>
		/// <exception cref="Exception">Lança uma exceção caso ocorra algum erro ao obter dados da API.</exception>
		public async Task<WeatherData> GetWeatherDataAsync()
		{
			string url = "https://api.hgbrasil.com/weather?woeid=457398";

			using (HttpClient client = new HttpClient())
			{
				try
				{
					var response = await client.GetStringAsync(url);

					if (string.IsNullOrEmpty(response))
						throw new Exception("Resposta vazia da API");

					var weatherData = JsonConvert.DeserializeObject<WeatherData>(response);

					if (weatherData == null || weatherData.results?.forecast == null || weatherData.results.forecast.Length < 2)
						throw new Exception("Dados da previsão do tempo inválidos ou incompletos!");

					for (int i = 1; i < weatherData.results.forecast.Length; i++)
					{
						var currentForecast = weatherData.results.forecast[i];
						var previousForecast = weatherData.results.forecast[i - 1];

						double currentTemp = currentForecast.max;
						double previousTemp = previousForecast.max;

						currentForecast.TemperatureChange = CompareTemperatures(previousTemp, currentTemp);
					}

					return weatherData;
				}
				catch (Exception ex)
				{
					throw new Exception($"Erro ao obter os dados do clima: {ex.Message}");
				}
			}
		}
	}
}