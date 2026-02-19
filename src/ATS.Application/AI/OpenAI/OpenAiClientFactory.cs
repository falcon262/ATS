using System;
using System.ClientModel;
using Microsoft.Extensions.Options;
using OpenAI;
using Volo.Abp.DependencyInjection;

namespace ATS.AI.OpenAI
{
    public class OpenAiClientFactory : ITransientDependency
    {
        private readonly AIOptions _options;

        public OpenAiClientFactory(IOptions<AIOptions> options)
        {
            _options = options.Value;
        }

        public OpenAIClient CreateClient()
        {
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new InvalidOperationException("OpenAI API Key is not configured");
            }

            var credential = new ApiKeyCredential(_options.ApiKey);
            var options = new OpenAIClientOptions();
            
            if (!string.IsNullOrWhiteSpace(_options.OrganizationId))
            {
                options.OrganizationId = _options.OrganizationId;
            }

            if (!string.IsNullOrWhiteSpace(_options.ProjectId))
            {
                options.ProjectId = _options.ProjectId;
            }

            return new OpenAIClient(credential, options);
        }
    }
}

