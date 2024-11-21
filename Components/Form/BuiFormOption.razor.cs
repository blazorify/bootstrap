using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormOption<T> : BuiInputComponentBase<T> {
		[CascadingParameter]
		public BuiInputComponentBase<T>? Parent { get; set; }

		[Parameter]
		[EditorRequired]
		public T Item { get; set; }

		[Parameter]
		[EditorRequired]
		public Func<T, Object?>? OptionValue { get; set; } = null;

		[Parameter]
		[EditorRequired]
		public Func<T, Object?>? OptionLabel { get; set; } = null;

		[Parameter]
		public Boolean Selected { get; set; } = false;

		protected override async Task OnParametersSetAsync() {
			ArgumentNullException.ThrowIfNull(this.Parent);

			await base.OnParametersSetAsync();
		}

		private async Task HandleSelect(EventArgs args) {
			ArgumentNullException.ThrowIfNull(this.Parent);

			await this.Parent.OnChange.InvokeAsync(this.Item);
		}
	}
}
