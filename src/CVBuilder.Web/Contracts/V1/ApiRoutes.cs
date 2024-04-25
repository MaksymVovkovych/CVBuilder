namespace CVBuilder.Web.Contracts.V1;

public static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = Root + "/" + Version;

    public static class Identity
    {
        private const string BaseIdentity = Base + "/identity";

        public const string Login = BaseIdentity + "/login";
        public const string LoginByUrl = BaseIdentity + "/login/{url}";
        public const string Register = BaseIdentity + "/register";
        public const string GetCurrentUserByToken = BaseIdentity + "/getcurrentuserbytoken";
        public const string LoginViaGoogle = BaseIdentity + "/login/google";
        public const string Refresh = BaseIdentity + "/refresh";
        public const string Logout = BaseIdentity + "/logout";
        public const string Revoke = BaseIdentity + "/revoke";

        public const string GenerateToken = BaseIdentity + "/generateToken";
    }

    public static class User
    {
        private const string BaseUser = Base + "/user";
        public const string GetAllUsers = BaseUser;
        public const string ChangeUserRole = BaseUser + "/role";
        public const string ByRole = BaseUser + "/role/{roleName}";
        public const string CurrentUser = BaseUser + "/current";
        public const string GetUserById = BaseUser + "/{id}";
        public const string UpdateUserStatus = BaseUser + "/status";
        public const string CreateUser = BaseUser;
        public const string UpdateUser = BaseUser;
    }

    public static class Role
    {        private const string BaseRole = Base + "/role";

        public const string GetAllRoles = BaseRole;
    }

    public static class Confirmation
    {
        private const string BaseConfirmation = Base + "/confirmation";

        public const string SendEmail = BaseConfirmation + "/sendEmail";
        public const string ConfirmEmail = BaseConfirmation + "/confirmEmail";
        public const string SendSms = BaseConfirmation + "/sendSms";
        public const string ConfirmPhone = BaseConfirmation + "/confirmPhone";
    }

    public static class Data
    {
        private const string BaseData = Base + "/data";
        private const string BaseDataType = BaseData + "/types";

        public const string LevelLanguage = BaseDataType + "/levelLanguages";
        public const string LevelSkill = BaseDataType + "/levelSkills";
        public const string UploadImage = BaseData + "/image";
        public const string Test = BaseData + "/test";
    }

    public static class Proposal
    {
        private const string BaseProposal = Base + "/proposals";
        public const string CreateProposal = BaseProposal;
        public const string GetProposalById = BaseProposal + "/{id}";
        public const string UpdateProposal = BaseProposal;
        public const string GetAllProposals = BaseProposal;
        public const string GetAllArchiveProposals = BaseProposal + "/archive";
        public const string ApproveProposal = BaseProposal + "/approve";
        public const string RecoverProposal = BaseProposal + "/recover/{id}";
        public const string GetProposalResume = BaseProposal + "/{proposalId}/resume/{proposalResumeId}";
        public const string GetProposalResumeHtml = BaseProposal + "/{proposalId}/resume/{proposalResumeId}/html";
        public const string GetPdfProposalResume = BaseProposal + "/{proposalId}/resume/{proposalResumeId}/pdf";
        public const string GetProposalResumeByUrl = BaseProposal + "/resume/{url}";
        public const string GetPdfProposalResumeByUrl = BaseProposal + "/resume/{url}/pdf";
        public const string GetDocxProposalResume = BaseProposal + "/{proposalId}/resume/{proposalResumeId}/docx";
        public const string GetDocxProposalResumeByUrl = BaseProposal + "/resume/{url}/docx";
        public const string GetProposalsCalendar = BaseProposal + "/calendar";
        public const string UpdateWorkDay = BaseProposal + "/calendar/workday/{proposalResumeId}";
        public const string CreateTimePlanning = BaseProposal + "/calendar/planning";
        public const string GetAllConsumption = BaseProposal + "/consumption";
        public const string GetExpensesByMonth = BaseProposal + "/expenses";
        public const string CreateExpense = BaseProposal + "/expenses";
        public const string UpdateExpense = BaseProposal + "/expenses";
        public const string DeleteExpense = BaseProposal + "/expenses/{expenseId}";
        public const string DuplicateExpense = BaseProposal + "/expenses/duplicate";
    }

    public static class ProposalBuild
    {
        private const string BaseProposalBuild = Base + "/proposalBuilds";
        public const string CreateProposalBuild = BaseProposalBuild;
        public const string UpdateProposalBuild = BaseProposalBuild;
        public const string GetAllProposalBuilds = BaseProposalBuild;
    }

    public static class Complexity
    {
        private const string BaseComplexity = Base + "/complexities";
        public const string GetAllComplexities = BaseComplexity;
        public const string CreateComplexity = BaseComplexity;
        public const string UpdateComplexity = BaseComplexity;
        public const string DeleteComplexity = BaseComplexity + "/{id}";
    }

    public static class Resume
    {
        private const string BaseCv = Base + "/resume";
        public const string CreateResume = BaseCv;
        public const string GetResumePdf = BaseCv + "/pdf/{id}";
        public const string GetResumePdfByUrl = BaseCv + "/url/{url}/pdf";
        public const string GetAllResume = BaseCv;
        public const string GetResumeById = BaseCv + "/{id}";
        public const string GetResumeByUrl = BaseCv + "/url/{url}";
        public const string UpdateResume = BaseCv;
        public const string DuplicateResume = BaseCv + "/duplicate/{id}";
        public const string DeleteResume = BaseCv + "/{id}";
        public const string UploadImage = BaseCv + "/{resumeId}/image";
        public const string DeleteImage = BaseCv + "/{resumeId}/image";
        public const string CreateImage = BaseCv + "/image";
        public const string RecoverResume = BaseCv + "/{id}/recover";
        public const string GetAllResumeTemplates = BaseCv + "/templates";
        public const string GetAllResumeByPositions = BaseCv + "/position";
        public const string GetAllResumeByProposalBuild = BaseCv + "/proposalBuild/{id}";
        public const string CreateTemplate = BaseCv + "/templates/{name}";
        public const string UpdateTemplate = BaseCv + "/templates/{id}/{name}";
        public const string GetAllTemplates = BaseCv + "/templates";
        public const string GetTemplateById = BaseCv + "/templates/{id}";
        public const string DeleteTemplateById = BaseCv + "/templates/{id}";
        public const string GetResumeHtmlById = BaseCv + "/{id}/html";
        public const string UpdateSalaryRate = BaseCv + "/{resumeId}/salaryRate/{salaryRate}";
        public const string GetResumeDocx = BaseCv + "/docx/{id}";
        public const string GetResumeDocxByUrl = BaseCv + "/url/{url}/docx";
        public const string UpdateDocxInTemplate = BaseCv + "/templates/docx/{id}";
        public const string GetDocxTemplateById = BaseCv + "/templates/docx/{id}";
        public const string GetAllResumeHistory = BaseCv + "/{id}/history";
    }

    public static class File
    {
        private const string BaseFile = Base + "/files";
        public const string CreatFile = BaseFile;
        public const string GetFileById = BaseFile + "/{id}";
        public const string GetAllFileUrl = BaseFile + "/list";
    }

    public static class Position
    {
        private const string BasePosition = Base + "/positions";
        public const string CreatePosition = BasePosition;
        public const string UpdatePosition = BasePosition;
        public const string DeletePosition = BasePosition + "/{id}";
        public const string GetAllPositions = BasePosition;
        public const string GetPositionsById = BasePosition + "/{id}";
    }

    public static class Skill
    {
        private const string BaseSkill = Base + "/skills";
        public const string CreateSkill = BaseSkill;
        public const string UpdateSkill = BaseSkill;
        public const string DeleteSkill = BaseSkill + "/{id}";
        public const string GetSkill = BaseSkill + "/search";
        public const string SkillsGetAll = BaseSkill;
    }

    public static class Education
    {
        private const string BaseEducation = Base + "/educations";
        public const string CreateEducation = BaseEducation;
        public const string GetAllEducation = BaseEducation;
        public const string GetEducation = BaseEducation + "/{id}";
    }

    public static class Experience
    {
        private const string BaseExperience = Base + "/experiences";
        public const string CreateExperience = BaseExperience;
        public const string GetAllExperience = BaseExperience;
        public const string GetExperienceById = BaseExperience + "/{id}";
    }

    public static class Language
    {
        private const string BaseLanguage = Base + "/languages";
        public const string CreateLanguage = BaseLanguage;
        public const string UpdateLanguage = BaseLanguage;
        public const string DeleteLanguage = BaseLanguage + "/{id}";
        public const string GetLanguage = BaseLanguage + "/search";
        public const string LanguageGetAll = BaseLanguage;
    }

    public static class Client
    {
        private const string BaseClient = Base + "/client";
        public const string GetAllClients = BaseClient;
        public const string GetClientById = BaseClient + "/{id}";
        public const string CreateClient = BaseClient;
        public const string UpdateClient = BaseClient;
    }

    public static class Holiday
    {
        private const string BaseHoliday = Base + "/holiday";
        public const string CreateHoliday = BaseHoliday;
        public const string UpdateHoliday = BaseHoliday;
        public const string DeleteHoliday = BaseHoliday+"/{id}";
        public const string GetAllHoliday = BaseHoliday;
    }
}