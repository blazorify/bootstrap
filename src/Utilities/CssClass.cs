using System;
using System.Collections.Generic;

namespace Blazorify.Bootstrap.Utilities {
	public class CssClass {
		private List<String> classes;

		public CssClass() {
			this.classes = new();
		}

		public CssClass(String? classString = "") : this() {
			if (String.IsNullOrWhiteSpace(classString) == false) {
				this.classes.AddRange(classString.Split(' ', StringSplitOptions.RemoveEmptyEntries));
			}
		}

		public void Add(String? className) {
			if (String.IsNullOrWhiteSpace(className)) {
				return;
			}

			if (this.Exists(className) == false) {
				this.classes.Add(className);
			}
		}

		public void Remove(String? className) {
			if (String.IsNullOrWhiteSpace(className)) {
				return;
			}

			this.classes.Remove(className);
		}

		public void Toggle(String? className) {
			if (String.IsNullOrWhiteSpace(className)) {
				return;
			}

			if (this.Exists(className)) {
				this.Remove(className);
			} else {
				this.Add(className);
			}
		}

		public Boolean Exists(String? className) {
			if (String.IsNullOrWhiteSpace(className)) {
				return false;
			}

			return this.classes.Contains(className);
		}

		// Implicit conversion from string
		public static implicit operator CssClass(String classString) {
			return new CssClass(classString);
		}

		// Implicit conversion to string
		public static implicit operator String(CssClass cssClass) {
			return cssClass.ToString();
		}

		// ToString override to return the classes as a single string
		public override String ToString() {
			return String.Join(" ", this.classes ?? []);
		}
	}
}
