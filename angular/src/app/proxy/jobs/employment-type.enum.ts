import { mapEnumToOptions } from '@abp/ng.core';

export enum EmploymentType {
  FullTime = 0,
  PartTime = 1,
  Contract = 2,
  Temporary = 3,
  Internship = 4,
  Freelance = 5,
}

export const employmentTypeOptions = mapEnumToOptions(EmploymentType);
