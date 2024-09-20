using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Collections.Generic {
	public static class DictionaryExtensions {
		public static Boolean TryGetValue<TKey, TValue>(
			this Dictionary<TKey, TValue> source,
			Func<TValue, Boolean> predicate,
			[NotNullWhen(true)] out TValue? value
		) where TKey : notnull {
			try {
				value = source.Values.First(predicate)!;

				return true;
			} catch {
				value = default;

				return false;
			}
		}
	}
}
