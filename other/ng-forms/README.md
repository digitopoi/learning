# Schwarzmuller - Angular - Forms Notes

## Introduction

Basic HTML form:
```html
<form>
    <label>Name</label>
    <input type="text" name="name">
    <label>EMail</label>
    <input type="text" name="email">
    <button type=submit>Save</button>
</form>
```

Angular gives you a JS/TS version of your form data to work with it (along with some metadata)

Check data and state

```js
{
    value: {
        name: 'Max',
        email: 'test@test.com'
    }
    valid: true
}
```

## Template Driven vs. Reactive Approach

1. Template driven - Angular infers the Form Object from the DOM

2. Reactive - form is created programmatically and synchronized with the DOM

## Template Driven

Angular creates the JS object behind the scenes when it detects a form element. 

You don't have to have a POST method, etc. on the form. The form element is bare <form>

However, Angular does not automatically detect your inputs.

You need to add:

1. ngModel to the input
2. name attribute

```js
<input
      type="text"
      id="username"
      class="form-control"
      name="username"
      ngModel>                 // Registers the input with Angular
```

### Submitting and Using the Form

JS/HTML default is to add a click listener on the submit button - NOT in Angular

In Angular, attach (ngSubmit)="<methodName>" to the form element:

```html
<form (ngSubmit)="onSubmit()">
```

To get access to the form created by Angular, you add a #reference:

[passes the form to the onSubmit() method]
```html
<form (ngSubmit)="onSubmit(f)" #f>
```

```js
onSubmit(form: HTMLFormElement) {
    console.log(form);
}
```

Can also attach ngForm to the reference to get access to it automatically:

```html
<form (ngSubmit)="onSubmit(f)" #f="ngForm">
```

Parameter is now of type NgForm (needs to be imported):
```js
onSubmit(form: NgForm) {
    console.log(form);
}
```

Form values are saved on the NgForm.value object

### Understanding Form State

NgForm has many other properties other than value

Controls (inputs), dirty (data changed?), enabled, invalid, touched, untouched

## Accessing the Form with @ViewChild

Alternative approach instead of passing (f) as a parameter:

```js
@ViewChild('f') signupForm: NgForm

...

onSubmit() {
    console.log(this.signupForm);
}
```

Useful if you need to access form before you submit it.

### Adding Validation to Check User Input

Since we're using the template driven approach, we can only add validation in the template (using HTML 5 attributes)

```html
<input
      type="text"
      id="username"
      class="form-control"
      ngModel
      name="username"
      required>                 //  Field is now required.
```

[Other validation you can add: ex. email](https://angular.io/docs/ts/latest/api/forms/index/Validators-class.html)

Adds classes to the HTML behind the scenes:

```html
<ng-dirty ng-touched ng-valid> //   etc.
```

Additionally, you might also want to enable HTML5 validation (by default, Angular disables it). You can do so by adding the ngNativeValidate  to a control in your template.

### Using the Form State

Disable submit button unless the form is valid:

```html
<button
       class="btn btn-primary"
       type="submit"
       [disabled]="!f.valid">Submit</button>
```

Take advantage of CSS classes added:

The whole form is invalid if one field is invalid - need to be explicit on which inputs to add it to

```css
input.ng-invalid, select.ng-invalid {
    border: 1px solid red;
}
```

But, now we have the border right from the start - need to give the user a chance to change it before displaying the red borders

Also check to see if it's touched:

```css
input.ng-invalid.ng-touched {
    border: 1px solid red;
}
```

### Outputting Validation Error Messages

Need to get access to the control created by Angular behind the scenes by adding a local reference (ex. #email="ngModel")
```html
<div class="form-group">
    <input
        type="email"
        id="email"
        class="form-control"
        ngModel
        name="email"
        required
        email
        #email="ngModel"> 
    <span class="help-block" *ngIf="!email.valid && email.touched">Please enter a valid email!</span>
</div>
```

### Set Default Values with ngModel Property Binding

Can use hardcoded string (don't forget '') or a property in the component
```html
<select
       id="secret"
       class="form-control"
       [ngModel]="defaultQuestion"
       name="secret">
    <option value="pet">Your first pet?</option>
    <option value="pet">Your first teacher?</option>
```

```js
defaultQuestion = 'pet';
```

### Using ngModel with Two-Way Binding

Sometimes, you don't just want to populate a default value, you want to instantly react to any changes.

```html
<div class="form-group">
    <textarea 
            name="questionAnswer" 
            rows="3"
            class="form-control"
            [(ngModel)]="answer"></textarea>
</div>
<p>Your reply: {{ answer }}</p>
```

```js
...
answer = '';
```

### Grouping Form Controls

Can group inputs and validate them as a group

Place ngModelGroup directive on the div containing the form-groups

```html
<div id="user-date" ngModelGroup="userData">
```

### Handling Radio Buttons

```js
genders = ['male', 'female'];
```

```html
<div class="radio" *ngFor="let gender of genders">       //  loop through genders
    <label>
        <input
            type="radio"
            name="gender"
            ngModel
            [value]="gender">
        {{ gender }}
    </label>
</div>
```

## The Reactive Approach

Allows you to configure the form in greater detail

### Reactive Setup

Create variable in the controller to hold the form - make the variable of type FormGroup

```js
import { FormGroup } from '@angular/forms';
//
//  ...
//
signupForm: FormGroup
```

In Angular, a form is just a group of controls. Previously, we imported FormGroup to use NgForm (a wrapper for the form).

Import ReactiveFormsModule into app.module.ts

### Creating a Form in Code

Controls are basically key-value pairs we pass as an object to the overall FormGroup

When initializing a FormControl - three parameters available:

1. Initial state, value

2. Single validator or an array of validators

3. Potential asynchronous validators

```js
export class AppComponent implements OnInit {
    genders = ['male', 'female'];
    signupForm: FormGroup;

    ngOnInit() {
        this.signupForm = new FormGroup({
            'username': new FormControl(null),          //   set to null to have empty field
            'email': new FormControl(null),
            'gender': new FormControl('male')
        });
    }
}
```

### Syncing HTML with the code

Angular's default behavior is to create a form object behind the scenes when it seems a <form> element. 

This time we don't want to do that, so we have to override it.

[formGroup] tells Angular to use MY form group - and, need to pass our form as an argument to the directive.

```html
<form [formGroup]="signupForm">
```

Next, we need to tell Angular which controls need to be connected with which inputs in the template

formControlName=""

```html
<input
    type="text"
    id="username"
    formControlName="username"
    class="form-control">
```

### Submitting a Reactive Form

As in the template driven form, we use ngSubmit directive on the form element

```html
<form [formGroup]="signupForm" (ngSubmit)="onSubmit()">
```

The difference is that we don't need to get the form via a local reference. Instead, we use our field we created.

```js
onSubmit() {
    console.log(this.signupForm);
}
```

### Adding Validation

In the template driven approach, we could simply add the 'required' HTML 5 attribute to the input element.

It doesn't work this way with reactive forms! - Not configuring the form in the template - only synchronizing it.

Instead, use second argument to specify validators in the controller:

Can pass a single validator or an array of validators
```js
ngOnInit() {
    this.signupForm = new FormGroup({
        'username': new FormControl(null, Validators.required),     //  single validator
        'email': new FormControl(null, [Validators.required, Validators.email]),        //  array validator
        //  ...
    })
}
```

### Getting Access to Controls

Displaying messages works differently since we have to access the controls differently using the reactive approach.

Instead, use *ngIf directive by accessing <form>.get(<field>).valid - can either pass in the control name or the path

```html
<input 
    type="text"
    id="username"
    formControlName="username"
    class="form-control">
<span
    *ngIf="signupForm.get('username').valid && signupForm.get('username').touched"
    class="help-block">Please enter a valid username</span>
```

### Grouping Controls

Simply nest them in another FormGroup
```js
ngOnInit() {
    this.signupForm: new FormGroup({
        'userData': new FormGroup({
            'username': new FormControl(null, Validators.required),
            'email': new FormControl(null, [Validators.required, Validators.email])
        }),
        'gender': new FormControl('male')
    });
}
```