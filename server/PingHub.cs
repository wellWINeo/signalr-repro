using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace server;

[Authorize]
public class PingHub : Hub
{
    [HubMethodName("getName")]
    public string? GetName() => Context.User?.Identity?.Name;
}
