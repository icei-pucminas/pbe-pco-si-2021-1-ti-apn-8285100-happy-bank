CREATE TABLE public.transaction (
    id uuid NOT NULL,
    bank_cnpj varchar,
	acount_bank varchar,
	user_bank varchar,
	access_password varchar (6) NOT NULL,
	birth_date date (100) NOT NULL,
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	state varchar (20) NOT NULL,
	email varchar (100) NOT NULL,
	phone varchar (20) NOT NULL,
    CONSTRAINT transaction_pk PRIMARY KEY (id),
    CONSTRAINT cnpj_fk FOREIGN KEY (bank_cnpj) REFERENCES public.bank(cnpj_pk),
    CONSTRAINT account_fk FOREIGN KEY (acount_bank) REFERENCES public.account(account_pk),
    CONSTRAINT user_fk FOREIGN KEY (user_bank) REFERENCES public.user(user_pk ),
);

ALTER TABLE public.transaction ADD CONSTRAINT (
);