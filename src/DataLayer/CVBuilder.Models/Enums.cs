using System.ComponentModel;

namespace CVBuilder.Models;

public enum RoleTypes
{
    [Description("Admin")] Admin = 1,
    [Description("HR")] Hr,
    [Description("User")] User,
    [Description("Client")] Client,
    [Description("Sale")] Sale,
    [Description("Finance")] Finance,
}

public enum LanguageLevel
{
    [Description("Elementary")] Elementary = 1,
    [Description("PreIntermediate")] PreIntermediate,
    [Description("Intermediate")] Intermediate,
    [Description("UpperIntermediate")] UpperIntermediate,
    [Description("Proficiency")] Proficiency,
    [Description("Native")] Native
}

public enum SkillLevel
{
    [Description("Basic")] Basic = 1,
    [Description("Intermediate")] Intermediate = 2,
    [Description("Advanced")] Advanced = 3
}

public enum StatusProposal
{
    [Description("Created")] Created = 1,
    [Description("InReview")] InReview,
    [Description("Approved")] Approved,
    [Description("Done")] Done,
    [Description("Denied")] Denied,
    [Description("In Working")] InWorking
}

public enum StatusProposalResume
{
    [Description("NotSelected")] NotSelected = 1,
    [Description("Selected")] Selected,
    [Description("Denied")] Denied
}

public enum PositionLevel
{
    Junior = 1,
    Middle = 2,
    Senior = 3,
    TechLead = 4,
    StrongJunior = 5,
    StrongMiddle = 6
}

public enum AvailabilityStatus
{
    Available = 1,
    PartialAvailable,
    AvailableSoon,
    VeryCarefulAvailable,
    Busy,
}

public enum DateInterval
{
    OneWeek = 1,
    TwoWeeks,
    ThreeWeeks,
    OneMonth,
    TwoMonths,
    ThreeMonths,
    MoreThanThreeMonths,
    MoreThanSixMonths,
    MoreThanOneYear,
}

public enum ExpenseType
{
    Office = 1,
    PermanentAdministrative,
    ChangingAdministrative
}

public enum Currency
{
    Usd = 1,
    Hryvnia
}

public enum ResumeHistoryStatus
{
    Created = 1,
    Updated,
    Deleted,
    Recovered,
    Duplicated,
}