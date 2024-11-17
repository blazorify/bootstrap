using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public abstract class BuiMaskedInputComponentBase<TValue> : BuiInputComponentBase<TValue> {
		[Parameter]
		public virtual String Mask { get; set; } = String.Empty;

		[Parameter]
		public String MaskedValue { get; set; } = String.Empty;

		[Parameter]
		public EventCallback<String> MaskedValueChanged { get; set; }

		[Parameter]
		public Boolean AutoUnmask { get; set; } = false;

		[Parameter]
		public Boolean ConformToMask { get; set; } = true;

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			await this.ApplyMask($"{this.Value}");
		}

		protected override async Task HandleChange(ChangeEventArgs args) {
			if (this.TryParseValueFromString(args.Value, out var value, out var error)) {
				this.Value = value;

				await this.ValueChanged.InvokeAsync(value);
				await this.OnChange.InvokeAsync(value);
			}
		}

		protected override async Task HandleInput(ChangeEventArgs args) {
			await this.ApplyMask($"{args.Value}");
			await this.RemoveMask(this.MaskedValue);
		}

		protected virtual async Task ApplyMask(Object? input) {
			var inputString = $"{input}";

			var result = new List<Char>();
			var inputIndex = 0;

			foreach (var maskChar in this.Mask) {
				if (inputIndex >= inputString.Length) {
					// Input exhausted
					break;
				}

				var inputChar = inputString[inputIndex];

				switch (maskChar) {
					// Required digit
					case '0':
						if (Char.IsDigit(inputChar)) {
							result.Add(inputChar);
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;

					case '9': // Optional digit or space
					case '#': // Optional digit, space, or sign
						if (Char.IsDigit(inputChar)) {
							result.Add(inputChar);
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;

					case 'L': // Required letter
						if (Char.IsLetter(inputChar)) {
							result.Add(inputChar);
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;

					case '?': // Optional letter
						if (Char.IsLetter(inputChar)) {
							result.Add(inputChar);
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;

					case 'A': // Required alphanumeric
						if (Char.IsLetterOrDigit(inputChar)) {
							result.Add(inputChar);
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;

					case 'a': // Optional alphanumeric
						if (Char.IsLetterOrDigit(inputChar)) {
							result.Add(inputChar);
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;

					default: // Literal characters
						result.Add(maskChar);
						if (maskChar == inputChar) {
							inputIndex++;
						} else if (this.ConformToMask) {
							// Ignore invalid input
							continue;
						}
						break;
				}
			}

			var maskedValue = new String(result.ToArray());

			if (this.MaskedValue != maskedValue) {
				this.MaskedValue = maskedValue;

				await this.MaskedValueChanged.InvokeAsync(this.MaskedValue);
			}
		}

		protected virtual async Task RemoveMask(object? input) {
			if (input is not string inputString || string.IsNullOrEmpty(Mask)) {
				// No mask or invalid input: Assign input as-is
				Value = input is TValue typedValue ? typedValue : default!;
			} else {
				// Process input against the mask to extract the raw value
				var rawValue = new List<char>();
				var inputIndex = 0;

				foreach (var maskChar in Mask) {
					if (inputIndex >= inputString.Length) {
						break; // Stop processing if input is exhausted
					}

					var inputChar = inputString[inputIndex];

					switch (maskChar) {
						case '0': // Required digit
						case '9': // Optional digit or space
						case '#': // Optional digit, space, or sign
							if (char.IsDigit(inputChar)) {
								rawValue.Add(inputChar);
							}
							inputIndex++;
							break;

						case 'L': // Required letter
						case '?': // Optional letter
							if (char.IsLetter(inputChar)) {
								rawValue.Add(inputChar);
							}
							inputIndex++;
							break;

						case 'A': // Required alphanumeric
						case 'a': // Optional alphanumeric
							if (char.IsLetterOrDigit(inputChar)) {
								rawValue.Add(inputChar);
							}
							inputIndex++;
							break;

						default: // Literal characters, skip them
							if (inputChar == maskChar) {
								inputIndex++; // Advance for matching literals
							}
							break;
					}
				}

				// Attempt to parse the raw value into TValue
				var rawValueString = new string(rawValue.ToArray());
				if (TryParseValueFromString(rawValueString, out TValue parsedValue, out string? validationErrorMessage)) {
					Value = parsedValue;
				} else {
					Value = default!; // Assign default if parsing fails
					Console.WriteLine($"Parsing error: {validationErrorMessage}"); // Optional for debugging
				}
			}

			// Notify consumers of the change
			await ValueChanged.InvokeAsync(Value);
		}
	}
}
