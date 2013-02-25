
namespace ReadRawDevice.Win32
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Generic wrapper for SetupDiGetDeviceRegistryProperty  function
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff551967(v=vs.85).aspx </remarks>
    internal class SetupDiGetDeviceProperty
    {
        /// <summary>
        /// Gets the property for SetupDiXXX function
        /// </summary>
        /// <param name="deviceHandle">The device handle.</param>
        /// <param name="deviceInformationData">The device information data.</param>
        /// <param name="property">The property.</param>
        /// <returns>String representation of the property</returns>
        internal static string GetProperty(IntPtr deviceHandle, SP_DEVINFO_DATA deviceInformationData, SetupDiGetDeviceRegistryPropertyEnum property)
        {
            bool functionAnswer = false;
            UInt32 propertyRegDataType = 0;
            IntPtr propertyBuffer = IntPtr.Zero;
            uint requireSize = 0;
            uint propertyMaximumSize = 1024;
            string actualData = string.Empty;

            try
            {
                propertyBuffer = Marshal.AllocHGlobal((int)propertyMaximumSize);

                functionAnswer = UnsafeNativeMethods.SetupDiGetDeviceRegistryProperty(deviceHandle,
                                                                                        ref deviceInformationData,
                                                                                        (uint)property,
                                                                                        out propertyRegDataType,
                                                                                        propertyBuffer,
                                                                                        propertyMaximumSize,
                                                                                        out requireSize);

                if (functionAnswer == false)
                {
                    if (Marshal.GetLastWin32Error() == UnsafeNativeMethods.ERROR_INVALID_DATA)
                    {
                        Marshal.ThrowExceptionForHR(UnsafeNativeMethods.ERROR_INVALID_DATA);
                    }
                }

                // We have the answer
                actualData = Marshal.PtrToStringAuto(propertyBuffer);
                return actualData;
            }
            catch (Exception exp_gen)
            {
                throw new Win32Exception("Could not receive property", exp_gen);
            }
            finally
            {
                if (propertyBuffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(propertyBuffer);
            }
        }
    }
}
