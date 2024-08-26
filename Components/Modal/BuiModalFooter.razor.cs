using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalFooter : BuiComponentBase {
		[CascadingParameter]
		public BuiModal? Modal { get; set; }
	}
}
