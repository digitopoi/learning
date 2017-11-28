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

## Routing

An Angular best practice is to load and configure the router in a separate, top-level module that is dedicated to routing and imported by the root AppModule.

By convention, the module class name is AppRoutingModule and it belongs in the app-routing.module.ts in the src/app folder.

```bash
ng generate module app-routing --flat --module=app
```

--flat puts the file in src/app instead of its own folder
--module=app tells the CLI to register it in the imports array of the AppModule

*Routes* tells the router which view to display when a user clicks a link or pastes a URL into the browser address bar.

A typical Angular Route has two properties:

1. path- a string that matches the URL in the browser address bar
2. component- the component that the router should create when navigating to this route.

### RouterModule.forRoot()

You must first initialize the router and start it listening for browser location changes.

In the @NgModule.imports array:

```javascript
imports: [ RouterModule.forRoot(routes) ]
```

This method calls forRoot() because you configure the router at the application's root level. The forRoot() method supplies the service providers and directives needed for routing, and performs the initial navigation based on the current browser URL.

### RouterLink

Need to provide a link to the new URL:

```javascript
<a routerLink="/heroes">
```

The routerLink is the selector for the RouterLink directive that turns user clicks into router navigations. It's a public directive in the RouterModule.

