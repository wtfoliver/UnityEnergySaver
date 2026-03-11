[System.Flags]
public enum PowerConstraints
{
    None = 0,
    NotFocused = 1 << 0,
    NotPluggedIn = 1 << 1
}