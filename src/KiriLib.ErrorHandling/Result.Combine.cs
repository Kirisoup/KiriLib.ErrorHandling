namespace KiriLib.ErrorHandling;

using Variant = Result.Variant;

public readonly partial struct Result<T, E>
{
	public Result<(T, U), E> Combine<U>(
		Result<U, E> other) => 
		(Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Result.Ok((_value!, other._value!)),
			(Variant.Ex, _) => Result.Ex(_error!),
			(_, Variant.Ex) => Result.Ex(other._error!)
		};
}

public static partial class Result
{
	public static Result<(T, U, V), E> Combine<T, U, V, E>(
		this Result<(T, U), E> source,
		Result<V, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T, U, V, W), E> Combine<T, U, V, W, E>(
		this Result<(T, U, V), E> source,
		Result<W, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T, U, V, W, X), E> Combine<T, U, V, W, X, E>(
		this Result<(T, U, V, W), E> source,
		Result<X, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T, U, V, W, X, Y), E> Combine<T, U, V, W, X, Y, E>(
		this Result<(T, U, V, W, X), E> source,
		Result<Y, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				source._value.Item5,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T, U, V, W, X, Y, Z), E> Combine<T, U, V, W, X, Y, Z, E>(
		this Result<(T, U, V, W, X, Y), E> source,
		Result<Z, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				source._value.Item5,
				source._value.Item6,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T1, T2, T3, T4, T5, T6, T7, T8), E> 
	Combine<T1, T2, T3, T4, T5, T6, T7, T8, E>(
		this Result<(T1, T2, T3, T4, T5, T6, T7), E> source,
		Result<T8, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				source._value.Item5,
				source._value.Item6,
				source._value.Item7,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9), E> 
	Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, E>(
		this Result<(T1, T2, T3, T4, T5, T6, T7, T8), E> source,
		Result<T9, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				source._value.Item5,
				source._value.Item6,
				source._value.Item7,
				source._value.Item8,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), E> 
	Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, E>(
		this Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9), E> source,
		Result<T10, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				source._value.Item5,
				source._value.Item6,
				source._value.Item7,
				source._value.Item8,
				source._value.Item9,
				other._value!)),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};

	public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9, (T10, T11)), E> 
	Combine<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, E>(
		this Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), E> source,
		Result<T11, E> other) => 
		(source.Variant, other.Variant) switch {
			(Variant.Ok, Variant.Ok) => Ok((
				source._value.Item1,
				source._value.Item2,
				source._value.Item3,
				source._value.Item4,
				source._value.Item5,
				source._value.Item6,
				source._value.Item7,
				source._value.Item8,
				source._value.Item9,
				(source._value.Item10, other._value!))),
			(Variant.Ex, _) => Ex(source._error!),
			(_, Variant.Ex) => Ex(other._error!)
		};
}
