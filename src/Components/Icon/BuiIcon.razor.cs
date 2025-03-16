using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiIcon : BuiComponentBase {
		[Parameter]
		[EditorRequired]
		[BindClass("bi-{0}")]
		public String Icon { get; set; } = String.Empty;
	}
}
