# CSS Isn't Scary - Stephanie Slattery

`Ugh, CSS, it seems like it has a mind of its own and gets overwhelming when you use it for anything more than a simple layout. Sometimes, it feels like you're just guessing on what property to use and hoping for the best. You might feel intimidated and find yourself needing a front end developer to make even tiny changes to your site. In this presentation, I'll help you tame that CSS beast and discuss tips and tricks for getting your site to look exactly right the first time. I'll explain common misunderstandings about CSS and show you that, no, CSS isn't that scary.`

tinyurl.com/y9p4ypw3

Separation of concerns - JS, HTML, CSS

CSS is flexible.

CSS is simply a selector and name value pairs - it's not complex

```css
.selector {
    name: value;
}
```

CSS is easy for other code to generate it.

### Write better CSS

Use your programming skills

- Reading the docs

- Planning your code

- Pseudocoding

- Refactoring

Understanding specificity

0. Inline styles

1. IDs

2. Classes, attributes, and pseudo-classes

```css
[type="radio"]      :hover
```

3. Elements and pseudo-elements

```html
<h1></h1>

:before

:after
```

**Don't guess** and check for specificity (Specificity calculator)

Don't overspecify - very specific selectors will be hard to override in the future

No !important flags

And no inline styles

Use a single class as your selector

DRY CSS

CSS extensions make this even easier - SASS, LESS

Organize your CSS

`hacks.css` file

Put it in `hacks.css` if you're

- using magic numbers

- writing overly specific selectors

- using !important flags

- undoing styles that are elsewhere in the file

Write why you did it.

Possible ways to fix it.


Other recs:
- Understand browser compatibility

- Learn the box model

- use flexbox

- pick a preprocessor

- implement a naming methodology like BEM OOCSS

- use a linter

- look at dev tools

- use a CSS reset