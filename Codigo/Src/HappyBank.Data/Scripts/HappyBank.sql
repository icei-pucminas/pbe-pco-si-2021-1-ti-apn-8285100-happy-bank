
CREATE TABLE public.user (
	id uuid NOT NULL,
	name varchar(100) NOT NULL,
    "username" varchar(100) NOT NULL,
	CONSTRAINT user_pk PRIMARY KEY (id)
);