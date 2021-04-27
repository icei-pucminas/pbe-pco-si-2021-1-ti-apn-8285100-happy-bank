CREATE TABLE public.bank
(
	id uuid NOT NULL,
	cnpj varchar (14) NOT NULL,
	"name" varchar(50) NOT NULL,
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	"state" varchar (20) NOT NULL,
	address_number varchar (20) NOT NULL,
    CONSTRAINT bank_pk PRIMARY KEY (id)
);

CREATE TABLE public.customer
(
	id uuid NOT NULL,
	"password" varchar (64) NOT NULL,
	govnumber varchar (11) NOT NULL UNIQUE,
	"name" varchar(100) NOT NULL,
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	"state" varchar (20) NOT NULL,
	email varchar (100) NOT NULL UNIQUE,
	phone varchar (20) NOT NULL,
	address_number varchar (20) NOT NULL,
	birth_date date NOT NULL DEFAULT NOW(),
	CONSTRAINT customer_pk PRIMARY KEY (id),
);

CREATE TABLE public.employees
(
    id uuid NOT NULL,
    resgistration varchar (6) NOT NULL UNIQUE,
    bank_fk uuid,
	wage varchar (50) NOT NULL,
	"name" varchar(100) NOT NULL,
	"function" varchar (100) NOT NULL,
    CONSTRAINT employees_pk PRIMARY KEY (id),
    CONSTRAINT bank_fk FOREIGN KEY (bank_fk) REFERENCES public.bank(id),
);

CREATE TABLE public.account 
(
    id uuid NOT NULL,
    bank_fk uuid,
	customer_fk uuid,
	agency_bank varchar(4) NOT NULL,
	account_bank varchar (9) NOT NULL UNIQUE,
    CONSTRAINT account_pk PRIMARY KEY (id),
	CONSTRAINT bank_fk FOREIGN KEY (bank_fk) REFERENCES public.bank(id)
	CONSTRAINT customer_fk FOREIGN KEY (customer_fk) REFERENCES public.customer(id)
);

CREATE TABLE public.transaction
(
    id uuid NOT NULL,
	acount_bank uuid,
	type_transaction varchar (20) NOT NULL,
	value_transaction varchar (100) NOT NULL,
	terminal_code varchar (50),
	deposit_code varchar (50),
	agency_recipient varchar (4),
	account_recipient varchar (9),
	date_transaction date NOT NULL DEFAULT NOW(),
    CONSTRAINT transaction_pk PRIMARY KEY (id),
	CONSTRAINT account_fk FOREIGN KEY (acount_bank) REFERENCES public.account(id),
);

ALTER TABLE public.bank ADD CONSTRAINT
(

);

ALTER TABLE public.transaction ADD
"name" varchar(100) NOT NULL,

DROP TABLE public."transaction";

INSERT INTO public.bank (id, name, username)
VALUES()