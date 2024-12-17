using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiLink : BuiButton {
		[Inject]
		private NavigationManager? navigationManager { get; set; }

		[Parameter]
		public String? Href { get; set; }

		protected override async Task OnParametersSetAsync() {
			ArgumentNullException.ThrowIfNull(this.navigationManager);

			await base.OnParametersSetAsync();

			if (!String.IsNullOrEmpty(this.Href)) {
				var currentPath = this.navigationManager.ToAbsoluteUri(this.navigationManager.Uri).AbsolutePath;
				var targetPath = this.navigationManager.ToAbsoluteUri(this.Href).AbsolutePath;

				if (currentPath.Equals(targetPath, StringComparison.OrdinalIgnoreCase)) {
					this.Active = true;
				}

				await this.InvokeAsync(StateHasChanged);
			}
		}
	}
}
