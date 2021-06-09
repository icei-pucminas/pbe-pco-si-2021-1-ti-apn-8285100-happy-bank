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