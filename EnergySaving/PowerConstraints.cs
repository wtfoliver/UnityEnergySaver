[System.Flags]
public enum PowerConstraints
{
    None = 0,
    RequiresFocus = 1 << 0,
    RequiresPluggedIn = 1 << 1
}