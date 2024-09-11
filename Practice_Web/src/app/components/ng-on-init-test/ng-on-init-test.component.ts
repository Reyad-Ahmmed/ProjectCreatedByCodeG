import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-ng-on-init-test',
  templateUrl: './ng-on-init-test.component.html',
  styleUrls: ['./ng-on-init-test.component.css']
})
export class NgOnInitTestComponent implements OnInit {
  message:string = "";
  constructor(){
    console.log("From constructor");
  }

  ngOnInit(): void {
    console.log("From ngOnInit");
    this.message = "Hello message from ngOnInit()";
  }
}
