
export interface EducationInfo {
  degree?: string;
  fieldOfStudy?: string;
  institution?: string;
  graduationYear?: number;
  grade?: string;
}

export interface ExperienceInfo {
  jobTitle?: string;
  company?: string;
  location?: string;
  startDate?: string;
  endDate?: string;
  isCurrent: boolean;
  description?: string;
  keyAchievements: string[];
}
