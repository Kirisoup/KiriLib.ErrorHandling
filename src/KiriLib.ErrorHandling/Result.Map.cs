namespace KiriLib.ErrorHandling;

using Variant = Result.Variant;
using InvalidVariantException = Result.InvalidVariantException;

public partial struct Result<T, E>
{
	/// <summary>
	/// Maps a <see cref="Result{T, E}"> to <see cref="Result{U, E}"> by applying a function 
	/// to the contained value, or leaving the error untouched.
	/// </summary>
	
	public Result<U, E> MapResult<U>(Func<T, U> Ok) => Variant switch {
		Variant.Ok => Result.Ok(Ok.Invoke(_value!)),
		Variant.Ex => Result.Ex(_error!),
		_ => throw new InvalidVariantException()
	};

	/// <summary>
	/// Maps a <see cref="Result{T, E}"> to <see cref="Result{T, F}"> by applying a function 
	/// to the contained error, or leaving the value untouched.
	/// </summary>
	
	public Result<T, F> MapResult<F>(Func<E, F> Ex) => Variant switch {
		Variant.Ok => Result.Ok(_value!),
		Variant.Ex => Result.Ex(Ex.Invoke(_error!)),
		_ => throw new InvalidVariantException()
	};

	/// <summary>
	/// Maps a <see cref="Result{T, E}"> to U by applying a function to the contained value,
	/// or returning the provided default value.
	/// </summary>
	/// <param name="Or">The default value</param>

	public U MapResult<U>(Func<T, U> Ok, U Or) => Variant is Variant.Ok ? Ok.Invoke(_value!) : Or;
	
	/// <summary>
	/// Maps a <see cref="Result{T, E}"> to U by applying a function to the contained value, 
	/// or a fallback function to the contained error.
	/// </summary>
	/// <param name="OrElse">The fallback function</param>
	
	public U MapResult<U>(Func<T, U> Ok, Func<E, U> OrElse) => Variant switch {
		Variant.Ok => Ok.Invoke(_value!),
		Variant.Ex => OrElse.Invoke(_error!),
		_ => throw new InvalidVariantException()
	};

	/// <summary>
	/// Maps a <see cref="Result{T, E}"> to <see cref="Result{U, E}"> from applying a function 
	/// with the contained value, or leaving the error untouched.
	/// </summary>
	/// <param name="AndThen">The function the contained value is called with</param>

	public Result<U, E> MapResult<U>(Func<T, Result<U, E>> AndThen) => Variant switch {
		Variant.Ok => AndThen.Invoke(_value!),
		Variant.Ex => Result.Ex(_error!),
		_ => throw new InvalidVariantException()
	};

	/// <summary>
	/// Maps a <see cref="Result{T, E}"> to <see cref="Result{T, F}"> from calling a function 
	/// with the contained error, or leaving the value untouched.
	/// </summary>
	/// <param name="AndThen">The function the contained error is called with</param>

	public Result<T, F> MapResult<F>(Func<E, Result<T, F>> OrElse) => Variant switch {
		Variant.Ok => Result.Ok(_value!),
		Variant.Ex => OrElse.Invoke(_error!),
		_ => throw new InvalidVariantException()
	};

	/// <summary>
	/// Invokes the corresponding action with the contained value or error, and returns self.
	/// </summary>
	/// <param name="Ok">The function the contained error is called with, skipped if null</param>
	/// <param name="Ex">The function the contained error is called with, skipped if null</param>

	public Result<T, E> PeekResult(
		Action<T>? Ok = null,
		Action<E>? Ex = null
	) {
		if (Ok is not null && Variant is Variant.Ok) Ok.Invoke(_value!);
		if (Ex is not null && Variant is Variant.Ex) Ex.Invoke(_error!);
		return this;
	}
}

