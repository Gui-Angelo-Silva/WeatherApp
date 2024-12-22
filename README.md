# 🌦️ WeatherApp - Aplicativo de Previsão do Tempo com HGBrasil API

Este projeto permite consultar e salvar dados de previsão do tempo utilizando a API do HGBrasil e armazená-los em um banco de dados.

<br />

## 📸 Capturas de Tela

### 1. Tela de Previsão do Tempo com Grid

<div style="display: grid; grid-template-columns: 1fr 1fr; gap: 10px;">
  <img src="https://github.com/user-attachments/assets/6527613e-f65b-4fc6-9110-37e85d74c4c7" alt="Tela de Previsão do Tempo com Grid" style="width: 100%; border: 1px solid #ccc; border-radius: 5px;" />
</div>

### 2. Tela de Previsão do Tempo sem Grid

<img src="https://github.com/user-attachments/assets/a73f6fa3-5458-4507-a192-351a9e514d2c" alt="Tela de Previsão do Tempo 1"  />
<img src="https://github.com/user-attachments/assets/f75597bd-a033-4581-ada2-567a379eeaa3" alt="Tela de Previsão do Tempo 2"  />

<br />

## ✅ Pré-requisitos

Antes de começar, você precisa ter as seguintes ferramentas instaladas:

- 🛠️ [.NET 8+](https://dotnet.microsoft.com/download) (para garantir que a aplicação seja executada corretamente)
- 🗄️ [SQL Server](https://www.microsoft.com/pt-br/sql-server) (ou outro banco de dados de sua escolha)
- 🔑 Conta na [API do HGBrasil](https://hgbrasil.com), que fornecerá o link da API para as previsões do tempo.

<br />

## 🌐 Passos para obter o link da API:
1. 🔗 Acesse o [site do HGBrasil](https://console.hgbrasil.com/documentation/weather#obter-cidade-por-codigo).
2. 🔍 Procure por Obter cidade por código.
3. 📋 Copie o link da API e insira o código correspondente à sua cidade (Ex: Jales = 457398). O link da API será utilizado como parâmetro de busca para obter os dados da previsão do tempo da sua cidade.

<br />

## 🗄️ Configuração do Banco de Dados

1. **Criar o Banco de Dados**:
   - Crie um banco de dados no SQL Server (ou outro banco de sua escolha).
   - Coloque o nome do banco de dados de WeatherForecastDB.
   - Abaixo está o script SQL para criar a tabela onde as previsões de tempo serão armazenadas:

```sql
CREATE TABLE WeatherForecast (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Date NVARCHAR(10) NOT NULL,
    Weekday NVARCHAR(50),
    MaxTemperature FLOAT NOT NULL,
    MinTemperature FLOAT NOT NULL,
    Humidity FLOAT NOT NULL,
	 Cloudiness INT NOT NULL,
    Rain FLOAT NOT NULL,
	 RainProbability FLOAT NOT NULL,
	 WindSpeedy NVARCHAR(14) NOT NULL,
	 Sunrise NVARCHAR(8) NOT NULL,
	 Sunset NVARCHAR(8) NOT NULL,
	 MoonPhase NVARCHAR(20) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
	 Condition NVARCHAR(20) NOT NULL,
    TemperatureChange NVARCHAR(50) NULL
);

INSERT INTO WeatherForecast (
    Date, Weekday, MaxTemperature, MinTemperature, Humidity, Cloudiness, 
    Rain, RainProbability, WindSpeedy, Sunrise, Sunset, MoonPhase, 
    Description, Condition, TemperatureChange
)
VALUES (
    '22/12',        -- Date
    'Dom',          -- Weekday
    27,             -- MaxTemperature
    21,             -- MinTemperature
    75,             -- Humidity
    100,            -- Cloudiness
    7.27,           -- Rain
    100,            -- RainProbability
    '3.36 km/h',    -- WindSpeedy
    '05:40 am',     -- Sunrise
    '07:01 pm',     -- Sunset
    'Última fase',  -- MoonPhase (traduzido)
    'Chuva',        -- Description
    'Chuva',        -- Condition (traduzido)
    NULL            -- TemperatureChange (não foi fornecido, então é NULL)
);
```

2. **Configuração da String de Conexão**:
   - A string de conexão está configurada no código da aplicação.
   - **Server**: Endereço do servidor de banco de dados (ex.: GUIGAS\\SQLEXPRESS ou o nome do seu servidor).
   - **Database**: Nome do banco de dados onde os dados serão armazenados (WeatherForecastDB).
   - **Integrated Security**: Utiliza a autenticação do Windows para acessar o banco de dados.
   - A string é a seguinte:
```csharp
public static string ConnectionString => "Server=GUIGAS\\SQLEXPRESS;Database=WeatherForecastDB;Integrated Security=True;";
```

<br />

## 🌦️ Configuração da API

Para consultar dados de previsão do tempo, o projeto utiliza a API do HGBrasil. Siga as instruções abaixo para configurar as credenciais da API.

1. **Obter a chave da API**:
   - Acesse o site da API do HGBrasil: [https://hgbrasil.com](https://hgbrasil.com).
   - Registre-se para obter uma chave de API.

2. **Configurar a URL da API**:
   - A URL para consultar a previsão do tempo é a seguinte:

```csharp
string url = "https://api.hgbrasil.com/weather?woeid=457398";
```

<br />

## 🚀 Executando o Projeto

1. **Clonar o Repositório**:
   - Clone o repositório para sua máquina local:

```cmd
git clone https://github.com/Gui-Angelo-Silva/WeatherApp.git
cd WeatherApp
```

2. **Abrir o Projeto**:
   - Abra a solução no Visual Studio (ou outra IDE de sua preferência):
```cmd
WeatherAppSolution.sln
```
  
3. **Executar o projeto**:
   - Execute o projeto pressionando F5 ou Ctrl + F5 para iniciar o aplicativo em modo de depuração ou sem depuração.

4. **Testando o Projeto**:
   - Ao rodar o aplicativo, ele irá se conectar à API do HGBrasil para obter as previsões do tempo e exibir as informações na interface gráfica.
   - A previsão será armazenada no banco de dados configurado, permitindo futuras consultas e visualizações.

5. **Configuração do Banco de Dados**:
   - Lembre-se de configurar a string de conexão corretamente e verificar se o banco de dados está acessível.
