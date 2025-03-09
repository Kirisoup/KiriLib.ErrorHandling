namespace KiriLib.ErrorHandling;

#pragma warning disable IDE1006 // Naming Styles

public readonly record struct unit : IEquatable<unit>
{
	public override int GetHashCode() => 0;
	public bool Equals(unit other) => true;
	public override string ToString() => "()";
}