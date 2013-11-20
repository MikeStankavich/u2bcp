declare @rtn char(1) = char(13)
/*
select k.*
,b.fld015.query('val[@loc=sql:column("k.loc")]/sub')
,stuff(b.fld016.query('for $i in val[@loc=sql:column("k.loc")]/sub return concat(sql:variable("@rtn"), $i/@loc)').value('.', 'varchar(8000)'), 1, 1, '')
from invff b
join (select k1.id, t.c.value('@loc', 'int') loc from INVFF k1 cross apply k1.fld014.nodes('/val') t(c)) k on k.id = b.id
*/
select b.id
--,stuff(b.fld003.query('for $i in val/sub/text() return concat(sql:variable("@rtn"), $i/.)').value('.', 'varchar(8000)'), 1, 1, '')
,dbo.xvc(fld003)
,b.fld003.query('val/sub/text()')
from invff b

create function dbo.xvc(@x xml) returns varchar(max) 
as
begin
--declare @v varchar(max)
declare @r char(1) = char(13)
return stuff(@x.query('for $i in val/sub/text() return concat(sql:variable("@r"), $i/.)').value('.', 'varchar(max)'), 1, 1, '')
end

create function dbo.xvv(@x xml, @l int) returns varchar(max) 
as
begin
--declare @v varchar(max)
declare @r char(1) = char(13)
return stuff(@x.query('for $i in val[sql:variable("@l")]/sub/text() return concat(sql:variable("@r"), $i/.)').value('.', 'varchar(max)'), 1, 1, '')
end


create function dbo.xvl(@x xml) returns table
as
return (
select t.c.value('@loc', 'int') loc
from @x.nodes('val') t(c)
)

select x.id
, dbo.xvv(x.fld014, k.loc)
, dbo.xvv(x.fld015, k.loc)
, dbo.xvv(x.fld016, k.loc)
, dbo.xvv(x.fld017, k.loc)
, dbo.xvv(x.fld018, k.loc)
, dbo.xvv(x.fld019, k.loc)
, dbo.xvv(x.fld020, k.loc)
, dbo.xvv(x.fld021, k.loc)
, dbo.xvv(x.fld022, k.loc)
, dbo.xvv(x.fld023, k.loc)
, dbo.xvv(x.fld024, k.loc)
, dbo.xvv(x.fld025, k.loc)
, dbo.xvv(x.fld026, k.loc)
, dbo.xvv(x.fld027, k.loc)
, dbo.xvv(x.fld028, k.loc)
, dbo.xvv(x.fld029, k.loc)
from invff x
cross apply dbo.xvl(x.fld014) k

select x.u2_id, k.loc
, dbo.xxvv(x.u2_data, 14, k.loc)
, dbo.xxvv(x.u2_data, 15, k.loc)
, dbo.xxvv(x.u2_data, 16, k.loc)
, dbo.xxvv(x.u2_data, 17, k.loc)
, dbo.xxvv(x.u2_data, 18, k.loc)
, dbo.xxvv(x.u2_data, 19, k.loc)
, dbo.xxvv(x.u2_data, 20, k.loc)
from invf x
cross apply dbo.xxvl(14, x.u2_data) k

create function dbo.xxvl(@f int, @x xml) returns table
as
return (
select t.c.value('@loc', 'int') loc
from @x.nodes('data/fld[sql:variable("@f")]/val') t(c) 
)

create function dbo.xxvv(@x xml, @f int, @v int) returns varchar(max) 
as
begin
--declare @v varchar(max)
declare @r char(1) = char(13)
return stuff(@x.query('for $i in data/fld[sql:variable("@f")]/val[sql:variable("@v")]/sub/text() return concat(sql:variable("@r"), $i/.)').value('.', 'varchar(max)'), 1, 1, '')
end


select x.u2_id, k.loc
, dbo.xxvv(x.u2_data, 14, k.loc)
, dbo.xxvv(x.u2_data, 15, k.loc)
, dbo.xxvv(x.u2_data, 16, k.loc)
, dbo.xxvv(x.u2_data, 17, k.loc)
, dbo.xxvv(x.u2_data, 18, k.loc)
, dbo.xxvv(x.u2_data, 19, k.loc)
, dbo.xxvv(x.u2_data, 20, k.loc)
from invf x
cross apply dbo.xxvl(14, x.u2_data) k

-- can i go back to blob per row & slice with fld[sql:variable("@loc")] ???
-- check performance on numbers table versus loc extract