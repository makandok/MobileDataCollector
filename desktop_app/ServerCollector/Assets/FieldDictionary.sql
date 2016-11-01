 create table FieldDictionary(
 --  drop table FieldDictionary
project varchar(255),
dataType varchar(255),
fieldName varchar(255),
fieldType varchar(255),
IsIndexed varchar(255),
IsRequired varchar(255),
Label varchar(255),
name varchar(255),
PageId varchar(255),
pageName varchar(255),
choiceName varchar(255),
groupKey varchar(255),
listName varchar(255),
lookupValue varchar(255)
);
go

ALTER view [dbo].[fd] as 
SELECT [Serial],
		[project]
      ,[dataType]
      ,[fieldName]
      ,[fieldType]
      ,[IsIndexed]
      ,[IsRequired]
      ,[Label]
      ,[name]
      ,[PageId]
      ,[pageName]
      ,[choiceName]
      ,[groupKey]
      ,[listName]
      ,[lookupValue]
      
  FROM [dbo].[FieldDictionary]union
SELECT 0, '' [project]
      ,'EditText' [dataType]
      ,'id' [fieldName]
      ,'Text' [fieldType]
      ,'True' [IsIndexed]
      ,'True'[IsRequired]
      ,'id' [Label]
      ,'id' [name]
      ,'0'[PageId]
      ,'ilsp_main1'[pageName]
      ,''[choiceName]
      ,''[groupKey],'','' union
SELECT 0,'' [project]
      ,'EditText' [dataType]
      ,'entityid' [fieldName]
      ,'Text' [fieldType]
      ,'True' [IsIndexed]
      ,'True'[IsRequired]
      ,'entityid' [Label]
      ,'entityid' [name]
      ,'0'[PageId]
      ,'ilsp_main1'[pageName]
      ,''[choiceName]
      ,''[groupKey],'','' union
	SELECT 0,'' [project]
      ,'DatePicker' [dataType]
      ,'sys_editdate' [fieldName]
      ,'Date' [fieldType]
      ,'True' [IsIndexed]
      ,'True'[IsRequired]
      ,'sys_editdate' [Label]
      ,'sys_editdate' [name]
      ,'0'[PageId]
      ,'ilsp_main1'[pageName]
      ,''[choiceName]
      ,''[groupKey],'','' union
SELECT 0,'' [project]
      ,'DatePicker' [dataType]
      ,'sys_datecreated' [fieldName]
      ,'Date' [fieldType]
      ,'True' [IsIndexed]
      ,'True'[IsRequired]
      ,'sys_datecreated' [Label]
      ,'sys_datecreated' [name]
      ,'0'[PageId]
      ,'ilsp_main1'[pageName]
      ,''[choiceName]
      ,''[groupKey],'','';
GO

ALTER view [dbo].[DataDictionary] as
select serial,
datatype, fieldtype, label,
case when len(LookupValue) > 0 then 
	replace(label, '['+LookupValue+']','')
	else Label
	end GeneralLabel
,groupkey, name longname,
REPLACE(FieldNameActual,'ilsp_','') VariableName,
--FieldNameActual fieldname, 
listname, lookupvalue, choicename lookupvalueclean,
lookupcode, pagename 
from (
	select f.*, lookupCode,
	--case when len(fieldname) = 0 then name else fieldname end  FieldNameActual
	case when datatype='CheckBox' then name
	else case when len(fieldname) = 0 then name else fieldname end 
	end FieldNameActual
	 From fd f 
	left join LookupValues v on f.listName = v.lookupname
	and f.lookupValue = v.lookupValue
)a
GO

--ALTER view [dbo].[DataDictionary] as
--select serial,
--datatype, fieldtype, label,
--case when len(LookupValue) > 0 then 
--	replace(label, '['+LookupValue+']','')
--	else Label
--	end GeneralLabel
--,groupkey, name longname,
--REPLACE(FieldNameActual,'ilsp_','') VariableName,
----FieldNameActual fieldname, 
--listname, lookupvalue, choicename lookupvalueclean,
--lookupcode, pagename 
--from (
--	select f.*, lookupCode,
--	case when len(fieldname) = 0 then name else fieldname end FieldNameActual
--	 From fd f 
--	left join LookupValues v on f.listName = v.lookupname
--	and f.lookupValue = v.lookupValue
--	--where (f.listname is not null and len(f.listname) > 0)
--	--and v.lookupname is null
--)a
--go


ALTER view [dbo].[finalDataset] as
select RecordId, Serial, GeneralLabel,FullVariableName,IsCodedValue,Value,GeneralVariableName,datatype
from (
	select RecordId, Serial, 
	 GeneralLabel
	, case when len(groupkey) > 0 then groupkey +'_'+ VariableName else VariableName end FullVariableName
	, case when lookupcode is null then 0 else 1 end as IsCodedValue
	, case when datatype = 'checkbox' then '1'
	else case when lookupcode is null then fieldvalue else convert(varchar(50),lookupcode) end 
	end as Value,
	--, case when lookupcode is null then fieldvalue else convert(varchar(50),lookupcode) end as Value,
	VariableName as GeneralVariableName,
	label DescriptiveLabel, longname uniquename,datatype
	From 
	(
		select FieldValue, RecordId
		, d.* from 
		lsp_main_local_fvs fvs left join DataDictionary d on fvs.fieldname = d.longname
	)b
) dataset
go