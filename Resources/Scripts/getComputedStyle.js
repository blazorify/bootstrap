(element) => {
	if (!element) {
		return 0;
	}

	let { transitionDuration, transitionDelay } = window.getComputedStyle(element);

	let floatTransitionDuration = parseFloat(transitionDuration.split(',')[0]);
	let floatTransitionDelay = parseFloat(transitionDelay.split(',')[0]);

	if (!floatTransitionDuration && !floatTransitionDelay) {
		return 0;
	}

	return (floatTransitionDuration + floatTransitionDelay) * 1000;
}
