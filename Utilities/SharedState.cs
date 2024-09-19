using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Blazorify.Bootstrap.Utilities {
	public abstract class SharedState<TState> : INotifyPropertyChanged {
		private readonly Dictionary<String, List<Action<Object?>>> mappings = [];

		public event PropertyChangedEventHandler? PropertyChanged;

		private String GetPropertyName<TProperty>(Expression<Func<TState, TProperty>> expression) {
			if (expression.Body is MemberExpression memberExpression) {
				return memberExpression.Member.Name;
			}

			if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand) {
				return operand.Member.Name;
			}

			throw new ArgumentException("Invalid property expression.");
		}

		public void OnPropertyChanged([CallerMemberName] String? propertyName = null) {
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public Boolean SetProperty<TProperty>(ref TProperty storage, TProperty value, [CallerMemberName] String? propertyName = null) {
			if (Equals(storage, value)) {
				return false;
			}

			storage = value;

			if (this.mappings.ContainsKey(propertyName!)) {
				var actions = this.mappings[propertyName!];

				foreach (var action in actions) {
					action.Invoke(value);
				}
			}

			this.OnPropertyChanged(propertyName);

			return true;
		}

		public void OnChange<TProperty>(Expression<Func<TState, TProperty>> propertyExpression, Action<TProperty> action) {
			var propertyName = this.GetPropertyName(propertyExpression);

			if (!this.mappings.ContainsKey(propertyName)) {
				this.mappings.Add(propertyName, []);
			}

			this.mappings[propertyName].Add(value => action((TProperty)value!));
		}
	}
}
