using System;
using System.Data.SqlClient;
using WeatherApp.Config;
using WeatherApp.Models;

namespace WeatherApp.Repositories
{
	/// <summary>
	/// Classe responsável pela interação com o banco de dados para salvar, atualizar e recuperar dados relacionados à previsão do tempo.
	/// </summary>
	public class WeatherDatabase
	{
		/// <summary>
		/// String de conexão com o banco de dados.
		/// </summary>
		private string connectionString = DatabaseConfig.ConnectionString;

		/// <summary>
		/// Salva os dados da previsão do tempo no banco de dados.
		/// </summary>
		/// <param name="weatherData">Objeto contendo os dados da previsão do tempo a serem salvos.</param>
		public void SaveWeatherDataToDatabase(WeatherData weatherData)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Itera sobre a previsão e salva cada dia no banco de dados
				foreach (var forecast in weatherData.results.forecast)
				{
					// Salva a data apenas como "dd/MM"
					string forecastDate = forecast.date;

					// Verifica se a data já existe no banco de dados
					if (!IsDateAlreadyInDatabase(connection, forecastDate))
					{
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
				}

				// Após salvar, chama a função para atualizar a mudança de temperatura
				UpdateTemperatureChange(connection, weatherData);
			}
		}

		/// <summary>
		/// Obtém todos os registros de previsão do tempo armazenados no banco de dados.
		/// </summary>
		/// <returns>Lista de objetos <see cref="Forecast"/> contendo os dados da previsão.</returns>
		public List<Forecast> GetAllWeatherDataFromDatabase()
		{
			var forecasts = new List<Forecast>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				string query = "SELECT Date, Weekday, MaxTemperature, MinTemperature, Humidity, Rain, Description, TemperatureChange FROM WeatherForecast";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var forecast = new Forecast
							{
								date = reader["Date"].ToString(),
								weekday = reader["Weekday"].ToString(),
								max = Convert.ToDouble(reader["MaxTemperature"]),
								min = Convert.ToDouble(reader["MinTemperature"]),
								humidity = reader["Humidity"] != DBNull.Value ? Convert.ToDouble(reader["Humidity"]) : 0,
								rain = reader["Rain"] != DBNull.Value ? Convert.ToDouble(reader["Rain"]) : 0,
								description = reader["Description"].ToString(),
								TemperatureChange = reader["TemperatureChange"]?.ToString()
							};

							forecasts.Add(forecast);
						}
					}
				}
			}

			return forecasts;
		}

		/// <summary>
		/// Verifica se uma data específica já existe na tabela de previsões do tempo no banco de dados.
		/// </summary>
		/// <param name="connection">Conexão ativa com o banco de dados.</param>
		/// <param name="forecastDate">Data da previsão a ser verificada.</param>
		/// <returns>Retorna <c>true</c> se a data já existir, caso contrário, <c>false</c>.</returns>
		private bool IsDateAlreadyInDatabase(SqlConnection connection, string forecastDate)
		{
			string checkQuery = "SELECT COUNT(1) FROM WeatherForecast WHERE Date = @Date";
			using (SqlCommand command = new SqlCommand(checkQuery, connection))
			{
				command.Parameters.AddWithValue("@Date", forecastDate);
				int count = Convert.ToInt32(command.ExecuteScalar());
				return count > 0; // Retorna true se a data já existe
			}
		}

		/// <summary>
		/// Atualiza a coluna "TemperatureChange" na tabela de previsões, indicando se a temperatura aumentou, diminuiu ou não mudou.
		/// </summary>
		/// <param name="connection">Conexão ativa com o banco de dados.</param>
		/// <param name="weatherData">Dados da previsão do tempo para calcular a mudança de temperatura.</param>
		private void UpdateTemperatureChange(SqlConnection connection, WeatherData weatherData)
		{
			for (int i = 1; i < weatherData.results.forecast.Length; i++) // Iniciando a comparação a partir do segundo item
			{
				var currentForecast = weatherData.results.forecast[i];
				var previousForecast = weatherData.results.forecast[i - 1];

				string forecastDate = currentForecast.date;
				double currentTemp = currentForecast.max;
				double previousTemp = previousForecast.max;

				// Verifica se houve aumento ou diminuição de temperatura
				string temperatureChange = "Sem mudança";
				if (currentTemp > previousTemp)
				{
					temperatureChange = "Aumentou";
				}
				else if (currentTemp < previousTemp)
				{
					temperatureChange = "Diminuíu";
				}

				// Atualiza a tabela com a mudança de temperatura
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