using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorify.Bootstrap {
	public partial class BuiModal : BuiContentComponentBase {
		[Inject]
		private IJSRuntime? jsRuntime { get; set; }

		[Inject]
		private BuiModalService? modalService { get; set; }

		[Parameter]
		public Boolean Fullscreen { get; set; } = false;

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
		public EventCallback<Object?> OnClose { get; set; }

		[Parameter]
		[BindClass("show", true)]
		public Boolean Shown { get; set; } = false;

		protected override async Task OnParametersSetAsync() {
			ArgumentNullException.ThrowIfNull(this.jsRuntime);

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

		public async Task Close(Object? payload = null) {
			ArgumentNullException.ThrowIfNull(this.modalService);

			await this.Hide(payload);

			await this.modalService.Close(this);
		}
	}
}
