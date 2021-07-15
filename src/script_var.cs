namespace KookaburraShell
{
    class script_var
    {
        public static string[] Intname { get; set; }
        public static int Intnamecount { get; set; }
        public static string Intname2 { get; set; }
        public static string Intname1 { get; set; }
        public static int Int2 { get; set; }
        public static int Int1 { get; set; }
        public static int Intcount { get; set; }
        // 1 = ignore 2 = execute 3 = remove tab and execute
        public static int Ifcount { get; set; }
        public static bool Tabbedresult { get; set; }
        public static string Tabbedstinrg { get; set; }
    }
    class Packages
    {
        public static bool FileIO { get; set; }
        public static bool Net { get; set; }
    }
}
