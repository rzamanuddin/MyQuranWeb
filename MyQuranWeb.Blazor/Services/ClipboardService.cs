using Microsoft.JSInterop;

namespace MyQuranWeb.Blazor.Services
{
    public class ClipboardService
    {
        private readonly IJSRuntime _jsRuntime;

        public ClipboardService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public ValueTask<string> ReadTextAsync()
        {
            return _jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
        }

        public ValueTask<string> WriteTextAsync(string text)
        {
            return _jsRuntime.InvokeAsync<string>("navigator.clipboard.writeText", text);
        }
    }
}
