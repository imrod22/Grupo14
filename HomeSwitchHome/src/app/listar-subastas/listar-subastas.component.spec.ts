import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarSubastasComponent } from './listar-subastas.component';

describe('ListarSubastasComponent', () => {
  let component: ListarSubastasComponent;
  let fixture: ComponentFixture<ListarSubastasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarSubastasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarSubastasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
