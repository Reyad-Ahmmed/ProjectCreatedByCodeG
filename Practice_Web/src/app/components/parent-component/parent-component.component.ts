import { Component } from '@angular/core';

@Component({
  selector: 'app-parent-component',
  templateUrl: './parent-component.component.html',
  styleUrls: ['./parent-component.component.css']
})
export class ParentComponentComponent {
  // parentValue: string = 'Initial Value';

  // changeValue(): void {
  //   this.parentValue = 'New Value';
  // }

  firstname = 'Tomasz'
  lastName = 'Kula'
  age = 28;
}
