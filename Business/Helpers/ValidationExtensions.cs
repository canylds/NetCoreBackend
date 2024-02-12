namespace Business.Helpers;

public static class ValidationExtensions
{
    public static bool StartWithA(string arg)
    {
        return arg.StartsWith('A');
    }
}
