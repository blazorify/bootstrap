using System;

namespace Blazorify.Bootstrap {
		public class BuiToastOptions {
		public String ID { get; set; } = $"{Guid.NewGuid()}";

		public String? Class { get; set; }

		public String? Style { get; set; }

		public String? Body { get; set; }

		public String? Title { get; set; }

		public String? Icon { get; set; }

		public DateTime? Time { get; set; }

		public Variant Variant { get; set; } = Variant.None;

		public TimeSpan? AutoClose { get; set; }
	}

	public class BuiToastOptions<TData> : BuiToastOptions {
		public TData? Data { get; set; }
	}
}
