## The motivation behind it all

What is the point of all this? Well, besides showcasing Bob Ross... I thought it would be clever to have a "real world" application to reference when talking architecture. I want something to set a standard, something to clone, something to utilize in all my next .Net Core projects. Like all software (and wine), this will improve with time.

## Key Points:

#### N-Tier Architecture

A 3 layered approach, to set us up for multiple UI's that use the same core business functionality/back-end. 

#### Separation of Concerns

Closely related to the ["Single Responsibility"](http://deviq.com/single-responsibility-principle/) principle, Separation of Concerns (SoC) makes your code more maintainable, by not co-locating ideas. When things change, SoC will help ensure your changes are limited to your feature set, and not spread throughout your application.

#### No Leaky Abstractions

By using interfaces, we can avoid leaky abstractions, and have better control of how our code is used. On the contrary, consumers of our code don't have to know the implementation details, they can simply use your interfaces/classes.

#### Testable

Testing code that is tightly coupled is nearly impossible. By following the SOC principal, and using interfaces our code will be easier to rest in result.

#### Modular

The code will be easier to modify in the future, because of a few reasons. Separation of concerns allows for fewer changes/testing throughout the application. You can build on specific feature sets, without modifying the rest of the code.

#### Maintainable

The code will inherently be more maintainable by following Clean Architecture patterns. Other developers will see the benefit of the clean separation, and follow suit.
