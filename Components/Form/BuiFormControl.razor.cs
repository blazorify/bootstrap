using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormControl/*<T, TModel>*/ : BuiContentComponentBase {
		//private T value = default!;

		//[CascadingParameter]
		//public BuiForm<TModel?>? Form { get; set; }

		//[Parameter]
		//public String? Name { get; set; } = String.Empty;

		[Parameter]
		public String? Label { get; set; } = String.Empty;

		[Parameter]
		[BindClass("form-floating", true)]
		public Boolean FloatingLabel { get; set; } = false;

		//[Parameter]
		//public Expression<Func<TModel?, T>>? For { get; set; }

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			//var propertyName = this.GetPropertyName();

			//if (String.IsNullOrWhiteSpace(this.Label)) {
			//	this.Label = propertyName;
			//}

			//if (String.IsNullOrWhiteSpace(this.Name)) {
			//	this.Name = propertyName;
			//}

			//if (String.IsNullOrWhiteSpace(this.ID)) {
			//	this.ID = propertyName;
			//}

			//if (this.For != null && this.Form != null) {
			//	this.value = (T)this.For.Compile().Invoke(this.Form.Model);
			//}
		}

		//private String? GetPropertyName() {
		//	if (this.For?.Body is MemberExpression memberExpression) {
		//		return memberExpression.Member.Name;
		//	}

		//	if (this.For?.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression member) {
		//		return member.Member.Name;
		//	}

		//	return null;
		//}

		//private async Task HandleValueChanged(T value) {
		//	if (this.For != null && this.Form != null) {
		//		await this.Form.UpdateModelValue(this.For, value);
		//	}
		//}
	}
}
