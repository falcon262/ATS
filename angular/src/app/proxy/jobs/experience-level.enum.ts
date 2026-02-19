import { mapEnumToOptions } from '@abp/ng.core';

export enum ExperienceLevel {
  EntryLevel = 0,
  Junior = 1,
  MidLevel = 2,
  Senior = 3,
  Lead = 4,
  Executive = 5,
}

export const experienceLevelOptions = mapEnumToOptions(ExperienceLevel);
