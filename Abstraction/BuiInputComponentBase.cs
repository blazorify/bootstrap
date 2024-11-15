using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public abstract class BuiInputComponentBase<TValue> : BuiComponentBase {
		[Parameter]
		public String? Name { get; set; } = String.Empty;

		[Parameter]
		public String? Placeholder { get; set; } = String.Empty;

		[Parameter]
		public Boolean Required { get; set; } = false;

		[Parameter]
		public Boolean ReadOnly { get; set; } = false;

		[Parameter]
		public Boolean Disabled { get; set; } = false;

		[Parameter]
		public TValue? Value { get; set; }

		[Parameter]
		public EventCallback<TValue?> ValueChanged { get; set; }

		[Parameter]
		public EventCallback<TValue?> OnChange { get; set; }

		[Parameter]
		public EventCallback<TValue?> OnInput { get; set; }

		protected virtual async Task HandleChange(ChangeEventArgs args) {
			if (this.TryParseValueFromString(args.Value, out var value, out var error)) {
				this.Value = value;

				await this.ValueChanged.InvokeAsync(value);
				await this.OnChange.InvokeAsync(value);
			}
		}

		protected virtual async Task HandleInput(ChangeEventArgs args) {
			if (this.TryParseValueFromString(args.Value, out var value, out var error)) {

				this.Value = value;

				await this.ValueChanged.InvokeAsync(value);
				await this.OnInput.InvokeAsync(value);
			}
		}

		protected Boolean TryParseValueFromString(Object? value, [MaybeNullWhen(false)] out TValue result, [MaybeNullWhen(true)] out String? validationErrorMessage) {
			switch (value) {
				case String stringValue when typeof(TValue) == typeof(String):
					result = (TValue)(Object)stringValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Int32) && Int32.TryParse(stringValue, out var intValue):
					result = (TValue)(Object)intValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Int32?) && Int32.TryParse(stringValue, out var intValue):
					result = (TValue)(Object)intValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Double) && Double.TryParse(stringValue, out var doubleValue):
					result = (TValue)(Object)doubleValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Double?) && Double.TryParse(stringValue, out var doubleValue):
					result = (TValue)(Object)doubleValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Decimal) && Decimal.TryParse(stringValue, out var decimalValue):
					result = (TValue)(Object)decimalValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Decimal?) && Decimal.TryParse(stringValue, out var decimalValue):
					result = (TValue)(Object)decimalValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(DateTime) && DateTime.TryParse(stringValue, out var dateTimeValue):
					result = (TValue)(Object)dateTimeValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(DateTime?) && DateTime.TryParse(stringValue, out var dateTimeValue):
					result = (TValue)(Object)dateTimeValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Boolean) && Boolean.TryParse(stringValue, out var boolValue):
					result = (TValue)(Object)boolValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue) == typeof(Boolean?) && Boolean.TryParse(stringValue, out var boolValue):
					result = (TValue)(Object)boolValue;
					validationErrorMessage = null;
					return true;

				case String stringValue when typeof(TValue).IsEnum && Enum.TryParse(typeof(TValue), stringValue, ignoreCase: true, out var enumValue):
					result = (TValue)enumValue!;
					validationErrorMessage = null;
					return true;

				default:
					result = default!;
					validationErrorMessage = $"The entered value '{value}' is not a valid {typeof(TValue).Name}.";
					return false;
			}
		}
	}
}
