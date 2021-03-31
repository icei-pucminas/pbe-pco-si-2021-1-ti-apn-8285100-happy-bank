
CREATE TABLE public.bank (
	cnpj varchar (14) NOT NULL,
    agency_bank varchar(4) NOT NULL,
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	state varchar (20) NOT NULL,
    CONSTRAINT cnpj_pk PRIMARY KEY (cnpj),
);

ALTER TABLE public.bank ADD CONSTRAINT (
);

INSERT INTO public.bank (id, name, username)
VALUES()