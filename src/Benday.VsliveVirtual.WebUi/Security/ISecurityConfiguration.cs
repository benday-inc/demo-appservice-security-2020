namespace Benday.VsliveVirtual.WebUi.Security
{
    public interface ISecurityConfiguration
    {
        bool DevelopmentMode { get; }
        bool AzureActiveDirectory { get; }
        bool Google { get; }
        bool MicrosoftAccount { get; }
    }
}