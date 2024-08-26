using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiCardImage : BuiImage {
		[Parameter]
		public String? Variant { get; set; }
	}
}
