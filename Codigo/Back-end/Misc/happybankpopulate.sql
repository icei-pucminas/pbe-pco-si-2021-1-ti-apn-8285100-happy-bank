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
 
   INSERT INTO customer(id, "name", gov_number, street, district, city, "state", address_number, birth_date, phone, email, "password") VALUES (customer_id, 'Hugo José', '283.179.080-89', 'José Inácio', 'Ingá', 'Betim', 'MG', '1001', '2002-10-10', '+553191111-1111', 'hugo@gmail.com', 'aleluia123');
 
   INSERT INTO employee(id, registration, bank_id, wage, "name", "function") VALUES (employess_id, '123456', bank_id, 1000, 'José Avelino', 'Financeiro');
 
   INSERT INTO account(id, bank_id, customer_id, agency_number, account_number) VALUES (account_id, bank_id, customer_id, 171, 123456789);
 
   INSERT INTO transaction(id, account_id, kind, value, execution_date) VALUES (transaction_id, account_id, 'd', 500, '2021-04-10');
 
   INSERT INTO deposit(id, envelope_code) VALUES (transaction_id, '00017100171');
 
   INSERT INTO operation(id, account_id, transaction_id, kind, value, execution_date) VALUES (operation_id, account_id, transaction_id, 'c', 500, '2021-04-10');

   customer_id := new_id();
   employess_id := new_id();
   account_id := new_id();
   transaction_id := new_id();
   operation_id := new_id();
   
  INSERT INTO customer(id, "name", gov_number, street, district, city, "state", address_number, birth_date, phone, email, "password") VALUES (customer_id, 'Maicon Douglas', '519.511.880-98', 'Av. Amazonas', 'Centro', 'Betim', 'MG', '201', '2000-02-01', '+553191212-1212', 'maicon@gmail.com', 'sonapaz123');
 
  INSERT INTO employee(id, registration, bank_id, wage, "name", "function") VALUES (employess_id, '654321', bank_id, 3000, 'João', 'TI');

  INSERT INTO account(id, bank_id, customer_id, agency_number, account_number) VALUES (account_id, bank_id, customer_id, 171, 987654321);
 
  INSERT INTO transaction(id, account_id, kind, value, execution_date) VALUES (transaction_id, account_id, 't', 1000, '2021-04-11');

  INSERT INTO transfer(id, account_destiny_id) VALUES (transaction_id, account_id);
 
  INSERT INTO operation(id, account_id, transaction_id, kind, value, execution_date) VALUES (operation_id, account_id, transaction_id, 'd', 1000, '2021-04-11');
  
   customer_id := new_id();
   employess_id := new_id();
   account_id := new_id();
   transaction_id := new_id();
   operation_id := new_id();

  INSERT INTO customer(id, "name", gov_number, street, district, city, "state", address_number, birth_date, phone, email, "password") VALUES (customer_id, 'Gabrielli', '244.890.980-65', 'Nossa Senhora do Rosário', 'Angola', 'Betim', 'MG', '805', '2001-10-04', '+553191313-1313', 'gabrielli@gmail.com', 'comsono123');

  INSERT INTO employee(id, registration, bank_id, wage, "name", "function") VALUES (employess_id, '543216', bank_id, 2500, 'Bernardo', 'Atendimento ao cliente');

  INSERT INTO account(id, bank_id, customer_id, agency_number, account_number) VALUES (account_id, bank_id, customer_id, 171, 912345678);

  INSERT INTO transaction(id, account_id, kind, value, execution_date) VALUES (transaction_id, account_id, 'w', 400, '2021-04-12');

  INSERT INTO withdraw(id, terminal_code) VALUES (transaction_id, '000171000');

  INSERT INTO operation(id, account_id, transaction_id, kind, value, execution_date) VALUES (operation_id, account_id, transaction_id, 'd', 400, '2021-04-12');

   customer_id := new_id();
   employess_id := new_id();
   account_id := new_id();
   transaction_id := new_id();
   operation_id := new_id();

  INSERT INTO customer(id, "name", gov_number, street, district, city, "state", address_number, birth_date, phone, email, "password") VALUES (customer_id, 'Gustavo', '000.743.710-29', 'Av. Gov. Valadares', 'Centro', 'Betim', 'MG', '31', '1999-10-05', '+553191414-1414', 'gustavo@gmail.com', 'arquiteturadedados123');

  INSERT INTO employee(id, registration, bank_id, wage, "name", "function") VALUES (employess_id, '234561', bank_id, 4000, 'Julio', 'Gerente');

  INSERT INTO account(id, bank_id, customer_id, agency_number, account_number) VALUES (account_id, bank_id, customer_id, 171, 891234567);

  INSERT INTO transaction(id, account_id, kind, value, execution_date) VALUES (transaction_id, account_id, 'd', 250, '2021-04-01');

  INSERT INTO deposit(id, envelope_code) VALUES (transaction_id, '00017100172');

  INSERT INTO operation(id, account_id, transaction_id, kind, value, execution_date) VALUES (operation_id, account_id, transaction_id, 'c', 250, '2021-04-01');

   customer_id := new_id();
   employess_id := new_id();
   account_id := new_id();
   transaction_id := new_id();
   operation_id := new_id();

  INSERT INTO customer(id, "name", gov_number, street, district, city, "state", address_number, birth_date, phone, email, "password") VALUES (customer_id, 'Breno Proti', '777.295.220-12', 'Av. Edmeia', 'Ingá Alto', 'Betim', 'MG', '302', '2000-05-16', '+553191515-1515', 'breno@gmail.com', 'cachorrada123');

  INSERT INTO employee(id, registration, bank_id, wage, "name", "function") VALUES (employess_id, '345612', bank_id, 6000, 'Marco', 'Diretor');

  INSERT INTO account(id, bank_id, customer_id, agency_number, account_number) VALUES (account_id, bank_id, customer_id, 171, 789123456);

  INSERT INTO transaction(id, account_id, kind, value, execution_date) VALUES (transaction_id, account_id, 'w', 300, '2021-05-05');

  INSERT INTO withdraw(id, terminal_code) VALUES (transaction_id, '000171001');

  INSERT INTO operation(id, account_id, transaction_id, kind, value, execution_date) VALUES (operation_id, account_id, transaction_id, 'd', 300, '2021-05-05');

END $$;
 
-- 0 = deposito;
-- 1 = transferência;
-- 2 = saque;