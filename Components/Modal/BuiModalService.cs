using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorify.Bootstrap {
	public class BuiModalService {

		private readonly Dictionary<BuiModal, RenderFragment> modalInstances = [];

		internal ObservableCollection<RenderFragment> ModalFragments { get; private set; } = [];

		public async Task<BuiModal> Show<TComponent>(Action<BuiModalOptions<TComponent>>? options = null) where TComponent : IComponent {
			var sequence = 0;
			var modalOptions = new BuiModalOptions<TComponent>();

			options?.Invoke(modalOptions);

			// TaskCompletionSource to capture the rendered instance
			var tcs = new TaskCompletionSource<BuiModal>();

			var modalFragment = new RenderFragment(builder => {
				builder.OpenComponent<BuiModal>(sequence++);

				// Set modal options
				sequence = builder.AddAttributesFromObject(sequence, modalOptions, [nameof(modalOptions.ComponentData)]);

				// Render child component and show the modal
				builder.AddAttribute(sequence++, nameof(BuiModal.Shown), true);
				builder.AddAttribute(sequence++, nameof(BuiModal.ChildContent), (RenderFragment)(contentBuilder => {
					var componentSequence = 0;

					contentBuilder.OpenComponent<TComponent>(componentSequence++);

					foreach (var kvp in modalOptions.ComponentData) {
						contentBuilder.AddAttribute(componentSequence++, kvp.Key, kvp.Value);
					}

					contentBuilder.CloseComponent();
				}));

				// Capture the modal instance
				builder.AddComponentReferenceCapture(sequence++, instance => {
					tcs.TrySetResult((BuiModal)instance);
				});

				builder.CloseComponent();
			});

			// Add the modal fragment to the collection so it gets rendered
			this.ModalFragments.Add(modalFragment);

			// Wait for the rendered instance
			var modalInstance = await tcs.Task;

			// Map rendered modal with the instance so we know which one to close and/or send events to
			this.modalInstances.Add(modalInstance, modalFragment);

			return modalInstance;
		}

		public async Task Close(BuiModal modalInstance) {
			await Task.CompletedTask;

			if (this.modalInstances.TryGetValue(modalInstance, out var modalFragment)) {
				this.ModalFragments.Remove(modalFragment);
			}
		}
	}
}
