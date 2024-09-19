using System;
using Blazorify.Bootstrap.Utilities;

namespace Blazorify.Bootstrap {
	internal class BuiDropdownSharedState : SharedState<BuiDropdownSharedState> {
		private Boolean open;

		internal Boolean Open {
			get {
				return this.open;
			}
			set {
				this.SetProperty(ref this.open, value);
			}
		}
	}
}
