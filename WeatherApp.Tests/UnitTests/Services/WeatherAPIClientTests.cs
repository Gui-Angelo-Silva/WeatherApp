using NUnit.Framework;
using WeatherApp.Services;

namespace WeatherApp.Tests
{
	/// <summary>
	/// Classe de testes unitários para a classe <see cref="WeatherApiClient"/>.
	/// Testa a lógica de comparação de temperaturas, garantindo que os valores de mudança de temperatura
	/// (Aumentou, Diminuíu ou Sem mudança) sejam retornados corretamente.
	/// </summary>
	[TestFixture] // Indica que esta classe contém testes unitários
	public class WeatherApiClientTests
	{
		private WeatherApiClient _weatherApiClient; // Instância da classe a ser testada

		/// <summary>
		/// Método que prepara o ambiente de testes antes da execução de cada teste.
		/// Cria uma nova instância da classe <see cref="WeatherApiClient"/> para cada teste.
		/// </summary>
		[SetUp] // Marca o método para ser executado antes de cada teste
		public void Setup()
		{
			_weatherApiClient = new WeatherApiClient(); // Inicializa a classe para ser testada
		}

		/// <summary>
		/// Testa a comparação de temperaturas quando a temperatura mínima e a máxima atual são maiores que as do dia anterior.
		/// O teste deve garantir que a função retorna "Aumentou" quando a temperatura atual for maior.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde a temperatura mínima e a máxima de hoje são maiores que as de ontem.
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double, double, double)"/>
		/// retorne o valor "Aumentou".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsAumentou_WhenBothCurrentTempsAreHigher()
		{
			// Act: Chama o método com temperaturas mínimas e máximas de 25°C/30°C e 28°C/35°C
			var result = _weatherApiClient.CompareTemperatures(25, 30, 28, 35);

			// Assert: Verifica se o resultado é "Aumentou"
			Assert.AreEqual("Aumentou", result);
		}

		/// <summary>
		/// Testa a comparação de temperaturas quando a temperatura mínima e a máxima atual são menores que as do dia anterior.
		/// O teste deve garantir que a função retorna "Diminuíu" quando a temperatura atual for menor.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde a temperatura mínima e a máxima de hoje são menores que as de ontem.
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double, double, double)"/>
		/// retorne o valor "Diminuíu".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsDiminuíu_WhenBothCurrentTempsAreLower()
		{
			// Act: Chama o método com temperaturas mínimas e máximas de 30°C/35°C e 25°C/30°C
			var result = _weatherApiClient.CompareTemperatures(30, 35, 25, 30);

			// Assert: Verifica se o resultado é "Diminuíu"
			Assert.AreEqual("Diminuíu", result);
		}

		/// <summary>
		/// Testa a comparação de temperaturas quando uma temperatura é maior e a outra menor.
		/// O teste deve garantir que a função retorna "Sem mudança" quando as temperaturas não indicam variação clara.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde a temperatura mínima de hoje é maior e a máxima de hoje é menor que as de ontem.
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double, double, double)"/>
		/// retorne o valor "Sem mudança".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsSemMudanca_WhenOneTempIsHigherAndOtherIsLower()
		{
			// Act: Chama o método com temperaturas mínimas e máximas de 28°C/32°C e 28°C/32°C
			var result = _weatherApiClient.CompareTemperatures(28, 32, 28, 32);

			// Assert: Verifica se o resultado é "Sem mudança"
			Assert.AreEqual("Sem mudança", result);
		}

		/// <summary>
		/// Testa a comparação de temperaturas quando as temperaturas mínimas e máximas são iguais.
		/// O teste deve garantir que a função retorna "Sem mudança" quando as temperaturas forem iguais.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde as temperaturas mínimas e máximas de hoje são iguais às de ontem (30°C/30°C).
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double, double, double)"/>
		/// retorne o valor "Sem mudança".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsSemMudanca_WhenCurrentTempsAreEqual()
		{
			// Act: Chama o método com temperaturas mínimas e máximas de 30°C/30°C e 30°C/30°C
			var result = _weatherApiClient.CompareTemperatures(30, 30, 30, 30);

			// Assert: Verifica se o resultado é "Sem mudança"
			Assert.AreEqual("Sem mudança", result);
		}
	}
}