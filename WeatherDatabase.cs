using System;
using System.Data.SqlClient;

namespace WeatherApp
{
	public class WeatherDatabase
	{
		private string connectionString = "Server=GUIGAS\\SQLEXPRESS;Database=WeatherForecastDB;Integrated Security=True;";

		// Função para salvar os dados
		public void SaveWeatherDataToDatabase(WeatherData weatherData)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Salvar os dados no banco
				foreach (var forecast in weatherData.results.forecast)
				{
					// Salvar a data apenas como "dd/MM"
					string forecastDate = forecast.date;

					string query = "INSERT INTO WeatherForecast (Date, Weekday, MaxTemperature, MinTemperature, Humidity, Rain, Description) " +
								   "VALUES (@Date, @Weekday, @MaxTemperature, @MinTemperature, @Humidity, @Rain, @Description)";

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Date", forecastDate);
						command.Parameters.AddWithValue("@Weekday", forecast.weekday);
						command.Parameters.AddWithValue("@MaxTemperature", forecast.max);
						command.Parameters.AddWithValue("@MinTemperature", forecast.min);
						command.Parameters.AddWithValue("@Humidity", forecast.humidity);
						command.Parameters.AddWithValue("@Rain", forecast.rain);
						command.Parameters.AddWithValue("@Description", forecast.description);

						command.ExecuteNonQuery();
					}
				}

				// Após salvar, chamamos a função para atualizar a mudança de temperatura
				UpdateTemperatureChange(connection, weatherData);
			}
		}

		// Função para atualizar a mudança de temperatura
		private void UpdateTemperatureChange(SqlConnection connection, WeatherData weatherData)
		{
			for (int i = 1; i < weatherData.results.forecast.Length; i++) // Iniciando a comparação a partir do segundo item
			{
				var currentForecast = weatherData.results.forecast[i];
				var previousForecast = weatherData.results.forecast[i - 1];

				string forecastDate = currentForecast.date;
				double currentTemp = currentForecast.max;
				double previousTemp = previousForecast.max;

				// Verificar se houve aumento ou diminuição de temperatura
				string temperatureChange = "Sem mudança";
				if (currentTemp > previousTemp)
				{
					temperatureChange = "Aumentou";
				}
				else if (currentTemp < previousTemp)
				{
					temperatureChange = "Diminuíu";
				}

				// Atualizar a tabela com a mudança de temperatura
				string updateQuery = "UPDATE WeatherForecast SET TemperatureChange = @TemperatureChange WHERE Date = @Date";
				using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
				{
					updateCommand.Parameters.AddWithValue("@TemperatureChange", temperatureChange);
					updateCommand.Parameters.AddWithValue("@Date", forecastDate);

					updateCommand.ExecuteNonQuery();
				}
			}
		}
	}
}