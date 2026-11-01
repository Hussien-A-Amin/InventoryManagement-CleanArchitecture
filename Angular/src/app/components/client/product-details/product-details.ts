import { ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertCenter } from '../../../services/alert-center';
import { ProductsApi } from '../../../services/products-api';
import { Iproduct } from '../../../models/iproduct';

@Component({
  selector: 'app-product-details',
  imports: [],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails {
  id?: string | null;
  prd?:Iproduct;
  constructor(private route: ActivatedRoute, private alertCenter: AlertCenter, private cdr: ChangeDetectorRef, private api: ProductsApi) {

  }
  ngOnInit(): void {

    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
    })
    if (this.id === null) {

      this.alertCenter.conflict();
      console.log(`this.id is null`)
      return;
    }

    this.api.getById(this.id!).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.prd = response.data;
          console.log(this.prd);
        }
        this.cdr.detectChanges();
      },
      error: (error) => {

        this.alertCenter.error("", "");

      },
    })
  }
}
