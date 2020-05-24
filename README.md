# Asynq

A tiny `ValueTask` extensions library.

[![Nuget](https://img.shields.io/nuget/v/Asynq)](https://www.nuget.org/packages/Asynq/)
[![Nuget](https://img.shields.io/nuget/dt/Asynq)](https://www.nuget.org/packages/Asynq/)

----
## Usage
```csharp
    
    // await WhenAll
    var values = await Asynq.WhenAll(valueTasks);

    // WhenAll extension 
    var values = await valueTasks.WhenAll();

    // await WhenAny
    var values = await Asynq.WhenAny(valueTasks);

    // WhenAny extension 
    var values = await valueTasks.WhenAny();

    // Fire and Forget
    valueTask.FireAndForget();

    // Fire and Forget with exception handling
    valueTask.FireAndForget(MyCustomExceptionHandler);

    // Convert to ValueTask
    task.AsValueTask();
