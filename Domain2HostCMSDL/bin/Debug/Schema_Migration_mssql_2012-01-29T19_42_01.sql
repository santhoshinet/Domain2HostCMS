-- modify column for field <Content>k__BackingField
ALTER TABLE [content_page] ALTER COLUMN [<_content>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <DomainName>k__BackingField
ALTER TABLE [content_page] ADD [<_dmain_name>k___backing_field] VARCHAR(255) NULL
go

