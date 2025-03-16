using System;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalDialog : BuiContentComponentBase {
		[Parameter]
		[BindClass("modal-sm", Size.Small)]
		[BindClass("modal-lg", Size.Large)]
		[BindClass("modal-xl", Size.ExtraLarge)]
		public Size Size { get; set; } = Size.Normal;

		[Parameter]
		[BindClass("modal-fullscreen", true)]
		public Boolean Fullscreen { get; set; } = false;

		[Parameter]
		[BindClass("modal-dialog-scrollable", true)]
		public Boolean Scrollable { get; set; } = false;

		[Parameter]
		[BindClass("modal-dialog-centered", true)]
		public Boolean Centered { get; set; } = false;
	}
}
