# Portfolio

This is the source for my portfolio website written in Blazor wasm and backing Azure Functions API. I'd love to get some time to generalize and have it pull content from a JSON file so others could stand up a similar portfolio with it.

## Prerequisites
To build and run the project, you'll need to install the following prequisites:
* [dotnet SDK](https://github.com/dotnet/sdk)
* [Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools)

## Building
To install the required packages, and build run the following command:
```
dotnet build
```

## Running
To run the Azure Functions locally, run the following commands:
```
cd .\Portfolio.Functions
func start
```

To run the Blazor front-end, run the following commands:
```
cd .\Portfolio.FrontEnd
dotnet watch run
```

## Contributing

Feel free to create issues, or submit a PR.

## License

MIT © Jason Shands
