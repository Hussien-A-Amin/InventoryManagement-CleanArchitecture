import { JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-register',
    imports: [ReactiveFormsModule,JsonPipe],
    templateUrl: './register.html',
    styleUrl: './register.css',
})
export class Register {
addTel() {

}

    registerForm:FormGroup;
    constructor(){
        this.registerForm=new FormGroup({
            name:new FormControl("",[Validators.required,Validators.minLength(5)]),
            email:new FormControl(""),
            password:new FormControl(""),
            address:new FormGroup({
                city:new FormControl(""),
                street:new FormControl("")
            }),
            phoneNums:new FormArray([new FormControl("")])
        });
    }
    
     get phone(){
    return "";
    }
register() {


}
}