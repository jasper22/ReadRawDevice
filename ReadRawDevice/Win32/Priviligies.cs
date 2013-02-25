
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    /// <summary>
    /// Manage user secuirty checks on windows platform
    /// </summary>
    internal sealed class Priviligies
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Priviligies"/> class from being created.
        /// </summary>
        private Priviligies()
        {
        }

        /// <summary>
        /// Function will return <c>true</c> if current loggged user is administrator (or in group of administrators)
        /// Special care taken in case of UAC
        /// </summary>
        /// <returns><c>true</c> if user is administrator (or in group of administrators), otherwise <c>false</c></returns>
        /// <remarks>Code: http://www.davidmoore.info/2011/06/20/how-to-check-if-the-current-user-is-an-administrator-even-if-uac-is-on/ </remarks>
        internal static bool IfUserAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                throw new InvalidOperationException("Couldn't get the current user identity");
            }

            var principal = new WindowsPrincipal(identity);

            // Check if this user has the Administrator role. If they do, return immediately.
            // If UAC is on, and the process is not elevated, then this will actually return false.
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                return true;
            }

            // If we're not running in Vista onwards, we don't have to worry about checking for UAC.
            if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 6)
            {
                // Operating system does not support UAC; skipping elevation check.
                return false;
            }

            int tokenInfLength = Marshal.SizeOf(typeof(int));
            IntPtr tokenInformation = Marshal.AllocHGlobal(tokenInfLength);

            try
            {
                var token = identity.Token;
                var result = UnsafeNativeMethods.GetTokenInformation(token, TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, out tokenInfLength);

                if (result == false)
                {
                    var exception = Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                    throw new InvalidOperationException("Couldn't get token information", exception);
                }

                var elevationType = (TokenElevationType)Marshal.ReadInt32(tokenInformation);

                switch (elevationType)
                {
                    case TokenElevationType.TokenElevationTypeDefault:
                        // TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
                        return false;
                    case TokenElevationType.TokenElevationTypeFull:
                        // TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
                        return true;
                    case TokenElevationType.TokenElevationTypeLimited:
                        // TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
                        return true;
                    default:
                        // Unknown token elevation type.
                        return false;
                }
            }
            finally
            {
                if (tokenInformation != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(tokenInformation);
                }
            }
        }

        /// <summary>
        /// GetVolumePathNamesForVolumeName is only available on Windows XP/2003 and above
        /// </summary>
        /// <returns><c>true</c> if available for current system, otherwise <c>false</c></returns>
        internal static bool GetVolumePathNamesForVolumeName_Available
        {
            get
            {
                // GetVolumePathNamesForVolumeName is only available on Windows XP/2003 and above
                int osVersionMajor = Environment.OSVersion.Version.Major;
                int osVersionMinor = Environment.OSVersion.Version.Minor;
                if (osVersionMajor < 5 || (osVersionMajor == 5 && osVersionMinor < 1))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
