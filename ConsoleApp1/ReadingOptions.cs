namespace ConsoleApp1
{
    internal class ReadingOptions
    {
        public ReadingOptions()
        {
        }

        public bool Decrypt { get; internal set; }
        public FileType FileType { get; internal set; }
        public bool? IsAdmin { get; internal set; }
    }
}