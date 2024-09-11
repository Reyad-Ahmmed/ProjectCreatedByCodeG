import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';

@Component({
  selector: 'app-child-component',
  templateUrl: './child-component.component.html',
  styleUrls: ['./child-component.component.css']
})
export class ChildComponentComponent implements OnChanges {
  @Input() firstname: string = '';
  @Input() lastName: string = '';
  @Input() age: number = 0;

  previousValue: string | undefined;
  currentValue: string | undefined;

  ngOnChanges(changes: SimpleChanges) {
    
    // if (changes['myInput']) {
    //   const change = changes['myInput'];
    //   this.previousValue = change.previousValue;
    //   this.currentValue = change.currentValue;
    //   console.log('myInput changed from', this.previousValue, 'to', this.currentValue);
    //   alert("Previous value: " + this.previousValue);
    //   alert("Current value: " + this.currentValue);
    // }

    console.log(this.firstname + " " + this.lastName + " " + this.age);
  }
}
