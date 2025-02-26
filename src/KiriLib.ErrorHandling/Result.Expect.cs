using System.Diagnostics;

namespace KiriLib.ErrorHandling;

using Variant = Result.Variant;
using InvalidVariantException = Result.InvalidVariantException;

public partial struct Result<T, E>
{
	/// <summary>
	/// Panics if Ex, else returns the contained value. 
	/// This is meant to represent a fatal mistake that the program is dependent on, if you
	/// only wish to output the error, consider using <see cref="PeekResult(Action{T}?, Action{E}?)"> 
	/// instead.
	/// </summary>
	/// <param name="requirement">
	/// Specif the iesrequirement that the result is expected to meet. 
	/// Consider wording your requirement in the form "A must be B".
	/// </param>
	/// <returns>
	/// The contained value
	/// </returns>
	/// <exception cref="ExpectationNotMetException">
	/// Throws if Ex.
	/// This is not
	/// </exception>

	public T Expect(string requirement) => Variant switch { 
		Variant.Ok => _value!, 
		Variant.Ex => throw new ExpectationNotMetException(requirement, _error!),
		_ => throw new InvalidVariantException()
	};

	/// <summary>
	/// Indicating a requirement from <see cref="Expect(string)"> is not satisfied. 
	/// Instead of catching this exception, it is meant to represent fatal mistake that the program
	/// is dependent on. 
	/// </summary>

	public sealed class ExpectationNotMetException : ResultException
	{
		internal ExpectationNotMetException(string requirement, E error) {
			Requirement = requirement;
			(Error, StackTrace) = error is Exception ex
				? (ex.Message, ex.StackTrace)
				: (error!.ToString(), new StackTrace().ToString());
		}

		public string Requirement { get; }
		public string Error { get; }
		public new string StackTrace { get; internal init; }

		public override string ToString() => $"""
			Result expectation not met: \"{Requirement} : {Error}\"
			StackTrace: {StackTrace}
			""";
	}
}