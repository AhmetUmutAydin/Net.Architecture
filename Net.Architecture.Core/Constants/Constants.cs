namespace Net.Architecture.Core.Constants
{
    public static class Constants
    {
        public static string WWWRoot = "wwwroot";
        public static string Files= "files";

        public static string[] AcceptedFileExtensions = new string[]
        {
            "jpg", "jpeg", "png", "doc", "docx", "pdf","xlsx","xls"
        };

        public static long MaxFileSize = 1048576;
        public static int MaxFileNameSize = 100;
    }
}
