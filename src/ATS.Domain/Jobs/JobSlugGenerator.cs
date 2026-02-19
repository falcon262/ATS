using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ATS.Jobs
{
    /// <summary>
    /// Helper class for generating URL-friendly slugs from job titles
    /// </summary>
    public static class JobSlugGenerator
    {
        /// <summary>
        /// Generates a URL-friendly slug from a job title and job ID
        /// </summary>
        /// <param name="title">The job title</param>
        /// <param name="jobId">The job ID for uniqueness</param>
        /// <returns>A URL-friendly slug</returns>
        /// <example>
        /// Input: "Senior Software Engineer", Guid
        /// Output: "senior-software-engineer-a1b2c3d4"
        /// </example>
        public static string GenerateFromTitle(string title, Guid jobId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            // Convert to lowercase
            var slug = title.ToLowerInvariant();

            // Remove special characters and replace with empty string
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Replace multiple spaces or hyphens with single hyphen
            slug = Regex.Replace(slug, @"[\s-]+", " ");

            // Trim whitespace
            slug = slug.Trim();

            // Replace spaces with hyphens
            slug = slug.Replace(" ", "-");

            // Limit length to 200 characters to leave room for GUID suffix
            if (slug.Length > 200)
            {
                slug = slug.Substring(0, 200);
            }

            // Append last 8 characters of GUID for uniqueness
            var guidSuffix = jobId.ToString("N").Substring(24, 8);
            slug = $"{slug}-{guidSuffix}";

            return slug;
        }

        /// <summary>
        /// Validates if a slug is in correct format
        /// </summary>
        /// <param name="slug">The slug to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            if (slug.Length > 250)
                return false;

            // Only lowercase letters, numbers, and hyphens allowed
            return Regex.IsMatch(slug, @"^[a-z0-9-]+$");
        }

        /// <summary>
        /// Sanitizes a custom slug provided by user
        /// </summary>
        /// <param name="customSlug">User-provided slug</param>
        /// <returns>Sanitized slug</returns>
        public static string SanitizeCustomSlug(string customSlug)
        {
            if (string.IsNullOrWhiteSpace(customSlug))
                throw new ArgumentException("Custom slug cannot be empty", nameof(customSlug));

            // Convert to lowercase
            var slug = customSlug.ToLowerInvariant();

            // Remove special characters
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Replace multiple spaces or hyphens with single hyphen
            slug = Regex.Replace(slug, @"[\s-]+", "-");

            // Trim hyphens from start and end
            slug = slug.Trim('-');

            // Limit length
            if (slug.Length > 250)
            {
                slug = slug.Substring(0, 250).TrimEnd('-');
            }

            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Custom slug contains no valid characters", nameof(customSlug));

            return slug;
        }
    }
}

