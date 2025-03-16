using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdown : BuiContentComponentBase {
		private ElementReference? elementReference = null;
		private DotNetObjectReference<BuiDropdown>? dotNetObjectReference = null;

		[Inject]
		private IJSRuntime jsRuntime { get; set; } = default!;

		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		[Parameter]
		public AutoClose AutoClose { get; set; } = AutoClose.ClickInside;

		internal BuiDropdownSharedState State { get; set; } = new();

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (this.dotNetObjectReference == null) {
				this.dotNetObjectReference = DotNetObjectReference.Create(this);
			}

			if (firstRender) {
				ArgumentNullException.ThrowIfNull(this.State);

				this.State.OnChange(s => s.Open, async open => {
					this.Open = open;

					if (this.AutoClose is AutoClose.ClickInside or AutoClose.ClickOutside) {
						if (this.Open) {
							await this.jsRuntime.InvokeVoidAsync(
								this.AutoClose == AutoClose.ClickInside ? "Blazorify.onClickInside" : "Blazorify.onClickOutside",
								this.dotNetObjectReference,
								nameof(this.HandleAutoClose),
								this.elementReference?.Id
							);
						} else {
							await this.jsRuntime.InvokeVoidAsync(
								this.AutoClose == AutoClose.ClickInside ? "Blazorify.offClickInside" : "Blazorify.offClickOutside",
								this.elementReference?.Id
							);
						}
					}

					await this.ApplyBindClassAttributes();
				});
			}
		}

		[JSInvokable]
		public async Task HandleAutoClose(MouseEventArgs args, String elementReferenceID) {
			if (elementReferenceID == this.elementReference?.Id && this.Open == true) {
				this.Open = false;

				await this.HandleClose(args);
			}
		}

		public async Task HandleOpen(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.State);

			// TODO: Implement transition
			this.State.Open = this.Open;

			await Task.CompletedTask;
		}

		public async Task HandleClose(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.State);

			// TODO: Implement transition
			this.State.Open = this.Open;

			await Task.CompletedTask;
		}

		public async Task HandleToggle(MouseEventArgs args) {
			if (this.Open) {
				await this.HandleClose(args);
			} else {
				await this.HandleOpen(args);
			}
		}
	}
}
