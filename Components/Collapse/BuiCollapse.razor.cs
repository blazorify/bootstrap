using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiCollapse : BuiContentComponentBase {
		[Parameter]
		[BindClass("show", true)]
		public Boolean Open { get; set; } = false;
	}
}
