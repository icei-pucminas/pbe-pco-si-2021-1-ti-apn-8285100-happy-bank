CREATE TABLE public.employees (
    resgistration varchar (6) NOT NULL,
    bank_cnpj varchar,
	wage varchar NOT NULL,
	name varchar(100) NOT NULL,
	function varchar (100) NOT NULL,
    CONSTRAINT registration_pk PRIMARY KEY (resgistration),
    CONSTRAINT cnpj_fk FOREIGN KEY (bank_cnpj) REFERENCES public.bank(cnpj_pk),
);

ALTER TABLE public.employees ADD CONSTRAINT (
);