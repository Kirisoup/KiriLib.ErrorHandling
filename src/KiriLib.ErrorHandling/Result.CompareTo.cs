namespace KiriLib.ErrorHandling;

public static partial class Result
{
	static Result<int, E> CompareToBase<T, E>(Result<T, E> a, T b,
		Func<T, T, int> compare)
	=> a.IsOk(out var value, out var ex)
		? Ok(compare(value, b))
		: Ex(ex);

	public static Result<int, E> CompareTo<T, E>(this Result<T, E> source, T value)
		where T : IComparable<T> 
	=> CompareToBase(source, value, static (x, y) => x.CompareTo(y));

	public static Result<int, E> CompareTo<T, E>(this Result<T, E> source, T value,
		IComparer<T>? comparer)
	=> CompareToBase(source, value, (comparer ?? Comparer<T>.Default).Compare);

	static Result<int, E> CompareToBase<T, E>(Result<T, E> a, Result<T, E> b,
		Func<T, T, int> compare)
	=> a.Combine(b).IsOk(out var value, out var ex)
		? Ok(compare(value.Item1!, value.Item2!))
		: Ex(ex);

	public static Result<int, E> CompareTo<T, E>(this Result<T, E> source, Result<T, E> other)
		where T : IComparable<T> 
	=> CompareToBase(source, other, static (x, y) => x.CompareTo(y));

	public static Result<int, E> CompareTo<T, E>(this Result<T, E> source, Result<T, E> other, 
		IComparer<T>? comparer)
	=> CompareToBase(source, other, (comparer ?? Comparer<T>.Default).Compare);
}