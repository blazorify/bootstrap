using System;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Reflection;

namespace Blazorify.Bootstrap {
	public partial class BuiFormFileDropZone : BuiInputComponentBase<IReadOnlyList<IBrowserFile>> {
		[Inject]
		public IJSRuntime? jsRuntime { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public String? Filter { get; set; }

		[Parameter]
		public Boolean Multiple { get; set; } = true;

		[Parameter]
		public RenderFragment<IReadOnlyList<IBrowserFile>>? PreviewTemplate { get; set; }

		protected async Task HandleChange(InputFileChangeEventArgs args) {
			this.Value = args.GetMultipleFiles();

			await this.OnChange.InvokeAsync(this.Value);
		}

		protected async Task HandleClick(EventArgs args) {
			ArgumentNullException.ThrowIfNull(this.jsRuntime);

			var assembly = Assembly.GetExecutingAssembly();
			var script = assembly.GetResourceAsText("Blazorify.Bootstrap.Resources.Scripts.openFileBrowser.js");

			await jsRuntime.InvokeVoidAsync("eval", $"({script})('{this.ID}_FileBrowser')");
		}
	}
}
