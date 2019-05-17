import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ServicesComponent } from './services/services.component';
import { ContactComponent } from './contact/contact.component';
import { CrearResidenciaComponent } from './crear-residencia/crear-residencia.component';
import { CrearSubastaComponent } from './crear-subasta/crear-subasta.component';
import { ListarResidenciasComponent } from './listar-residencias/listar-residencias.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'services', component: ServicesComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'crearResidencia', component: CrearResidenciaComponent },
  { path: 'crearSubasta', component: CrearSubastaComponent },
  { path: 'listarResidencias', component: ListarResidenciasComponent }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
