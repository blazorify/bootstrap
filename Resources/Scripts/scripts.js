window.Blazorify = (() => {
	class Blazorify {
		#clickListeners = {};

		constructor() {
			this.#initialize();
		}

		#initialize() {
			document.addEventListener('click', async (event) => {
				await this.#handleClick(event);
			});
		}

		async #handleClick(event) {
			for (let listener of Object.values(this.#clickListeners)) {
				let element = event.target.closest(`[_bl_${listener.elementID}]`);

				if (listener.mode === 'inside' || (listener.mode === 'outside' && !element)) {
					await listener.dotnetReference.invokeMethodAsync(listener.callbackName, event, listener.elementID);
				}
			}
		}

		onClickInside(dotnetReference, callbackName, elementID) {
			if (!elementID) {
				return;
			}

			let element = document.querySelector(`[_bl_${elementID}]`);

			if (!element) {
				return;
			}

			this.#clickListeners[elementID] = {
				dotnetReference: dotnetReference,
				callbackName: callbackName,
				elementID: elementID,
				element: element,
				mode: 'inside'
			};
		}

		offClickInside(elementID) {
			if (!elementID) {
				return;
			}

			let element = document.querySelector(`[_bl_${elementID}]`);

			if (!element) {
				return;
			}

			delete this.#clickListeners[elementID];
		}

		onClickOutside(dotnetReference, callbackName, elementID) {
			if (!elementID) {
				return;
			}

			let element = document.querySelector(`[_bl_${elementID}]`);

			if (!element) {
				return;
			}

			this.#clickListeners[elementID] = {
				dotnetReference: dotnetReference,
				callbackName: callbackName,
				elementID: elementID,
				element: element,
				mode: 'outside'
			};
		}

		offClickOutside(elementID) {
			if (!elementID) {
				return;
			}

			let element = document.querySelector(`[_bl_${elementID}]`);

			if (!element) {
				return;
			}

			delete this.#clickListeners[elementID];
		}
	}

	return new Blazorify();
})();
