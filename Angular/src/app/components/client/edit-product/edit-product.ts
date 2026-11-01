import { ChangeDetectorRef, Component } from '@angular/core';
import { IEditproduct, INewproduct } from '../../../models/iproduct';
import { AlertCenter } from '../../../services/alert-center';
import { ProductsApi } from '../../../services/products-api';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-edit-product',
    imports: [FormsModule],
    templateUrl: './edit-product.html',
    styleUrl: './edit-product.css',
})
export class EditProduct {

    title: string = "";
    description: string = "";
    price: number = 0;
    editPrd?: IEditproduct;
    id: string | null = "";

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


        this.api.getById("").subscribe({
            next: (response) => {
                if (response.isSuccess) {
                    this.title = response.data.title;
                    this.description = response.data.description;
                    this.price = response.data.price;
                }
                this.cdr.detectChanges();
            },
            error: (error) => {

                this.alertCenter.error("", "");

            },
        })
    }


    submit() {

        let editPrd: IEditproduct = {
            id: this.id!,
            title: this.title,
            description: this.description,
            price: this.price,
        }


        this.api.Create(editPrd).subscribe({
            next: (response) => {
                this.alertCenter.success("Saved", "Done")
                this.title = "";
                this.description = "";
                // this.selectedCat = this.cats[0]?.id;

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
