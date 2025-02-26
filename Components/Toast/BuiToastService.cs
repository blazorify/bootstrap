using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Blazorify.Bootstrap {
	public class BuiToastService {
		internal ObservableCollection<BuiToastOptions> Toasts = [];

		internal TimeSpan? AutoClose = null;

		public async Task Show(Action<BuiToastOptions>? options = null) {
			await this.Show<Object?>(options);
		}

		public async Task Show<TData>(Action<BuiToastOptions<TData>>? options = null) {
			if (options != null) {
				var toastOptions = new BuiToastOptions<TData>();

				options.Invoke(toastOptions);

				this.Toasts.Add(toastOptions);

				var autoClose = toastOptions.AutoClose ?? this.AutoClose;

				if (autoClose != null) {
					var timer = new Timer(autoClose.Value);

					timer.Elapsed += async (s, e) => await this.Hide(toastOptions.ID);
					timer.Start();
				}
			}

			await Task.CompletedTask;
		}

		public async Task Success(String message) {
			await this.Show<Object?>(options => {
				options.Variant = Variant.Success;
				options.Body = message;
			});
		}

		public async Task Success(String title, String message) {
			await this.Show<Object?>(options => {
				options.Variant = Variant.Success;
				options.Title = title;
				options.Body = message;
			});
		}

		public async Task Warning(String message) {
			await this.Show<Object?>(options => {
				options.Variant = Variant.Warning;
				options.Body = message;
			});
		}

		public async Task Warning(String title, String message) {
			await this.Show<Object?>(options => {
				options.Variant = Variant.Warning;
				options.Title = title;
				options.Body = message;
			});
		}

		public async Task Error(String message) {
			await this.Show<Object?>(options => {
				options.Variant = Variant.Danger;
				options.Body = message;
			});
		}

		public async Task Error(String title, String message) {
			await this.Show<Object?>(options => {
				options.Variant = Variant.Danger;
				options.Title = title;
				options.Body = message;
			});
		}

		public async Task Hide(String toastID) {
			await Task.CompletedTask;

			var toast = this.Toasts.FirstOrDefault(m => m.ID == toastID);

			if (toast != null) {
				this.Toasts.Remove(toast);
			}
		}
	}
}
