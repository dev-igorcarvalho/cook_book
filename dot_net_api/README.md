# DotNet Core Api

## Index:

* [Instalação dos plugins do VS code](#instalação-dos-plugins-do-vs-code)
* [Preparação do banco de dados em container](#Preparação-do-banco-de-dados-em-container)
* Criação a aplicação com o CLI
* Instalação das dependências do Entity Framework
* Criação e configuração do DB context
* Instalação das dependências do Identity Framework
* Instalação das dependências do JWT
* Criação e configuração do Identity
* Criação dos modelos de entidade relacional
    * Relacionamento de entidades 1-1
    * Relacionamento de entidades 1-N
    * Relacionamento de entidades N-N
* Adicionando validação dos campos das entidades
* Editar padroes especificos das migrantions no bdContex modelBuilder
* Criação das migrations
* Criação seed migrations para addicionar conteúdo ao banco
* Criação exception handler
* Criação http interceptor
* Criação do padrao repositry para os acessos ao banco
* Criação dos mappers de entidades pra Dtos
* Criação e configuração do identity framework pra trabalhar com tokens
* Criação endpoints de autenticação
* Criação  de um controller com endpoints protegiddos
* Criação dos controllers com as 5 opçoes basicas de endpoints
    * GET  
        * Simples
        * Com path variable
        * Com query params
        * Com paginação
    * POST
    * PUT
    * DELETE

---
    
### Instalação dos plugins do VS code:
_Com o VS code aberto aperte as teclas CTRL+P e rode individualente os comandos abaixo:_

    ext install ms-dotnettools.csharp
    ext install formulahendry.dotnet
    ext install k--kato.docomment
    ext install jmrog.-nuget-package-manager

### Preparação do banco de dados em container:

#### Pré-Requisitos:
* Docker e Docker-compose

#### Instruções:
* Criar um arquivo chamado < docker-compose.yml > de accordo com o modelo abaixo:

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
   
* No terminal, na mesma pasta do arquivo, rodar o comando:

        docker-compose up -d
* Conexão com o db:
    * _Url: localhost:1433_
    * _Usuário: SA_
    * _Senha: utilizar a senha do arquivo

### Criação a aplicação com o CLI
#### Pré-Requisitos:
* DotNet SDK 
* No terminal, na mesma pasta destino do projeto, rodar o comando:

        dotnet new webapi

* Após a crição do projeto, abrir o mesmo no VS code com o comando:
        
        code .

### Instalação das dependências do Entity Framework
### Criação e configuração do DB context
### Instalação das dependências do Identity Framework
### Instalação das dependências do JWT
### Criação e configuração do Identity
### Criação dos modelos de entidade relacional