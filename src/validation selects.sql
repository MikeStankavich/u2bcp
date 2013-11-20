select b.u2_id i, t.c.query('fld[@loc=1]') q
from INVF b cross apply b.u2_data.nodes('/data') as t(c)




select c.*
, p.u2_data.query('data/fld[@loc=15]/val[@loc=sql:column("c.val_idx")]/sub/text()') v15
, p.u2_data.query('data/fld[@loc=16]/val[@loc=sql:column("c.val_idx")]/sub/text()') v16
, p.u2_data.query('data/fld[@loc=17]/val[@loc=sql:column("c.val_idx")]/sub/text()') v17
, p.u2_data.query('data/fld[@loc=18]/val[@loc=sql:column("c.val_idx")]/sub/text()') v18
, p.u2_data.query('data/fld[@loc=19]/val[@loc=sql:column("c.val_idx")]/sub/text()') v19
, p.u2_data.query('data/fld[@loc=20]/val[@loc=sql:column("c.val_idx")]/sub/text()') v20
, p.u2_data.query('data/fld[@loc=21]/val[@loc=sql:column("c.val_idx")]/sub/text()') v21
, p.u2_data.query('data/fld[@loc=22]/val[@loc=sql:column("c.val_idx")]/sub/text()') v22

from INVF p 
join (select b.u2_id, t.c.value('@loc', 'int') val_idx, t.c.query('sub/text()') val_txt
      from INVF b cross apply b.u2_data.nodes('/data/fld[@loc=14]/val') as t(c)) c on p.u2_id = c.u2_id


USE [pmsteel]
GO

ALTER TABLE [dbo].[INVF] DROP CONSTRAINT [PK__INVF__B885638F41F8B7BD]
GO


alter table INVF alter column u2_id VARCHAR(128) NOT NULL
go

ALTER TABLE [dbo].[INVF] ADD PRIMARY KEY CLUSTERED 
(
	[u2_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE PRIMARY XML INDEX [IX_INVF] 
ON INVF(u2_data)
GO