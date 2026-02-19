using System;

namespace ATS.AI
{
    public class AIOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string OrganizationId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string Provider { get; set; } = "OpenAI";
        public string Model { get; set; } = "gpt-4o-mini";
        public int MaxBudgetPerAnalysisCents { get; set; } = 5;
        public bool Enabled { get; set; } = true;
        public int MaxRetries { get; set; } = 3;
        public int TimeoutSeconds { get; set; } = 60;
        public decimal Temperature { get; set; } = 0.1m;
        public int MaxTokens { get; set; } = 2000;
        public int ConcurrencyLimit { get; set; } = 4;
    }
}

