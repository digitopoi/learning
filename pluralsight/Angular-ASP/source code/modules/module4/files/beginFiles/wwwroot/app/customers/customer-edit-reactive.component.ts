import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { DataService } from '../core/data.service';
import { ICustomer, IState } from '../shared/interfaces';
import { ValidationService } from '../shared/validation.service';

@Component({
    moduleId: module.id,
    selector: 'customer-edit-reactive',
    templateUrl: './customer-edit-reactive.component.html' 
})
export class CustomerEditReactiveComponent implements OnInit {

    customerForm: FormGroup;
    customer: ICustomer = {
        firstName: '',
        lastName: '',
        gender: '',
        address: '',
        email: '',
        city: '',
        zip: 0
    };
    states: IState[];
    errorMessage: string;
    deleteMessageEnabled: boolean;
    operationText: string = 'Insert';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private dataService: DataService,
        private formBuilder: FormBuilder) { }

    ngOnInit() {
        let id = this.route.snapshot.params['id'];
        if (id !== '0') {
            this.operationText = 'Update';
            this.getCustomer(id);
        }

        this.getStates();
        this.buildForm();
    }

    getCustomer(id: string) {
        this.dataService.getCustomer(id)
            .subscribe((customer: ICustomer) => {
                this.customer = customer;
                this.buildForm();
            },
            (err) => console.log(err));
    }

    buildForm() {
        this.customerForm = this.formBuilder.group({
            firstName: [this.customer.firstName, Validators.required],
            lastName: [this.customer.lastName, Validators.required],
            gender: [this.customer.gender, Validators.required],
            email: [this.customer.email, [Validators.required, ValidationService.emailValidator]],
            address: [this.customer.address, Validators.required],
            city: [this.customer.city, Validators.required],
            stateId: [this.customer.stateId, Validators.required]
        });
    }

    getStates() {
        this.dataService.getStates().subscribe((states: IState[]) => this.states = states);
    }

    submit({ value, valid }: { value: ICustomer, valid: boolean }) {

        value.id = this.customer.id;
        value.zip = this.customer.zip || 0;

        if (value.id) {


        } else {



        }
    }

    cancel(event: Event) {
        event.preventDefault();
        this.router.navigate(['/customers']);
    }

    delete(event: Event) {

    }

}