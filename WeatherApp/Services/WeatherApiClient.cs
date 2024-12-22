using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
	public class WeatherApiClient
	{
		/// <summary>
		/// Compara as temperaturas mínimas e máximas de dois dias consecutivos e determina se a temperatura aumentou,
		/// diminuiu ou não houve mudança.
		/// </summary>
		/// <param name="previousMinTemp">A temperatura mínima do dia anterior.</param>
		/// <param name="previousMaxTemp">A temperatura máxima do dia anterior.</param>
		/// <param name="currentMinTemp">A temperatura mínima do dia atual.</param>
		/// <param name="currentMaxTemp">A temperatura máxima do dia atual.</param>
		/// <returns>Retorna uma string indicando a mudança de temperatura: "Aumentou", "Diminuíu" ou "Sem mudança".</returns>
		public string CompareTemperatures(double previousMinTemp, double previousMaxTemp, double currentMinTemp, double currentMaxTemp)
		{
			double avgPreviousTemp = (previousMinTemp + previousMaxTemp) / 2;
			double avgCurrentTemp = (currentMinTemp + currentMaxTemp) / 2;

			// Se a temperatura mínima e a máxima de hoje são maiores do que as de ontem, consideramos que aumentou
			if (avgCurrentTemp > avgPreviousTemp)
				return "Aumentou";

			// Se a temperatura mínima e a máxima de hoje são menores do que as de ontem, consideramos que diminuiu
			if (avgCurrentTemp < avgPreviousTemp)
				return "Diminuíu";

			// Caso contrário, consideramos que não houve uma mudança significativa
			return "Sem mudança";
		}

		/// <summary>
		/// Obtém os dados da previsão do tempo por meio de API externa.
		/// Compara as temperaturas mínimas e máximas dos dias consecutivos e adiciona o status de mudança.
		/// </summary>
		/// <returns>Retorna um objeto WeatherData contendo as previsões do clima.</returns>
		/// <exception cref="Exception">Lança uma exceção caso ocorra algum erro ao obter dados da API.</exception>
		public async Task<WeatherData> GetWeatherDataAsync()
		{
			string url = "https://api.hgbrasil.com/weather?woeid=457398"; // Exemplo de URL

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

					// Comparar as temperaturas máximas e mínimas dos dias consecutivos
					for (int i = 1; i < weatherData.results.forecast.Length; i++)
					{
						var currentForecast = weatherData.results.forecast[i];
						var previousForecast = weatherData.results.forecast[i - 1];

						double currentMinTemp = currentForecast.min;
						double currentMaxTemp = currentForecast.max;
						double previousMinTemp = previousForecast.min;
						double previousMaxTemp = previousForecast.max;

						// Atribuindo a mudança de temperatura para cada dia
						currentForecast.TemperatureChange = CompareTemperatures(previousMinTemp, previousMaxTemp, currentMinTemp, currentMaxTemp);
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