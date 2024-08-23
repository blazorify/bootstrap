using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorify.Bootstrap.Components {
	public partial class BuiModal : ComponentBase {
		internal ElementReference ElementReference;

		[Inject]
		private IJSRuntime? jsRuntime { get; set; }

		[Parameter]
		public String? ID { get; set; } = $"{Guid.NewGuid()}";

		[Parameter]
		public String? Class { get; set; }

		[Parameter]
		public String? Style { get; set; }

		[Parameter]
		public Boolean Fullscreen { get; set; } = false;

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public RenderFragment<BuiModalHeader>? Header { get; set; }

		[Parameter]
		public RenderFragment<BuiModalBody>? Body { get; set; }

		[Parameter]
		public RenderFragment<BuiModalFooter>? Footer { get; set; }

		[Parameter]
		public EventCallback<Object?> OnShow { get; set; }

		[Parameter]
		public EventCallback<Object?> OnHide { get; set; }

		[Parameter]
		public Boolean Shown { get; set; } = false;

		protected override async Task OnParametersSetAsync() {
			ArgumentNullException.ThrowIfNull(this.jsRuntime, nameof(this.jsRuntime));

			await base.OnParametersSetAsync();
		}

		public async Task Show() {
			// TODO: Ensure to respect bootstrap's transition effects

			// TODO: Remove .modal-open to the <body>
			//await this.jsRuntime!.InvokeAsync<Window>(window => window.document.body.classList.add("modal-open"));

			this.Shown = true;

			// TODO: implement OnShown event

			await this.OnShow.InvokeAsync();


			await this.InvokeAsync(this.StateHasChanged);
		}

		public async Task Hide(Object? payload = null) {
			// TODO: Ensure to respect bootstrap's transition effects

			this.Shown = false;

			// TODO: Remove .modal-open to the <body>
			//await this.jsRuntime!.InvokeAsync<Window>(window => window.document.body.classList.re("modal-open"));

			// TODO: implement OnHidden event

			if (payload != null) {
				await this.OnHide.InvokeAsync(payload);
			}

			await this.InvokeAsync(this.StateHasChanged);
		}
	}

	public class BuiModalOptions {
		public String? ID { get; set; } = $"{Guid.NewGuid()}";

		public String? Class { get; set; }

		public String? Style { get; set; }

		public Boolean Fullscreen { get; set; } = false;
	}

	public class BuiModalOptions<TData> : BuiModalOptions {
		public TData? Data { get; set; }
	}
}
