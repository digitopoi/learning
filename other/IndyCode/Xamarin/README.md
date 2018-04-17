# Building Offline-Sync-Capable Mobile Data Apps with Xamarin and Azure

`As mobile apps enter ever more verticals, building data-capable apps that must work in non-connected scenarios is increasingly important. Azure and Xamarin work together to bring you online/office synchronization without writing a custom framework. Node and WebAPI back-ends are easily created. Let me show you the happy path to mobile app data sync.`

aka.ms/devessentials - Azure credit for a year.

Offline data access on a case-by-case basis.

**Azure development subscription**

Build native IOS and Android app to familiarize yourself with operating system, designs and practices of the native platforms.

Modern apps are all about data.

- take data from source (generally flat data)

- display it

- optionally, add or update data

- save it

- repeat

Because you're just displaying data and rendering a UI - two fully 'native' apps aren't as necessary (both apps have a list, etc.). Write once, run anywhere, maintain one codebase.

XAML maps to the native controls.

### What is Azure Mobile Services?

Provides standardized data synchronization, push notification, single sign-on, hosting, and other services.

One call, multiple platforms. Change resistant.

SDKs for every major platform.

Easy to get started, and relatively affordable. Free tiers for most services.

Push notifications, federated security, social sign-on.

Be careful about building your application to coupled to it.

Generally in the US, we have data connection most of the time. AMS is best for applications targeting areas that may not have service (Farming app, Push notification heavy,)

### When not to use it

One-way data sync, such as lookup data. Just write the API.

If you need control about **when** data synchronizes.

Don't want to tie it to a framework.

Highly opinionated.