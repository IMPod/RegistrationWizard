// src/app/registration-wizard/registration-wizard.component.ts

import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidatorFn
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { CountriesService } from '../services/countries.service';

@Component({
  selector: 'app-registration-wizard',
  templateUrl: './registration-wizard.component.html',
  styleUrls: ['./registration-wizard.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class RegistrationWizardComponent implements OnInit {
  step1Form!: FormGroup;
  step2Form!: FormGroup;
  step = 1;
  countries: any[] = [];
  provinces: any[] = [];
  registrationSuccess = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private registrationService: RegistrationService,
    private countriesService: CountriesService
  ) { }

  ngOnInit(): void {
    this.step1Form = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, this.strongPasswordValidator]],
        confirmPassword: ['', Validators.required],
        agree: [false, Validators.requiredTrue]
      },
      { validators: this.passwordMatchValidator }
    );

    this.step2Form = this.fb.group({
      countryId: [null, Validators.required],
      provinceId: [null, Validators.required]
    });

    this.loadCountries();
  }

  private loadCountries(): void {
    let retries = 0;
    const interval = setInterval(() => {
      this.countriesService.getCountries().subscribe(
        (data) => {
          this.countries = data.data;
          clearInterval(interval);
        },
        (err) => {
          retries++;
          if (retries > 5) {
            console.error('Error loading countries:', err);
            clearInterval(interval);
          }
        }
      );
    }, 2000);
  }

  strongPasswordValidator(control: AbstractControl) {
    const value = control.value || '';
    if (!value) {
      return null;
    }

    const errors: string[] = [];
    if (value.length < 6) {
      errors.push('Passwords must be at least 6 characters.');
    }
    if (!/[A-Z]/.test(value)) {
      errors.push('Passwords must have at least one uppercase letter.');
    }
    if (!/[a-z]/.test(value)) {
      errors.push('Passwords must have at least one lowercase letter.');
    }
    if (!/[0-9]/.test(value)) {
      errors.push('Passwords must have at least one digit.');
    }
    if (!/[^A-Za-z0-9]/.test(value)) {
      errors.push('Passwords must have at least one non-alphanumeric character.');
    }

    return errors.length > 0 ? { strength: errors } : null;
  }

  passwordMatchValidator: ValidatorFn = (group: AbstractControl) => {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    if (password && confirmPassword && password !== confirmPassword) {
      return { passwordMismatch: true };
    }
    return null;
  };

  nextStep(): void {
    if (this.step1Form.valid) {
      this.step = 2;
    } else {
      this.step1Form.markAllAsTouched();
    }
  }

  onCountryChange(): void {
    const countryId = this.step2Form.get('countryId')?.value;
    if (countryId) {
      this.countriesService.getProvincesByCountry(countryId).subscribe(
        (data) => {
          this.provinces = data.data;
          this.step2Form.get('provinceId')?.setValue(null);
        },
        (err) => console.error('Error loading provinces', err)
      );
    }
  }

  submit(): void {
    if (this.step2Form.valid) {
      const registrationData = {
        email: this.step1Form.get('email')?.value,
        password: this.step1Form.get('password')?.value,
        countryId: this.step2Form.get('countryId')?.value,
        provinceId: this.step2Form.get('provinceId')?.value
      };

      this.registrationService.register(registrationData).subscribe(
        (res) => {
          if (res.success) {
            this.registrationSuccess = true;
          }
        },
        (err) => {
          // Например, если сервер вернёт ошибку "User creation failed:
          // Passwords must have..." - можно показать её тут
          this.errorMessage = err.error?.error || 'Registration failed.';
        }
      );
    } else {
      this.step2Form.markAllAsTouched();
    }
  }
}
