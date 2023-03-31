# AiController
AI control under distributed systems

# Demo
![](./doc/Demo.gif)

# Run
+ Build and run [`AiController.Server`](./AiController/AiController.Server/)

+ Build and run [`AiController.Console`](./AiController/AiController.Console/)


# Core
Define a set of semantic-based wrappers and unpacks which allows client messages to pass through more AI layers unconsciously

They may contains
+ Risk control
+ Scheduling
+ Authentication

`Dynamic proxy` enable AI to intervene in every link.

# Structure
+ [`Abstraction`](./AiController/AiController.Abstraction/) Interfaces that define how to communicate and transform messages.
+ `Operation`
  + [`Operation`](./AiController/AiController.Operation/) Implements of message transformations.
+ `Communication`
  + [`Communication`](./AiController/AiController.Communication/) Implements of communication with AI.
+ `Execution`
  + [`Client`](./AiController/AiController.Client/) Client behaviors
  + [`Console`](./AiController/AiController.Console/) Demo
  + [`Desktop.Wpf`](./AiController/AiController.Desktop.Wpf/) Desktop
  + [`Server`](./AiController/AiController.Server/) SignalR server