import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { INewproduct, Iproduct } from '../../../models/iproduct';
import { FormsModule } from '@angular/forms';
import { ProductsApi } from '../../../services/products-api';
import { ICategory } from '../../../models/i-category';
import { AlertCenter } from '../../../services/alert-center';

@Component({
  selector: 'app-add-product',
  imports: [FormsModule],
  templateUrl: './add-product.html',
  styleUrl: './add-product.css',
})
export class AddProduct implements OnInit {

  selectedCat: string = "";
  name: string = "";
  price: number = 0;
  quantity: number = 0;
  newPrd?: INewproduct;
  cats: ICategory[] = [];
  constructor(private alertCenter: AlertCenter, private cdr: ChangeDetectorRef, private api: ProductsApi) {

  }
  ngOnInit(): void {
    this.api.getAllCategories().subscribe({
      next: (response) => {
        this.cats = response.data;
        this.selectedCat = this.cats[0]?.id;
        this.cdr.detectChanges();
      },
      error: (error) => {

        this.alertCenter.error("", "");

      },
    })
  }


  submit() {

    let newPrd = {
      name: this.name,
      price: this.price,
      quantity: this.quantity,
      categoryId: this.selectedCat,
    }


    this.api.Create(newPrd).subscribe({
      next: (response) => {
        this.alertCenter.success("Saved", "Done")
        this.name = "";
        this.quantity = 0;
        this.selectedCat = this.cats[0]?.id;

        this.price = 0;
        this.cdr.detectChanges();
      },
      error: (error) => {

        this.alertCenter.error("error", error.msg);
console.log(error);
      }


    });
  }

}
