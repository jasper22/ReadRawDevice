
namespace ReadRawDevice.Core
{
    using System;
    using System.ComponentModel;
    using Microsoft.Win32.SafeHandles;
    using ReadRawDevice.Win32;

    /// <summary>
    /// Object hold an Handle to device 
    /// </summary>
    public abstract class DeviceHandle : IDisposable
    {
        private string specialDevicePath;
        private static object lockMe = new object();
        private Nullable<IntPtr> unsafeHandle = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceHandle"/> class.
        /// </summary>
        /// <param name="volumeName">Name of the volume.</param>
        internal DeviceHandle(string volumeName)
        {
            specialDevicePath = volumeName;
        }

        /// <summary>
        /// Gets the device handle.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// Could not get access to file/device:  + device.DevicePath
        /// or
        /// Could not get access to file/device:  + device.DevicePath
        /// </exception>
        internal IntPtr OpenDeviceHandle()
        {
            lock (lockMe)
            {
                //
                // Every call to DeviceIoControl must use new/fresh 'handle' otherwise Exception throws
                if (unsafeHandle != null)
                {
                    // Already open
                    CloseDeviceHandle();
                }
            }

            try
            {
                lock (lockMe)
                {
                    unsafeHandle = null;

                    System.Diagnostics.Trace.WriteLine("[]=== Handle is open for device: " + specialDevicePath);

                    unsafeHandle = UnsafeNativeMethods.CreateFile(specialDevicePath,
                                                        Convert.ToUInt32(FileAccess.GenericRead | FileAccess.GenericWrite),
                                                        Convert.ToUInt32(FileShare.FILE_SHARE_READ | FileShare.FILE_SHARE_WRITE),
                                                        IntPtr.Zero,
                                                        Convert.ToUInt32(CreationDisposition.OpenExisting),
                                                        Convert.ToUInt32(FileAttributes.Normal),
                                                        IntPtr.Zero);
                }
            }
            catch (Exception exp_gen)
            {
                lock (lockMe)
                {
                    if (unsafeHandle != null)
                    {
                        CloseDeviceHandle();
                    }
                }

                throw new Win32Exception("Could not get access to file/device: " + specialDevicePath, exp_gen);
            }

            if (unsafeHandle.Value.ToInt32() == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
            {
                CloseDeviceHandle();
                throw new Win32Exception("Could not get access to file/device: " + specialDevicePath);
            }

            GC.KeepAlive(unsafeHandle);
            return unsafeHandle.Value;
        }

        /// <summary>
        /// Closes the device handle.
        /// </summary>
        internal void CloseDeviceHandle()
        {
            lock (lockMe)
            {
                if (unsafeHandle != null)
                {
                    System.Diagnostics.Trace.WriteLine("CloseDeviceHandle():  Going to close handle for device: " + specialDevicePath);
                    UnsafeNativeMethods.CloseHandle(unsafeHandle.Value);
                }
            }

            unsafeHandle = null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            System.Diagnostics.Trace.WriteLine("Dispose() was called");
            Dispose(true);
            GC.SuppressFinalize(unsafeHandle);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">If <c>true</c> then method has been called directly by user, otherwise if <c>false</c> called by Framework</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources.
                if (unsafeHandle != null)
                {
                    System.Diagnostics.Trace.WriteLine("Dispose(true) was called and going to close handle for device: " + specialDevicePath);
                    UnsafeNativeMethods.CloseHandle(unsafeHandle.Value);
                }

                unsafeHandle = null;
            }
            else
            {
                // Dispose unmanaged resources
            }
        }
    }
}
