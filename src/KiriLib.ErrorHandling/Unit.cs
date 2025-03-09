namespace KiriLib.ErrorHandling;

public readonly record struct Unit : IEquatable<Unit> 
{
	public override int GetHashCode() => 0;
	public bool Equals(Unit other) => true;
	public override string ToString() => "()";
}
