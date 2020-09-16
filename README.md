# SupermarketCheckout

A supermarket checkout website example

## Commitizen friendly
[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)](http://commitizen.github.io/cz-cli/)

Please use `npm run cm` to add new commits, in this way you can create beautiful commits that your partner will appreciate

## Introduction

The application has been develop using .Net Core 3 for the backend and Angular 10 with Material in the frontend.

We have used [Azure Devops](https://dev.azure.com/littlepeterr/SupermarketCheckout) to plan the tasks.
And the repository code is under [github](https://github.com/PedroJesusRomeroOrtega/SupermarketCheckout). 
We have add integration between *Azure Devops* and *github* so when we do a commit we mark with the associated *Azure Board Task*

To see the example working, just set *WebApplication* as startup project and execute it!

### Backend

It has been develop thinking in a *Clean architecture* to have separation of concerns.

All the tests are in *tests* folder. There are missing integration tests and functional tests, but *unit test* are the core and the most important tests so we have focused on them.

Inside *src* we have three principals projects:

* **Core** has the domain of the application and we try to don´t add dependencies with external frameworks.
* **Infrastructure** has the implementation of the interfaces defined in Core layer.
  *Entity Framework* has been added in this layer.
* **WebAplication** contains the *webApi* and the *Angular* UI.

### Frontend

All the Checkout functionality is inside *checkout* folder.

All the logic is inside *checkout.service.ts* where the interaction with the API has been develop using RXJS.

It has a basic routing with to principal components, *checkout-list* and *checkout-detail*.

We have three components:

* **Checkout-list** where we show a list with all the checkouts
  
* **Checkout-detail**, is the component to add or edit a checkout. It shows the list of product we can add to the checkout.
  
* **Sku-checkout** is the component that represent the product.

### What else we would have added

* Do more test. Integration tests and e2e (end to end tests) are missing

* We would have like to add Docker support. 
  In this way it would be easy to deploy in Azure Cloud.

* Add CI/CD. With this, the tests would have been executed automatically