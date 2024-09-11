import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './components/customer/customer.component';
import { AppComponent } from './app.component';
import { ParentComponentComponent } from './components/parent-component/parent-component.component';
import { ChildComponentComponent } from './components/child-component/child-component.component';
import { NgOnInitTestComponent } from './components/ng-on-init-test/ng-on-init-test.component';

const routes: Routes = [
  {path:'home', component:AppComponent},
  {path:'customer', component:CustomerComponent},
  {path:'parent', component:ParentComponentComponent},
  {path:'child', component:ChildComponentComponent},
  {path:'testOnInit', component:NgOnInitTestComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
