using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorify.Bootstrap.Services {
	public class BuiToastService {
		public RenderFragment? modalFragment;

		public ObservableCollection<BuiToastOptions> Toasts = [];

		public async Task Show(Action<BuiToastOptions>? options = null) {
			await this.Show<Object?>(options);
		}

		public async Task Show<TData>(Action<BuiToastOptions<TData>>? options = null) {
			if (options != null) {
				var toastOptions = new BuiToastOptions<TData>();

				options.Invoke(toastOptions);

				this.Toasts.Add(toastOptions);

				if (toastOptions.AutoClose != null) {
					var timer = new Timer(toastOptions.AutoClose.Value);

					timer.Elapsed += async (s, e) => await this.Hide(toastOptions.ID!);
					timer.Start();
				}
			}

			await Task.CompletedTask;
		}

		public async Task Hide(String toastID) {
			var toast = this.Toasts.FirstOrDefault(m => m.ID == toastID);

			if (toast != null) {
				this.Toasts.Remove(toast);
			}

			await Task.CompletedTask;
		}
	}
}
