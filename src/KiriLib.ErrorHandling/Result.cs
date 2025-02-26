namespace KiriLib.ErrorHandling;

using static Result;

public readonly partial struct Result<T, E> : IEquatable<Result<T, E>>
{
	private Result(T? value, E? error, Variant variant) => 
		(_value, _error, Variant) = (value, error, variant);

	[Obsolete($"You should avoid using the default constructor.",
		error: true)]
	public Result() => throw new InvalidVariantException();

	public static Result<T, E> Ok(T value) => new(value, default, Variant.Ok);

	public static Result<T, E> Ex(E error) => new(default, error, Variant.Ex);

	public static implicit operator Result<T, E>(OkVariant<T> variant) => Ok(variant.Value);
	public static implicit operator Result<T, E>(ExVariant<E> variant) => Ex(variant.Error);

	internal readonly T? _value;
	internal readonly E? _error;

	public Variant Variant { get; }

	public override string ToString() => Variant switch {
		Variant.Ok => $"{nameof(Result)}.{nameof(Variant.Ok)}({_value})",
		Variant.Ex => $"{nameof(Result)}.{nameof(Variant.Ex)}({_error})",
		_ => $"{nameof(Result)}.!InvalidVariant"
	};

	public override int GetHashCode() => Variant switch {
		Variant.Ok => (Variant.Ok, _value).GetHashCode(),
		Variant.Ex => (Variant.Ex, _error).GetHashCode(),
		_ => 0
	};

	public override bool Equals(object obj) => obj is Result<T, E> other && Equals(other);
	public bool Equals(Result<T, E> other) => Variant switch {
		Variant.Ok => other.Variant is Variant.Ok && _value!.Equals(other._value!),
		Variant.Ex => other.Variant is Variant.Ex && _error!.Equals(other._error!),
		_ => false
	};
}

public static partial class Result
{
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
		public T Value { get; }
		internal OkVariant(T Value) => this.Value = Value;

		[Obsolete(
			$"You should avoid using the default constructor. " + 
			$"Please use the factory method {nameof(Result)}.{nameof(Ok)}<{nameof(T)}>({nameof(T)}).", 
			error: true)]
		public OkVariant() => throw new InvalidVariantException();
	}

	public readonly record struct ExVariant<E>
	{
		public E Error { get; }
		internal ExVariant(E Error) => this.Error = Error;

		[Obsolete(
			$"You should avoid using the default constructor. " + 
			$"Please use the factory method {nameof(Result)}.{nameof(Ex)}<{nameof(E)}>({nameof(E)}).", 
			error: true)]
		public ExVariant() => throw new InvalidVariantException();
	}

	/// <summary>
	/// Indicating the default constructor of result types is invoked, 
	/// or a <see cref="Result{T, E}"/> with an invalid <see cref="Variant"/> field is encountered
	/// </summary>
	public sealed class InvalidVariantException() : InvalidOperationException(
		$"encountered a {nameof(Result)} with an invalid variant");
}