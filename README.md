# WeatherApp

Este projeto permite consultar e salvar dados de previsão do tempo utilizando a API do HGBrasil e armazená-los em um banco de dados.

## Pré-requisitos

Antes de começar, você precisa ter as seguintes ferramentas instaladas:

- .NET Core ou .NET 6+ (dependendo da versão do projeto)
- SQL Server (ou outro banco de dados de sua escolha)
- Conta no HGBrasil API para obter as credenciais de acesso

## Configuração do Banco de Dados

1. **Criar o Banco de Dados**:
   - Crie um banco de dados no SQL Server (ou outro banco de sua escolha).
   - Abaixo está o script SQL para criar a tabela onde as previsões de tempo serão armazenadas:

```sql
CREATE TABLE WeatherForecast (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Date NVARCHAR(10) NOT NULL,
    Weekday NVARCHAR(50),
    MaxTemperature FLOAT NOT NULL,
    MinTemperature FLOAT NOT NULL,
    Humidity FLOAT NOT NULL,
    Rain FLOAT NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    TemperatureChange NVARCHAR(50) NULL
);
```

2. **Configuração da String de Conexão**:
   - A string de conexão está configurada no código da aplicação.
   - Server: Endereço do servidor de banco de dados (ex.: GUIGAS\\SQLEXPRESS ou o nome do seu servidor).
   - Database: Nome do banco de dados onde os dados serão armazenados (WeatherForecastDB).
   - Integrated Security: Utiliza a autenticação do Windows para acessar o banco de dados.
   - A string é a seguinte:
```c#

public static string ConnectionString => "Server=GUIGAS\\SQLEXPRESS;Database=WeatherForecastDB;Integrated Security=True;";
```

## Configuração do Banco de Dados

Para consultar dados de previsão do tempo, o projeto utiliza a API do HGBrasil. Siga as instruções abaixo para configurar as credenciais da API.

1. **Obter a chave da API**:
   - Acesse o site da API do HGBrasil: https://hgbrasil.com.
   - Registre-se para obter uma chave de API.

2. **Configuração da URL da API**:
   - A URL para consultar a previsão do tempo é a seguinte:

```c#
string url = "https://api.hgbrasil.com/weather?woeid=457398";
```

## Executando o Projeto

1. **Clonar o Repositório**:

```cmd
git clone https://github.com/Gui-Angelo-Silva/WeatherApp.git
cd WeatherApp
```

2. **Abra a pasta WeatherAppSolution**:
   - Dê dois cliques em cima de WeatherAppSolution.sln
  
3. **Executar o projeto**
   - Aperte as seguintes teclas: F5 ou fn + F5
