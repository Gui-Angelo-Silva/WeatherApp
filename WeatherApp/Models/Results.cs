using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
	/// <summary>
	/// Representa os resultados da previsão do tempo para uma cidade específica.
	/// </summary>
	public class Results
	{
		/// <summary>
		/// Obtém ou define o nome da cidade.
		/// </summary>
		/// <value>
		/// O nome da cidade para a qual a previsão do tempo foi obtida, como "Jales".
		/// </value>
		public string city { get; set; }

		/// <summary>
		/// Obtém ou define as previsões diárias para a cidade.
		/// </summary>
		/// <value>
		/// Um array de objetos <see cref="Forecast"/> representando as previsões diárias.
		/// </value>
		public Forecast[] forecast { get; set; }

		/// <summary>
		/// Obtém ou define o fuso horário da cidade.
		/// </summary>
		/// <value>
		/// O fuso horário em que a previsão foi calculada.
		/// </value>
		public string timezone { get; set; }

		/// <summary>
		/// Obtém ou define a temperatura atual da cidade.
		/// </summary>
		/// <value>
		/// A temperatura atual em graus Celsius (°C).
		/// </value>
		public double temp { get; set; }
		
		/// <summary>
		/// Obtém ou define a condição do clima da cidade.
		/// </summary>
		/// <value>
		/// Condição do clima descrito.
		/// </value>
		public string condition_slug { get; set; }
		
		/// <summary>
		/// Obtém ou define a descrição do clima da cidade.
		/// </summary>
		/// <value>
		/// Descrição do clima descrito.
		/// </value>
		public string description { get; set; }
	}
}