using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormSelect<T> : BuiInputComponentBase<T> {
		private Dictionary<Int32, T> itemsMap = [];

		[Parameter]
		public IEnumerable<T> Items { get; set; } = [];

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public Func<T, Object?>? Label { get; set; } = null;

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			if (this.Items != null) {
				this.itemsMap = this.Items.ToDictionary(item => item!.GetHashCode(), item => item);
			}
		}

		protected override async Task HandleChange(ChangeEventArgs args) {
			if (this.ChildContent != null) {
				await this.ValueChanged.InvokeAsync((T?)args.Value);
				await this.OnChange.InvokeAsync((T?)args.Value);

				return;
			}

			if (Int32.TryParse($"{args.Value}", out var key) && this.itemsMap.TryGetValue(key, out var value)) {
				await this.ValueChanged.InvokeAsync(value);
				await this.OnChange.InvokeAsync(value);

				return;
			}
		}
	}
}
