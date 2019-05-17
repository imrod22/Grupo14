import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarResidenciasComponent } from './listar-residencias.component';

describe('ListarResidenciasComponent', () => {
  let component: ListarResidenciasComponent;
  let fixture: ComponentFixture<ListarResidenciasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarResidenciasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarResidenciasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
