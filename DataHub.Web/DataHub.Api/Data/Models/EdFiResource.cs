namespace DataHub.Api.Data.Models
{
    public class EdFiResource
    {
        public string ResourceNamespace { get; }
        public string ResourceName { get; }

        public EdFiResource(string resourceName, string resourceNamespace = "ed-fi")
        {
            ResourceNamespace = resourceNamespace;
            ResourceName = resourceName;
        }
    }
}