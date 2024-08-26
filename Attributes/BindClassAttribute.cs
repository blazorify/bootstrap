using System;
using Blazorify.Bootstrap.Utilities;

namespace Blazorify.Bootstrap.Attributes {
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class BindClassAttribute : Attribute {
		private readonly String className;
		private readonly Object? expectedValue;

		public BindClassAttribute(
			String className,
			Object? expectedValue = null
		) {
			this.className = className;
			this.expectedValue = expectedValue;
		}

		public void Apply<TValue>(CssClass cssClass, TValue propertyValue) {
			if (cssClass == null || propertyValue == null) {
				return;
			}

			if (this.expectedValue == null || this.expectedValue.Equals(propertyValue)) {
				cssClass.Add(this.className);
			} else {
				cssClass.Remove(this.className);
			}
		}
	}
}
