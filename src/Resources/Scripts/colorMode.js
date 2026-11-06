// Bootstrap 5.3 color modes (light / dark / auto), modeled on the color mode toggler from the Bootstrap docs.
// Load it as a blocking <script> in <head> so the page is painted in the right mode (no flash of the wrong theme).
// Safe to run more than once: later runs only re-apply the stored mode.
(() => {
	if (window.BlazorifyColorMode) {
		window.BlazorifyColorMode.apply();
		return;
	}

	const storageKey = 'theme';
	const modes = ['light', 'dark', 'auto'];
	const root = document.documentElement;
	const media = window.matchMedia('(prefers-color-scheme: dark)');
	const script = document.currentScript;
	const defaultMode = modes.includes(script?.dataset.defaultMode) ? script.dataset.defaultMode : 'auto';
	const subscribers = [];
	let lastSubscriberID = 0;

	const read = () => {
		try {
			return localStorage.getItem(storageKey);
		} catch {
			return null;
		}
	};

	const write = (mode) => {
		try {
			localStorage.setItem(storageKey, mode);
		} catch {
			// Storage can be unavailable (private mode, blocked cookies): the mode still applies to this page.
		}
	};

	const getMode = () => {
		const stored = read();

		return modes.includes(stored) ? stored : defaultMode;
	};

	const resolve = (mode) => mode === 'auto' ? (media.matches ? 'dark' : 'light') : mode;

	const apply = () => {
		const mode = getMode();

		root.setAttribute('data-bs-theme', resolve(mode));
		root.setAttribute('data-bui-theme-mode', mode);
	};

	// The toggle renders every icon on the server; these rules show the one that matches before Blazor is interactive.
	const style = document.createElement('style');
	style.textContent =
		'.bui-theme-toggle [data-bui-theme-mode-icon],.bui-theme-toggle [data-bui-theme-icon]{display:none}' +
		modes.map(mode => `:root[data-bui-theme-mode="${mode}"] .bui-theme-toggle [data-bui-theme-mode-icon="${mode}"]`).join(',') + ',' +
		['light', 'dark'].map(theme => `:root[data-bs-theme="${theme}"] .bui-theme-toggle [data-bui-theme-icon="${theme}"]`).join(',') +
		'{display:inline-block}';
	document.head.appendChild(style);

	media.addEventListener('change', () => {
		if (getMode() === 'auto') {
			apply();
		}
	});

	// Another tab changed the mode.
	window.addEventListener('storage', (event) => {
		if (event.key !== storageKey && event.key !== null) {
			return;
		}

		apply();

		const mode = getMode();

		for (const subscriber of subscribers) {
			subscriber.reference.invokeMethodAsync(subscriber.callbackName, mode);
		}
	});

	window.BlazorifyColorMode = {
		getMode,
		getTheme: () => resolve(getMode()),
		setMode: (mode) => {
			if (!modes.includes(mode)) {
				return;
			}

			write(mode);
			apply();
		},
		apply,
		// Returns an id to pass to unsubscribe.
		subscribe: (reference, callbackName) => {
			const id = ++lastSubscriberID;

			subscribers.push({ id, reference, callbackName });

			return id;
		},
		unsubscribe: (id) => {
			const index = subscribers.findIndex(subscriber => subscriber.id === id);

			if (index >= 0) {
				subscribers.splice(index, 1);
			}
		},
	};

	apply();
})();
