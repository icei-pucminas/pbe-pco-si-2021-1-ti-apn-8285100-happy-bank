-- 1-Todas as transações de nossos usuários
select
	t.id,
	c.name,
	case
		when t.kind = 'd' then 'DEPÓSITO'
		when t.kind = 't' then 'TRANSFERÊNCIA'
		when t.kind = 'w' then 'SAQUE'
	end as description,
	t.execution_date,
	coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
from
	operation o
inner join transaction t on
	o.transaction_id = t.id
inner join account as a on a.id = o.account_id
inner join customer as c on c.id = a.customer_id
group by
	t.id,
	t.kind,
	c.name
order by 4

-- *******************************************************************************************************************************
-- 2-Todas as transações do usuário Hugo José
select
	t.id,
	c.name,
	case
		when t.kind = 'd' then 'DEPÓSITO'
		when t.kind = 't' then 'TRANSFERÊNCIA'
		when t.kind = 'w' then 'SAQUE'
	end as description,
	t.execution_date,
	coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
from
	operation o
inner join transaction t on
	o.transaction_id = t.id
inner join account as a on a.id = o.account_id
inner join customer as c on c.id = a.customer_id
where c.name = 'Hugo José',
group by
	t.id,
	t.kind,
	c.name
order by 4

-- *******************************************************************************************************************************
-- 3-Todas as transações dentro de um ano no qual o usuário se chama Hugo
select
	t.id,
	c.name,
	case
		when t.kind = 'd' then 'DEPÓSITO'
		when t.kind = 't' then 'TRANSFERÊNCIA'
		when t.kind = 'w' then 'SAQUE'
	end as description,
	t.execution_date,
	coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
from
	operation o
inner join transaction t on
	o.transaction_id = t.id
inner join account as a on a.id = o.account_id
inner join customer as c on c.id = a.customer_id
where c.name like 'Hugo%' and t.execution_date between '2021-01-01' and '2022-01-01'
group by
	t.id,
	t.kind,
	c.name
order by 4

-- ***********************************************************************************************************************************
-- 4-Todas as transações onde os usuários são Hugo e Breno e o valor e diferente de nulo

select
	t.id,
	c.name,
	case
		when t.kind = 'd' then 'DEPÓSITO'
		when t.kind = 't' then 'TRANSFERÊNCIA'
		when t.kind = 'w' then 'SAQUE'
	end as description,
	t.execution_date,
	coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
from
	operation o
inner join transaction t on
	o.transaction_id = t.id
inner join account as a on a.id = o.account_id
inner join customer as c on c.id = a.customer_id
where c.name in ('Hugo José', 'Breno Proti') and o.value is not null
group by
	t.id,
	t.kind,
	c.name
order by 4

-- *****************************************************************************************************************************************
-- 5-Somátorio de operações de crédito ou débito atualizando o valor do campo value

select
	t.id,
	c.name,
	case
		when t.kind = 'd' then 'DEPÓSITO'
		when t.kind = 't' then 'TRANSFERÊNCIA'
		when t.kind = 'w' then 'SAQUE'
	end as description,
	t.execution_date,
	coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
from
	operation o
inner join transaction t on
	o.transaction_id = t.id
inner join account as a on a.id = o.account_id
inner join customer as c on c.id = a.customer_id
group by
	t.id,
	t.kind,
	c.name
order by 4

-- *********************************************************************************************************************************************
-- 6-Todas as transações com valor maior que 10

select
	t.id,
	c.name,
	case
		when t.kind = 'd' then 'DEPÓSITO'
		when t.kind = 't' then 'TRANSFERÊNCIA'
		when t.kind = 'w' then 'SAQUE'
	end as description,
	t.execution_date,
	coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
from
	operation o
inner join transaction t on
	o.transaction_id = t.id
inner join account as a on a.id = o.account_id
inner join customer as c on c.id = a.customer_id
group by
	t.id,
	t.kind,
	c.name
having coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) > 10
order by 4

-- *********************************************************************************************************************************************
-- 7-Clientes com saldo entre R$ 200 e R$ 600
select c.id, c."name", c.phone from customer c where c.id in 
(
	select
		a.customer_id
    from operation o
    inner join account a on o.account_id = a.id
    group by a.customer_id 
    having coalesce(sum(case when kind = 'c' then value else value * -1 end), 0) 
    	between 200 and 600
)

-- *********************************************************************************************************************************************
-- 8-Clientes com data da última operação
select
	c.id,
	c."name",
	c.phone,
	(select
			max(o.execution_date)
		from
			operation o
		inner join account a on
			a.id = o.account_id
			and a.customer_id = c.id ) last_operation
from
	customer c

-- *********************************************************************************************************************************************
-- 9-Clientes que já realizaram pelo menos um saque
select
	c.id,
	c."name",
	c.phone
from
	customer c
where exists (select t.id from "transaction" t inner join account a on t.account_id = a.id and a.customer_id = c.id where t.kind = 'w')

-- *********************************************************************************************************************************************
-- 10-Clientes que possuem saldo maior que zero e maior que todas as transaçõe de saque
select * from (
	select
		c.id,
		c."name",
		c.phone,
		coalesce(sum(case when kind = 'c' then value else value * -1 end), 0) balance
	from
		customer c
	inner join account a on a.customer_id = c.id 
	left outer join operation o on o.account_id  = a.id 
	group by c.id, c."name", c.phone
	having coalesce(sum(case when kind = 'c' then value else value * -1 end), 0) > 0
) cs where cs.balance > all (
	select value from "transaction" t where t.kind = 'w'
)

