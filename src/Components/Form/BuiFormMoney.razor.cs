using System;

namespace Blazorify.Bootstrap {
	public partial class BuiFormMoney<T> : BuiMaskedInputComponentBase<T> {
		public override String Mask { get; set; } = "$9,999.00";
	}
}
