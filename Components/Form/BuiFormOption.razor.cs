using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormOption<T> : BuiInputComponentBase<T> {
		[CascadingParameter]
		public BuiInputComponentBase<T>? Parent { get; set; }

		[Parameter]
		public Boolean Selected { get; set; } = false;

		[Parameter]
		public RenderFragment? ChildContent { get; set; }
	}
}
