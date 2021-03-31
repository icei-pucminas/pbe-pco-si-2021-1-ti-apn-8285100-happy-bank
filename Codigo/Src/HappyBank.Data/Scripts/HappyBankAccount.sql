CREATE TABLE public.account (
    id uuid NOT NULL,
    bank_cnpj varchar,
	account_bank varchar (9) NOT NULL,
	govnumber varchar (11) NOT NULL,
	access_password varchar (6) NOT NULL,
	birth_date date (100) NOT NULL,
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	state varchar (20) NOT NULL,
	email varchar (100) NOT NULL,
	phone varchar (20) NOT NULL,
    CONSTRAINT account_pk PRIMARY KEY (id),
    CONSTRAINT cnpj_fk FOREIGN KEY (bank_cnpj) REFERENCES public.bank(cnpj_pk),
);

ALTER TABLE public.account ADD CONSTRAINT (
);