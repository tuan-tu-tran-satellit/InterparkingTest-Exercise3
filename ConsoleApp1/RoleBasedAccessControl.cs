using InterParkingTestFileReadinLib;

namespace ConsoleApp1
{
    internal class RoleBasedAccessControl : ISecurityContext
    {
        private bool _isAdmin;

        public RoleBasedAccessControl(bool isAdmin)
        {
            this._isAdmin = isAdmin;
        }

        public bool AllowsToReadFile(string path, out string reasonForDenial)
        {
            if(_isAdmin)
            {
                reasonForDenial = null;
                return true;
            }
            else
            {
                bool allowed = path.EndsWith(".txt") || path.EndsWith(".txt.encrypted");
                reasonForDenial = allowed ? null : "Non admin users can only access .txt and .txt.encrypted files";
                return allowed;
            }
        }
    }
}