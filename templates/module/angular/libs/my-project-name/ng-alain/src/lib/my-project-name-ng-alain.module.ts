import { NgModule } from '@angular/core';
import { NgAlainBasicModule } from '@fs/ng-alain/basic';
import { SharedModule } from './shared/shared.module';
import { SampleModule } from './sample/sample.module';
import { Store } from '@ngxs/store';
import { AddRoute,ABP } from '@abp/ng.core';
import { MyProjectNameModule } from '@fs/my-project-name';
import { MyProjectNameNgAlainRoutingModule } from './my-project-name-ng-alain-routing.module';

@NgModule({
  imports: [
    SharedModule,
    MyProjectNameModule,
    MyProjectNameNgAlainRoutingModule,
    SampleModule

  ]
})
export class MyProjectNameNgAlainModule {
}
