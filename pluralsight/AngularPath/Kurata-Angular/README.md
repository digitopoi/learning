# Angular: Getting Started (Kurata)

## Introduction

Angular is a JavaScript framework for building client-side applications using HTML, CSS, and JavaScript. 

### Why Angular?

  - Makes our HTML more expressive

  - Powerful data binding

  - Modular by design 

  - Built-in Back-end Integration

### Why a new Angular?

  - Built for speed

  - Modern (takes advantage of latest JS standards)

  - Simplified API

  - Enhances our productivity

### Anatomy of an Angular Application

An application is comprised of a set of components and services that provide services across those components

#### Angular Component
 
  1. Template - user interface fragment, defining the view

  2. Class - code associated with the view (properties/data elements, methods)

  3. Metadata - providing additional information about the component to Angular

#### Angular Modules

  - help us organize our application into cohesive blocks of functionality

  - every Angular application has at least one module (root)

## Course Repo

*****
[link](https://github.com/DeborahK/Angular-GettingStarted)
*****

## Application Architecture

![architecture](/images/architecture.png)


## Modules

With JavaScript, there is always a problem of namespaces. 

Easy to end up accidentally with variables in the global namespace

Also makes code organization more difficult

ES 2015 set a standard for JS modules - a module is a file and a file is a module

Angular leverages ES 2015 modules. 

Angular also has Angular modules (separate and different from ES 2015 modules)

### ES 2015 Modules

product.ts
```js
export class Product {

}
```

Product is exported and becomes a module because the class is exported - can then use the class in another module by importing it.

product-list.ts
```js
import { Product } from './product'
```

### Angular Modules

Angular modules help organize an application into cohesive blocks of functionality.

Components / features can be grouped into a related module

Can also define shared modules

Each component we create belongs to an Angular module.

### Differences

ES Modules are files that import or export something

Angular modules are code files that organize the application into cohesive blocks of functionality.

ES Modules organize our code files

Angular modules organize our application

ES Modules organize our code

Angular modules organize our application

ES Modules promote code reuse

Angular modules promote application boundaries

## Introduction to Components

Angular applications are sets of components

Components are created and then arranged to form the application

### What is a component?

#### Template
An Angular component includes a template which lays out the user interface fragment defining a view for the application.

Angular binding and directives are used in the HTML to power up the view.

#### Class

An Angular component also has a class created with TypScript. 

Properties and methods available to the view

#### Metadata

Extra data for Angular

Defined with a decorator

app.component.ts
```js
import { Component } from '@angular/core';

@Component({
    selector: 'pm-root',
    template: `
        <div><h1>{{ pageTitle }}</h1>
            <div>My First Component</div>
        </div>
    `
})
export class AppComponent {
    pageTitle: string = 'Acme Product Management';
}
```

### Class Component

```js
export class AppComponent {
    pageTitle: string = 'Acme Product Management';
}
```

### Template

```js
@Component({
    selector: 'pm-root',
    template: `
        <div><h1>{{ pageTitle }}</h1>
            <div>My First Component</div>
        </div>
    `
})
```

**decorator**- a function that adds metadata to a class, its members, or its method arguments

prefixed with an @ 

**selector**- defines the component's **directive** name.

**directive**- custom HTML tag

### Importing

Before we can use an external function or class, we define where to find it.

Allows us to use exported members from external ES modules

Angular is **modular**

[List of available Angular library packages](https://www.npmjs.com/~angular)

```js
import { Component } from '@angular/core';
```

### Single Page Application (SPA)

index.html contains the main page for the application

This is often the only web page of the application

Hence, an Angular application is often called a Single Page Application (SPA)

index.html
```html
<body>
    <pm-root></pm-root>
</body>
```

When the Angular compiler sees a directive in a template, it looks to the Angular module for the definition

Module is also used to bootstrap our application

app.module.ts
```js
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

@NgModule({
    imports: [ BrowserModule ],
    declarations: [ AppComponent ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }
```

Identified as a module by passing the NgModule decorator and passing in metadata defining the details of this Angular module.

For the NgModule decorator, the properties are arrays.

The bootstrap array defines the startup component of the application

## Component Checklist

We create a class for the component with the code to support the view

We use a decorator to define the component metadata, which includes the HTML for the component's template

We import what we need from any third party library, from our modules, or from Angular itself

When creating the component class, we give it a clear name - not only the name of the class, but also the name of the component

Be sure to include export keyword signature when declaring a class.

Set the appropriate data type and default value when possible when declaring properties.

### Something is wrong checklist

F12 and use developer tools to check the console. Angular error messages are often quite clear if you read past the initial technical details.

Recheck your code.