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
  - [Entidade sem relacionamento](#entidade-sem-relacionamento)
  * [Entidade não gerenciada pelo db context](#entidade-não-gerenciada-pelo-db-context)
  * [Relacionamento de entidades 1-1](#relacionamento-de-entidades-1-1)
  * [Relacionamento de entidades 1-N](#relacionamento-de-entidades-1-N)
  * [Relacionamento de entidades N-N](#relacionamento-de-entidades-N-N)
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

## Instalação dos plugins do VS code:

### Pré-Requisitos:

- Ter o VS Code instalado.

_Com o VS code aberto aperte as teclas CTRL+P e rode individualente os comandos abaixo:_

    ext install ms-dotnettools.csharp
    ext install formulahendry.dotnet
    ext install k--kato.docomment
    ext install jmrog.-nuget-package-manager
    ext install jchannon.csharpextensions
    ext install Fudge.auto-using
    ext install ms-dotnettools.vscode-dotnet-pack

## Preparação do banco de dados em container:

### Pré-Requisitos:

- Docker e Docker-compose

### Instruções:

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
  - _Senha: utilizar a senha do arquivo_

## Criação a aplicação com o CLI

### Pré-Requisitos:

- DotNet SDK
- No terminal, na mesma pasta destino do projeto, rodar os comandos:

        dotnet new webapi
        dotnet new gitignore

- Após a crição do projeto, abrir o mesmo no VS code com o comando:
  code .

## Instalação das dependências do Entity Framework

- No terminal, na mesma pasta destino do projeto, rodar o comando:

      dotnet tool install --global dotnet-ef
      dotnet add package Microsoft.EntityFrameworkCore -v 3.1.21
      dotnet add package Microsoft.EntityFrameworkCore.tools -v 3.1.21
      dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 3.1.21

## Criação e configuração do DB context

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

## Instalação das dependências do Identity Framework

## Instalação das dependências do JWT

## Criação e configuração do Identity

## Criação dos modelos de entidade relacional

_Para mais detalhes verificar a [documentação](https://docs.microsoft.com/pt-br/ef/core/modeling/relationships?tabs=data-annotations-simple-key%2Csimple-key%2Cfluent-api)_

### Entidade sem relacionamento

- Criar um classe basica

  _[Exemplo](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Evento.cs)_

  _Por convenção, uma propriedade chamada Id será configurada como a chave primária de uma entidade._

        namespace dot_net_api.Models
        {
            public class Evento
            {
                public string Id { get; set; }
                public string Nome { get; set; }
                public string Local { get; set; }
                public int QuantidadeParticipantes { get; set; }
            }
        }

* Configurar o db context para gerenciar a entidade

  _Na classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs) adicionar atributo público DbSet<T>_

      public DbSet<Evento> Eventos { get; set; }

### Entidade com chave primaria composta

### Entidade não gerenciada pelo Db Context

_Uma entidade nao gerenciada pelo Db Context é uma classe de entidade que nao foi acrescentada no atributo público DbSet<T> da classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs)_

_É um recurso que pode ser usado quando temos alguma entidade que depende de outra para ser gerenciada e nao deve ser acessada ou modificada diretamente._

_Como [exemplo](#exemplo) podemos citar uma relação entre Cliente e Endereço, onde por um questão de design não desejamos que o endereço seja manipulado independentemente._
_Obs: Não existe regra definida para exemplo a cima, é uma questão de escolha que varia de projeto para projeto de acordo com suas necessidades._

#### **Exemplo**:

_Neste exemplo vamos usar um relacionamento 1-1_

- Criar a entidade Pai (gerenciada pelo db context)

      namespace dot_net_api.Models
      {
          public class Cliente
          {
              public int Id { get; set; }
              public string Nome { get; set; }
              public Endereco Endereco { get; set; }
          }
      }

- Configurar o db context para gerenciar a entidade pai.

  _Na classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs) adicionar atributo público DbSet<T>_

      public DbSet<Cliente> Clientes { get; set; }

- Criar a entidade Filha (não gerenciada pelo db context)

  _É possível observar que essa entidade não tem um id_

        namespace dot_net_api.Models
        {
            public class Endereco
            {
                public Cliente Cliente { get; set; }
                public string Logradouro { get; set; }
                public int Numero { get; set; }
                public string Complemento { get; set; }
                public string Bairro { get; set; }
            }
        }

- Configuração da classe filha no Db Context

  _Na classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs) sobrescrever o methodo OnModelCreating(ModelBuilder modelBuilder) para configurar detalhes da classe Endereco_

      protected override void (modelBuilder){}

  _Incluir um nome para a tabela da classe._

      protected override void OnModelCreati(ModelBuilder modelBuilder)
      {
        modelBuilder.Entity<Endereco>().ToTable("Enderecos");
      }

  _Incluir uma Shadow Porperty para servir de chave estrangeira para a relação com o cliente._

      protected override void OnModelCreati(ModelBuilder modelBuilder)
      {
        modelBuilder
          .Entity<Endereco>().ToTable Enderecos");

        modelBuilder
          .Entity<Endereco>().Property<int> ClienteId");
      }

  _Configurar uma chave primaria para a tabela. No caso vamos usar o clienteId novamente, pois como a entidade filha só vai ser acessada a partir da entidade pai queremos que ambas tenham o mesmo id_

      protected override void OnModelCreati(ModelBuilder modelBuilder)
      {
        modelBuilder
          .Entity<Endereco>().ToTable Enderecos");

        modelBuilder
          .Entity<Endereco>().Property<int> ClienteId");

        modelBuilder
          .Entity<Endereco>().HasKey("ClienteId");
      }

### Relacionamento de entidades 1-1

_Relações um para um têm uma propriedade de navegação de referência em ambos os lados. Eles seguem as mesmas convenções que as relações um-para-muitos, mas um índice exclusivo é introduzido na propriedade Foreign Key para garantir que apenas um dependente esteja relacionado a cada entidade de segurança._

_O EF escolherá uma das entidades como dependente, com base em sua capacidade de detectar uma propriedade de chave estrangeira. se a entidade incorreta for escolhida como dependente, você poderá usar a API Fluent para corrigir isso._

_Entidade principal:_

    namespace dot_net_api.Models
    {
        public class Pessoa
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public CarteiraNacionalHabilitacao Cnh { get; set; }

        }
    }

_Entidade dependente:_

    namespace dot_net_api.Models
    {
        public class CarteiraNacionalHabilitacao
        {
            public int Id { get; set; }
            public int Numero { get; set; }
            public string OrgaoExpeditor { get; set; }
            public Pessoa Pessoa { get; set; }
            public int PessoaId { get; set; }
        }
    }

### Relacionamento de entidades 1-N

_Entidade principal:_

    using System.Collections.ObjectModel;
    using System.Collections.Generic;

    namespace dot_net_api.Models
    {
      public class Categoria
        {
          public Categoria()
          {
            Produtos = new Collection<Produto>();
          }

            public int id { get; set; }
            public string Nome { get; set; }
            public ICollection<Produto> Produtos { get; set; }

        }
    }

_Entidade dependente:_

    namespace dot_net_api.Models
    {
        public class Produto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public Categoria Categoria { get; set; }
            public int CategoriaId { get; set; }
        }
    }

### Relacionamento de entidades N-N

_Relações muitos para muitos exigem uma propriedade de navegação de coleção em ambos os lados. Eles serão descobertos por convenções como outros tipos de relações._

_A maneira como essa relação é implementada no banco de dados é por uma tabela de junção que contém chaves estrangeiras para ambos entidades_

_Entidade A_

    using System.Collections.Generic;
    namespace dot_net_api.Models
    {
        public class Carro
        {
            public int Id { get; set; }
            public string Modelo { get; set; }
            public string Cor { get; set; }

            public ICollection<Motorista> Condutores { get; set; }
        }
    }

_Entidade B_

    using System.Collections.Generic;
    namespace dot_net_api.Models
    {
        public class Motorista
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public ICollection<Carro> Carros { get; set; }
        }
    }

### Entidade de junção

### Anotações de entidades
