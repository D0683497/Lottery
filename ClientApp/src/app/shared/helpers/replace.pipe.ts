import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'replace'
})
export class ReplacePipe implements PipeTransform {

  transform(value: string, regexValue: string, replaceValue: string, replaceLength: number): any {
    return value.replace(new RegExp(regexValue, 'gm'), `$1${replaceValue.repeat(replaceLength)}$3`);
  }

}
