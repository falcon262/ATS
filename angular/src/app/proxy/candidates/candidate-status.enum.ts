import { mapEnumToOptions } from '@abp/ng.core';

export enum CandidateStatus {
  Active = 0,
  Inactive = 1,
  Blacklisted = 2,
  Hired = 3,
}

export const candidateStatusOptions = mapEnumToOptions(CandidateStatus);
