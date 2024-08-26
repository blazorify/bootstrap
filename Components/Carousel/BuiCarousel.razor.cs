using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazorify.Bootstrap {
	public partial class BuiCarousel : BuiComponentBase {
		private TimeSpan transitionDelay = TimeSpan.FromSeconds(0.6);

		private Boolean isSliding = false;

		private List<BuiCarouselItem> items = [];

		private Int32 activeItemIndex = 0;
		private Timer autoplayTimer = new Timer();

		[Inject]
		private IJSRuntime? jsRuntime { get; set; }

		[Parameter]
		public Boolean WithControls { get; set; } = true;

		[Parameter]
		public Boolean WithIndicators { get; set; } = true;

		[Parameter]
		public Boolean Fade { get; set; } = true;

		[Parameter]
		public Boolean Wrap { get; set; } = true;

		[Parameter]
		public Boolean Autoplay { get; set; } = true;

		[Parameter]
		public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(5);

		public BuiCarouselItem? ActiveItem {
			get {
				return this.items.ElementAtOrDefault(activeItemIndex);
			}
		}

		protected override async Task OnParametersSetAsync() {
			await base.OnParametersSetAsync();
		}

		protected override async Task OnAfterRenderAsync(Boolean firstRender) {
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender) {
				await this.SetActiveItem(this.activeItemIndex);

				if (this.Autoplay) {
					this.autoplayTimer.Interval = this.Duration.TotalMilliseconds;
					this.autoplayTimer.Elapsed += async (e, a) => await this.OnSlideNext();
					this.autoplayTimer.Start();
				}
			}
		}

		private async Task SetActiveItem(Int32 itemIndex) {
			this.activeItemIndex = itemIndex;

			var activeItem = this.items.ElementAtOrDefault(this.activeItemIndex);

			if (activeItem != null) {
				foreach (var item in this.items) {
					await item.SetActive(false);
				}

				await activeItem.SetActive(true);
			}
		}

		public async Task AddItem(BuiCarouselItem item) {
			this.items.Add(item);

			if (item.Active == true) {
				await this.SetActiveItem(this.items.IndexOf(item));
			}

			await this.InvokeAsync(this.StateHasChanged);
		}

		public async Task RemoveItem(BuiCarouselItem item) {
			this.items.Remove(item);

			await this.InvokeAsync(this.StateHasChanged);
		}

		private async Task OnSlideTo(Int32 targetItemIndex) {
			if (this.isSliding) {
				return;
			}

			if (this.activeItemIndex == targetItemIndex) {
				return;
			}

			this.isSliding = true;

			var isForward = this.activeItemIndex < targetItemIndex;
			var isBackward = this.activeItemIndex > targetItemIndex;

			if (isForward && targetItemIndex > this.items.Count - 1) {
				if (this.Wrap) {
					targetItemIndex = 0;
				} else {
					return;
				}
			}

			if (isBackward && targetItemIndex < 0) {
				if (this.Wrap) {
					targetItemIndex = this.items.Count - 1;
				} else {
					return;
				}
			}

			// TODO: Investigate element reflow, to find out how much of resources it consumes

			var activeItem = this.items.ElementAtOrDefault(this.activeItemIndex);
			var targetItem = this.items.ElementAtOrDefault(targetItemIndex);

			if (activeItem == null || targetItem == null) {
				return;
			}

			await targetItem.AddClass(isForward ? "carousel-item-next" : "carousel-item-prev");
			await this.ReflowSlide(targetItem.elementReference);

			await activeItem.AddClass(isForward ? "carousel-item-start" : "carousel-item-end");
			await this.ReflowSlide(activeItem.elementReference);

			await targetItem.AddClass(isForward ? "carousel-item-start" : "carousel-item-end");

			await Task.Delay(this.transitionDelay);

			await targetItem.RemoveClass(isForward ? "carousel-item-next carousel-item-start" : "carousel-item-prev carousel-item-end");
			await targetItem.SetActive(true);

			await activeItem.SetActive(false);
			await activeItem.RemoveClass(isForward ? "carousel-item-next carousel-item-start" : "carousel-item-prev carousel-item-end");

			this.isSliding = false;
			this.activeItemIndex = targetItemIndex;

			await this.InvokeAsync(this.StateHasChanged);
		}

		private async Task OnSlidePrev() {
			await this.OnSlideTo(this.activeItemIndex - 1);
		}

		private async Task OnSlideNext() {
			await this.OnSlideTo(this.activeItemIndex + 1);
		}

		private async Task ReflowSlide(ElementReference element) {
			if (this.jsRuntime != null) {
				await jsRuntime.InvokeAsync<Object?>("getElementProperty", element, "offsetHeight");
			}
		}

		private void HandleMouseOver() {
			if (this.Autoplay && this.autoplayTimer.Enabled) {
				this.autoplayTimer.Stop();
			}
		}

		private void HandleMouseOut() {
			if (this.Autoplay) {
				this.autoplayTimer.Start();
			}
		}

		public void Dispose() {
			this.autoplayTimer?.Dispose();
		}
	}
}
