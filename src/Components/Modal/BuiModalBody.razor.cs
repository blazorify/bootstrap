using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalBody : BuiContentComponentBase {
		[CascadingParameter]
		public BuiModal? Modal { get; set; }
	}
}
