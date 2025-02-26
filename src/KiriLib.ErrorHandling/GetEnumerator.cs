using System.Collections;

namespace KiriLib.ErrorHandling;

using Variant = Result.Variant;

public readonly partial struct Result<T, E>
{
	public Enumerator GetEnumerator() => Variant is Variant.Ok ? new(_value!) : new();

	public struct Enumerator : IEnumerator<T>
	{
		IterState _state = IterState.Inaccessible;
		readonly T? _value;

		internal Enumerator(T value) {
			_state = IterState.Moveable;
			_value = value;
		}

		public readonly T Current => _state is IterState.Moved ? _value! : default!;
		readonly object IEnumerator.Current => Current!;

		public bool MoveNext() {
			if (_state is not IterState.Moveable) return false;
			_state = IterState.Moved;
			return true;
		}

		public void Reset() => _state = (_state is IterState.Moved) ? IterState.Moveable : _state;
		public void Dispose() => _state = IterState.Inaccessible;

		private enum IterState : byte {
			Inaccessible = 0,
			Moveable = 1,
			Moved = 2
		}
	}
}