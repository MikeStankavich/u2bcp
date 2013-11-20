--create table #numbers (n int primary key)
--insert #numbers values (1),(2),(3),(4),(5),(6)

declare @rtn char(1) = char(13)

select b.u2_id i, n.n
  ,t.c.query('fld[@loc=14]/val[@loc=sql:column("n.n")]/sub[1]').value('.', 'varchar(8000)') sub_key
--  ,t.c.query('for $i in fld[@loc=14]/val[@loc=sql:column("n.n")]/sub/text() return concat($i, ",")') --.value('.', 'varchar(8000)') subval
from INVF b 
cross join #numbers n
cross apply b.u2_data.nodes('/data') as t(c)
where t.c.exist('fld[@loc=14]/val[@loc=sql:column("n.n")]/sub') = 1

select b.u2_id i, n.n
  ,t.c.query('fld[@loc=14]/val[@loc=sql:column("n.n")]/sub[1]').value('.', 'varchar(8000)') sub_key
--  ,t.c.query('for $i in fld[@loc=14]/val[@loc=sql:column("n.n")]/sub/text() return concat($i, ",")') --.value('.', 'varchar(8000)') subval
from INVF b 
cross join #numbers n
cross apply b.u2_data.nodes('/data') as t(c)
cross apply b.u2_data.nodes('/data/fld[14]') as k(c)
where t.c.exist('fld[@loc=14]/val[@loc=sql:column("n.n")]/sub') = 1

--    Tbl.Col.query('for $i in value return concat($i/@code, ";")').value('.', 'nvarchar(max)')


