
CREATE TABLE public.user (
	id uuid NOT NULL,
	name varchar(100) NOT NULL,
    "username" varchar(100) NOT NULL,
	acount_bank varchar,
	CONSTRAINT user_pk PRIMARY KEY (id),
);

ALTER TABLE public.user ADD CONSTRAINT (
	password varchar (64) NOT NULL,
	CONSTRAINT account_fk FOREIGN KEY (account_bank) REFERENCES public.account(account_pk),
);

INSERT INTO public.user (id, name, username)
VALUES()