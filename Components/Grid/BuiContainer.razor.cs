using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiContainer : BuiContentComponentBase {
		[Parameter]
		public Boolean Fluid { get; set; } = false;
	}
}
