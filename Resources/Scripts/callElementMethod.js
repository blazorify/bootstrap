(target, method, parameters) => {
	console.log(target, method, parameters);

	let segments = method.split('.');

	method = segments.shift();

	if (segments.length == 0) {
		return target[method](parameters);
	}

	return this(target[method], segments.join('.'), parameters);
}
