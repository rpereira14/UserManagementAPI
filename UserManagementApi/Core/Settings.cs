namespace UserManagementApi.Core
{
    public class Settings
    {
        public string AlloAllowedHosts { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DbServer { get; set; } = string.Empty;
        public string DbPort { get; set; } = string.Empty;
        public string DbCatalog { get; set; } = string.Empty;
        public string DbUser { get; set; } = string.Empty;
        public string DbPassword { get; set; } = string.Empty;
    }
}
