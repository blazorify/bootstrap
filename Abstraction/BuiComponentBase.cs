using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Blazorify.Bootstrap.Utilities;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public abstract class BuiComponentBase : ComponentBase {
		protected CssClass ClassList = new();

		[Parameter]
		public String? ID { get; set; }

		[Parameter]
		[SuppressMessage("Usage", "BL0007:Component parameters should be auto properties")]
		public String? Class {
			get => this.ClassList;
			set => this.ClassList = new CssClass(value);
		}

		[Parameter]
		public String? Style { get; set; }

		[Parameter]
		[BindClass("rounded-pill", true)]
		public Boolean Pill { get; set; } = false;

		[Parameter]
		public Boolean AutoFocus { get; set; } = false;

		[Parameter]
		public virtual String? Tag { get; set; }

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			await this.ApplyBindClassAttributes();
		}

		protected async Task ApplyBindClassAttributes() {
			var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (var property in properties) {
				var attributes = property.GetCustomAttributes(typeof(BindClassAttribute), false).Cast<BindClassAttribute>();

				foreach (var attribute in attributes) {
					var value = property.GetValue(this);
					var method = typeof(BindClassAttribute).GetMethod(nameof(BindClassAttribute.Apply))?.MakeGenericMethod(property.PropertyType);

					method?.Invoke(attribute, [this.ClassList, value]);
				}
			}

			await this.OnBindClassAppliedAsync();

			await this.InvokeAsync(StateHasChanged);
		}

		protected virtual async Task OnBindClassAppliedAsync() {
			await Task.CompletedTask;
		}
	}
}
