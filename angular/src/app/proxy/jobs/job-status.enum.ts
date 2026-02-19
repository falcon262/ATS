import { mapEnumToOptions } from '@abp/ng.core';

export enum JobStatus {
  Draft = 0,
  Active = 1,
  Paused = 2,
  Closed = 3,
  Archived = 4,
}

export const jobStatusOptions = mapEnumToOptions(JobStatus);
