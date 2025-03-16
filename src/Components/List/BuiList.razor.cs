using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiList : BuiItemsComponentBase {
		[Parameter]
		[BindClass("list-group-flush", true)]
		public Boolean Flush { get; set; } = false;

		[Parameter]
		[BindClass("list-group-numbered", true)]
		public Boolean Numbered { get; set; } = false;

		protected override async Task OnBindClassAppliedAsync() {
			await base.OnBindClassAppliedAsync();
		}
	}
}
