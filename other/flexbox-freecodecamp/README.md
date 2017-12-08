# Understanding Flexbox: Everything you need to know

## Introduction

- Flexbox provides an efficient way to layout, align, and distribute space among elements within your document - even when the viewport and the size of your elements is dynamic or unknown.

- To start using flex box, you must first define a **flex container**

```html
<ul>                <!-- parent element -->

    <li></li>            <!-- first child element -->

    <li></li>            <!-- second child element -->

    <li></li>            <!-- third child element -->
</ul>
```

- To make use the flexbox model, you must make the parent container a flex container, aka *flexible container*

```css
ul {
    display: flex;
}
```

Applying a flex container to the parent [<ul>] causes the boxes to stack horizontally instead the the default vertical.

## Two important definitions

**flex container**- the parent element you've set to: display: flex

**flex items**- the child elements within a flex container

## Flex Container Properties

[axis](https://i.imgur.com/6HL57uT.png)

1. Flex-direction - controls which direction the flex-items are laid along the **main axis** (horizontally, vertically, or reversed in either direction)

  1. row (default) - aligns the flex-items along the main axis

  2. column

  3. row-reverse

  4. column-reverse

2. Flex-wrap the flex container adapts to accomodate flex items added to it (default is nowrap)

  1. wrap - flex items now wrap to the next line, stay in default widths

  2. nowrap

  3. wrap-reverse - breaks into multiple lines, but in the opposite direction

3. Flex-flow - shorthand for flex-direction AND flex-wrap values

```css
ul {
    flex-flow: row wrap;
}
```

4. Justify-content - defines how flex items are laid out on the main axis

  1. flex-start - groups all flex-items to the start of the main axis

  2. flex-end - groups items to the end of the main axis

  3. center - centers the items along the main axis

  4. space-between - keeps the same space between each item

  5. space-around - keeps the same spacing around the flex items

5. Align-items - how items are aligned on the **cross** axis

  1. flex-start

  2. flex-end

  3. center - aligns the items to the center of the flex container

  4. stretch - stretches the items so they fill the entire height of the flex container

  5. baseline

6. Align-content - used on multi-line flex-containers (ex. after being wrapped)

  - same values as Align-items

## The Flex Item Properties

1. Ordering - allows for reordering items in a flex container

  - default value is 0, reordered from lowest to highest (number can be negative or positive)

2. flex-grow and flex-shrink - control how much a flex-item should grow/extend if there are extra spaces, or shrink if there are no extra spaces

3. flex-basis - specifies the initial size of a flex-item

4. flex shorthand

flex-grow, flex-shrink, flex-basis

```css
li {
    flex: 0 1 auto;
}
```

## Auto-margin alignment

- Beware of auto alignment on flex items


