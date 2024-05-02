import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';
import { CurrencyService } from './services/currency.service';
import { CurrencyDto } from './models/dataTransferObjects/currency.dto';
import { Observable, Subscriber, Subscription, map, of, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Currency.UI';
  public addCurrencyFormGroup: FormGroup;
  public validCurrencies: CurrencyDto[] = [];
  public filteredCurrencies: CurrencyDto[] = [];
  private _subscription = new Subscription();
  public currencyControlName = 'currency';
  public savedCurrencyList: CurrencyDto[] = [];
  private _currencyService = inject(CurrencyService);
  public editIndex = -1;
  public editRecord = new CurrencyDto();
  constructor(){
    this.addCurrencyFormGroup = new FormGroup({
      [this.currencyControlName]: new FormControl<string>("")
    });
  }

  ngOnInit(){
    this.subscribe();
    this.getSavedCurrencies();
  }

  subscribe(){
    this._subscription.add(this._currencyService
    .getCurrencies()
    .subscribe(response => {
       this.validCurrencies = response;
       this.addCurrencyFormGroup.get(this.currencyControlName)?.addValidators(this.currencyFieldValidator);
    }));

    this._subscription.add(this.addCurrencyFormGroup.get(this.currencyControlName)?.valueChanges
    .pipe(tap((val: string | null) => {
       if(val === null || val.trim() === ''){
         this.filteredCurrencies = [...this.validCurrencies];
       }
       else{
         var matchPattern = new RegExp(`(${val})`, 'i');
         this.filteredCurrencies = [...this.validCurrencies.filter(c => matchPattern.test(c.name))];
       }
    })).subscribe());
  }

  ngOnDestroy(){
    this._subscription.unsubscribe();
  }

  onAddClick(currencyNameToAdd: string){
    const addedCurrency = this.validCurrencies.find(x => x.name === currencyNameToAdd);
    if(addedCurrency){
      this._currencyService.addCurrency(addedCurrency)
      .subscribe(response => {
        if(response){
          alert(`${response.name} added`);
          this.getSavedCurrencies();
        }
      });
    }
  }

  onDeleteClick(currencyId: number){
    this._currencyService.deleteCurrency(currencyId)
    .subscribe(response => {
      if(response){
        alert(`${response.code} deleted`);
        this.getSavedCurrencies();
      }
    })
  }

  onEditClick(edited: CurrencyDto){
    this.editRecord = {...edited};
  }

  getFormControl(fG: FormGroup, controlName: string): FormControl{
    return fG.get(controlName) as FormControl;
  }

  currencyFieldValidator: ValidatorFn  =  (control: AbstractControl) =>{
    let currencyFieldControl = control as FormControl;
    const fieldIsValid = this.validCurrencies?.some(c => c.name.toLowerCase() === (currencyFieldControl.value as string).toLowerCase()) ?? false;
    return fieldIsValid ? null : {invalidInput: "Invalid Input"};
  };

  getSavedCurrencies(){
    this._currencyService.getSavedCurrencies()
    .subscribe(response => {
      this.savedCurrencyList = [...response];
    })
  }

  onSaveClick(currency: CurrencyDto){
    this.editRecord.id = currency.id;
    this._currencyService
    .updateCurrency(this.editRecord)
    .subscribe(response => {
      if(response){
        alert(`${response.code} currency updated`);
        this.getSavedCurrencies();
        this.editIndex = -1;
      }
      else{
        const invalidCodeProvided = this.validCurrencies.every(x => x.code !== this.editRecord.code);
        if(invalidCodeProvided){
          alert(`Save Failed: ${this.editRecord.code} is not a valid currency code.`);
        }
       
      }
    });
  }

}
