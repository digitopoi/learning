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

## Pipes

A pipe takes in data as input and transforms it to a desired output. 

Angular comes with many built-in pipes: DatePipe, UpperCasePipe, LowerCasePipe, CurrencyPipe, and PercentPipe. 

A pipe can accept a number of optional parameters to fine-tune its output.

```javascript
<p>The hero's birthday is {{ birthday | date:"MM/dd/yy" }} </p>
```

### Chaining pipes

You can chain pipes together:

```javascript
{{ birthday | date | uppercase}}

// FRIDAY, APRIL 15, 1988
```

## Services

Components shouldn't fetch or save data directly - should focus on presenting data and delegate data access to a service.

Services are a great way to share information among classes that *don't know each other*

@Injectable() decorator tells Angular that this service *might* itself have injected dependencies. Whether or not it does eventually - the recommednation is to use the decorator.

Services can get data from anywhere - a web service, local storage, or a mock data source.

Removing data access from components means you can change your mind about the implementation anytime, without touching any components. They don't know how the service works.

You must *provide* the service in the *dependency injection system* before Angular can *inject* it into a component.

You can tell the CLI which module to provide it to when creating the service:
```javascript
ng generate service hero --module=app
```

## Observable Data

```javascript
this.heroes = this.heroService.getHeroes();
```

HeroService.getHeroes() has a *synchronous signature* - implies that HeroService can fetch heroes synchronously. The HeroesComponent consumes the service as if it could be fetched synchronously.

**This will not work in a real app**

We're getting away with it now because we're only using mock data. When we fetch data from a real server - it needs to be done asynchronously.

HeroService.getHeroes() needs to have an *asynchronous signature* of some kind - it can take a callback with either a Promise or an Observable. 

We'll use an Observable - because it will use the HttpClient.get method to fetch the heroes and HttpClient.get() returns an Observable. 

Observable is one of the key classes in the RxJS library.

```javascript
getHeroes(): Observable<Hero[]> {
    return of(HEROES);
}
```

of(HEROES) returns an Observable<Hero[]> that emits a *single value*: the array of heroes.

Since we're returning an Observable now, we'll have to subscribe to it in the HeroesComponent.

```javascript
getHeroes(): void {
    this.heroService.getHeroes()
      .subscribe(heroes => this.heroes = heroes);
  }
```


### Service-in-service

Injecting the MessageService into the HeroService which is injected into the HeroesComponent

