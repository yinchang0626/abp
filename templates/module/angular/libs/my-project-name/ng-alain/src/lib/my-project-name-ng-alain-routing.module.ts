import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DynamicLayoutComponent, AuthGuard, PermissionGuard } from '@abp/ng.core';
import { LayoutPassportComponent, LayoutDefaultComponent } from '@fs/ng-alain/basic';
import { SampleModule } from './sample/sample.module';

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'sample' },
    {
        path: '',
        component: LayoutDefaultComponent,
        //canActivate: [AuthGuard, PermissionGuard],
        children: [
            {
                path: 'sample',
                loadChildren: ()=>SampleModule
            }
        ],
    }
];
// @dynamic
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MyProjectNameNgAlainRoutingModule { }
