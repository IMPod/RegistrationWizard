// src/app/registration-wizard/registration-wizard.component.ts
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
  ValidatorFn
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { CountriesService, Country, Province } from '../services/countries.service';

interface Step1FormModel {
  email: FormControl<string | null>;
  password: FormControl<string | null>;
  confirmPassword: FormControl<string | null>;
  agree: FormControl<boolean | null>;
}

interface Step2FormModel {
  countryId: FormControl<number | null>;
  provinceId: FormControl<number | null>;
}

@Component({
  selector: 'app-registration-wizard',
  templateUrl: './registration-wizard.component.html',
  styleUrls: ['./registration-wizard.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class RegistrationWizardComponent implements OnInit {
  step1Form!: FormGroup<Step1FormModel>;
  step2Form!: FormGroup<Step2FormModel>;
  step = 1;
  countries: Country[] = [];
  provinces: Province[] = [];
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
        confirmPassword: ['', [Validators.required]],
        agree: [false, [Validators.requiredTrue]]
      },
      { validators: this.passwordMatchValidator }
    ) as FormGroup<Step1FormModel>;

    this.step2Form = this.fb.group({
      countryId: [null, Validators.required],
      provinceId: [null, Validators.required]
    }) as FormGroup<Step2FormModel>;

    this.loadCountries();
  }

  private loadCountries(): void {
    this.countriesService.getCountries().subscribe({
      next: (response) => {
        this.countries = response.data;
      },
      error: (err) => {
        console.error('Error loading countries:', err);
      }
    });
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
      this.countriesService.getProvincesByCountry(countryId).subscribe({
        next: (response) => {
          this.provinces = response.data;
          this.step2Form.get('provinceId')?.setValue(null);
        },
        error: (err) => console.error('Error loading provinces', err)
      });
    }
  }

  submit(): void {
    if (this.step2Form.valid && this.step1Form.valid) {
      const registrationData = {
        email: this.step1Form.get('email')?.value,
        password: this.step1Form.get('password')?.value,
        countryId: this.step2Form.get('countryId')?.value,
        provinceId: this.step2Form.get('provinceId')?.value
      };

      this.registrationService.register(registrationData).subscribe({
        next: (res) => {
          if (res.success) {
            this.registrationSuccess = true;
          }
        },
        error: (err) => {
          this.errorMessage = err.error?.error || 'Registration failed.';
        }
      });
    } else {
      this.step2Form.markAllAsTouched();
      this.step1Form.markAllAsTouched();
    }
  }
}
