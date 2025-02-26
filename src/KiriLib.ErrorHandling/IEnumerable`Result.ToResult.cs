namespace KiriLib.ErrorHandling;

public static partial class Result
{
	public static Result<IEnumerable<T>, E> ToResult<T, E>(
		this IEnumerable<Result<T, E>> source
	) {
		foreach (var result in source) if (result.IsEx(out var ex)) return Ex(ex);
		return Ok(EnumerateValues(source));

		static IEnumerable<T> EnumerateValues(IEnumerable<Result<T, E>> source) {
			foreach(var result in source) yield return result._value!;
		}
	}
}