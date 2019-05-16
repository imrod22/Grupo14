import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FinalizarResidenciaComponent } from './finalizar-residencia.component';

describe('FinalizarResidenciaComponent', () => {
  let component: FinalizarResidenciaComponent;
  let fixture: ComponentFixture<FinalizarResidenciaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FinalizarResidenciaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinalizarResidenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
