using System;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.JSInterop {
	public static class IJSRuntimeExtensions {
		public static async Task<TResponse> InvokeScriptAsync<TResponse>(this IJSRuntime runtime, String scriptName, params Object[]? parameters) {
			var assembly = Assembly.GetExecutingAssembly();
			var script = assembly.GetResourceAsText(scriptName);

			var jsonSerializerOptions = new JsonSerializerOptions {
				MaxDepth = 32,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				PropertyNameCaseInsensitive = true,
				//Converters = {
				//	new DotNetObjectReferenceJsonConverterFactory(runtime),
				//	new JSObjectReferenceJsonConverter(runtime),
				//	new JSStreamReferenceJsonConverter(runtime),
				//	new DotNetStreamReferenceJsonConverter(runtime),
				//	new ByteArrayJsonConverter(runtime),
				//}
			};

			var args = String.Empty;

			if (parameters != null) {
				args = $"...{JsonSerializer.Serialize(parameters, jsonSerializerOptions)}";
			}

			return await runtime.InvokeAsync<TResponse>("eval", $"({script})({args})");
		}
	}
}
