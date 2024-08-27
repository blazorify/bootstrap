using System;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiCardImage : BuiImage {
		[Parameter]
		public Placement Placement { get; set; }
	}
}
