using ATS.Jobs;
using Shouldly;
using System;
using Xunit;

namespace ATS.Domain.Tests.Jobs
{
    public class JobSlugGeneratorTests
    {
        [Fact]
        public void GenerateFromTitle_Should_Create_Valid_Slug()
        {
            // Arrange
            var title = "Senior Software Engineer";
            var jobId = Guid.Parse("12345678-1234-1234-1234-123456789abc");

            // Act
            var slug = JobSlugGenerator.GenerateFromTitle(title, jobId);

            // Assert
            slug.ShouldNotBeNullOrWhiteSpace();
            slug.ShouldStartWith("senior-software-engineer-");
            slug.ShouldEndWith("9abc"); // Last 8 chars of GUID
            JobSlugGenerator.IsValidSlug(slug).ShouldBeTrue();
        }

        [Fact]
        public void GenerateFromTitle_Should_Remove_Special_Characters()
        {
            // Arrange
            var title = "C# / .NET Developer (Senior!)";
            var jobId = Guid.NewGuid();

            // Act
            var slug = JobSlugGenerator.GenerateFromTitle(title, jobId);

            // Assert
            slug.ShouldContain("c-net-developer-senior");
            slug.ShouldNotContain("#");
            slug.ShouldNotContain("/");
            slug.ShouldNotContain("(");
            slug.ShouldNotContain(")");
            slug.ShouldNotContain("!");
            slug.ShouldNotContain(".");
        }

        [Fact]
        public void GenerateFromTitle_Should_Replace_Multiple_Spaces_With_Single_Hyphen()
        {
            // Arrange
            var title = "Senior    Software     Engineer";
            var jobId = Guid.NewGuid();

            // Act
            var slug = JobSlugGenerator.GenerateFromTitle(title, jobId);

            // Assert
            slug.ShouldNotContain("--");
            slug.ShouldContain("senior-software-engineer");
        }

        [Fact]
        public void GenerateFromTitle_Should_Convert_To_Lowercase()
        {
            // Arrange
            var title = "SENIOR SOFTWARE ENGINEER";
            var jobId = Guid.NewGuid();

            // Act
            var slug = JobSlugGenerator.GenerateFromTitle(title, jobId);

            // Assert
            slug.ShouldBe(slug.ToLowerInvariant());
            slug.ShouldContain("senior-software-engineer");
        }

        [Fact]
        public void GenerateFromTitle_Should_Trim_Whitespace()
        {
            // Arrange
            var title = "  Senior Software Engineer  ";
            var jobId = Guid.NewGuid();

            // Act
            var slug = JobSlugGenerator.GenerateFromTitle(title, jobId);

            // Assert
            slug.ShouldNotStartWith("-");
            slug.ShouldNotEndWith("-");
            slug.ShouldContain("senior-software-engineer");
        }

        [Fact]
        public void GenerateFromTitle_Should_Limit_Length()
        {
            // Arrange
            var longTitle = new string('a', 250);
            var jobId = Guid.NewGuid();

            // Act
            var slug = JobSlugGenerator.GenerateFromTitle(longTitle, jobId);

            // Assert
            slug.Length.ShouldBeLessThanOrEqualTo(250);
        }

        [Fact]
        public void GenerateFromTitle_Should_Throw_When_Title_Is_Empty()
        {
            // Arrange
            var jobId = Guid.NewGuid();

            // Act & Assert
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.GenerateFromTitle("", jobId));
            
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.GenerateFromTitle(null!, jobId));
            
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.GenerateFromTitle("   ", jobId));
        }

        [Fact]
        public void GenerateFromTitle_Should_Append_GUID_Suffix_For_Uniqueness()
        {
            // Arrange
            var title = "Software Engineer";
            var jobId1 = Guid.NewGuid();
            var jobId2 = Guid.NewGuid();

            // Act
            var slug1 = JobSlugGenerator.GenerateFromTitle(title, jobId1);
            var slug2 = JobSlugGenerator.GenerateFromTitle(title, jobId2);

            // Assert
            slug1.ShouldNotBe(slug2); // Different GUIDs should produce different slugs
        }

        [Fact]
        public void IsValidSlug_Should_Return_True_For_Valid_Slugs()
        {
            // Assert
            JobSlugGenerator.IsValidSlug("software-engineer-abc123de").ShouldBeTrue();
            JobSlugGenerator.IsValidSlug("senior-developer-12345678").ShouldBeTrue();
            JobSlugGenerator.IsValidSlug("product-manager").ShouldBeTrue();
            JobSlugGenerator.IsValidSlug("123-test").ShouldBeTrue();
        }

        [Fact]
        public void IsValidSlug_Should_Return_False_For_Invalid_Slugs()
        {
            // Assert
            JobSlugGenerator.IsValidSlug("").ShouldBeFalse();
            JobSlugGenerator.IsValidSlug(null!).ShouldBeFalse();
            JobSlugGenerator.IsValidSlug("   ").ShouldBeFalse();
            JobSlugGenerator.IsValidSlug("Software Engineer").ShouldBeFalse(); // Spaces
            JobSlugGenerator.IsValidSlug("software_engineer").ShouldBeFalse(); // Underscores
            JobSlugGenerator.IsValidSlug("Software-Engineer").ShouldBeFalse(); // Uppercase
            JobSlugGenerator.IsValidSlug("software.engineer").ShouldBeFalse(); // Dots
            JobSlugGenerator.IsValidSlug(new string('a', 251)).ShouldBeFalse(); // Too long
        }

        [Fact]
        public void SanitizeCustomSlug_Should_Create_Valid_Slug()
        {
            // Arrange
            var customSlug = "My Custom Job-Slug!";

            // Act
            var sanitized = JobSlugGenerator.SanitizeCustomSlug(customSlug);

            // Assert
            sanitized.ShouldBe("my-custom-job-slug");
            JobSlugGenerator.IsValidSlug(sanitized).ShouldBeTrue();
        }

        [Fact]
        public void SanitizeCustomSlug_Should_Remove_Leading_And_Trailing_Hyphens()
        {
            // Arrange
            var customSlug = "-my-slug-";

            // Act
            var sanitized = JobSlugGenerator.SanitizeCustomSlug(customSlug);

            // Assert
            sanitized.ShouldBe("my-slug");
            sanitized.ShouldNotStartWith("-");
            sanitized.ShouldNotEndWith("-");
        }

        [Fact]
        public void SanitizeCustomSlug_Should_Replace_Multiple_Hyphens()
        {
            // Arrange
            var customSlug = "my---custom----slug";

            // Act
            var sanitized = JobSlugGenerator.SanitizeCustomSlug(customSlug);

            // Assert
            sanitized.ShouldBe("my-custom-slug");
            sanitized.ShouldNotContain("--");
        }

        [Fact]
        public void SanitizeCustomSlug_Should_Throw_When_Slug_Is_Empty()
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.SanitizeCustomSlug(""));
            
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.SanitizeCustomSlug(null!));
            
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.SanitizeCustomSlug("   "));
        }

        [Fact]
        public void SanitizeCustomSlug_Should_Throw_When_No_Valid_Characters()
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => 
                JobSlugGenerator.SanitizeCustomSlug("!@#$%^&*()"));
        }

        [Fact]
        public void SanitizeCustomSlug_Should_Limit_Length_To_250()
        {
            // Arrange
            var longSlug = new string('a', 300);

            // Act
            var sanitized = JobSlugGenerator.SanitizeCustomSlug(longSlug);

            // Assert
            sanitized.Length.ShouldBeLessThanOrEqualTo(250);
        }
    }
}

