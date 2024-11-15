using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;

namespace Blazorify.Bootstrap {
	public partial class BuiFormSelect<T> : BuiInputComponentBase<T> {
		[Parameter]
		[EditorRequired]
		public IEnumerable<T> Items { get; set; } = [];

		[Parameter]
		[EditorRequired]
		public Func<T, Object?>? OptionValue { get; set; } = null;

		[Parameter]
		[EditorRequired]
		public Func<T, Object?>? OptionLabel { get; set; } = null;

		protected override async Task HandleChange(ChangeEventArgs args) {
			ArgumentNullException.ThrowIfNull(this.OptionValue);

			var selectedItem = this.Items.FirstOrDefault(item => {
				var itemValue = this.OptionValue.Invoke(item);

				if (itemValue != null) {
					return $"{itemValue}".Equals(args.Value);
				}

				return false;
			});

			await this.ValueChanged.InvokeAsync(selectedItem);
			await this.OnChange.InvokeAsync(selectedItem);
		}
	}
}
