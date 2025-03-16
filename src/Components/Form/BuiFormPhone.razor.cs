using System;

namespace Blazorify.Bootstrap {
	public partial class BuiFormPhone : BuiMaskedInputComponentBase<String> {
		public override String Mask { get; set; } = "(999) 000-0000";
	}
}
