using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorify.Bootstrap.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorify.Bootstrap.Services {
	public class BuiModalService {
		public RenderFragment? modalFragment;

		private Dictionary<Guid, RenderFragment?> modalInstances = new();

		public async Task Show<TComponent>(Action<BuiModalOptions<Object?>>? options = null) where TComponent : IComponent {
			await this.Show<TComponent, Object?>(options);
		}

		public async Task Show<TComponent, TData>(Action<BuiModalOptions<TData>>? options = null) where TComponent : IComponent {
			var sequence = 0;
			var modalOptions = new BuiModalOptions<TData>();

			this.modalFragment = new RenderFragment(builder => {
				builder.OpenComponent<BuiModal>(sequence++);

				if (options != null) {
					options.Invoke(modalOptions);
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

			await Task.CompletedTask;
		}
	}
}
