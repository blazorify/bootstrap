using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap.Components {
	public partial class BuiCarouselItem : ComponentBase, IDisposable {
		private String specialClasses = String.Empty;

		public ElementReference elementReference { get; private set; }

		[Parameter]
		public String Class { get; set; } = String.Empty;

		[Parameter]
		public Boolean Active { get; set; } = false;

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[CascadingParameter]
		public BuiCarousel? Carousel { get; set; }

		[Parameter]
		public Object? Context { get; set; }

		protected override async Task OnInitializedAsync() {
			await base.OnInitializedAsync();

			if (this.Carousel == null) {
				throw new InvalidOperationException($"{nameof(BuiCarouselItem)} must be used within a {nameof(BuiCarousel)}.");
			}

			await this.Carousel.AddItem(this);
		}

		public async Task SetActive(Boolean active) {
			this.Active = active;

			if (active) {
				await this.AddClass("active");
			} else {
				await this.RemoveClass("active");
			}

			await InvokeAsync(StateHasChanged);
		}

		public async Task AddClass(String classNames) {
			foreach (var className in classNames.Split(' ')) {
				if (this.specialClasses.Contains(className) == false) {
					this.specialClasses = $"{this.specialClasses.Trim()} {className}";
				}
			}

			await InvokeAsync(StateHasChanged);
		}

		public async Task RemoveClass(String classNames) {
			// TODO: Cover cases when className is found more than once
			// Example: btn btn-primary; where className = btn
			foreach (var className in classNames.Split(' ')) {
				if (this.specialClasses.Contains(className) == true) {
					this.specialClasses = this.specialClasses.Replace(className, String.Empty).Trim();
				}
			}

			await InvokeAsync(StateHasChanged);
		}

		public void Dispose() {
			this.Carousel?.RemoveItem(this);
		}
	}
}
