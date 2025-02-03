using System;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Attributes;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiTab : BuiContentComponentBase {
		[CascadingParameter]
		private BuiTabs? Tabs { get; set; }

		[Parameter]
		public String? Header { get; set; }

		[Parameter]
		[BindClass("show active", true)]
		public Boolean Active { get; set; } = false;

		protected override async Task OnInitializedAsync() {
			ArgumentNullException.ThrowIfNull(this.Tabs);

			await base.OnInitializedAsync();

			await this.Tabs.AddTab(this);
		}

		public async Task SetActive(Boolean active) {
			if (this.Active != active) {
				this.Active = active;

				await this.InvokeAsync(this.StateHasChanged);
			}
		}

		public String GetClassList() {
			return this.ClassList.ToString();
		}
	}
}
