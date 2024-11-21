using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiCol : BuiContentComponentBase {
		[Parameter]
		[BindClass("col-xs-{0}")]
		public String? XS { get; set; }

		[Parameter]
		[BindClass("col-sm-{0}")]
		public String? SM { get; set; }

		[Parameter]
		[BindClass("col-md-{0}")]
		public String? MD { get; set; }

		[Parameter]
		[BindClass("col-lg-{0}")]
		public String? LG { get; set; }

		[Parameter]
		[BindClass("col-xl-{0}")]
		public String? XL { get; set; }

		[Parameter]
		[BindClass("col-xxl-{0}")]
		public String? XXL { get; set; }

		protected override async Task OnBindClassAppliedAsync() {
			await base.OnBindClassAppliedAsync();

			if (new String?[] { this.XS, this.SM, this.MD, this.LG, this.XL, this.XXL }.Any(m => m != null)) {
				this.ClassList.Remove("col");
			}
		}
	}
}
