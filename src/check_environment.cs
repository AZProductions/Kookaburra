namespace KookaburraShell
{
    class check_environment
    {
        public static bool Is64x { get; set; } //Checks if pc is 64 based.
        public static string Username { get; set; } //Checks the current username.
        public static string OSversion { get; set; } //Gets the OSVersion.
        public static bool Debugoff { get; set; } //Disable DebugMode, used when calling 'app.debug-off'.
    }
}
