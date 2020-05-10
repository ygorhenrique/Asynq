# Asynq

Adds WhenAll, WhenAny, and FireAndForget methods to `ValueTask`


----
## Usage

    var values = await Asynq.WhenAll(valueTasks)

**Or via extension methods**

    var values = await valueTasks.WhenAll()


