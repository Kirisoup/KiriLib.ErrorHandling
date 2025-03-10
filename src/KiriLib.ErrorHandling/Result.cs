namespace KiriLib.ErrorHandling;

using static Result;

public readonly partial struct Result<T, E> : IEquatable<Result<T, E>>
{
	private Result(T? value, E? error, Variant variant) => 
		(_value, _error, _variant) = (value, error, variant);

	[Obsolete($"You should avoid using the default constructor.",
		error: true)]
	public Result() => throw new InvalidVariantException();

	public static Result<T, E> Ok(T value) => new(value, default, Variant.Ok);
	public static Result<T, E> Ex(E error) => new(default, error, Variant.Ex);

	public static implicit operator Result<T, E>(OkVariant<T> variant) => Ok(variant.Value);
	public static implicit operator Result<T, E>(ExVariant<E> variant) => Ex(variant.Error);

	internal readonly T? _value;
	internal readonly E? _error;
	private readonly Variant _variant;
	
	public Variant Variant => (_variant is Variant.Ok or Variant.Ex)
		? _variant
		: throw new InvalidVariantException();
	
	public override string ToString() => Variant switch {
		Variant.Ok => $"{nameof(Result)}.{nameof(Variant.Ok)}({_value})",
		Variant.Ex => $"{nameof(Result)}.{nameof(Variant.Ex)}({_error})"
	};

	public override int GetHashCode() => Variant switch {
		Variant.Ok => (Variant.Ok, _value).GetHashCode(),
		Variant.Ex => (Variant.Ex, _error).GetHashCode()
	};

	public override bool Equals(object obj) => obj is Result<T, E> other && Equals(other);
	public bool Equals(Result<T, E> other) => Variant switch {
		Variant.Ok => other.Variant is Variant.Ok && _value!.Equals(other._value!),
		Variant.Ex => other.Variant is Variant.Ex && _error!.Equals(other._error!)
	};
}

public static partial class Result
{
	public static OkVariant<unit> Ok() => new(new unit());
	public static ExVariant<unit> Ex() => new(new unit());

	public static OkVariant<T> Ok<T>(T value) => new(value);
	public static ExVariant<E> Ex<E>(E error) => new(error);

	public static Result<T, E> Ok<T, E>(T value) => Result<T, E>.Ok(value);
	public static Result<T, E> Ex<T, E>(E error) => Result<T, E>.Ex(error);

	public enum Variant : byte {
		Ok = 1,
		Ex = 2,
	}

	public readonly record struct OkVariant<T> 
	{
		readonly bool _isValid = false;
		readonly T _value;

		public T Value => _isValid ? _value : throw new InvalidVariantException();

		internal OkVariant(T Value) {
			_isValid = true;
			_value = Value;
		}
	}

	public readonly record struct ExVariant<E>
	{
		readonly bool _isValid = false;
		readonly E _error;

		public E Error => _isValid ? _error : throw new InvalidVariantException();

		internal ExVariant(E Error) {
			_isValid = true;
			_error = Error;
		}
	}

	/// <summary>
	/// Indicating the default constructor of result types is invoked, 
	/// or a <see cref="Result{T, E}"/> with an invalid <see cref="Variant"/> field is encountered
	/// </summary>
	public sealed class InvalidVariantException() : InvalidOperationException(
		$"encountered a {nameof(Result)} with an invalid variant");
}