using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorify.Bootstrap {
	public class BuiModalService : ViewModelBase {

		private ObservableCollection<(BuiModal instance, RenderFragment<BuiModal> fragment)> modalInstances = [];
		public ObservableCollection<(BuiModal instance, RenderFragment<BuiModal> fragment)> ModalInstances {
			get {
				return this.modalInstances;
			}
			set {
				this.SetProperty(ref this.modalInstances, value);
			}
		}

		public async Task<BuiModal> Show<TComponent>(Action<BuiModalOptions<Object?>>? options = null) where TComponent : IComponent {
			return await this.Show<TComponent, Object?>(options);
		}

		public async Task<BuiModal> Show<TComponent, TData>(Action<BuiModalOptions<TData>>? options = null) where TComponent : IComponent {
			await Task.CompletedTask;

			var sequence = 0;
			var modalOptions = new BuiModalOptions<TData>();

			if (options != null) {
				options.Invoke(modalOptions);
			}

			var modalInstance = new BuiModal();

			var modalFragment = new RenderFragment<BuiModal>(target => builder => {
				builder.OpenComponent<BuiModal>(sequence++);

				if (modalOptions.Data != null) {
					sequence = builder.AddAttributesFromObject(sequence, modalOptions, [nameof(modalOptions.Data)]);
				}

				builder.AddAttribute(sequence++, nameof(BuiModal.Shown), true);
				builder.AddAttribute(sequence++, nameof(BuiModal.ChildContent), (RenderFragment)(contentBuilder => {
					contentBuilder.OpenComponent<TComponent>(0);
					contentBuilder.AddAttributesFromObject(1, modalOptions.Data);
					contentBuilder.CloseComponent();
				}));

				builder.CloseComponent();
			});

			this.ModalInstances.Add((modalInstance, modalFragment));

			return modalInstance;
		}

		public async Task Close(BuiModal instance) {
			await Task.CompletedTask;

			var tuple = this.modalInstances.FirstOrDefault(m => String.Equals(m.instance.ID, instance.ID));

			if (tuple.instance != null && tuple.fragment != null) {
				this.ModalInstances.Remove(tuple);
			}
		}
	}
}
