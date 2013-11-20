
select * from (
select b.u2_id i, t.c.query('.') q, t.c.value('@loc', 'int') l, t.c.value('.', 'varchar(max)') v
from INVF b cross apply b.u2_data.nodes('/data/fld[@loc=177]/val/sub') as t(c)
) x1
full join (
select b.u2_id i, t.c.query('.') q, t.c.value('@loc', 'int') l, t.c.value('.', 'varchar(max)') v
from INVF b cross apply b.u2_data.nodes('/data/fld[@loc=178]/val/sub') as t(c)
) x2 on x2.i = x1.i and x2.l = x1.l
full join (
select b.u2_id i, t.c.query('.') q, t.c.value('@loc', 'int') l, t.c.value('.', 'varchar(max)') v
from INVF b cross apply b.u2_data.nodes('/data/fld[@loc=2]/val/sub') as t(c)
) x3 on x3.i = x1.i and x3.l = x1.l

sp_spaceused INVF

select b.u2_id i, t.c.query('.') q, t.c.value('@loc', 'int') l, t.c.value('.', 'varchar(max)') v
from INVF b cross apply b.u2_data.nodes('/data/fld[@loc=177]/val/sub') as t(c)


/*
select b.u2_id
, t1.c.query('.'), t1.c.value('.', 'varchar(max)')
--, t2.c.query('.'), t2.c.value('.', 'varchar(max)')
from INVF b
cross apply b.u2_data.nodes('/data/fld[@loc=177]/val/sub') as t1(c)
--cross apply b.u2_data.nodes('/data/fld[@loc=178]/val/sub') as t2(c)

--cross apply b.u2_data.nodes('/data/fld[@loc=178]/val/sub') as t2(c)
*/

select * from INVF


select b.u2_id i, t.c.query('.') q, t.c.value('@loc', 'int') l, t.c.value('sub[1]', 'varchar(max)') v
from INVF b cross apply b.u2_data.nodes('/data/fld[@loc=14]/val') as t(c)

select b.u2_id i, t.c.query('.') q, t.c.value('@loc', 'int') l, t.c.value('sub[1]', 'varchar(max)') v
from INVF b cross apply b.u2_data.nodes('/data/fld[last()]/val') as t(c)



WITH numbers(seqnum) AS
(
SELECT 1 AS seqnum
UNION ALL
SELECT seqnum+1 AS seqnum
FROM numbers
WHERE seqnum < 1000	
)
SELECT seqnum FROM numbers
OPTION (MAXRECURSION 1000)

vts


select max(l) fld_count from (select t.c.value('@loc', 'int') l from INVF b cross apply b.u2_data.nodes('/data/fld[last()]') as t(c)) x


    Tbl.Col.query('for $i in value return concat($i/@code, ";")').value('.', 'nvarchar(max)')

declare @rtn char(1) = char(13)
select b.u2_id i, n.n, t.c.query('fld[@loc=14]/val[@loc=sql:column("n.n")]') --.value('sub[1]', 'varchar(8000)') q
from INVF b 
cross join #numbers n
cross apply b.u2_data.nodes('/data') as t(c)
where t.c.exist('fld[@loc=14]/val[@loc=sql:column("n.n")]/sub') = 1

create table #numbers (n int primary key)
insert #numbers values (1),(2),(3)
insert #numbers values (4),(5),(6)
