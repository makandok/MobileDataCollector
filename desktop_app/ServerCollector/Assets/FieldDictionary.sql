 create table FieldDictionary(
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
groupKey varchar(255)
);
go

create view fd as 
select * from FieldDictionary union
SELECT '' [project]
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
      ,''[groupKey] union
SELECT '' [project]
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
      ,''[groupKey] union
	SELECT '' [project]
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
      ,''[groupKey] union
SELECT '' [project]
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
      ,''[groupKey]
	;