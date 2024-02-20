
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

const string URL = "http://localhost:5047/hub";
const string ACCESS_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzcGVuIiwic3ViIjoidXNwZW4iLCJqdGkiOiJiZDE2MWVmMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTA0NyIsIm5iZiI6MTcwODQyMjI4NCwiZXhwIjoxNzE2MTk4Mjg0LCJpYXQiOjE3MDg0MjIyODUsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.4thMRYcPQHwIw1ou3G3iJ1CgGk6j0Atj6oynrfwgsu8";

var connection = new HubConnectionBuilder()
    .WithUrl(URL, HttpTransportType.WebSockets, options =>
    {
        options.AccessTokenProvider = () => Task.FromResult<string?>(ACCESS_TOKEN);
    })
    .ConfigureLogging(logging =>
    {
        logging.SetMinimumLevel(LogLevel.Trace);
        logging.AddConsole();
    })
    .Build();

await connection.StartAsync();

var response = await connection.InvokeAsync<string?>("getName");

Console.WriteLine(response);