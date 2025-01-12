using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiNavLink : BuiContentComponentBase {
		[Inject]
		private NavigationManager? navigationManager { get; set; }

		[Parameter]
		[BindClass("active", true)]
		public Boolean Active { get; set; } = false;

		[Parameter]
		[BindClass("disabled", true)]
		public Boolean Disabled { get; set; } = false;

		[Parameter]
		public String? Href { get; set; }

		[Parameter]
		public EventCallback<MouseEventArgs> OnClick { get; set; }

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			ArgumentNullException.ThrowIfNull(this.navigationManager);

			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				this.navigationManager.LocationChanged += async (s, e) => {
					await this.SetActiveState();
				};
			}
		}

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			await this.SetActiveState();
		}

		private async Task SetActiveState() {
			ArgumentNullException.ThrowIfNull(this.navigationManager);

			if (String.IsNullOrEmpty(this.Href)) {
				return;
			}

			var currentPath = this.navigationManager.ToAbsoluteUri(this.navigationManager.Uri).AbsolutePath;
			var targetPath = this.navigationManager.ToAbsoluteUri(this.Href).AbsolutePath;

			this.Active = currentPath.Equals(targetPath, StringComparison.OrdinalIgnoreCase);

			await this.InvokeAsync(StateHasChanged);
		}
	}
}