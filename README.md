# TechiesMoneyExchange

## Sample .NET MAUI project for ViewModel refactoring

This sample showcase a Money-Exchange app to buy/sell **USD** and **PEN (Peru New Sol)**.

### Links in Docs

<https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm>

<https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/dependency-injection>

<https://docs.microsoft.com/en-us/xamarin/xamarin-forms/xaml/xaml-basics/data-bindings-to-mvvm>Ading 

<https://docs.microsoft.com/en-us/learn/modules/design-a-mvvm-viewmodel-for-xamarin-forms/>

### 1. DI and IoC 🚀

Without some king of service locator or Dependency-Injection container your ViewModels have to deal with the dependencies manually and keep up with the changes. That situation is not easy to work with and it is not a good practice.

### 2. Use Cases 💻

Some ViewModels are complex and have a lot of dependencies by nature, even more common in Line-of-Business applications. Use cases are a composition pattern that allows to decouple the ViewModel from the business/interaction logic.

### 3. Fixing Anemic models 💾

Anemic models is a common problem in the world of software development and it happens when our model is only a data structure and not reprsent the behaviors of the real world object.

### 4. Event Aggregator ⌨️

Sometimes makes sense to decompose a ViewModel into smaller ViewModels and use an Event Aggregator to notify the changes between them.

### 5. Adding Controllers and Interactors 🤖

Some controls espcially third-party or community developed controsl don't have a Mvvm orientation and lack Bindable properties. Make those controls work with your VM usually leads to lot of boilerplate code and coupling to the View. Introducing a controller or interactor allow us a better separation of concerns.