using Volo.Abp.BlobStoring;

namespace ATS.Domain.Candidates.BlobContainer
{
    [BlobContainerName("candidates")]
    public class CandidateContainer
    {
        // This class is used to configure the blob container for candidate files
        // Configure in your module:
        /*
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<CandidateContainer>(container =>
            {
                container.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = "/app-data/candidates";
                });
            });
        });
        */
    }
}
