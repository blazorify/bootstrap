using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdownToggle : BuiButton {
		[Parameter]
		public override ButtonType Type { get; set; } = ButtonType.Button;

		[Parameter]
		[BindClass("dropdown-toggle", true)]
		public Boolean Caret { get; set; } = true;

		[Parameter]
		[BindClass("dropdown-toggle-split", true)]
		public Boolean Split { get; set; } = false;

		[CascadingParameter]
		public BuiDropdown? Dropdown { get; set; }

		protected override async Task HandleClick(MouseEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.Dropdown);

			await base.HandleClick(args);

			if (!this.Disabled) {
				await this.OnClick.InvokeAsync(args);
			}

			await this.Dropdown.HandleToggle(args);
		}
	}
}
