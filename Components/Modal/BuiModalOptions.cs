using System;

namespace Blazorify.Bootstrap {

	public class BuiModalOptions {
		public String? ID { get; set; } = $"{Guid.NewGuid()}";

		public String? Class { get; set; }

		public String? Style { get; set; }

		public Boolean Fullscreen { get; set; } = false;
	}

	public class BuiModalOptions<TData> : BuiModalOptions {
		public TData? Data { get; set; }
	}
}
