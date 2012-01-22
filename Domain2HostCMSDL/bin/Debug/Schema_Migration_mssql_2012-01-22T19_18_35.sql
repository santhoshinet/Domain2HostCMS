-- Domain2HostCMSDL.UserAuthentication
CREATE TABLE [user_authentication] (
    [user_authentication_id] INT NOT NULL,  -- <internal-pk>
    [<_domain>k___backing_field] VARCHAR(255) NULL, -- <Domain>k__BackingField
    [<_username>k___backing_field] VARCHAR(255) NULL, -- <Username>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_user_authentication] PRIMARY KEY ([user_authentication_id])
)
go

