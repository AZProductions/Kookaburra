namespace KookaburraShell
{
    class Isettingsconf
    {
        public static string Currentdir { get; set; }
        // true = file false = dir
        public static bool Filedir { get; set; }
        public static bool Networkkey { get; set; }
        public static bool Quietmode { get; set; }
        public static string Envloc { get; set; }
        // Env loc of the running file.
        public static int SavedHeight { get; set; }
        public static int SavedWidth { get; set; }
        public static bool EasterEgg1 { get; set; }
    }
}
