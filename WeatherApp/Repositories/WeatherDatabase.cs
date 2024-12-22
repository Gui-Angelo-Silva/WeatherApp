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
		/// Salva os dados da previsão do tempo no banco de dados, verificando se os dados já existem antes de inseri-los.
		/// </summary>
		/// <param name="weatherData">Objeto contendo os dados da previsão do tempo a serem salvos.</param>
		/// <remarks>
		/// Este método insere os dados no banco de dados, mas apenas para as datas que ainda não estão presentes. 
		/// Ele também traduz a fase lunar para o português antes de salvar.
		/// </remarks>
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
					string moonPhaseInPortuguese = TranslateMoonPhaseToPortuguese(forecast.moon_phase);
					string conditionInPortuguese = TranslateConditionToPortuguese(forecast.condition);

					// Verifica se a data já existe no banco de dados
					if (!IsDateAlreadyInDatabase(connection, forecastDate))
					{
						// Query SQL para inserir os dados na tabela WeatherForecast
						string query = @"INSERT INTO WeatherForecast (Date, Weekday, MaxTemperature, MinTemperature, 
                                 Humidity, Cloudiness, Rain, RainProbability, WindSpeedy, Sunrise, Sunset, 
                                 MoonPhase, Description, Condition) 
                                 VALUES (@Date, @Weekday, @MaxTemperature, @MinTemperature, 
                                 @Humidity, @Cloudiness, @Rain, @RainProbability, @WindSpeedy, 
                                 @Sunrise, @Sunset, @MoonPhase, @Description, @Condition)";

						// Comando SQL para inserção
						using (SqlCommand command = new SqlCommand(query, connection))
						{
							// Adiciona os parâmetros necessários para a query
							command.Parameters.AddWithValue("@Date", forecastDate);
							command.Parameters.AddWithValue("@Weekday", forecast.weekday);
							command.Parameters.AddWithValue("@MaxTemperature", forecast.max);
							command.Parameters.AddWithValue("@MinTemperature", forecast.min);
							command.Parameters.AddWithValue("@Humidity", forecast.humidity);
							command.Parameters.AddWithValue("@Cloudiness", forecast.cloudiness); 
							command.Parameters.AddWithValue("@Rain", forecast.rain);
							command.Parameters.AddWithValue("@RainProbability", forecast.rain_probability); 
							command.Parameters.AddWithValue("@WindSpeedy", forecast.wind_speedy); 
							command.Parameters.AddWithValue("@Sunrise", forecast.sunrise); 
							command.Parameters.AddWithValue("@Sunset", forecast.sunset); 
							command.Parameters.AddWithValue("@MoonPhase", moonPhaseInPortuguese); 
							command.Parameters.AddWithValue("@Description", forecast.description);
							command.Parameters.AddWithValue("@Condition", conditionInPortuguese); 

							// Executa o comando SQL para inserir os dados
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
		/// <remarks>
		/// Este método executa uma consulta no banco de dados e retorna todos os registros de previsão armazenados.
		/// </remarks>
		public List<Forecast> GetAllWeatherDataFromDatabase()
		{
			var forecasts = new List<Forecast>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				// Query SQL para selecionar todos os campos da tabela WeatherForecast
				string query = @"SELECT Date, Weekday, MaxTemperature, MinTemperature, 
                                 Humidity, Cloudiness, Rain, RainProbability, 
                                 WindSpeedy, Sunrise, Sunset, MoonPhase, 
                                 Description, Condition, TemperatureChange 
                          FROM WeatherForecast";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var forecast = new Forecast
							{
								// Mapeamento das colunas para as propriedades de Forecast
								date = reader["Date"].ToString(),
								weekday = reader["Weekday"].ToString(),
								max = Convert.ToDouble(reader["MaxTemperature"]),
								min = Convert.ToDouble(reader["MinTemperature"]),
								humidity = reader["Humidity"] != DBNull.Value ? Convert.ToDouble(reader["Humidity"]) : 0,
								rain = reader["Rain"] != DBNull.Value ? Convert.ToDouble(reader["Rain"]) : 0,
								rain_probability = reader["RainProbability"] != DBNull.Value ? Convert.ToDouble(reader["RainProbability"]) : 0,
								cloudiness = reader["Cloudiness"] != DBNull.Value ? Convert.ToInt32(reader["Cloudiness"]) : 0,
								wind_speedy = reader["WindSpeedy"].ToString(),
								sunrise = reader["Sunrise"].ToString(),
								sunset = reader["Sunset"].ToString(),
								moon_phase = reader["MoonPhase"].ToString(),
								description = reader["Description"].ToString(),
								condition = reader["Condition"].ToString(),
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
		/// <remarks>
		/// Este método consulta a tabela para verificar se a data da previsão já foi inserida anteriormente no banco.
		/// </remarks>
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
		/// <remarks>
		/// Este método percorre os dados de previsão do tempo, compara as temperaturas médias e atualiza a tabela 
		/// de previsões com a mudança de temperatura para cada data (Aumentou, Diminuiu, ou Sem Mudança).
		/// </remarks>
		private void UpdateTemperatureChange(SqlConnection connection, WeatherData weatherData)
		{
			for (int i = 1; i < weatherData.results.forecast.Length; i++) // Iniciando a comparação a partir do segundo item
			{
				var currentForecast = weatherData.results.forecast[i];
				var previousForecast = weatherData.results.forecast[i - 1];

				string forecastDate = currentForecast.date;
				double avgCurrentTemp = (currentForecast.min + currentForecast.max) / 2;
				double avgPreviousTemp = (previousForecast.min + previousForecast.max) / 2;

				// Inicializa a variável para armazenar o status de mudança
				string temperatureChange = "Sem mudança";

				// Lógica de comparação de temperaturas médias
				if (avgCurrentTemp > avgPreviousTemp)
					temperatureChange = "Aumentou";
				else if (avgCurrentTemp < avgPreviousTemp)
					temperatureChange = "Diminuiu";
				else
					temperatureChange = "Sem mudança";

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

		/// <summary>
		/// Traduza a fase da lua do inglês para o português.
		/// </summary>
		/// <param name="moonPhaseInEnglish">Fase da lua em inglês.</param>
		/// <returns>Fase da lua traduzida para o português.</returns>
		/// <remarks>
		/// Este método converte as fases da lua de inglês para português, com base em um conjunto predeterminado de fases.
		/// Se a fase da lua não for reconhecida, retorna "Desconhecida".
		/// </remarks>
		private string TranslateMoonPhaseToPortuguese(string moonPhaseInEnglish)
		{
			switch (moonPhaseInEnglish.ToLower())
			{
				case "first_quarter":
					return "Primeiro Quarto";
				case "full":
					return "Cheia";
				case "last_quarter":
					return "Último Quarto";
				case "new":
					return "Nova";
				case "waning_crescent":
					return "Crescente Minguante";
				case "waning_gibbous":
					return "Gibosa Minguante";
				case "waxing_crescent":
					return "Crescente";
				case "waxing_gibbous":
					return "Gibosa Crescente";
				default:
					return "Desconhecida";
			}
		}

		/// <summary>
		/// Traduz a condição climática do inglês para o português.
		/// </summary>
		/// <param name="conditionInEnglish">Condição climática em inglês.</param>
		/// <returns>Condição climática traduzida para o português.</returns>
		/// <remarks>
		/// Este método converte as condições climáticas de inglês para português, com base em um conjunto predeterminado de condições.
		/// Se a condição não for reconhecida, retorna "Desconhecida".
		/// </remarks>
		private string TranslateConditionToPortuguese(string conditionInEnglish)
		{
			switch (conditionInEnglish.ToLower())
			{
				case "clear_day":
					return "Dia Limpo";
				case "clear_night":
					return "Noite Limpa";
				case "cloud":
					return "Nuvens";
				case "cloudly_day":
					return "Dia Nublado";
				case "cloudly_night":
					return "Noite Nublada";
				case "fog":
					return "Névoa";
				case "hail":
					return "Granizo";
				case "none_day":
					return "Dia Sem Condições Específicas";
				case "none_night":
					return "Noite Sem Condições Específicas";
				case "rain":
					return "Chuva";
				case "snow":
					return "Neve";
				case "storm":
					return "Tempestade";
				default:
					return "Desconhecida";
			}
		}
	}
}