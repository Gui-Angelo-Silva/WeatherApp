using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
	public class WeatherApiClient
	{
		/// <summary>
		/// Obtém os dados da previsão do tempo por meio de API externa.
		/// Compara as temperaturas máximas dos dias consecutivos e adicionar o status de mudança.
		/// </summary>
		/// <returns>Retorna um objeto WeatherData contendo as previsões do clima.</returns>
		/// <exception cref="Exception">Lança uma exceção caso ocorra algum erro ao obter dados da API.</exception>
		public async Task<WeatherData> GetWeatherDataAsync()
		{
			// URL da API que fornece a previsão do tempo de Jales
			string url = "https://api.hgbrasil.com/weather?woeid=457398";

			using (HttpClient client = new HttpClient())
			{
				try
				{
					// Envia a solicitação para a API e recebe como string
					var response = await client.GetStringAsync(url);

					// Verifica se a resposta estiver vazia
                    if (string.IsNullOrEmpty(response))
                    {
						throw new Exception("Resposta vazia da API");
                    }

					// Deserializa os dados JSON da resposta para o modelo do WeatherData
					var weatherData = JsonConvert.DeserializeObject<WeatherData>(response);

					// Valida se os dados deserializados estão válidos e se a lista possui mais de 2 itens
                    if (weatherData == null || weatherData.results?.forecast == null || weatherData.results.forecast.Length < 2)
                    {
						throw new Exception("Dados da previsão do tempo inválidos ou incompletos!");
                    }

					// Lógica para comparar as temperaturas dos dias consecutivos
					for (int i = 1; i < weatherData.results.forecast.Length; i++)
					{
						var currentForecast = weatherData.results.forecast[i];
						var previousForecast = weatherData.results.forecast[i - 1];

						double currentTemp = currentForecast.max;
						double previousTemp = previousForecast.max;

						currentForecast.TemperatureChange = currentTemp > previousTemp ? "Aumentou" :
															 currentTemp < previousTemp ? "Diminuíu" : "Sem mudança";
					}

					// Retorna os dados da previsão do tempo
					return weatherData;
				}
				catch (Exception ex)
				{
					// Caso ocorra algum erro, exibe a mensagem de erro
					throw new Exception($"Erro ao obter os dados do clima: {ex.Message}");
				}
			}
		}
	}
}