// src/app/registration-wizard/registration-wizard.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidatorFn, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
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
  step: number = 1;
  countries: any[] = [];
  provinces: any[] = [];
  registrationSuccess: boolean = false;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private registrationService: RegistrationService,
    private countriesService: CountriesService
  ) { }

  ngOnInit(): void {
    this.step1Form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).+$/)]],
      confirmPassword: ['', Validators.required],
      agree: [false, Validators.requiredTrue]
    }, { validators: this.passwordMatchValidator });

    this.step2Form = this.fb.group({
      countryId: [null, Validators.required],
      provinceId: [null, Validators.required]
    });

    let retries = 0;
    const interval = setInterval(() => {
      this.countriesService.getCountries().subscribe(
        data => {
          this.countries = data.data;
          clearInterval(interval); 
        },
        err => {
          retries++;
          if (retries > 5) { 
            console.error('Error loading countries:', err);
            clearInterval(interval);
          }
        }
      );
    }, 2000);
  }

  passwordMatchValidator: ValidatorFn = (group: AbstractControl): { [key: string]: any } | null => {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { 'passwordMismatch': true };
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
        data => {
          this.provinces = data.data;
          this.step2Form.get('provinceId')?.setValue(null);
        },
        err => console.error('Error loading provinces', err)
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
        res => {
          if (res.success) {
            this.registrationSuccess = true;
          }
        },
        err => {
          this.errorMessage = err.error?.error || 'Registration failed.';
        }
      );
    } else {
      this.step2Form.markAllAsTouched();
    }
  }
}
