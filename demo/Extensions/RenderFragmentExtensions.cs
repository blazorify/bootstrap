using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Components {
	[SuppressMessage("Usage", "BL0006:Do not use RenderTree types")]
	public static class RenderFragmentExtensions {
		public static string AsDeclared(this RenderFragment fragment) {
			var builder = new RenderTreeBuilder();
			fragment(builder);

			var frames = builder.GetFrames().Array;
			var result = new StringBuilder();
			var elementStack = new Stack<string>();

			for (var i = 0; i < frames.Length; i++) {
				var frame = frames[i];

				switch (frame.FrameType) {
					case RenderTreeFrameType.Component:
						var componentName = frame.ComponentType.Name;
						result.Append($"<{componentName}");
						AppendAttributes(ref i, frames, result);
						result.Append(">");
						elementStack.Push(componentName);
						break;

					case RenderTreeFrameType.Element:
						var elementName = frame.ElementName;
						result.Append($"<{elementName}");
						AppendAttributes(ref i, frames, result);
						result.Append(">");
						elementStack.Push(elementName);
						break;

					case RenderTreeFrameType.Text:
						result.Append(frame.TextContent);
						break;

					case RenderTreeFrameType.Markup:
						result.Append(frame.MarkupContent);
						break;

					case RenderTreeFrameType.Attribute:
						// Attributes are handled in AppendAttributes, so skip this case
						break;

					default:
						break;
				}
			}

			// Ensure all open elements/components are closed
			while (elementStack.Count > 0) {
				var closingTag = elementStack.Pop();
				result.Append($"</{closingTag}>");
			}

			return result.ToString();
		}

		private static void AppendAttributes(ref int index, RenderTreeFrame[] frames, StringBuilder result) {
			while (index + 1 < frames.Length && frames[index + 1].FrameType == RenderTreeFrameType.Attribute) {
				index++;
				var frame = frames[index];

				if (frame.AttributeName == "ChildContent" && frame.AttributeValue is RenderFragment childContent) {
					// Recursively process child content
					result.Append(">");
					result.Append(childContent.AsDeclared());
				} else {
					// Append regular attributes
					result.Append($" {frame.AttributeName}=\"{frame.AttributeValue}\"");
				}
			}
		}
	}
}
