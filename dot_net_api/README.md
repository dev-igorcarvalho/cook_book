# DotNet Core Api

## Index:

- [Instalação dos plugins do VS code](#instalação-dos-plugins-do-vs-code)
- [Preparação do banco de dados em container](#Preparação-do-banco-de-dados-em-container)
- [Criação a aplicação com o CLI](#criação-a-aplicação-com-o-cli)
- [Instalação das dependências do Entity Framework](#instalação-das-dependências-do-entity-framework)
- [Criação e configuração do DB context](#criação-e-configuração-do-db-context)
- [Instalação das dependências do Identity Framework](#instalação-das-dependências-do-identity-framework)
- [Instalação das dependências do JWT](#instalação-das-dependências-do-jwt)
- [Criação e configuração do Identity](#criação-e-configuração-do-identity)
- [Criação dos modelos de entidade relacional](#criação-dos-modelos-de-entidade-relacional)
  - Entidade sem relacionamento
  * Relacionamento de entidades 1-1
  * Relacionamento de entidades 1-N
  * Relacionamento de entidades N-N
  * Entidade não gerenciada pelo db context
- Adicionando validação dos campos das entidades
- Editar padroes especificos das migrantions no bdContex modelBuilder
- Criação das migrations
- Criação seed migrations para addicionar conteúdo ao banco
- Criação exception handler
- Criação http interceptor
- Criação do padrao repositry para os acessos ao banco
- Criação dos mappers de entidades pra Dtos
- Criação e configuração do identity framework pra trabalhar com tokens
- Criação endpoints de autenticação
- Criação de um controller com endpoints protegiddos
- Criação dos controllers com as 5 opçoes basicas de endpoints
  - GET
    - Simples
    - Com path variable
    - Com query params
    - Com paginação
  - POST
  - PUT
  - DELETE

---

### Instalação dos plugins do VS code:

#### Pré-Requisitos:

- Ter o VS Code instalado.

_Com o VS code aberto aperte as teclas CTRL+P e rode individualente os comandos abaixo:_

    ext install ms-dotnettools.csharp
    ext install formulahendry.dotnet
    ext install k--kato.docomment
    ext install jmrog.-nuget-package-manager
    ext install jchannon.csharpextensions
    ext install Fudge.auto-using
    ext install ms-dotnettools.vscode-dotnet-pack

### Preparação do banco de dados em container:

#### Pré-Requisitos:

- Docker e Docker-compose

#### Instruções:

- Criar um arquivo chamado < docker-compose.yml > de accordo com o modelo abaixo:

_Obs: A senha precisa ter no mínimo 8 digitos, letras maiúsculas, minúsculas, números, e simbolos. Nada de tomcat_tomcat._

        version: '3'
        volumes:
          database:
        services:
          db:
            container_name: dg-sql-server
            image: mcr.microsoft.com/mssql/server:2017-latest-ubuntu
            ports:
              - "1433:1433"
            volumes:
              - database:/var/lib/sqlserver/data
              - ./init.sql:/docker-entrypoint-initdb.d/init.sql
            environment:
              SA_PASSWORD: "ESCOLHA_UMA_SENHA"
              ACCEPT_EULA: "Y"

- No terminal, na mesma pasta do arquivo, rodar o comando:

        docker-compose up -d

- Conexão com o db:
  - _Url: localhost:1433_
  - _Usuário: SA_
  - \_Senha: utilizar a senha do arquivo

### Criação a aplicação com o CLI

#### Pré-Requisitos:

- DotNet SDK
- No terminal, na mesma pasta destino do projeto, rodar os comandos:

        dotnet new webapi
        dotnet new gitignore

- Após a crição do projeto, abrir o mesmo no VS code com o comando:
  code .

### Instalação das dependências do Entity Framework

- No terminal, na mesma pasta destino do projeto, rodar o comando:

      dotnet add package Microsoft.EntityFrameworkCore -v 3.1.21
      dotnet add package Microsoft.EntityFrameworkCore.tools -v 3.1.21
      dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 3.1.21

### Criação e configuração do DB context

- Criar a classe de contexto conforme o código a seguir:

      using Microsoft.EntityFrameworkCore;

      namespace dot_net_api.Context
      {
        public class ApplicationDbContext : DbContext
          {
              public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              : base(options)
              {}
          }
      }

- Configurar o uso do novo contexto na classe Startup.cs
  _Adicionar a configuração do Db Context no método abaixo:_

  _Antes:_

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

  _Depois:_

        public void ConfigureServices(IServiceCollection services)
        {
          services
            .AddDbContext<ApplicationDbContext>
            (
              options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
          services.AddControllers();
        }

* Definir os detalhes da conexão com o banco na classe appsettings.json

  _Adicionar a configuração abaixo na raiz do objeto:_

      "ConnectionStrings": {
      "DefaultConnection": "Server=url_do_bancoo;DataBase=nome_da_db:Uid=nome_usuario;Pwd=senha_usuario"
      },

### Instalação das dependências do Identity Framework

### Instalação das dependências do JWT

### Criação e configuração do Identity

### Criação dos modelos de entidade relacional

#### Entidade sem relacionamento

- Criar um classe basica

  _[Exemplo]()_

        namespace dot_net_api.Models
        {
            public class Evento
            {
                public string Nome { get; set; }
                public string Local { get; set; }
                public int QuantidadeParticipantes { get; set; }
            }
        }

* Configuar o db context para gerenciar a entidade

  _Na classe [ApplicationDbContext]() adicionar atributo público DbSet<T>_

      public DbSet<Evento> Eventos { get; set; }

#### Relacionamento de entidades 1-1

#### Relacionamento de entidades 1-N

#### Relacionamento de entidades N-N

#### Entidade não gerenciada pelo db context
