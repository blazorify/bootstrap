using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormOption<T> : BuiInputComponentBase<T> {
		[Parameter]
		[EditorRequired]
		public T Item { get; set; }

		[Parameter]
		[EditorRequired]
		public Func<T, Object?>? OptionValue { get; set; } = null;

		[Parameter]
		[EditorRequired]
		public Func<T, Object?>? OptionLabel { get; set; } = null;

		[Parameter]
		public Boolean Selected { get; set; } = false;

	}
}
