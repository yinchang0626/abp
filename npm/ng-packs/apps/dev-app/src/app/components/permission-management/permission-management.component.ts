import { Component, OnInit, Renderer2, Input, EventEmitter, Output } from '@angular/core';
import { PermissionManagementComponent as AbpPermissionManagementComponent, PermissionManagement } from '@abp/ng.permission-management'
import { Store } from '@ngxs/store';

@Component({
  selector: 'my-permission-management',
  templateUrl: './permission-management.component.html',
  styleUrls: ['./permission-management.component.scss']
})
export class MyPermissionManagementComponent extends AbpPermissionManagementComponent
  implements
  PermissionManagement.PermissionManagementComponentInputs,
  PermissionManagement.PermissionManagementComponentOutputs,
  OnInit {
  @Input()
  readonly providerName: string;

  @Input()
  readonly providerKey: string;

  @Input()
  readonly hideBadges = false;

  protected _visible = false;

  @Input()
  get visible(): boolean {
    return this._visible;
  }

  set visible(value: boolean) {
    if (value === this._visible) return;

    if (value) {
      this.openModal().subscribe(() => {
        this._visible = true;
        this.visibleChange.emit(true);
      });
    } else {
      this.selectedGroup = null;
      this._visible = false;
      this.visibleChange.emit(false);
    }
  }

  @Output() readonly visibleChange = new EventEmitter<boolean>();
  constructor(private _store: Store, private _renderer: Renderer2) { super(_store, _renderer) }

  ngOnInit() {

  }

}
