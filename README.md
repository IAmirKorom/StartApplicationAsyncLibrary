
>> Hello World!

In short: 
--
`````This is a library in nuget for make development easily in asp.net`````
_____________

Use this code instead "app.run":
-----
`````var startApplicationAsync = new StartApplicationAsyncLibraryHelper(() => app.RunAsync(), builder.Configuration);`````
`````await startApplicationAsync.StartAsync();`````
