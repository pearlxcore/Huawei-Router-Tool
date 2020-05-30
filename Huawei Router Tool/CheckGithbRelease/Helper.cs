using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdate
{
    #region stolen code
    // https://github.com/octokit/octokit.net/blob/master/Octokit/Helpers/Ensure.cs

    partial class Helper
    {
        public static void ArgumentNotNull([ValidatedNotNull]object value, string name)
        {
            if (value != null) return;

            throw new ArgumentNullException(name);
        }
        public static void ArgumentNotNullOrEmptyString([ValidatedNotNull]string value, string name)
        {
            ArgumentNotNull(value, name);
            if (!string.IsNullOrWhiteSpace(value)) return;

            throw new ArgumentException("String cannot be empty", name);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }

    #endregion

    internal static partial class Helper
    {
        public static SemVersion StripInitialV(string version)
        {
            if (version[0] == 'v')
                version = version.Substring(1);

            SemVersion result = SemVersion.Parse(version);

            return result;
        }
    }
}