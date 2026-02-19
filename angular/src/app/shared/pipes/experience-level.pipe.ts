import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'experienceLevelText',
  standalone: true
})
export class ExperienceLevelPipe implements PipeTransform {
  transform(value: number): string {
    const levelMap: Record<number, string> = {
      0: 'Entry Level',
      1: 'Junior',
      2: 'Mid-Level',
      3: 'Senior',
      4: 'Lead',
      5: 'Executive'
    };
    return levelMap[value] || 'Unknown';
  }
}

