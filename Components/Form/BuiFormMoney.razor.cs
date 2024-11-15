using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Blazorify.Bootstrap {
	public partial class BuiFormMoney<T> : BuiFormNumeric<T> {
		[Parameter]
		public String Mask { get; set; } = String.Empty;

		private String MaskedValue { get; set; }

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			if (this.Value != null) {
				this.MaskedValue = this.ApplyMask(this.Value);
			}
		}

		protected override async Task HandleInput(ChangeEventArgs args) {
			await Task.CompletedTask;

			if (this.TryParseValueFromString(args.Value, out var result, out var errors)) {
				this.MaskedValue = this.ApplyMask(result);
			}
		}

		protected override async Task HandleChange(ChangeEventArgs args) {
			await Task.CompletedTask;

			if (this.TryParseValueFromString(args.Value, out var result, out var errors)) {
				this.MaskedValue = this.ApplyMask(result);
			}
		}

		public String ApplyMask(T value) {
			return String.Format(this.Mask, value);
		}
	}
}
