using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalFooter : BuiContentComponentBase {
		[CascadingParameter]
		public BuiModal? Modal { get; set; }
	}
}
