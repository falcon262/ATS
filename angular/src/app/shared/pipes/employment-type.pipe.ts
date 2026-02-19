import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'employmentTypeText',
  standalone: true
})
export class EmploymentTypePipe implements PipeTransform {
  transform(value: number): string {
    const typeMap: Record<number, string> = {
      0: 'Full-Time',
      1: 'Part-Time',
      2: 'Contract',
      3: 'Temporary',
      4: 'Internship',
      5: 'Freelance'
    };
    return typeMap[value] || 'Unknown';
  }
}

