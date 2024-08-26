using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiModalBody : BuiComponentBase {
		[CascadingParameter]
		public BuiModal? Modal { get; set; }
	}
}
