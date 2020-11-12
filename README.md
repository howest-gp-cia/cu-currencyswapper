# Currency Swapper

> Demo app to recap Unit Testing and learn Mocking with Moq

<br/>

## Use the branches

-   ### No seperation, no tests
    Untestable version no separation of concerns.<br />
    Available as a MVC, WPF and Console application

    Branch: `demo/1-no-separation` 

-   ### Basic seperation, basic Unit Tests
    Splits business logic form UI, making the logic testable.
    
    Branch: `demo/2-basic-separation` 

    
-   ### Mocked dependencies in Unit Tests
    Extends the conversion to retrieve (live) exchange rates from any provider.<br />
    This creates a dependency which will need to be mocked.

    Branch: `demo/3-extend-conversion` 