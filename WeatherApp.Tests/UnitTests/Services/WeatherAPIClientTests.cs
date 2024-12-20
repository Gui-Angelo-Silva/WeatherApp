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
		/// Testa a comparação de temperaturas quando a temperatura atual é maior que a temperatura anterior.
		/// O teste deve garantir que a função retorna "Aumentou" quando a temperatura atual for maior.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde a temperatura atual (35°C) é maior que a anterior (30°C).
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double)"/>
		/// retorne o valor "Aumentou".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsAumentou_WhenCurrentTempIsHigher()
		{
			// Act: Chama o método com temperaturas 30°C e 35°C
			var result = _weatherApiClient.CompareTemperatures(30, 35);

			// Assert: Verifica se o resultado é "Aumentou"
			Assert.AreEqual("Aumentou", result);
		}

		/// <summary>
		/// Testa a comparação de temperaturas quando a temperatura atual é menor que a temperatura anterior.
		/// O teste deve garantir que a função retorna "Diminuíu" quando a temperatura atual for menor.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde a temperatura atual (25°C) é menor que a anterior (30°C).
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double)"/>
		/// retorne o valor "Diminuíu".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsDiminuíu_WhenCurrentTempIsLower()
		{
			// Act: Chama o método com temperaturas 30°C e 25°C
			var result = _weatherApiClient.CompareTemperatures(30, 25);

			// Assert: Verifica se o resultado é "Diminuíu"
			Assert.AreEqual("Diminuíu", result);
		}

		/// <summary>
		/// Testa a comparação de temperaturas quando as temperaturas são iguais.
		/// O teste deve garantir que a função retorna "Sem mudança" quando as temperaturas forem iguais.
		/// </summary>
		/// <remarks>
		/// Este teste simula o caso onde as temperaturas são iguais (30°C e 30°C).
		/// Espera-se que o método <see cref="WeatherApiClient.CompareTemperatures(double, double)"/>
		/// retorne o valor "Sem mudança".
		/// </remarks>
		[Test] // Indica que este método é um teste
		public void CompareTemperatures_ReturnsSemMudanca_WhenCurrentTempIsEqual()
		{
			// Act: Chama o método com temperaturas 30°C e 30°C
			var result = _weatherApiClient.CompareTemperatures(30, 30);

			// Assert: Verifica se o resultado é "Sem mudança"
			Assert.AreEqual("Sem mudança", result);
		}
	}
}