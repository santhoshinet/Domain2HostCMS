-- HopestrackDL.Attachment
CREATE TABLE [attachment] (
    [attachment_id] INT NOT NULL,           -- <internal-pk>
    [<_filedata>k___backing_field] image NULL, -- <Filedata>k__BackingField
    [<_filename>k___backing_field] VARCHAR(255) NULL, -- <Filename>k__BackingField
    [<_id>k___backing_field] VARCHAR(255) NULL, -- <Id>k__BackingField
    [<_mime_type>k___backing_field] VARCHAR(255) NULL, -- <MimeType>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_attachment] PRIMARY KEY ([attachment_id])
)
go

-- HopestrackDL.JobSeeker
CREATE TABLE [job_seeker] (
    [job_seeker_id] INT NOT NULL,           -- <internal-pk>
    [<_crrent_ctc>k___backing_field] FLOAT NOT NULL, -- <CurrentCtc>k__BackingField
    [<_dt_f_birth>k___backing_field] DATETIME NOT NULL, -- <DateOfBirth>k__BackingField
    [<_email_i_d>k___backing_field] VARCHAR(255) NULL, -- <EmailID>k__BackingField
    [<_xpcted_ctc>k___backing_field] FLOAT NOT NULL, -- <ExpectedCtc>k__BackingField
    [<_first_name>k___backing_field] VARCHAR(255) NULL, -- <FirstName>k__BackingField
    [<_functional>k___backing_field] VARCHAR(255) NULL, -- <Functional>k__BackingField
    [<_fnctnl_thrs>k___bcking_field] VARCHAR(255) NULL, -- <FunctionalOthers>k__BackingField
    [<_gender>k___backing_field] VARCHAR(255) NULL, -- <Gender>k__BackingField
    [<_industry>k___backing_field] VARCHAR(255) NULL, -- <Industry>k__BackingField
    [<_ndstry_thrs>k___bcking_field] VARCHAR(255) NULL, -- <IndustryOthers>k__BackingField
    [<_last_name>k___backing_field] VARCHAR(255) NULL, -- <LastName>k__BackingField
    [<_mobile_no>k___backing_field] VARCHAR(255) NULL, -- <MobileNo>k__BackingField
    [<_ntc_period>k___backing_field] FLOAT NOT NULL, -- <NoticePeriod>k__BackingField
    [<_ptnl_ml__d>k___backing_field] VARCHAR(255) NULL, -- <OptionalEmailID>k__BackingField
    [<_pg_others>k___backing_field] VARCHAR(255) NULL, -- <PgOthers>k__BackingField
    [<_pg_qlfcton>k___backing_field] VARCHAR(255) NULL, -- <PgQualification>k__BackingField
    [<_phone_no>k___backing_field] VARCHAR(255) NULL, -- <PhoneNo>k__BackingField
    [<_prmry_skll_st>k___bckng_feld] VARCHAR(255) NULL, -- <PrimarySkillSet>k__BackingField
    [<_rlvnt_xprnc>k___bcking_field] FLOAT NOT NULL, -- <RelaventExperience>k__BackingField
    [attachment_id] INT NULL,               -- <ResumeAttachment>k__BackingField
    [<_scndry_skll_st>k___bckng_fld] VARCHAR(255) NULL, -- <SecondarySkillSet>k__BackingField
    [<_ttl_xprnce>k___backing_field] FLOAT NOT NULL, -- <TotalExperience>k__BackingField
    [<_ug_others>k___backing_field] VARCHAR(255) NULL, -- <UgOthers>k__BackingField
    [<_g_qlfction>k___backing_field] VARCHAR(255) NULL, -- <UgQualification>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_job_seeker] PRIMARY KEY ([job_seeker_id])
)
go

