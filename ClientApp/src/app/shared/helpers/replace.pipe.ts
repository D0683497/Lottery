import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'replace'
})
export class ReplacePipe implements PipeTransform {

  transform(value: string, regexValue: string, replaceValue: string, replaceLength: number): string {
    const newValue = value.trim(); // 去掉空白
    return newValue.replace(new RegExp(regexValue, 'gm'), `$1${replaceValue.repeat(replaceLength)}$3`);
  }

}
