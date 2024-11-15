(elementID, property) => {
	let element = document.getElementById(elementID);

	if (!element) {
		console.error(`Element with ID '${elementID}' was not found`);
		return null;
	}

	return element[property];
}
