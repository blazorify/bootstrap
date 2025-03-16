using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Blazorify.Bootstrap {
	public abstract class BuiItemsComponentBase : BuiContentComponentBase {
		[Parameter]
		public IEnumerable<Object?>? Items { get; set; }
	}
}
