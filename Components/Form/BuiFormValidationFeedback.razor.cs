using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiFormValidationFeedback : BuiContentComponentBase {
		[BindClass("valid-feedback", true)]
		[BindClass("invalid-feedback", false)]
		public Boolean Valid { get; set; } = true;

		[Parameter]
		public Func<Boolean> Validate { get; set; } = () => true;

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();

			this.Valid = Validate.Invoke();

			await this.InvokeAsync(StateHasChanged);
		}
	}
}
