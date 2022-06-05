import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClipyComponent } from './clipy.component';

describe('ClipyComponent', () => {
  let component: ClipyComponent;
  let fixture: ComponentFixture<ClipyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClipyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClipyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
