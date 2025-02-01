// src/app/app.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationWizardComponent } from './registration-wizard/registration-wizard.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [CommonModule, RegistrationWizardComponent]
})
export class AppComponent { }
