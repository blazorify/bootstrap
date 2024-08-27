using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiDropdownMenu : BuiItemsComponentBase {
		[Parameter]
		[BindClass("dropdown-menu-end", Placement.End)]
		[BindClass("dropdown-menu-start", Placement.Start)]
		public Placement Placement { get; set; }

		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		[CascadingParameter]
		public BuiDropdown? Dropdown { get; set; }
	}
}
