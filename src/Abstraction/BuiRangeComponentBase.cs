using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public abstract class BuiRangeComponentBase : BuiComponentBase {
		[Parameter]
		public Int32? Min { get; set; }

		[Parameter]
		public Int32? Max { get; set; }

		[Parameter]
		public Int32? Value { get; set; }
	}
}
