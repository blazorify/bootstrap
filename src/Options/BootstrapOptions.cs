using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blazorify.Bootstrap {
	public class BootstrapOptions {
		public readonly Dictionary<String, BootstrapThemeOptions> Themes = new() {
			{
				"bootstrap", new() {
					Assembly = typeof(BootstrapOptions).Assembly,
					Namespace = "Blazorify.Bootstrap.Resources",
				}
			}
		};
	}

	public class BootstrapThemeOptions {
		public required Assembly Assembly { get; set; }

		public required String Namespace { get; set; }
	}
}
