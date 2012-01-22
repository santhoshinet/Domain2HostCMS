-- HopestrackDL.Employer
CREATE TABLE [employer] (
    [employer_id] INT NOT NULL,             -- <internal-pk>
    [<_academic>k___backing_field] VARCHAR(255) NULL, -- <Academic>k__BackingField
    [<_ddtnl_bnfts>k___bcking_field] VARCHAR(255) NULL, -- <AdditionalBenefits>k__BackingField
    [<_ddrss_fth_cmpny>k___bckng_fl] VARCHAR(255) NULL, -- <AddressOftheCompany>k__BackingField
    [<_age_from>k___backing_field] VARCHAR(255) NULL, -- <AgeFrom>k__BackingField
    [<_age_to>k___backing_field] VARCHAR(255) NULL, -- <AgeTo>k__BackingField
    [<_cntct_prsn>k___backing_field] VARCHAR(255) NULL, -- <ContactPerson>k__BackingField
    [<_cntrctbslbrs>k___bckng_field] VARCHAR(255) NULL, -- <Contractbaselabours>k__BackingField
    [<_email_i_d>k___backing_field] VARCHAR(255) NULL, -- <EmailID>k__BackingField
    [<_xprnc_from>k___backing_field] VARCHAR(255) NULL, -- <ExperienceFrom>k__BackingField
    [<_xprence_to>k___backing_field] VARCHAR(255) NULL, -- <ExperienceTo>k__BackingField
    [<_fax_no>k___backing_field] VARCHAR(255) NULL, -- <FaxNo>k__BackingField
    [<_grss_slry_frm>k___bckng_feld] VARCHAR(255) NULL, -- <GrossSalaryFrom>k__BackingField
    [<_grss_slry_t>k___bcking_field] VARCHAR(255) NULL, -- <GrossSalaryTo>k__BackingField
    [<_industry>k___backing_field] VARCHAR(255) NULL, -- <Industry>k__BackingField
    [<_ntrvw_lctn>k___backing_field] VARCHAR(255) NULL, -- <InterviewLocation>k__BackingField
    [<_ntrvwr_nme>k___backing_field] VARCHAR(255) NULL, -- <InterviewerName>k__BackingField
    [<_ntrvwr_pstn>k___bcking_field] VARCHAR(255) NULL, -- <InterviewerPosition>k__BackingField
    [<_jbdscrpton>k___backing_field] VARCHAR(255) NULL, -- <Jobdescription>k__BackingField
    [<_mobile_no>k___backing_field] VARCHAR(255) NULL, -- <MobileNo>k__BackingField
    [<_nm_fth_cmpny>k___bckng_field] VARCHAR(255) NULL, -- <NameOftheCompany>k__BackingField
    [<_ntr_f_bsnss>k___bcking_field] VARCHAR(255) NULL, -- <NatureOfBusiness>k__BackingField
    [<_n_f_pstns_rqrd>k___bckng_fld] VARCHAR(255) NULL, -- <NoOfPositionsRequired>k__BackingField
    [<_phn_n_ffce>k___backing_field] VARCHAR(255) NULL, -- <PhoneNoOffice>k__BackingField
    [<_phn_n_rsdnt>k___bcking_field] VARCHAR(255) NULL, -- <PhoneNoResident>k__BackingField
    [<_plc_f_work>k___backing_field] VARCHAR(255) NULL, -- <PlaceOfWork>k__BackingField
    [<_position>k___backing_field] VARCHAR(255) NULL, -- <Position>k__BackingField
    [<_prferences>k___backing_field] VARCHAR(255) NULL, -- <Preferences>k__BackingField
    [<_prbbl_date>k___backing_field] VARCHAR(255) NULL, -- <ProbableDate>k__BackingField
    [<_prbbl_dt_f_ntrvw>k___bckng_f] VARCHAR(255) NULL, -- <ProbableDateOfInterview>k__BackingField
    [<_tchncl_r_prfssnl>k___bckng_f] VARCHAR(255) NULL, -- <TechnicalOrProfessional>k__BackingField
    [<_ttlrglrmnpwr>k___bckng_field] VARCHAR(255) NULL, -- <Totalregularmanpower>k__BackingField
    [<_ttltrnover>k___backing_field] VARCHAR(255) NULL, -- <Totalturnover>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_employer] PRIMARY KEY ([employer_id])
)
go

