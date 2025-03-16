(elementID) => {
	let element = document.getElementById(elementID) ?? document.querySelector(`[_bl_${elementID}]`);

	if (!element) {
		console.error(`Element with ID '${elementID}' was not found`);
		return;
	}

	return element.click();
}
