using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Components.Rendering {
	public static class RenderTreeBuilderExtensions {
		public static Int32 AddAttributesFromObject(
			this RenderTreeBuilder builder,
			Int32 sequence = 0,
			Object? source = null,
			ICollection<String>? except = null
		) {
			if (source != null) {
				var sourceProperties = source.GetType().GetProperties();

				foreach (var sourceProperty in sourceProperties) {
					if (except?.Contains(sourceProperty.Name) == true) {
						continue;
					}

					builder.AddAttribute(sequence++, sourceProperty.Name, sourceProperty.GetValue(source));
				}
			}

			return sequence;
		}
	}
}
