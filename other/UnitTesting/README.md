# Jared - Unit Testing

```c#
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(NoteCreate model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var service = CreateNoteService();

    if (service.CreateNote(model))
    {
        TempData["SaveResult"] = "Your note was created.";
        return RedirectToAction("Index");
    }

    ModelState.AddModelError("", "Note could not be created");
    return View(model);
}

private NoteService CreateNoteService()
{
    var userId = Guid.Parse(User.Identity.GetUserId());
    var service = new NoteService(userId);
    return service;
}
```

- Create()'s primary concern is returning a View of a created note

- **Member**- all of the things that belong to the class - functions (functions belonging to a class are methods), properties, fields, etc.

- "Lazy object" - defer creation of object to later

```c#
public NoteController()
{
    _noteService = new Lazy<NoteService>(() =>
        new NoteService(Guid.Parse(User.Identity.GetUserId)))
}
```

We don't have a user to use in the construtor, so we can "lazy" create it and instantiate it later when we have a user
[link](https://docs.microsoft.com/en-us/dotnet/framework/performance/lazy-initialization)
Using Lazy<> with .Value:
```c#
var model = _noteService.Value.GetNotes();
```

- "Class under test", "function under test", "system under test" - class, function, system being tested

**Arrange, Act, Assert**

