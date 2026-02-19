using System;

namespace ATS.Candidates.Dtos
{
    public class ResumeDownloadDto
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}

