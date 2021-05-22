/* HappyBank */
CREATE OR REPLACE function new_id() returns uuid as $$
begin 
	return md5(random()::text || clock_timestamp()::text)::uuid;
end;
$$ LANGUAGE plpgsql;

CREATE TABLE bank
(
	id uuid NOT NULL PRIMARY KEY DEFAULT new_id(),
	bank_number int NOT NULL,
	"name" varchar(100) NOT NULL,
	gov_number varchar (20) NOT NULL,	
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	"state" varchar (20) NOT NULL,
	address_number varchar (20) NOT NULL
);

CREATE TABLE customer
(
	id uuid NOT null PRIMARY KEY DEFAULT new_id(),
	"name" varchar(100) NOT NULL,
	gov_number varchar (20) NOT NULL,
	street varchar (50) NOT NULL,
	district varchar (50) NOT NULL,
	city varchar (50) NOT NULL,
	"state" varchar (20) NOT NULL,
	address_number varchar (20) NOT NULL,
	birth_date date NOT NULL,
	phone varchar (30) NOT NULL,
	email varchar (100) NOT NULL UNIQUE,
	"password" varchar (50) NOT NULL
);

CREATE TABLE employee
(
    id uuid NOT NULL PRIMARY KEY DEFAULT new_id(),
	name varchar(100) NOT NULL,
	bank_id uuid,
    registration varchar (6) NOT NULL UNIQUE,
	wage decimal NOT NULL,
	"function" varchar (100) NOT NULL,
    CONSTRAINT employee_bank_fk FOREIGN KEY (bank_id) REFERENCES bank(id)
);

CREATE SEQUENCE account_number_seq START 1;

CREATE TABLE account 
(
    id uuid NOT NULL PRIMARY KEY DEFAULT new_id(),
    bank_id uuid,
	customer_id uuid,
	agency_number int NOT null default 1,
	account_number int NOT NULL UNIQUE DEFAULT nextval('account_number_seq'),
    CONSTRAINT account_bank_fk FOREIGN KEY (bank_id) REFERENCES bank(id),
	CONSTRAINT account_customer_fk FOREIGN KEY (customer_id) REFERENCES customer(id)
);

CREATE TABLE "transaction"
(
    id uuid NOT NULL PRIMARY KEY DEFAULT new_id(),
	account_id uuid NOT NULL,
	kind char(1) NOT NULL,
	value decimal NOT NULL,
	execution_date timestamp,
    CONSTRAINT transaction_account_fk FOREIGN KEY (account_id) REFERENCES account(id)
);

CREATE TABLE transfer
(
    id uuid NOT NULL PRIMARY KEY,
	account_destiny_id uuid NOT null,
	CONSTRAINT transfer_transaction_fk FOREIGN KEY (id) REFERENCES transaction(id),
	CONSTRAINT transfer_account_destiny_fk FOREIGN KEY (account_destiny_id) REFERENCES account(id)
);

CREATE TABLE withdraw
(
    id uuid NOT NULL PRIMARY KEY,
	terminal_code varchar(50) NOT NULL,
	CONSTRAINT withdraw_transaction_fk FOREIGN KEY (id) REFERENCES transaction(id)
);

CREATE TABLE deposit
(
    id uuid NOT NULL PRIMARY KEY,
	envelope_code varchar(50) NOT NULL,
	CONSTRAINT deposit_transaction_fk FOREIGN KEY (id) REFERENCES transaction(id)
);

CREATE TABLE operation
(
    id uuid NOT NULL PRIMARY KEY DEFAULT new_id(),
	account_id uuid NOT NULL,
	transaction_id uuid NOT NULL,
	kind char(1) NOT NULL,
	value decimal NOT NULL,
	execution_date timestamp,
    CONSTRAINT operation_account_fk FOREIGN KEY (account_id) REFERENCES account(id),
    CONSTRAINT operation_transaction_fk FOREIGN KEY (transaction_id) REFERENCES transaction(id)
);


