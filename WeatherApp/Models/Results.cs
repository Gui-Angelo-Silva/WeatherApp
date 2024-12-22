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
		/// Obtém ou define a condição do clima da cidade, geralmente representada por uma palavra-chave
		/// ou identificador que descreve o tipo de clima (ex.: "ensolarado", "nublado", etc.).
		/// </summary>
		/// <value>
		/// Um identificador ou string que descreve a condição do clima da cidade, como "clear", "cloudy", etc.
		/// </value>
		public string condition_slug { get; set; }

		/// <summary>
		/// Obtém ou define a descrição detalhada do clima da cidade, fornecendo uma explicação textual
		/// do estado atual do tempo (ex.: "céu limpo", "chuvas esparsas", etc.).
		/// </summary>
		/// <value>
		/// Uma string que descreve de forma detalhada o clima da cidade, como "Céu claro com poucas nuvens", "Chuva leve", etc.
		/// </value>
		public string description { get; set; }

		/// <summary>
		/// Obtém ou define a data da busca do clima para a cidade.
		/// </summary>
		/// <value>
		/// A data no formato "dd/MM", representando o dia da busca do clima.
		/// </value>
		public string date { get; set; }
	}
}