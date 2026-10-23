import { ChangeDetectorRef, Component, EventEmitter, OnChanges, OnInit, Output } from '@angular/core';
import { Iproduct } from '../../../models/iproduct';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ICategory } from '../../../models/i-category';
import { Router, RouterLink } from '@angular/router';
import { ProductsApi } from '../../../services/products-api';
import { AlertCenter } from '../../../services/alert-center';

@Component({
  selector: 'app-shop',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './shop.html',
  styleUrl: './shop.css',
})
export class Shop implements OnChanges, OnInit {



  categories!: ICategory[];
  selectedCatId: string="" ;




  goTo(id: string) {
    this.router.navigateByUrl(`/ProductsDetails/${id}`);
  }


  filterdProducts: Iproduct[] = [];
  total: number = 0;
  myDate = new Date();
  @Output() onTotalChanged: EventEmitter<number>;




  constructor(private alertCenter:AlertCenter,private productsApi: ProductsApi, private router: Router, private cdr: ChangeDetectorRef) {


    this.onTotalChanged = new EventEmitter<number>();


  }
  ngOnInit(): void {

    this.productsApi.getAllCategories().subscribe({
      next: (response) => {
        
        
        this.categories = response.data;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.log(error);
      }
    });
    this.filter();
     

  }
  ngOnChanges() {
    // console.log("ngOnChanges");
  }

  buy(qunt: string, prd: Iproduct) {
    this.total += +qunt * prd.price;
    prd.quantity -= +qunt;

    this.onTotalChanged.emit(this.total);


  }




  trackByFn(index: number, item: Iproduct) {
    return item.id;
  }

  filter() {
    if (this.selectedCatId == "") {
      this.productsApi.getAll().subscribe({
        next: (response) => {


          this.filterdProducts = response.data;
          this.cdr.detectChanges();
        },
        error: (error) => {
                    this.alertCenter.error('Error' ," wrong State");

        }
      })

    }

    else {

      this.productsApi.getByCatId(this.selectedCatId).subscribe({
        next: (response) => {
          this.filterdProducts = response.data;
          this.cdr.detectChanges();
        },
        error: (error) => {
          this.alertCenter.error('Error' ," wrong State");
        }
      })

    }
    
  }
  test() {

  }

}
