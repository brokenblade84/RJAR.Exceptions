# RJAR.Exceptions

Component to centralize error handling in net core web api applications. This component provides a middleware to configure the global exception handling in your application with the required configuration to be able to log the exceptions thrown.

The component defines the custom exceptions needed to cover all possible outcomes when handling errors in the application. You won't need to define a try/catch block anymore in your app. (Unless a third party component or specific functionality requires it).

## Setting up the middleware in the application

In your startup.cs file in your ConfigureService Method add the following:

``` csharp
public void ConfigureServices(IServiceCollection services)
{
   //...
   services.UseExceptionMiddleware();
   //...
}
```

and in your Configure method add the extension to use the middleware:

``` csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
   //...
   app.UseExceptionMiddleware();
   //...
}
```

## How to use

This component will provide 3 different types of exceptions to handle errors in the application. Depending of the exception type thrown you will get a response with the required information as a result. The component will guarantee that the response will always have the same format no matter what kind of exception was raised.

### Functional Exception

Functional exceptions are used to throw business validation errors. This exceptions will return the real exception message configured in the response.

To throw a functional exception just throw the exception directly in your code after your validations. No try/catch block is required.

``` csharp
   throw new FunctionalException("<Add your custom message here>");
```

e.g.

``` json
{
   statusCode: 400, //Bad Request
   errorMessage: "An error has ocurred", //The business validation error defined.
   validationFieldMessages: [
      {
         "fieldName":"Error thrown"
      },
      {
         "fieldName2":"Error thrown 2"
      },
   ] //List of attributes with the business error assosiated.
}
```

This exception needs to be thrown manually by the user.

### Technical Exceptions

Technical exceptions will be raised automatically if for example the application has thrown an ArgumentNullException or a NullReferenceException, but this type of exception can be thrown manually by the user too. For example, if you need to validate a DbContext or a third party library that requires a try/catch block you can use this exception as following:

``` csharp

try
{
   //The code that could raise an exception (Defined or not).
}
catch (Exception tex)
{
  //You do not need to log the exception or add other type of setting because
  //it will be handled by the custom middleware internally.
  var technicalException = new TechnicalException(ex.Message, ex);
  throw tex;
}
```

The response generated for this exception will have the same format as the functional exception, but the message returned as a result will be generic. If you want to know the real message of the exception you would need to find it in your application logging,

e.g.

``` json
{
   statusCode: 501,
   errorMessage: "An error has ocurred. Please contact your system administrator for further details."
}
```

### Unhandled Exceptions

Unhandled exceptions englobe all the exceptions that the application can throw and that are not expected.  
This exception cannot be thrown manually by the user, it will be generated automatically by the middleware and the response will be exactly the same as the technical exceptions. You will need to check your application logging to find the real cause of the exception.

e.g.

``` json
{
   statusCode: 501,
   errorMessage: "An error has ocurred. Please contact your system administrator for further details."
}
```
