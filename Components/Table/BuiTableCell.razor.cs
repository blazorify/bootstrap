using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiTableCell : BuiContentComponentBase {
		[CascadingParameter]
		public BuiTableHead? Head { get; set; } = null;

		[CascadingParameter]
		public BuiTableBody? Body { get; set; } = null;
	}
}
