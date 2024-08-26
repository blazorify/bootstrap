using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiContainer : BuiComponentBase {
		[Parameter]
		public Boolean Fluid { get; set; } = false;
	}
}
