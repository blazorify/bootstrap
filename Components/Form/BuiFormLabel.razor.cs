using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormLabel : BuiContentComponentBase {
		[Parameter]
		public String? For { get; set; }
	}
}
