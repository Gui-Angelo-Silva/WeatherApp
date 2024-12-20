using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Config
{
	/// <summary>
	/// Classe que contém a string de conexão com o banco de dados
	/// </summary>
	public class DatabaseConfig
	{
		/// <summary>
		/// Obtém a string de conexão com o banco de dados.
		/// </summary>
		/// <remarks>
		/// A string de conexão está configurada para utilizar um servidor SQL Local (Ex:GUIGAS\SQLEXPRESS) e um banco de dados
		/// chamado "WeatherForecastDB" com autenticação integrada do Windows.
		/// </remarks>
		/// <value>
		/// Retorna a string de conexão configurada para o banco de dados.
		/// </value>
		public static string ConnectionString => "Server=GUIGAS\\SQLEXPRESS;Database=WeatherForecastDB;Integrated Security=True;";
	}
}