# SQL

## Index

- ### DDL - Data Definition Language
  - [Create database](#create-database)
  - [Drop database](#drop-database)
  - [Create table](#create-table)
  - [Show Tables](#show-tables)
  - [Desc Table](#desc-table)
  - [Alter table](#alter-table)
  - [Drop table](#drop-table)
  - [Truncate table](#truncate-table)
  - [Constraints](#constraints)
- ### DML - Data Manipulation Language
  - [Insert](#insert)
  - [Update](#update)
  - [Delete](#delete)
- ### DQL - Data Query Language
  - [Select](#select)
  - [Where](#where)
  - [And](#and)
  - [Or](#or)
  - [Like](#like)
  - [Between](#between)
  - [Not](#not)
  - [Is](#is)
  - [Distinct](#distinct)
  - [Count](#count)
  - [Avg](#avg)
  - [Sum](#sum)
  - [Left Join](#left-join)
  - [Right Join](#right-join)
  - [Inner Join](#inner-join)
  - [Exists](#exists)
  - [In](#in)
  - [Group By](#group-by)
  - [Order By](#order-by)
  - [Having](#having)
  - [View](#view)
  - [Index](#indexes)
- ### DCL - Data Control Language

---

## DDL - Data Definition Language

### Create Database

Cria um banco de dados

    CREATE DATABASE nome_banco;

Cria um banco de dados caso ele não exista

```sql
CREATE DATABASE IF NOT EXISTS nome_banco;
```

### Drop Database

Deleta um banco de dados

    DROP DATABASE nome_banco;

Deleta um banco de dados caso exista

```sql
DROP DATABASE IF EXISTS nome_banco;
```

### Create Table:

Cria uma tabela de acordo com as definições.

    CREATE TABLE nome_table (id int auto_increment primary key, valor double, data date, recebido boolean, observacoes varchar(255));

Com primary key especificada no atributo.

ou

    CREATE TABLE compras (
          id int NOT NULL AUTO_INCREMENT,
          valor double,
          data datetime,
          observacoes text NOT NULL,
          recebido tinyint(1) DEFAULT 1,
          forma_pagto ENUM('DINHEIRO', 'CARTAO', 'BOLETO'),
          PRIMARY KEY (id)
        );

Com primary key especificada a parte.

### Show Tables:

Exibe as tabelas.

    SHOW TABLES;

### Desc Table:

Exibe a estrutura da tabela.

    DESC nome_tablea;

### Alter Table:

Altera a tabela de acordo com os paremetros passados.

_Modifica a coluna observacoes da tabela compras, para ser texto não nulo._

    ALTER TABLE compras MODIFY observacoes VARCHAR(255) NOT NULL;

_Modifica a coluna recebido na tabela comprar para ter um valor default. Obs: Valores default também podem ser usados na criação de tabela._

    ALTER TABLE compras MODIFY recebido TINYINT(1) DEFAULT '0';

_Altera a coluna forma_pagt da tabela compras para ser do tipo ENUM._

    ALTER TABLE compras ADD COLUMN forma_pagt ENUM('CARTAO', 'BOLETO', 'DINHEIRO');

_Abaixo temos exemplos esclusivos do sql server_

_Verificamos se a coluna não existe e abrimos um transaction para acrescentar a coluna nova_

```sql
IF COL_LENGTH('Eventos', 'ativo') IS NULL
BEGIN
 ALTER TABLE Eventos
    ADD [ativo] INT
END
```

_Abre uma transação para rodar um comando, e caso nao tenha erro commita a transação, se tiver erro da rollback ao estado anterior_

```sql
BEGIN TRANSACTION
UPDATE FROM TbContas
SET NuSaldo= 10.000
WHERE NuSaldo < 50
IF @@ERROR = 0
COMMIT
ELSE
ROLLBACK
END
```

### Drop Table:

Deleta uma tabela.

    DROP TABLE nome_tabela;

### Truncate Table:

Deleta todo o conteúdo de uma tabela, mas não deleta a tabela.

    TRUNCATE TABLE nome_tabela;

### Constraints:

Constraints sao colocadas após o dataType de cada coluna.

_Temos abaixo a lista de constrains mais usadas._

    *  NOT NULL - Ensures that a column cannot have a NULL value
    * UNIQUE - Ensures that all values in a column are different
    * PRIMARY KEY - A combination of a NOT NULL and UNIQUE. Uniquely identifies each row in a table
    * FOREIGN KEY - Prevents actions that would destroy links between tables
    * CHECK - Ensures that the values in a column satisfies a specific condition
    * DEFAULT - Sets a default value for a column if no value is specified
    * CREATE INDEX - Used to create and retrieve data from the database very quickly

---

## DML - Data Manipulation Language

### Insert:

Insere registros na tabela

    INSERT INTO Customers
    (CustomerName, City, Country)
    VALUES
    ('Cardinal', 'Stavanger', 'Norway');

### Update:

Edita registros da tabela

    UPDATE compras SET valor = 900 WHERE id =20;

    UPDATE compras SET valor = 900 , observacoes = ' string aleatoria' WHERE id =20;

    UPDATE compras SET observacoes = 'super barato' WHERE valor < 15;

    UPDATE Compras SET valor= valor + 10 WHERE data < '2009-06-01';

### Delete:

Deleta registros da tabela

    DELETE FROM compras WHERE id=15;

---

## DQL - Data Query Language

### Select:

_Exibe todas colunas de todos os registros contidos na tabela._

    SELECT * FROM sua_tabela;

_Exibe valores de uma coluna de todos registros contidos na tabela._

    SELECT nome_coluna FROM nome_tabela;

_Exibe valores de multiplas colunas de todos registros contidos na tabela._

    SELECT nome_coluna, nome_coluna2 FROM nome_tabela;

### Where:

_Exibe os registros de acordo com a condição descrita na cláusula WHERE_

    SELECT qualquer_coisa FROM nome_tabela WHERE valor > 10;

### And:

_Exibe os registros de acordo com ambas condiçôes descritas na cláusula WHERE_

    SELECT qualquer_coisa FROM nome_tabela WHERE coluna1 > 10 AND coluna2 < 15 ;

### Or:

_Exibe os registros de acordo com qualquer uma das condiçôes descritas na cláusula WHERE_

    SELECT qualquer_coisa FROM nome_tabela WHERE coluna1 > 10 OR coluna2 < 15 ;

### Like:

_Exibe os registros em que os valores da cláusula WHERE que **começam** com a string passada como parâmetro._

    SELECT * FROM compras WHERE observacoes LIKE lanchonete%';

_Exibe os registros em que os valores da cláusula WHERE que **terminam** com a string passada como parâmetro._

    SELECT * FROM compras WHERE observacoes LIKE %lanchonete';

_Exibe os registros em que os valores da cláusula WHERE **contém** a string passada como parâmetro_.

    SELECT * FROM compras WHERE observacoes LIKE %lanchonete%';

_Exibe os registros em que os valores da cláusula WHERE estão **entrea** as duas strings passadas como parâmetro_.

    SELECT * FROM compras WHERE observacoes LIKE a%z';

### Between:

_Exibe os registros em que os valores da cláusula WHERE estejam **entre** os valores passados como parâmetro, incluindo o valor inicial e o final._

    SELECT * FROM compras WHERE valor BETWEEN 200 AND 700;

### Not:

_Exibe os registros contrários a condição descrita na cláusula WHERE_

    SELECT * FROM compras WHERE NOT valor = 108;

### Is:

_Exibe os registros de acordo com a condição descrita na cláusula WHERE (para booleanos)._

    SELECT * FROM compras WHERE observacoes IS NULL;

    SELECT * FROM compras WHERE observacoes IS NOT NULL;

### Distinct

_Exibe todos os registros **não repetidos** da tabela escolhida_:

    SELECT DISTINCT tipo FROM matricula;

### Count:

_Retorna a quantidade de registros que satsifazem a condição epecificada._

    SELECT COUNT(preco)
    FROM produtos
    WHERE preco > 10;

### Avg:

_Retorna a média dos valores da coluna selecionada._

    SELECT AVG(preco)
    FROM produtos;

### Sum:

_Retorna a soma dos valores da coluna selecionada._

    SELECT SUM(preco)
    FROM produtos

### Left Join:

_O LEFT JOIN favorece a tabela a **esquerda** da relação. Ou seja, mesmo se não existirem elementos na tabela da direita, ele trará a linha._

    SELECT * FROM aluno AS a LEFT JOIN curso AS c ON a.id = c.alunoId

### Right Join:

_O RIGH JOIN favorece a tabela a **direita** da relação. Ou seja, mesmo se não existirem elementos na tabela da direita, ele trará a linha._

    SELECT * FROM aluno AS a LEFT JOIN curso AS c ON a.id = c.alunoId

### Inner Join:

_O INNER JOIN não favorece nenhuma das tabelas. Ou seja, só exibe os registros contidos na cláusula ON.._

    SELECT * FROM aluno AS a JOIN curso AS c ON a.id = c.alunoId

### Exists

Exibe todos os registros nos quais subquery retorna true\_

    SELECT a.nome FROM aluno a WHERE EXISTS
    (select m.id from matricula m where m.aluno_id = a.id);

Exibe todos os registros nos quais subquery retorna false\_

    SELECT a.nome FROM aluno a WHERE NOT EXISTS
    (select m.id from matricula m where m.aluno_id = a.id);

### IN

Exibe todos os registros nos quais a condição da cláusula WHERE está contida no array passado como parãmetro.

    SELECT * FROM Customers
    WHERE Country IN ('Germany', 'France', 'UK');

    SELECT * FROM Customers
    WHERE Country NOT IN ('Germany', 'France', 'UK');

    SELECT * FROM Customers
    WHERE Country IN (SELECT Country FROM Suppliers);

### Group By:

_Agrupa os registros que tem o mesmo valor na coluna selecionada._

    SELECT column_name(s)
    FROM table_name
    WHERE condition
    GROUP BY column_name(s)

_Pode ser usado em conunto com funções agregadoras (COUNT(), MAX(), MIN(), SUM(), AVG(),)_

    SELECT COUNT(CustomerID), Country
    FROM Customers
    GROUP BY Country;

_Pode ser usado com ordenação desde que a ordenação seja declarada após o agrupamento_

    SELECT COUNT(CustomerID), Country
    FROM Customers
    GROUP BY Country
    ORDER BY COUNT(CustomerID) DESC;

_O group by aceita mais de 1 parametro , é so separar por virgulas._

    SELECT coluna1, coluna2 FROM tabela
    GROUP BY coluna1, coluna2;

### Order By:

_Ordena o resultado da pesquisa em ordem ascendente por default, ou na ordem selecionada ASC / DESC:_

    SELECT * FROM Customers
    ORDER BY Country;

    SELECT * FROM Customers
    ORDER BY Country DESC;

    SELECT * FROM Customers
    ORDER BY Country, CustomerName;

    SELECT * FROM Customers
    ORDER BY Country ASC, CustomerName DESC;

### Having:

_HAVING existe porque o WHERE não funciona em funções agregadoras.Para filtrar algo pelo resultado de um função agregadora deve se usasr o HAVING._

    SELECT c.nome, count(m.id) FROM
    curso c JOIN matricula m ON c.id = m.curso_id
    GROUP BY c.nome
    HAVING count(m.id) > 1;

### View:

_View é uma tabela virtual contendo o resultado de um pesquisa. Pode ser criada, atualizada e deletada_

    CREATE VIEW nome AS
    SELECT coluna1, coluna2, ...
    FROM tabela;

    CREATE OR REPLACE VIEW nome AS
    SELECT coluna1, coluna2, ...
    FROM tabela;

    DROP VIEW nome;

### indexes:

_Index é uma estratégia para agilizar o resultado de pesquisas tornando a pesquisa mais rapida, porém ao usar um index os inserts, updates, e deletes ficam mais lento._
Usando index.

    create index nome_do_indice
    on nome_tabela(nome_coluna):

    create index nome_do_indice
    on nome_tabela(nome_coluna, outra_coluna, ...):

    CREATE INDEX indice_por_lancamento ON livros(data_de_lancamento);

_Para remover um index a sintaxe muda de acordo com o db._

_MySQL_

    ALTER TABLE "tabela" DROP INDEX "nome_do_indice";

_SqlServer_

    DROP INDEX nome_tabela.nome_index;

_Db2 / Oracle_

    DROP INDEX nome_index;

---

## DCL - Data Control Language

Falta implementar
