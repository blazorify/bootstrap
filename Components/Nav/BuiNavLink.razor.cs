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
