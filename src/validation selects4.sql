
select b.*, ',fld' + right('00' + t.c.value('@loc[1]', 'varchar(3)'), 3) + ' xml null'
from INVF b
cross apply b.u2_data.nodes('/data/fld') as t(c)
where b.u2_id = 79705

select ',t.c.query(''fld[' + t.c.value('@loc[1]', 'varchar(3)') + ']/val'')'
from INVF b
cross apply b.u2_data.nodes('/data/fld') as t(c)
where b.u2_id = 79705

insert INVFF
select b.u2_id
,t.c.query('fld[1]/val')
,t.c.query('fld[2]/val')
,t.c.query('fld[3]/val')
,t.c.query('fld[4]/val')
,t.c.query('fld[5]/val')
,t.c.query('fld[6]/val')
,t.c.query('fld[7]/val')
,t.c.query('fld[8]/val')
,t.c.query('fld[9]/val')
,t.c.query('fld[10]/val')
,t.c.query('fld[11]/val')
,t.c.query('fld[12]/val')
,t.c.query('fld[13]/val')
,t.c.query('fld[14]/val')
,t.c.query('fld[15]/val')
,t.c.query('fld[16]/val')
,t.c.query('fld[17]/val')
,t.c.query('fld[18]/val')
,t.c.query('fld[19]/val')
,t.c.query('fld[20]/val')
,t.c.query('fld[21]/val')
,t.c.query('fld[22]/val')
,t.c.query('fld[23]/val')
,t.c.query('fld[24]/val')
,t.c.query('fld[25]/val')
,t.c.query('fld[26]/val')
,t.c.query('fld[27]/val')
,t.c.query('fld[28]/val')
,t.c.query('fld[29]/val')
,t.c.query('fld[30]/val')
,t.c.query('fld[31]/val')
,t.c.query('fld[32]/val')
,t.c.query('fld[33]/val')
,t.c.query('fld[34]/val')
,t.c.query('fld[35]/val')
,t.c.query('fld[36]/val')
,t.c.query('fld[37]/val')
,t.c.query('fld[38]/val')
,t.c.query('fld[39]/val')
,t.c.query('fld[40]/val')
,t.c.query('fld[41]/val')
,t.c.query('fld[42]/val')
,t.c.query('fld[43]/val')
,t.c.query('fld[44]/val')
,t.c.query('fld[45]/val')
,t.c.query('fld[46]/val')
,t.c.query('fld[47]/val')
,t.c.query('fld[48]/val')
,t.c.query('fld[49]/val')
,t.c.query('fld[50]/val')
,t.c.query('fld[51]/val')
,t.c.query('fld[52]/val')
,t.c.query('fld[53]/val')
,t.c.query('fld[54]/val')
,t.c.query('fld[55]/val')
,t.c.query('fld[56]/val')
,t.c.query('fld[57]/val')
,t.c.query('fld[58]/val')
,t.c.query('fld[59]/val')
,t.c.query('fld[60]/val')
,t.c.query('fld[61]/val')
,t.c.query('fld[62]/val')
,t.c.query('fld[63]/val')
,t.c.query('fld[64]/val')
,t.c.query('fld[65]/val')
,t.c.query('fld[66]/val')
,t.c.query('fld[67]/val')
,t.c.query('fld[68]/val')
,t.c.query('fld[69]/val')
,t.c.query('fld[70]/val')
,t.c.query('fld[71]/val')
,t.c.query('fld[72]/val')
,t.c.query('fld[73]/val')
,t.c.query('fld[74]/val')
,t.c.query('fld[75]/val')
,t.c.query('fld[76]/val')
,t.c.query('fld[77]/val')
,t.c.query('fld[78]/val')
,t.c.query('fld[79]/val')
,t.c.query('fld[80]/val')
,t.c.query('fld[81]/val')
,t.c.query('fld[82]/val')
,t.c.query('fld[83]/val')
,t.c.query('fld[84]/val')
,t.c.query('fld[85]/val')
,t.c.query('fld[86]/val')
,t.c.query('fld[87]/val')
,t.c.query('fld[88]/val')
,t.c.query('fld[89]/val')
,t.c.query('fld[90]/val')
,t.c.query('fld[91]/val')
,t.c.query('fld[92]/val')
,t.c.query('fld[93]/val')
,t.c.query('fld[94]/val')
,t.c.query('fld[95]/val')
,t.c.query('fld[96]/val')
,t.c.query('fld[97]/val')
,t.c.query('fld[98]/val')
,t.c.query('fld[99]/val')
,t.c.query('fld[100]/val')
,t.c.query('fld[101]/val')
,t.c.query('fld[102]/val')
,t.c.query('fld[103]/val')
,t.c.query('fld[104]/val')
,t.c.query('fld[105]/val')
,t.c.query('fld[106]/val')
,t.c.query('fld[107]/val')
,t.c.query('fld[108]/val')
,t.c.query('fld[109]/val')
,t.c.query('fld[110]/val')
,t.c.query('fld[111]/val')
,t.c.query('fld[112]/val')
,t.c.query('fld[113]/val')
,t.c.query('fld[114]/val')
,t.c.query('fld[115]/val')
,t.c.query('fld[116]/val')
,t.c.query('fld[117]/val')
,t.c.query('fld[118]/val')
,t.c.query('fld[119]/val')
,t.c.query('fld[120]/val')
,t.c.query('fld[121]/val')
,t.c.query('fld[122]/val')
,t.c.query('fld[123]/val')
,t.c.query('fld[124]/val')
,t.c.query('fld[125]/val')
,t.c.query('fld[126]/val')
,t.c.query('fld[127]/val')
,t.c.query('fld[128]/val')
,t.c.query('fld[129]/val')
,t.c.query('fld[130]/val')
,t.c.query('fld[131]/val')
,t.c.query('fld[132]/val')
,t.c.query('fld[133]/val')
,t.c.query('fld[134]/val')
,t.c.query('fld[135]/val')
,t.c.query('fld[136]/val')
,t.c.query('fld[137]/val')
,t.c.query('fld[138]/val')
,t.c.query('fld[139]/val')
,t.c.query('fld[140]/val')
,t.c.query('fld[141]/val')
,t.c.query('fld[142]/val')
,t.c.query('fld[143]/val')
,t.c.query('fld[144]/val')
,t.c.query('fld[145]/val')
,t.c.query('fld[146]/val')
,t.c.query('fld[147]/val')
,t.c.query('fld[148]/val')
,t.c.query('fld[149]/val')
,t.c.query('fld[150]/val')
,t.c.query('fld[151]/val')
,t.c.query('fld[152]/val')
,t.c.query('fld[153]/val')
,t.c.query('fld[154]/val')
,t.c.query('fld[155]/val')
,t.c.query('fld[156]/val')
,t.c.query('fld[157]/val')
,t.c.query('fld[158]/val')
,t.c.query('fld[159]/val')
,t.c.query('fld[160]/val')
,t.c.query('fld[161]/val')
,t.c.query('fld[162]/val')
,t.c.query('fld[163]/val')
,t.c.query('fld[164]/val')
,t.c.query('fld[165]/val')
,t.c.query('fld[166]/val')
,t.c.query('fld[167]/val')
,t.c.query('fld[168]/val')
,t.c.query('fld[169]/val')
,t.c.query('fld[170]/val')
,t.c.query('fld[171]/val')
,t.c.query('fld[172]/val')
,t.c.query('fld[173]/val')
,t.c.query('fld[174]/val')
,t.c.query('fld[175]/val')
,t.c.query('fld[176]/val')
,t.c.query('fld[177]/val')
,t.c.query('fld[178]/val')
,t.c.query('fld[179]/val')
,t.c.query('fld[180]/val')
,t.c.query('fld[181]/val')
,t.c.query('fld[182]/val')
,t.c.query('fld[183]/val')
,t.c.query('fld[184]/val')
,t.c.query('fld[185]/val')
,t.c.query('fld[186]/val')
,t.c.query('fld[187]/val')
,t.c.query('fld[188]/val')
,t.c.query('fld[189]/val')
,t.c.query('fld[190]/val')
,t.c.query('fld[191]/val')
,t.c.query('fld[192]/val')
,t.c.query('fld[193]/val')
,t.c.query('fld[194]/val')
,t.c.query('fld[195]/val')
,t.c.query('fld[196]/val')
,t.c.query('fld[197]/val')
,t.c.query('fld[198]/val')
,t.c.query('fld[199]/val')
,t.c.query('fld[200]/val')
from INVF b
cross apply b.u2_data.nodes('/data') as t(c)



drop table INVFF
go
create table INVFF
(id varchar(900) not null primary key
,fld001 xml null
,fld002 xml null
,fld003 xml null
,fld004 xml null
,fld005 xml null
,fld006 xml null
,fld007 xml null
,fld008 xml null
,fld009 xml null
,fld010 xml null
,fld011 xml null
,fld012 xml null
,fld013 xml null
,fld014 xml null
,fld015 xml null
,fld016 xml null
,fld017 xml null
,fld018 xml null
,fld019 xml null
,fld020 xml null
,fld021 xml null
,fld022 xml null
,fld023 xml null
,fld024 xml null
,fld025 xml null
,fld026 xml null
,fld027 xml null
,fld028 xml null
,fld029 xml null
,fld030 xml null
,fld031 xml null
,fld032 xml null
,fld033 xml null
,fld034 xml null
,fld035 xml null
,fld036 xml null
,fld037 xml null
,fld038 xml null
,fld039 xml null
,fld040 xml null
,fld041 xml null
,fld042 xml null
,fld043 xml null
,fld044 xml null
,fld045 xml null
,fld046 xml null
,fld047 xml null
,fld048 xml null
,fld049 xml null
,fld050 xml null
,fld051 xml null
,fld052 xml null
,fld053 xml null
,fld054 xml null
,fld055 xml null
,fld056 xml null
,fld057 xml null
,fld058 xml null
,fld059 xml null
,fld060 xml null
,fld061 xml null
,fld062 xml null
,fld063 xml null
,fld064 xml null
,fld065 xml null
,fld066 xml null
,fld067 xml null
,fld068 xml null
,fld069 xml null
,fld070 xml null
,fld071 xml null
,fld072 xml null
,fld073 xml null
,fld074 xml null
,fld075 xml null
,fld076 xml null
,fld077 xml null
,fld078 xml null
,fld079 xml null
,fld080 xml null
,fld081 xml null
,fld082 xml null
,fld083 xml null
,fld084 xml null
,fld085 xml null
,fld086 xml null
,fld087 xml null
,fld088 xml null
,fld089 xml null
,fld090 xml null
,fld091 xml null
,fld092 xml null
,fld093 xml null
,fld094 xml null
,fld095 xml null
,fld096 xml null
,fld097 xml null
,fld098 xml null
,fld099 xml null
,fld100 xml null
,fld101 xml null
,fld102 xml null
,fld103 xml null
,fld104 xml null
,fld105 xml null
,fld106 xml null
,fld107 xml null
,fld108 xml null
,fld109 xml null
,fld110 xml null
,fld111 xml null
,fld112 xml null
,fld113 xml null
,fld114 xml null
,fld115 xml null
,fld116 xml null
,fld117 xml null
,fld118 xml null
,fld119 xml null
,fld120 xml null
,fld121 xml null
,fld122 xml null
,fld123 xml null
,fld124 xml null
,fld125 xml null
,fld126 xml null
,fld127 xml null
,fld128 xml null
,fld129 xml null
,fld130 xml null
,fld131 xml null
,fld132 xml null
,fld133 xml null
,fld134 xml null
,fld135 xml null
,fld136 xml null
,fld137 xml null
,fld138 xml null
,fld139 xml null
,fld140 xml null
,fld141 xml null
,fld142 xml null
,fld143 xml null
,fld144 xml null
,fld145 xml null
,fld146 xml null
,fld147 xml null
,fld148 xml null
,fld149 xml null
,fld150 xml null
,fld151 xml null
,fld152 xml null
,fld153 xml null
,fld154 xml null
,fld155 xml null
,fld156 xml null
,fld157 xml null
,fld158 xml null
,fld159 xml null
,fld160 xml null
,fld161 xml null
,fld162 xml null
,fld163 xml null
,fld164 xml null
,fld165 xml null
,fld166 xml null
,fld167 xml null
,fld168 xml null
,fld169 xml null
,fld170 xml null
,fld171 xml null
,fld172 xml null
,fld173 xml null
,fld174 xml null
,fld175 xml null
,fld176 xml null
,fld177 xml null
,fld178 xml null
,fld179 xml null
,fld180 xml null
,fld181 xml null
,fld182 xml null
,fld183 xml null
,fld184 xml null
,fld185 xml null
,fld186 xml null
,fld187 xml null
,fld188 xml null
,fld189 xml null
,fld190 xml null
,fld191 xml null
,fld192 xml null
,fld193 xml null
,fld194 xml null
,fld195 xml null
,fld196 xml null
,fld197 xml null
,fld198 xml null
,fld199 xml null
,fld200 xml null
)