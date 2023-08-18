import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
    name: 'dateAgo',
    pure: true
})
export class DateAgoPipe implements PipeTransform {
    constructor(private datePipe: DatePipe) {
        //2019-07-22
    }
    transform(value: any, args?: any): any {
        if (value) {
            const seconds = Math.floor((+new Date() - +new Date(value)) / 1000);
            if (seconds < 29) // less than 30 seconds ago will show as 'Just now'
                return 'Just now';
            const intervals: any = {
                'year': 31536000,
                'month': 2592000,
                'week': 604800,
                'day': 86400,
                'hour': 3600,
                'minute': 60,
                'second': 1
            };
            let counter;
            let result;
            for (const i in intervals) {
                counter = Math.floor(seconds / intervals[i]);
                if (counter > 0)
                    if (counter === 1) {
                        result = counter + ' ' + i + ' ago'; // singular (1 day ago)
                        break;
                    } else {
                        result = counter + ' ' + i + 's ago'; // plural (2 days ago)
                        break;
                    }
            }

            if (seconds > 604800)
                return `${result} | ${this.datePipe.transform(new Date(value), 'dd-MM-yyyy')}`;
            return result;
        }
        return value;
    }

}