import { noop } from '@abp/ng.core';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { MyProjectNameConfigService } from './services/my-project-name-config.service';

@NgModule({
  providers: [{ provide: APP_INITIALIZER, deps: [MyProjectNameConfigService], useFactory: noop, multi: true }],
})
export class MyProjectNameConfigModule {}
