(elementID) => {
	let element = document.getElementById(elementID) ?? document.querySelector(`[_bl_${elementID}]`);

	if (!element) {
		console.error(`Element with ID '${elementID}' was not found`);
		return null;
	}

	let { transitionDuration, transitionDelay } = window.getComputedStyle(element);

	let floatTransitionDuration = parseFloat(transitionDuration.split(',')[0]);
	let floatTransitionDelay = parseFloat(transitionDelay.split(',')[0]);

	if (!floatTransitionDuration && !floatTransitionDelay) {
		return 0;
	}

	return (floatTransitionDuration + floatTransitionDelay) * 1000;
}
