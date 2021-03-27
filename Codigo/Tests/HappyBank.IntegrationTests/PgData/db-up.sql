--Criação do banco de dados
CREATE DATABASE happybanktests;

--Seleciona o banco de dados
\c happybanktests;

--Criação da tabela de teste
CREATE TABLE public.sample (
	id uuid NOT NULL,
	title varchar(100) NOT NULL,
	CONSTRAINT sample_pk PRIMARY KEY (id)
);
