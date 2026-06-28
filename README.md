# Sistemas Web com ASP.NET - API

Projeto desenvolvido em **ASP.NET Core Web API** para uma aplicação de streaming de música chamada **Vibra**.

A API permite gerenciar usuários, bandas, álbuns, músicas, playlists, planos, assinaturas, cartões, favoritos e transações.

## Tecnologias utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger
* AutoMapper
* Arquitetura em camadas / DDD

## Estrutura do projeto

```bash
Vibra/
├── DomainModel/              # Entidades, DTOs, interfaces e regras de domínio
├── Vibra.DomainService/      # Serviços de domínio
├── Vibra.Infra/              # Contexto do banco, migrations, mappings e repositórios
├── Music.Api/                # Projeto principal da API
└── Vibra.sln                 # Solution do projeto
```

## Pré-requisitos

Antes de rodar o projeto, é necessário ter instalado:

* Visual Studio 2022 ou superior
* .NET SDK 8
* SQL Server ou SQL Server Express
* SQL Server Management Studio, opcional
* Git, opcional

## Como rodar o projeto

### 1. Clonar o repositório

```bash
git clone git@github.com:Caua-Holanda/Sistemas_Web_com_ASP_NET-API.git
```

Depois acesse a pasta do projeto:

```bash
cd Sistemas_Web_com_ASP_NET-API/Vibra
```

## 2. Restaurar os pacotes

Execute o comando abaixo dentro da pasta onde está o arquivo `Vibra.sln`:

```bash
dotnet restore
```

## 3. Configurar a conexão com o banco de dados

Abra o arquivo:

```bash
Music.Api/appsettings.json
```

Verifique a string de conexão com o SQL Server.

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VibraDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Caso use SQL Server Express, a conexão pode ficar parecida com:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VibraDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## 4. Aplicar as migrations no banco

Dentro da pasta `Vibra`, execute:

```bash
dotnet ef database update --project Vibra.Infra --startup-project Music.Api
```

Esse comando cria o banco de dados e as tabelas a partir das migrations do Entity Framework Core.

Caso o comando `dotnet ef` não funcione, instale a ferramenta com:

```bash
dotnet tool install --global dotnet-ef
```

Depois execute novamente:

```bash
dotnet ef database update --project Vibra.Infra --startup-project Music.Api
```

## 5. Rodar a API

Execute o projeto principal da API:

```bash
dotnet run --project Music.Api
```

A API será iniciada localmente.

Normalmente o Swagger ficará disponível em um endereço parecido com:

```bash
https://localhost:7000/swagger
```

ou

```bash
http://localhost:5000/swagger
```

A porta exata pode variar de acordo com a configuração do Visual Studio ou do arquivo `launchSettings.json`.

## Rodando pelo Visual Studio

Também é possível rodar o projeto pelo Visual Studio:

1. Abra o arquivo `Vibra.sln`
2. Defina o projeto `Music.Api` como projeto de inicialização
3. Verifique a string de conexão em `appsettings.json`
4. Abra o Package Manager Console
5. Rode o comando:

```bash
Update-Database
```

6. Pressione `F5` ou clique em `Iniciar`

## Principais recursos da API

A API possui endpoints para gerenciamento de:

* Usuários
* Contas
* Cartões
* Planos
* Assinaturas
* Bandas
* Álbuns
* Músicas
* Playlists
* Associação de músicas em playlists
* Bandas favoritas
* Músicas favoritas
* Transações

## Documentação da API

A documentação dos endpoints pode ser acessada pelo Swagger após rodar o projeto:

```bash
/swagger
```

Exemplo:

```bash
https://localhost:7000/swagger
```

## Observações

* O projeto utiliza Entity Framework Core para comunicação com o banco de dados.
* As migrations estão no projeto `Vibra.Infra`.
* O projeto principal de execução é o `Music.Api`.
* O banco utilizado é SQL Server.
* A API foi estruturada em camadas para separar responsabilidades entre domínio, serviço, infraestrutura e apresentação.

## Comandos úteis

Restaurar pacotes:

```bash
dotnet restore
```

Compilar o projeto:

```bash
dotnet build
```

Aplicar migrations:

```bash
dotnet ef database update --project Vibra.Infra --startup-project Music.Api
```

Rodar a API:

```bash
dotnet run --project Music.Api
```
