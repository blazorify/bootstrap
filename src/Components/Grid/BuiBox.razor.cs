using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiBox : BuiContentComponentBase {
		[Parameter]
		[BindClass("flex-column", true)]
		public Boolean Column { get; set; }
	}
}
