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

		public async Task<BuiModal> Show<TComponent>(Action<BuiModalOptions<TComponent>>? options = null) where TComponent : IComponent {
			await Task.CompletedTask;

			var sequence = 0;
			var modalOptions = new BuiModalOptions<TComponent>();

			if (options != null) {
				options.Invoke(modalOptions);
			}

			var modalInstance = new BuiModal();

			var modalFragment = new RenderFragment<BuiModal>(target => builder => {
				builder.OpenComponent<BuiModal>(sequence++);

				sequence = builder.AddAttributesFromObject(sequence, modalOptions, [nameof(modalOptions.ComponentData)]);

				builder.AddAttribute(sequence++, nameof(BuiModal.Shown), true);
				builder.AddAttribute(sequence++, nameof(BuiModal.ChildContent), (RenderFragment)(contentBuilder => {
					var componentSequence = 0;

					contentBuilder.OpenComponent<TComponent>(componentSequence++);

					foreach (var kvp in modalOptions.ComponentData) {
						contentBuilder.AddAttribute(componentSequence++, kvp.Key, kvp.Value);
					}

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
