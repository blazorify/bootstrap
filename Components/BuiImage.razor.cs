using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap.Components {
	public partial class BuiImage : ComponentBase {
		private Boolean isLoading = true;
		private Boolean hasLoadingError = false;
		private String source = String.Empty;

		[Parameter]
		public String ID { get; set; } = String.Empty;

		[Parameter]
		public String Class { get; set; } = String.Empty;

		[Parameter]
		public String Style { get; set; } = String.Empty;

		[Parameter]
		[EditorRequired]
		public String Source { get; set; } = String.Empty;

		[Parameter]
		public RenderFragment? BuiImageFallback { get; set; }

		[Parameter]
		public RenderFragment? BuiImagePlaceholder { get; set; }

		[Parameter]
		public String Alt { get; set; } = String.Empty;

		[Parameter]
		public Func<ProgressEventArgs, Task>? OnLoad { get; set; } = null;

		[Parameter]
		public Func<ErrorEventArgs, Task>? OnError { get; set; } = null;

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			// Check if source has been changed
			if (this.source != this.Source) {
				this.isLoading = true;
				this.hasLoadingError = false;
			}

			this.source = this.Source;
		}

		// TODO: Handle loadstart and loadend
		private async Task HandleLoad(ProgressEventArgs e) {
			this.isLoading = false;

			if (this.OnLoad != null) {
				await this.OnLoad.Invoke(e);
			}
		}

		private async Task HandleError(ErrorEventArgs e) {
			this.hasLoadingError = true;
			this.isLoading = false;

			if (this.OnError != null) {
				await this.OnError.Invoke(e);
			}
		}
	}
}
