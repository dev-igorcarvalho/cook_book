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
  * [Entidade com chave primaria composta](#entidade-com-chave-primaria-composta)
  * [Entidade não gerenciada pelo db context](#entidade-não-gerenciada-pelo-db-context)
  * [Relacionamento de entidades 1-1](#relacionamento-de-entidades-1-1)
  * [Relacionamento de entidades 1-N](#relacionamento-de-entidades-1-N)
  * [Relacionamento de entidades N-N](#relacionamento-de-entidades-N-N)
- [Anotações de entidades](#anotações-de-entidades)
- [Editar padroes especificos das migrantions no bdContex modelBuilder](#editar-padroes-especificos-das-migrantions-no-bdContex-modelBuilder)
- [Criação das migrations](#criação-das-migrations)Criação das migrations
- [Criação de Seeder usando migrations para addicionar conteúdo ao banco](#criação-de-Seeder-usando-migrations-para-addicionar-conteúdo-ao-banco)
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

_Para criar uma chave primária composta é necessario configurar suas caracteristicas no DbContext de acordo com o [exemplo](#configurando-a-chave-composta-da-entidade-de-junção)_

### Entidade não gerenciada pelo Db Context

_Uma entidade nao gerenciada pelo Db Context é uma classe de entidade que nao foi acrescentada no atributo público DbSet<T> da classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs)_

_É um recurso que pode ser usado quando temos alguma entidade que depende de outra para ser gerenciada e nao deve ser acessada ou modificada diretamente._

_Como [exemplo](#exemplo) podemos citar uma relação entre Cliente e Endereço, onde por um questão de design não desejamos que o endereço seja manipulado independentemente._
_Obs: Não existe regra definida para exemplo a cima, é uma questão de escolha que varia de projeto para projeto de acordo com suas necessidades._

#### **Exemplo**:

_Neste exemplo vamos usar um relacionamento 1-1_

- [Criar a entidade Pai (gerenciada pelo db context)](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Cliente.cs)

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

- [Criar a entidade Filha (não gerenciada pelo db context)](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Endereco.cs)

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

  _Na classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs) sobrescrever o methodo **OnModelCreating(ModelBuilder modelBuilder)**para configurar detalhes da classe Endereco_

      protected override void OnModelCreating(modelBuilder){}

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

- [Entidade principal:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Pessoa.cs)

      namespace dot_net_api.Models
      {
          public class Pessoa
          {
              public int Id { get; set; }
              public string Nome { get; set; }
              public CarteiraNacionalHabilitacao Cnh { get; set; }

          }
      }

- [Entidade dependente:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/CarteiraNacionalHabilitacao.cs)

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

- [Entidade principal:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Categoria.cs)

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

- [Entidade dependente:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Produto.cs)

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

_Na versão usada do EF usada só é possivel fazer o relacionamento N-N com uma entidade de junção._

- [Entidade A:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Carro.cs)

      using System.Collections.Generic;
      using System.Collections.ObjectModel;

      namespace dot_net_api.Models
      {
          public class Carro
          {
              public Carro()
              {
                  Motoristas = new Collection<MotoristaCarro>();
              }
              public int Id { get; set; }
              public string Modelo { get; set; }
              public string Cor { get; set; }
              public ICollection<MotoristaCarro> Motoristas { get; set; }
          }
      }

- [Entidade B:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/Motorista.cs)

      using System.Collections.Generic;
      using System.Collections.ObjectModel;

      namespace dot_net_api.Models
      {
          public class Motorista
          {
              public Motorista()
              {
                  Carros = new Collection<MotoristaCarro>();
              }
              public int Id { get; set; }
              public string Nome { get; set; }
              public ICollection<MotoristaCarro> Carros { get; set; }
          }
      }

* [Entidade de junção:](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Models/MotoristaCarro.cs)

      namespace dot_net_api.Models
      {
          public class MotoristaCarro
          {
              public int MotoristaId { get; set; }
              public int CarroId { get; set; }
              public Carro Carro { get; set; }
              public Motorista Motorista { get; set; }
          }
      }

  _Como a entidade de junção é dependente das outras duas entidades, não desejamos que ela seja independentemente gerenciada pelo Db Context. Não é necessario que a mesma tenha um Id próprio, porém é nessario que ela tenha uma chave privada No caso em questao essa chave primária será uma chave composta conforme o exemplo abaixo:_

* #### Configurando a chave composta da entidade de junção:

  _Na classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs) sobrescrever o methodo **OnModelCreating(ModelBuilder modelBuilder)** para configurar detalhes da chave composta da entidade de junção._

      protected override void OnModelCreating(modelBuilder){}

  _Indicar para o modelBuilder que a entidade de junção vai ter uma chave primária, passando como parâmetro um objeto anônimo composto pelos campos CarroId e MotoristaID._

      protected override void OnModelCreati(ModelBuilder modelBuilder)
      {
        modelBuilder.Entity<MotoristaCarro>()
                .HasKey(o => new { o.CarroId, o.MotoristaId });
      }

### Anotações de entidades

- [Table("nome_tabela")]

  _Nomeia a tabela no banco_

      [Table("nome_tabela")]
      public class SuaClasse
      {
      public int ID { get; set; }
      }

- [Column("nome_coluna")]

  _Nomeia uma coluna da tabela_

      public class SuaClasse
      {
      [Column("nome_coluna")]
      public string MeuAtributo { get; set; }
      }

- [Key]

  _Marca um atributo como chave primária_

      public class SuaClasse
      {
      [Key]
      public int CodigoDoEstoque { get; set; }
      }

* [ForeignKey("nome_chave")]

  _Marca um atributo como chave extrangeira_

      public class Carro
      {
          public int Id { get; set; }
          public string Modelo { get; set; }

          public int PessoaId { get; set; }
          public Pessoa Pessoa { get; set; }
      }

      public class Pessoa
      {
          public int Id { get; set; }

          [ForeignKey("PessoaId")]
          public ICollection<Carro> Carros { get; set; }
      }

- [NotMapped]

  _Marca um atributo para não ser mapeado como coluna da tabela no DB_

      public class SuaClasse
      {
      [NotMapped]
      public int MeuAtributo { get; set; }
      }

- [Required]

  _Marca um atributo como non nullable_

      public class SuaClasse
      {
      [Required]
      public int MeuAtributo { get; set; }
      }

- [MaxLength(50)]

  _Limita a quantidaade de caracteres de uma string ou byte[]_

        public class SuaClasse
        {
        [MaxLength(50)]
        public string Nome { get; set; }
        }

- [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

  _Especifica que um valor vai ser atribuido a propriedade apenas na hora da primeira gravação no banco, não sendo atualizado nas gravações e modificações futuras deste mesmo registro_

        public class SuaClasse
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataCriacao { get; set; }
        }

- [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

  _Especifica que um valor vai ser atribuido a propriedade na hora da primeira gravação no banco e nas gravações e modificações futuras deste mesmo registro, atualizando o valor a cada nova gravação_

        public class SuaClasse
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DataEdicao { get; set; }
        }

### Editar padroes especificos das migrantions no bdContex modelBuilder

_É possível editar e modificar propriedades especificas de uma entidade / tabela modificando o metodo **OnModelCreating(ModelBuilder modelBuilder)** na classe [ApplicationDbContext](https://github.com/dev-igorcarvalho/cook_book/blob/master/dot_net_api/Context/ApplicationDbContext.cs)_

_Existem diversas opçoes de modificações possiveis, já demonstramos algumas em exemplos anteriores, conforme o código repetido abaixo:_

    protected override void OnModelCreati(ModelBuilder modelBuilder)
      {
        modelBuilder
          .Entity<Endereco>().ToTable Enderecos");

        modelBuilder
          .Entity<Endereco>().Property<int> ClienteId");

        modelBuilder
          .Entity<Endereco>().HasKey("ClienteId");
      }

### Criação das migrations

- Para criar uma migration usar no terminal o comando

      dotnet ef migrations add nome_da_migration

- Para remover uma migration usar no terminal o comando

      dotnet ef migrations remove

- Para atualizar o banco a partir da migration usar no terminal o comando

      dotnet ef database update

### Criação de Seeder usando migrations para addicionar conteúdo ao banco

- Criar uma nova migration

      dotnet ef migrations add nome_da_migration

- Editar o código da migration criada para rodar SQLs no banco de dados

      using Microsoft.EntityFrameworkCore.Migrations;
      namespace dot_net_api.Migrations
      {
          public partial class evento_seeder : Migration
          {
              protected override void Up(MigrationBuilder migrationBuilder)
              {
                  migrationBuilder
                  .Sql(@"INSERT INTO Eventos
                          (Id, Nome, Local, QuantidadeParticipantes)
                      VALUES
                          (1,'Carnaval','Rj',500);");
              }

              protected override void Down(MigrationBuilder migrationBuilder)
              {
                  migrationBuilder.Sql("DELETE FROM Eventos");
              }
          }
      }

### Criação exception handler

### Criação http interceptor

### Criação do padrao repositry para os acessos ao banco

### Criação dos mappers de entidades pra Dtos

### Criação e configuração do identity framework pra trabalhar com tokens

### Criação endpoints de autenticação

### Criação de um controller com endpoints protegiddos
