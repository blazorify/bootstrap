using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiForm<TModel> : BuiContentComponentBase {
		[Parameter]
		public TModel? Model { get; set; } = default!;

		[Parameter]
		public EventCallback<TModel> OnSubmit { get; set; }

		protected virtual async Task HandleSubmit(EventArgs args) {
			await this.OnSubmit.InvokeAsync(this.Model);
		}

		public async Task UpdateModelValue<TValue>(Expression<Func<TModel, TValue>> propertyExpression, TValue value) {
			if (propertyExpression.Body is MemberExpression memberExpression) {
				var propertyInfo = memberExpression.Member as System.Reflection.PropertyInfo;

				if (propertyInfo != null && propertyInfo.CanWrite) {
					propertyInfo.SetValue(Model, value);

					await this.InvokeAsync(StateHasChanged);
				}
			}
		}
	}
}
