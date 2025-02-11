using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiLink : BuiButton {
		[Inject]
		private NavigationManager? navigationManager { get; set; }

		[Parameter]
		public String? Href { get; set; }

		[Parameter]
		public String? Target { get; set; }

		[Parameter]
		public String? Rel { get; set; }

		[Parameter]
		[BindClass("text-decoration-none", TextDecoration.None)]
		[BindClass("text-decoration-underline", TextDecoration.Underline)]
		[BindClass("text-decoration-line-through", TextDecoration.LineThrough)]
		public TextDecoration Decoration { get; set; } = TextDecoration.Unset;

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
