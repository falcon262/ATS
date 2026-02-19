import { mapEnumToOptions } from '@abp/ng.core';

export enum ApplicationStatus {
  New = 0,
  InReview = 1,
  Shortlisted = 2,
  Interview = 3,
  Offered = 4,
  Hired = 5,
  Rejected = 6,
  Withdrawn = 7,
  OnHold = 8,
}

export const applicationStatusOptions = mapEnumToOptions(ApplicationStatus);
