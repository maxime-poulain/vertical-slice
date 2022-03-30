using Ardalis.SmartEnum;

namespace Catalog.Shared.Enumerations.Training;

public class Topic : SmartEnum<Topic>
{
    public static readonly Topic Marketing               = new(nameof(Marketing), 1);
    public static readonly Topic HealthCare              = new(nameof(HealthCare), 2);
    public static readonly Topic Accounting              = new(nameof(Accounting), 3);
    public static readonly Topic MediaAndArts            = new(nameof(MediaAndArts), 4);
    public static readonly Topic Fashion                 = new(nameof(Fashion), 5);
    public static readonly Topic BusinessManagementLegal = new(nameof(BusinessManagementLegal), 6);
    public static readonly Topic ProjectManagement       = new(nameof(ProjectManagement), 7);
    public static readonly Topic Sport                   = new(nameof(Sport), 8);
    public static readonly Topic OperationAndPurchasing  = new(nameof(OperationAndPurchasing), 9);
    public static readonly Topic SoftwareWebDevelopment  = new(nameof(SoftwareWebDevelopment), 10);

    private Topic(string name, int value) : base(name, value)
    {
    }
}