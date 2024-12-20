using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
	/// <summary>
	/// Representa os dados da previsão do tempo retornados pela API.
	/// </summary>
	public class WeatherData
	{
		/// <summary>
		/// Obtém ou define os resultados da previsão do tempo para a cidade especificada.
		/// </summary>
		/// <value>
		/// Um objeto <see cref="Results"/> que contém informações sobre a cidade, previsão e temperatura.
		/// </value>
		public Results results { get; set; }
	}
}