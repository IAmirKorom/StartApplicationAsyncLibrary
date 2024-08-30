
>> Hello World!


In short: 
--
`````____This is a library in nuget for make development easily in asp.net`````

Installation:
-----
`````dotnet add package StartApplicationAsyncLibrary --version 1.0.3`````

Use this code instead "app.run":
-----
`````using StartApplicationAsyncLibrary;`````

`````var startApplicationAsync = new StartApplicationAsyncLibraryHelper(() => app.RunAsync(), builder.Configuration);`````

`````await startApplicationAsync.StartAsync();`````
