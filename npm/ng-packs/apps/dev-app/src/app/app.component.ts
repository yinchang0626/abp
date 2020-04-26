import { LazyLoadService, LOADING_STRATEGY, AddReplaceableComponent } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { Store } from '@ngxs/store';
import { ePermissionManagementComponents } from '@abp/ng.permission-management';
import { MyPermissionManagementComponent } from './components/permission-management/permission-management.component';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <router-outlet></router-outlet>
  `,
})
export class AppComponent implements OnInit {
  constructor(private lazyLoadService: LazyLoadService,private store: Store) { }

  ngOnInit() {
    forkJoin(
      this.lazyLoadService.load(
        LOADING_STRATEGY.PrependAnonymousStyleToHead('fontawesome-v4-shims.min.css'),
      ),
      this.lazyLoadService.load(
        LOADING_STRATEGY.PrependAnonymousStyleToHead('fontawesome-all.min.css'),
      ),
    ).subscribe();

    this.store.dispatch(
      new AddReplaceableComponent({
        component: MyPermissionManagementComponent,
        key: ePermissionManagementComponents.PermissionManagement,
      }));

  }
}
