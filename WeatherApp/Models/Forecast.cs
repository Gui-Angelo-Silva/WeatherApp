using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
	/// <summary>
	/// Representa a previsão do tempo para um dia específico.
	/// </summary>
	public class Forecast
	{
		/// <summary>
		/// Obtém ou define a data da previsão.
		/// </summary>
		/// <value>
		/// Uma string que representa a data da previsão (por exemplo, "19/12").
		/// </value>
		public string date { get; set; }

		/// <summary>
		/// Obtém ou define o dia da semana da previsão.
		/// </summary>
		/// <value>
		/// Uma string que representa o dia da semana (por exemplo, "Qui").
		/// </value>
		public string weekday { get; set; }

		/// <summary>
		/// Obtém ou define a temperatura máxima prevista para o dia.
		/// </summary>
		/// <value>
		/// Um valor numérico representando a temperatura máxima em graus Celsius.
		/// </value>
		public double max { get; set; }

		/// <summary>
		/// Obtém ou define a temperatura mínima prevista para o dia.
		/// </summary>
		/// <value>
		/// Um valor numérico representando a temperatura mínima em graus Celsius.
		/// </value>
		public double min { get; set; }

		/// <summary>
		/// Obtém ou define a umidade do ar prevista para o dia.
		/// </summary>
		/// <value>
		/// Um valor numérico representando a umidade em percentual (%).
		/// </value>
		public double humidity { get; set; }

		/// <summary>
		/// Obtém ou define a quantidade de chuva prevista para o dia.
		/// </summary>
		/// <value>
		/// Um valor numérico representando a quantidade de chuva em milímetros (mm).
		/// </value>
		public double rain { get; set; }

		/// <summary>
		/// Obtém ou define a descrição geral do clima para o dia.
		/// </summary>
		/// <value>
		/// Uma string que descreve o clima, como "Chuvas", "Chuvas Esparsas", etc.
		/// </value>
		public string description { get; set; }

		/// <summary>
		/// Obtém ou define a mudança de temperatura em relação ao dia anterior.
		/// </summary>
		/// <value>
		/// Uma string que indica se a temperatura aumentou, diminuiu ou permaneceu a mesma em relação ao dia anterior.
		/// </value>
		public string TemperatureChange { get; set; }
	}
}
