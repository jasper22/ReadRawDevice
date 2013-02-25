
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An SP_DEVINFO_DATA structure defines a device instance that is a member of a device information set.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff552344(v=vs.85).aspx </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SP_DEVINFO_DATA
    {
        /// <summary>
        /// The size, in bytes, of the SP_DEVINFO_DATA structure. For more information, see the following Remarks section.
        /// </summary>
        public int cbSize;

        /// <summary>
        /// The GUID of the device's setup class.
        /// </summary>
        public Guid ClassGuid;

        /// <summary>
        /// An opaque handle to the device instance (also known as a handle to the devnode).
        /// Some functions, such as SetupDiXxx functions, take the whole SP_DEVINFO_DATA structure as input to identify a device in a device information set. Other functions, such as CM_Xxx functions like CM_Get_DevNode_Status, take this DevInst handle as input.
        /// </summary>
        public uint DevInst;

        /// <summary>
        /// Reserved. For internal use only.
        /// </summary>
        public IntPtr Reserved;
    }
}
