# Angular Docs

[link](https://angular.io/tutorial/toh-pt0)

```bash
ng serve --open
```

--open flag opens a browser to <http://localhost:4200/>

**application shell**- controlled by AppComponent

**interpolation binding**- {{ title }}

**ngOnInit**- lifecycle hook Angular calls shortly after creating a component. Good place to put initialization logic.

## Lifecycle hooks and OnInit()
[link](https://angular.io/guide/lifecycle-hooks)

A component has a lifecycle managed by Angular. 

- Create component, render it, create and render its children, checks it when its data-bound properties change, and destroys it before removing it from the DOM.

- Developers can tap into key moments in the lifecycle by implementing one or more of the lifecycle hook interfaces in the Angular core library:

1. ngOnChanges
2. ngOnInit
3. ngDoCheck
  - ngAfterContentInit
  - ngAfterContentChecked
  - ngAfterViewInit
  - ngAfterViewChecked
4. ngOnDestroy

example: the OnInit interface has a hook method named ngOnInit() that Angular calls shortly after creating a component:

```javascript
export class PeekABoo implements OnInit {
    constructor(private logger: LoggerService) { }

    //  implements OnInit's 'ngOnInit' method
    ngOnInit() { this.logIt('OnInit'); }

    logIt(msg: string) {
        this.logger.log(`#${ nextId++ } ${ msg }`);
    }
}
```

No directive or component will implement all of the lifecycle hooks and some of the hooks only make sense for components. Angular only calls a directive/component hook method *if it is defined*

### OnInit()

Use ngOnInit() for two main reasons:

1. To perform complex initializations shortly after construction.

2. To set up the component after Angular sets the input properties.

[Components should be cheap and safe to construct](http://misko.hevery.com/code-reviewers-guide/flaw-constructor-does-real-work/)

Don't fetch data in component constructor. Constructors should do no more than set the initial local variables to simple values.

ngOnInit() is a good place for a component to fetch its initial data.