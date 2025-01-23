using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {
	public partial class BuiTabs : BuiContentComponentBase {
		private List<BuiTab> tabs = [];
		private Int32 activeTabIndex = 0;

		[Parameter]
		public Boolean WithNav { get; set; } = true;

		[Parameter]
		public Boolean Vertical { get; set; } = false;

		[Parameter]
		[SuppressMessage("Usage", "BL0007:Component parameters should be auto properties")]
		public Int32 ActiveTabIndex {
			get {
				return this.activeTabIndex;
			}
			set {
				Task.Run(async () => {
					await this.SetActiveTab(value);
				});
			}
		}

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				await this.SetActiveTab(this.activeTabIndex);
			}
		}

		private async Task SetActiveTab(Int32 tabIndex) {
			this.activeTabIndex = tabIndex;

			var activeTab = this.tabs.ElementAtOrDefault(this.activeTabIndex);

			if (activeTab != null) {
				foreach (var tab in this.tabs) {
					await tab.SetActive(false);
				}

				await activeTab.SetActive(true);
			}

			await this.InvokeAsync(this.StateHasChanged);
		}

		public async Task AddTab(BuiTab tab) {
			if (this.tabs.Contains(tab)) {
				return;
			}

			this.tabs.Add(tab);

			if (tab.Active == true) {
				await this.SetActiveTab(this.tabs.IndexOf(tab));
			}

			await this.InvokeAsync(this.StateHasChanged);
		}

		public async Task RemoveTab(BuiTab item) {
			this.tabs.Remove(item);

			await this.InvokeAsync(this.StateHasChanged);
		}
	}
}
