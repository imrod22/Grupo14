import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearResidenciaComponent } from './crear-residencia.component';

describe('CrearResidenciaComponent', () => {
  let component: CrearResidenciaComponent;
  let fixture: ComponentFixture<CrearResidenciaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrearResidenciaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrearResidenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
