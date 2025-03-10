using System.Diagnostics.CodeAnalysis;

namespace KiriLib.ErrorHandling;

using Variant = Result.Variant;

public partial struct Result<T, E>
{
	public bool IsOk() => Variant is Variant.Ok;
	public bool IsEx() => Variant is Variant.Ex;
	public bool IsOk(Predicate<T> And) => Variant is Variant.Ok && And.Invoke(_value!);
	public bool IsEx(Predicate<E> And) => Variant is Variant.Ex && And.Invoke(_error!);

	/// <param name="value">the result's value, not null when method returns true</param>
	public bool IsOk([NotNullWhen(true)] out T? value) {
		value = Variant is Variant.Ok ? _value! : default;
		return Variant is Variant.Ok;
	}

	/// <param name="error">the result's error, not null when method returns true</param>
	public bool IsEx([NotNullWhen(true)] out E? error) {
		error = Variant is Variant.Ex ? _error! : default;
		return Variant is Variant.Ex;
	}

	/// <param name="value">the result's value, not null when method returns true</param>
	/// <param name="error">the result's error, not null when method returns false</param>
	public bool IsOk(
		[NotNullWhen(true)] out T? value,
		[NotNullWhen(false)] out E? error
	) {
		(value, error) = Variant switch {
			Variant.Ok => (_value, default(E)),
			Variant.Ex => (default(T), _error)
		};
		return Variant is Variant.Ok;
	}
}

