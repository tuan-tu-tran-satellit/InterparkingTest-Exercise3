namespace InterParkingTestFileReadinLib
{
    /// <summary>
    /// Defines whether reading a file is allowed or not (and why), in a particular context
    /// </summary>
    public interface ISecurityContext
    {
        bool AllowsToReadFile(string path, out string reasonForDenial);
    }
}