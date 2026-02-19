using ATS.AI;
using ATS.AI.Dtos;
using ATS.Applications;
using ATS.Applications.Dtos;
using ATS.Candidates;
using ATS.Candidates.Dtos;
using ATS.Departments;
using ATS.Departments.Dtos;
using ATS.Jobs;
using ATS.Jobs.Dtos;
using ATS.Jobs.Public;
using AutoMapper;

namespace ATS;

public class ATSApplicationAutoMapperProfile : Profile
{
    public ATSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        // ========== JOB MAPPINGS ==========
        CreateMap<Job, JobDto>()
            .ForMember(dest => dest.RequiredSkills, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredSkills, opt => opt.Ignore())
            .ForMember(dest => dest.DepartmentName, opt => opt.Ignore());

        CreateMap<Job, JobListDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.Ignore());

        CreateMap<CreateUpdateJobDto, Job>()
            .ForMember(dest => dest.RequiredSkillsJson, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredSkillsJson, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Applications, opt => opt.Ignore())
            .ForMember(dest => dest.Department, opt => opt.Ignore());

        // ========== CANDIDATE MAPPINGS ==========
        CreateMap<Candidate, CandidateDto>()
            .ForMember(dest => dest.Skills, opt => opt.Ignore())
            .ForMember(dest => dest.Education, opt => opt.Ignore())
            .ForMember(dest => dest.Experience, opt => opt.Ignore())
            .ForMember(dest => dest.Certifications, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore());

        CreateMap<Candidate, CandidateListDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => $"{src.City}, {src.State}"))
            .ForMember(dest => dest.TopSkills, opt => opt.Ignore());

        CreateMap<CreateUpdateCandidateDto, Candidate>()
            .ForMember(dest => dest.SkillsJson, opt => opt.Ignore())
            .ForMember(dest => dest.EducationJson, opt => opt.Ignore())
            .ForMember(dest => dest.ExperienceJson, opt => opt.Ignore())
            .ForMember(dest => dest.CertificationsJson, opt => opt.Ignore())
            .ForMember(dest => dest.TagsJson, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Applications, opt => opt.Ignore());

        // ========== APPLICATION MAPPINGS ==========
        CreateMap<Application, ApplicationDto>()
            .ForMember(dest => dest.JobTitle, opt => opt.Ignore())
            .ForMember(dest => dest.DepartmentName, opt => opt.Ignore())
            .ForMember(dest => dest.CandidateName, opt => opt.Ignore())
            .ForMember(dest => dest.CandidateEmail, opt => opt.Ignore())
            .ForMember(dest => dest.AIAnalysisDetails, opt => opt.Ignore())
            .ForMember(dest => dest.SkillMatchScores, opt => opt.Ignore())
            .ForMember(dest => dest.ScreeningAnswers, opt => opt.Ignore())
            .ForMember(dest => dest.ActivityLog, opt => opt.Ignore());

        CreateMap<Application, ApplicationListDto>()
            .ForMember(dest => dest.JobTitle, opt => opt.Ignore())
            .ForMember(dest => dest.CandidateName, opt => opt.Ignore())
            .ForMember(dest => dest.CandidateEmail, opt => opt.Ignore());

        CreateMap<CreateApplicationDto, Application>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Job, opt => opt.Ignore())
            .ForMember(dest => dest.Candidate, opt => opt.Ignore());

        // ========== DEPARTMENT MAPPINGS ==========
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.JobCount, opt => opt.MapFrom(src => src.Jobs.Count))
            .ForMember(dest => dest.ParentDepartmentName, opt => opt.Ignore());

        CreateMap<Department, DepartmentListDto>()
            .ForMember(dest => dest.JobCount, opt => opt.MapFrom(src => src.Jobs.Count))
            .ForMember(dest => dest.SubDepartmentCount, opt => opt.MapFrom(src => src.SubDepartments.Count));

        CreateMap<CreateUpdateDepartmentDto, Department>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Jobs, opt => opt.Ignore())
            .ForMember(dest => dest.SubDepartments, opt => opt.Ignore())
            .ForMember(dest => dest.ParentDepartment, opt => opt.Ignore());

        // ========== AI MAPPINGS ==========
        CreateMap<AIAnalysisResult, AIAnalysisResultDto>()
            .ForMember(dest => dest.Strengths, opt => opt.Ignore())
            .ForMember(dest => dest.Weaknesses, opt => opt.Ignore())
            .ForMember(dest => dest.ExtractedKeywords, opt => opt.Ignore())
            .ForMember(dest => dest.RedFlags, opt => opt.Ignore())
            .ForMember(dest => dest.RecommendationType, opt => opt.MapFrom(src => src.RecommendationType.ToString()));

        // ========== PUBLIC JOB MAPPINGS ==========
        CreateMap<Job, PublicJobDto>()
            .ForMember(dest => dest.RequiredSkills, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredSkills, opt => opt.Ignore())
            .ForMember(dest => dest.DepartmentName, opt => opt.Ignore());

        // ========== CANDIDATE PORTAL MAPPINGS ==========
        CreateMap<Application, CandidateApplicationListDto>()
            .ForMember(dest => dest.JobTitle, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore());
    }
}
