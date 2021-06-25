This implements CQS (Command-Query-Separation).

Basically CQS means that commands and queries are handled separately and therefore allow segregation of these operations.

The main punch line of the pattern is that
1. commands never return a value and always change state
2. queries always return a value and never change state

The benefits are:
1. queries can be faster because they can work on a cache, so the persistance access workload is reduced by that.
2. event sourcing can be a natural consequence as only commands will store domain events

How to use it:
- In a ViewModel inject a command or query dispatcher (for the command to dispatch)
- Depending on the architecture we either
  1. inject the services we need into a handler and evaluate
  2. derive our services from a handler