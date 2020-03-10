import { async, TestBed } from '@angular/core/testing';
import { MyProjectNameModule } from './my-project-name.module';

describe('MyProjectNameModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MyProjectNameModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(MyProjectNameModule).toBeDefined();
  });
});
