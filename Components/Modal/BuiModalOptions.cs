using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Blazorify.Bootstrap {

	public class BuiModalOptions<TComponent> where TComponent : IComponent {
		public String? ID { get; set; } = $"{Guid.NewGuid()}";

		public String? Class { get; set; }

		public String? Style { get; set; }

		public Boolean Fullscreen { get; set; } = false;

		public Boolean Scrollable { get; set; } = false;

		public Boolean Centered { get; set; } = false;

		public Size Size { get; set; } = Size.Normal;

		internal readonly Dictionary<String, Object?> ComponentData = [];

		public void Data<TProperty>(Expression<Func<TComponent, TProperty>> propertyExpression, TProperty value) {
			if (propertyExpression.Body is not MemberExpression memberExpression) {
				throw new InvalidOperationException("Invalid property expression.");
			}

			this.ComponentData.Add(memberExpression.Member.Name, value);
		}
	}
}
