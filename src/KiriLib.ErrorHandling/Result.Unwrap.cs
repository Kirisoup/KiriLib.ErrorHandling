using System.Diagnostics;

namespace KiriLib.ErrorHandling;

using Variant = Result.Variant;
using InvalidVariantException = Result.InvalidVariantException;

public partial struct Result<T, E>
{
	public T GetValue(T Or) => Variant is Variant.Ok ? _value! : Or;
	public T GetValue(Func<T> OrElse) => Variant is Variant.Ok ? _value! : OrElse.Invoke();

	[Obsolete(
		$"Calling {nameof(GetValue)} on an ex variant {nameof(Result)} will cause an {nameof(UnwrapException)} to throw. " + 
		$"Consider explicitly handle the result variant, unless you are completely sure the result will be ok.")]
	public T GetValue() => Variant is Variant.Ok 
		? _value! 
		: throw new UnwrapException(_error!);

	[Obsolete(
		$"Calling {nameof(GetError)} on an ok variant {nameof(Result)} will cause an {nameof(UnwrapException)} to throw. " + 
		$"Consider explicitly handle the result variant, unless you are completely sure the result won't be ok.")]
	public E GetError() => Variant is Variant.Ex 
		? _error! 
		: throw new UnwrapException(_value!);

	public abstract class ResultException : Exception
	{
		internal ResultException() {}
		internal ResultException(string message) : base(message) {}
	}
	
	/// <summary>
	/// Indicating that a result is being unwrapped with the wrong assumption. 
	/// Instead of catching this exception, it is meant to warn you to avoid
	/// the use of <see cref="GetValue()"> and <see cref="GetError()">.
	/// </summary>

	public sealed class UnwrapException : ResultException 
	{
		internal UnwrapException(E error) : base(
			$"Attempted to unwrap value from a result with error {error}") {}

		internal UnwrapException(T value) : base(
			$"Attempted to unwrap error from a result with value {value}") {}
	}
}

