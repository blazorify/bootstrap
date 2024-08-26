using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiOffcanvas : BuiComponentBase {
		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;

		[Parameter]
		[BindClass("offcanvas-start", Placement.Start)]
		[BindClass("offcanvas-end", Placement.End)]
		public Placement Placement { get; set; } = Placement.Start;
	}
}
