import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { ServicesComponent } from './services/services.component';
import { CrearResidenciaComponent } from './crear-residencia/crear-residencia.component';
import { CrearSubastaComponent } from './crear-subasta/crear-subasta.component';
import { ListarResidenciasComponent } from './listar-residencias/listar-residencias.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactComponent,
    HomeComponent,
    ServicesComponent,
    CrearResidenciaComponent,
    CrearSubastaComponent,
    ListarResidenciasComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
