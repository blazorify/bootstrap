using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormTextArea : BuiInputComponentBase<String> {
		[Parameter]
		public Int32 Rows { get; set; } = 3;
	}
}
