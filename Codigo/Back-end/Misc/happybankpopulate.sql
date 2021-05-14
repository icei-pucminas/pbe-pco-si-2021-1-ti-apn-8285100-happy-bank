DO $$
DECLARE
   bank_id uuid := new_id();
   customer_id uuid := new_id();
   employess_id uuid := new_id();
   account_id uuid := new_id();
   transaction_id uuid := new_id();
   operation_id uuid := new_id();
 
begin
   INSERT INTO bank (id, bank_number, "name", gov_number, street, district, city, "state", address_number) VALUES (bank_id, 171, 'HappyBank', '09.316.493/0001-48', 'Av. Amazonas', 'Centro', 'Betim', 'MG', '756');
 
   INSERT INTO customer(id, "name", gov_number, street, district, city, "state", address_number, birth_date, phone, email, "password") VALUES (customer_id, 'Hugo José', '283.179.080-89', 'José Inácio', 'Ingá', 'Betim', 'MG', '1001', '10/10/2002', '+553191111-1111', 'hugo@gmail.com', 'aleluia123');
 
   INSERT INTO employees(id, resgistration, bank_id, wage, "name", "function") VALUES (employess_id, '123456', bank_id, '1.000', 'José Avelino', 'Financeiro');
 
   INSERT INTO account(id, bank_id, customer_id, agency_number, account_bank) VALUES (account_id, bank_id, customer_id, 171, 123456789);
 
   INSERT INTO transaction(id, acount_id, kind, value, execution_date) VALUES (transaction_id, account_id, 0, '500.00', '10/04/2021');
 
   INSERT INTO transfer(id, acount_destiny_id) VALUES (transaction_id, account_id);
 
   INSERT INTO withdraw(id, terminal_code) VALUES (transaction_id, '000171000');
 
   INSERT INTO deposit(id, envelope_code) VALUES (transaction_id, '00017100171');
 
   INSERT INTO operation(id, acount_id, transaction_id, kind, value, execution_date) VALUES (operation_id, account_id, transaction_id, 0, '500.00', '10/04/2021');
 
END $$;
 
 
   0 = deposito;
   1 = transação;
   2 = saque;