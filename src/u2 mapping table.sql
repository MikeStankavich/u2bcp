/*

must begin with the end in mind
the end is to load data in mssql
so... make a dictionary that's keyed on sql table and column + seq in case comes from more than one place in u2
dictionary shows file, location, format, char separator for concatenation, default, error behavior (fail, default)

*/

create table u2_mapping (
     tbl sysname not null
	,col sysname not null
	,seq tinyint not null
	,fle varchar(255) null
	,loc smallint null
	,fmt varchar(255) null
	,dflt varchar(255) null
	,sep varchar(10) null
	,

	,constraint pk_u2_mapping (tbl, col, seq)
)
