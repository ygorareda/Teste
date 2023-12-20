# Teste

-- create
CREATE TABLE CLIENTE (
  Id int not null primary key,
  Nome varchar(50), 
  CPF varchar(11) not null, 
  UF varchar(2) not null, 
  Celular varchar(50)
);

CREATE TABLE FINANCIAMENTO (
  Id int not null primary key,
  CPF varchar(11) not null, 
  TipoFinanciamento varchar(50) not null, 
  Valor money not null, 
  DataUltimoVencimento date not null,
  ClienteId int FOREIGN KEY REFERENCES CLIENTE(Id)
);

CREATE TABLE PARCELA (
  Id int not null primary key, 
  QtyParcelas int not null, 
  ValorParcela money not null, 
  DataVencimento date not null, 
  DataPagamento date,
  FinanciamentoId int FOREIGN KEY REFERENCES FINANCIAMENTO(Id)
);

-- insert
INSERT INTO CLIENTE(id,Nome,CPF,UF,Celular) VALUES (1, 'ygor', '00998877665','DF','99888888');
INSERT INTO FINANCIAMENTO(Id,CPF,TipoFinanciamento,Valor,DataUltimoVencimento,ClienteId) VALUES (1, '07678877665','Imobiliario','10000','2017-10-25',1);
INSERT INTO PARCELA(Id,QtyParcelas,ValorParcela,DataVencimento,DataPagamento,FinanciamentoId) VALUES (1,20,'5000','2023-11-20','2017-10-25',1);
INSERT INTO PARCELA(Id,QtyParcelas,ValorParcela,DataVencimento,DataPagamento,FinanciamentoId) VALUES (4,20,'5000','2013-12-19','2023-12-20',1);
INSERT INTO PARCELA(Id,QtyParcelas,ValorParcela,DataVencimento,DataPagamento,FinanciamentoId) VALUES (5,20,'5000','2013-11-19',null,1);
INSERT INTO PARCELA(Id,QtyParcelas,ValorParcela,DataVencimento,DataPagamento,FinanciamentoId) VALUES (6,20,'5000','2013-11-19','2023-12-20',1);

INSERT INTO CLIENTE(id,Nome,CPF,UF,Celular) VALUES (2, 'joao', '00998877664','DF','99888888');
INSERT INTO FINANCIAMENTO(Id,CPF,TipoFinanciamento,Valor,DataUltimoVencimento,ClienteId) VALUES (2, '07678877664','Imobiliario','10000','2017-10-25',2);
INSERT INTO PARCELA(Id,QtyParcelas,ValorParcela,DataVencimento,DataPagamento,FinanciamentoId) VALUES (2,20,'5000','2023-12-20',null,2);

INSERT INTO CLIENTE(id,Nome,CPF,UF,Celular) VALUES (3, 'caio', '00998877663','DF','99888888');
INSERT INTO FINANCIAMENTO(Id,CPF,TipoFinanciamento,Valor,DataUltimoVencimento,ClienteId) VALUES (3, '07678877663','Imobiliario','10000','2017-10-25',3);
INSERT INTO PARCELA(Id,QtyParcelas,ValorParcela,DataVencimento,DataPagamento,FinanciamentoId) VALUES (3,20,'5000','2017-11-25',null,3);

-- fetch 
--SELECT * FROM CLIENTE;
--SELECT * FROM FINANCIAMENTO;
--SELECT * FROM PARCELA;

select CLIENTE.Id, CLIENTE.Nome from CLIENTE
  inner join FINANCIAMENTO
      on  FINANCIAMENTO.ClienteId = CLIENTE.Id
  inner join PARCELA 
      on PARCELA.FinanciamentoId = FINANCIAMENTO.Id
  group by CLIENTE.Id,CLIENTE.Nome
  having (count(PARCELA.DataPagamento) *100/count(*) ) >= 60



select CLIENTE.Id, CLIENTE.Nome,PARCELA.DataVencimento, DATEDIFF(second,PARCELA.DataVencimento,GETDATE())/86400 as dias from CLIENTE
  inner join FINANCIAMENTO
      on  FINANCIAMENTO.ClienteId = CLIENTE.Id
  inner join PARCELA 
      on PARCELA.FinanciamentoId = FINANCIAMENTO.Id
  group by CLIENTE.Id, CLIENTE.Nome,PARCELA.DataVencimento, PARCELA.DataPagamento
  having DATEDIFF(second,PARCELA.DataVencimento,GETDATE())/86400 > 5 and PARCELA.DataPagamento is null