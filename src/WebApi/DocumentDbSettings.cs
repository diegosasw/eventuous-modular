using System.Diagnostics.CodeAnalysis;

namespace WebApi;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
internal class DocumentDbSettings
{
    public string Hostname { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string DatabaseName { get; init; } = string.Empty;
    public bool IsCluster { get; init; } = false;
    private string Suffix => 
        IsCluster 
            ? "/?replicaSet=rs0&readPreference=secondaryPreferred&retryWrites=false" 
            : string.Empty;
    
    public string ConnectionString => $"mongodb://{Username}:{Password}@{Hostname}:27017{Suffix}";
}