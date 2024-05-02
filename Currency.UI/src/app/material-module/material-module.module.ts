import { NgModule } from '@angular/core';
import { MatButtonModule} from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule} from '@angular/material/input';
import { MatAutocompleteModule} from '@angular/material/autocomplete';
import { MatIconModule} from '@angular/material/icon';


var toBeExported = [
    MatAutocompleteModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule
  ];
@NgModule({
  declarations: [],
  imports: [
    ...toBeExported
  ],
  exports: [...toBeExported]
})
export class MaterialModule { }
