import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'squre',
})
export class SqurePipe implements PipeTransform {
  transform(value: number,pow :number=2, ...args: unknown[]): number {
    return Math.pow( value,pow);
  }
}
