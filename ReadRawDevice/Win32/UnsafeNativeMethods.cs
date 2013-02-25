
namespace ReadRawDevice.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Native win32 functions
    /// </summary>
    [System.Security.SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        /// <summary>
        /// Maximum length of path of the volume
        /// </summary>
        internal const int MAX_PATH = 260;

        /// <summary>
        /// Creates a new file, always.
        /// If the specified file exists and is writable, the function overwrites the file, the function succeeds, and last-error code is set to 
        /// ERROR_ALREADY_EXISTS (183).
        /// If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and the last-error code is set to zero.
        /// For more information, see the Remarks section of this topic.
        /// </summary>
        internal const uint CREATE_ALWAYS = 2;

        /// <summary>
        /// Creates a new file, only if it does not already exist.
        /// If the specified file exists, the function fails and the last-error code is set to ERROR_FILE_EXISTS (80).
        /// If the specified file does not exist and is a valid path to a writable location, a new file is created.
        /// </summary>
        internal const uint CREATE_NEW = 1;

        /// <summary>
        /// Opens a file, always.
        /// If the specified file exists, the function succeeds and the last-error code is set to ERROR_ALREADY_EXISTS (183).
        /// If the specified file does not exist and is a valid path to a writable location, the function creates a file and the last-error code is set to zero.
        /// </summary>
        internal const uint OPEN_ALWAYS = 4;

        /// <summary>
        /// Opens a file or device, only if it exists.
        /// If the specified file or device does not exist, the function fails and the last-error code is set to ERROR_FILE_NOT_FOUND (2).
        /// For more information about devices, see the Remarks section.
        /// </summary>
        internal const uint OPEN_EXISTING = 3;

        /// <summary>
        /// Opens a file and truncates it so that its size is zero bytes, only if it exists.
        /// If the specified file does not exist, the function fails and the last-error code is set to ERROR_FILE_NOT_FOUND (2).
        /// The calling process must open the file with the GENERIC_WRITE bit set as part of the dwDesiredAccess parameter.
        /// </summary>
        internal const uint TRUNCATE_EXISTING = 5;

        /// <summary>
        /// Define a value for 'Invalid handle'
        /// </summary>
        internal const int INVALID_HANDLE_VALUE = -1;

        /// <summary>
        /// Retrieves the device type, device number, and, for a partitionable device, the partition number of a device. 
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/bb968800(v=vs.85).aspx</remarks>
        internal const int IOCTL_STORAGE_GET_DEVICE_NUMBER = 0x2D1080;

        /// <summary>
        /// Error: The data area passed to a system call is too small.
        /// </summary>
        internal const int ERROR_INSUFFICIENT_BUFFER = 122;

        /// <summary>
        /// Error: No more items
        /// </summary>
        internal const int ERROR_NO_MORE_ITEMS = (int)259L;

        /// <summary>
        /// The data is invalid.
        /// </summary>
        internal const int ERROR_INVALID_DATA = 13;

        /// <summary>
        /// The error: Sector is not found
        /// </summary>
        internal const int ERROR_SECTOR_NOT_FOUND = 27;

        /// <summary>
        /// Creates or opens a file or I/O device. The most commonly used I/O devices are as follows: file, file stream, directory, physical disk, volume, 
        /// console buffer, tape drive, communications resource, mail slot, and pipe. The function returns a handle that can be used to access the file or device 
        /// for various types of I/O depending on the file or device and the flags and attributes specified.
        /// </summary>
        /// <param name="FileName">The name of the file or device to be created or opened. You may use either forward slashes (/) or backslashes (\) in this name. 
        /// In the ANSI version of this function, the name is limited to MAX_PATH characters. To extend this limit to 32,767 wide characters, call the Unicode 
        /// version of the function and prepend "\\?\" to the path. For more information, see Naming Files, Paths, and Namespaces.</param>
        /// <param name="DesiredAccess">The requested access to the file or device, which can be summarized as read, write, both or neither zero). 
        /// The most commonly used values are GENERIC_READ, GENERIC_WRITE, or both (GENERIC_READ | GENERIC_WRITE). For more information, see Generic Access Rights, 
        /// File Security and Access Rights, File Access Rights Constants, and ACCESS_MASK. 
        /// If this parameter is zero, the application can query certain metadata such as file, directory, or device attributes without accessing that file or 
        /// device, even if GENERIC_READ access would have been denied.
        /// You cannot request an access mode that conflicts with the sharing mode that is specified by the dwShareMode parameter in an open request that 
        /// already has an open handle.</param>
        /// <param name="ShareMode">The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none (refer to the 
        /// following table). Access requests to attributes or extended attributes are not affected by this flag.
        /// If this parameter is zero and CreateFile succeeds, the file or device cannot be shared and cannot be opened again until the handle to the file or 
        /// device is closed. For more information, see the Remarks section.
        /// You cannot request a sharing mode that conflicts with the access mode that is specified in an existing request that has an open handle. 
        /// CreateFile would fail and the GetLastError function would return ERROR_SHARING_VIOLATION.</param>
        /// <param name="lpSecurityAttributes">A pointer to a SECURITY_ATTRIBUTES structure that contains two separate but related data members: an optional 
        /// security descriptor, and a Boolean value that determines whether the returned handle can be inherited by child processes.
        /// This parameter can be NULL.
        /// If this parameter is NULL, the handle returned by CreateFile cannot be inherited by any child processes the application may create and the file or 
        /// device associated with the returned handle gets a default security descriptor.
        /// The lpSecurityDescriptor member of the structure specifies a SECURITY_DESCRIPTOR for a file or device. If this member is NULL, the file or device 
        /// associated with the returned handle is assigned a default security descriptor.
        /// CreateFile ignores the lpSecurityDescriptor member when opening an existing file or device, but continues to use the bInheritHandle member.
        /// The bInheritHandlemember of the structure specifies whether the returned handle can be inherited.</param>
        /// <param name="CreationDisposition">An action to take on a file or device that exists or does not exist.
        /// For devices other than files, this parameter is usually set to OPEN_EXISTING.
        /// For more information, see the Remarks section.</param>
        /// <param name="dwFlagsAndAttributes">
        /// The file or device attributes and flags, FILE_ATTRIBUTE_NORMAL being the most common default value for files.
        /// This parameter can include any combination of the available file attributes (FILE_ATTRIBUTE_*). All other file attributes override 
        /// FILE_ATTRIBUTE_NORMAL.
        /// This parameter can also contain combinations of flags (FILE_FLAG_*) for control of file or device caching behavior, access modes, and other 
        /// special-purpose flags. These combine with any FILE_ATTRIBUTE_* values.
        /// This parameter can also contain Security Quality of Service (SQOS) information by specifying the SECURITY_SQOS_PRESENT flag. Additional 
        /// SQOS-related flags information is presented in the table following the attributes and flags tables.
        /// </param>
        /// <param name="hTemplateFile">A valid handle to a template file with the GENERIC_READ access right. The template file supplies file attributes and 
        /// extended attributes for the file that is being created.
        /// This parameter can be NULL.
        /// When opening an existing file, CreateFile ignores this parameter.
        /// When opening a new encrypted file, the file inherits the discretionary access control list from its parent directory. For additional information, 
        /// see File Encryption.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot. 
        /// If the function fails, the return value is INVALID_HANDLE_VALUE. To get extended error information, call GetLastError.</returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363858(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "CreateFile", CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr CreateFile(string FileName, 
                                                        uint DesiredAccess, 
                                                        uint ShareMode, 
                                                        IntPtr lpSecurityAttributes,
                                                        uint CreationDisposition, 
                                                        uint dwFlagsAndAttributes,
                                                        IntPtr hTemplateFile);

        /// <summary>
        /// Reads data from the specified file or input/output (I/O) device. Reads occur at the position specified by the file pointer if supported by the device.
        /// This function is designed for both synchronous and asynchronous operations. For a similar function designed solely for asynchronous operation, see ReadFileEx.
        /// </summary>
        /// <param name="hFile">A handle to the device (for example, a file, file stream, physical disk, volume, console buffer, tape drive, socket, communications resource, mailslot, or pipe).
        /// The hFile parameter must have been created with read access. For more information, see Generic Access Rights and File Security and Access Rights.
        /// For asynchronous read operations, hFile can be any handle that is opened with the FILE_FLAG_OVERLAPPED flag by the CreateFile function, or a socket handle returned by the socket or accept function.</param>
        /// <param name="lpBuffer">A pointer to the buffer that receives the data read from a file or device.
        /// This buffer must remain valid for the duration of the read operation. The caller must not use this buffer until the read operation is completed.</param>
        /// <param name="nNumberOfBytesToRead">The maximum number of bytes to be read.</param>
        /// <param name="lpNumberOfBytesRead">A pointer to the variable that receives the number of bytes read when using a synchronous hFile parameter. ReadFile sets this value to zero before doing any work or error checking. Use NULL for this parameter if this is an asynchronous operation to avoid potentially erroneous results.
        /// This parameter can be NULL only when the lpOverlapped parameter is not NULL.</param>
        /// <param name="lpOverlapped">A pointer to an OVERLAPPED structure is required if the hFile parameter was opened with FILE_FLAG_OVERLAPPED, otherwise it can be NULL.
        /// If hFile is opened with FILE_FLAG_OVERLAPPED, the lpOverlapped parameter must point to a valid and unique OVERLAPPED structure, otherwise the function can incorrectly report that the read operation is complete.
        /// For an hFile that supports byte offsets, if you use this parameter you must specify a byte offset at which to start reading from the file or device. This offset is specified by setting the Offset and OffsetHigh members of the OVERLAPPED structure. For an hFile that does not support byte offsets, Offset and OffsetHigh are ignored.</param>
        /// <returns>If the function succeeds, the return value is nonzero (TRUE).
        /// If the function fails, or is completing asynchronously, the return value is zero (FALSE). To get extended error information, call the GetLastError function.
        /// Note  The GetLastError code ERROR_IO_PENDING is not a failure; it designates the read operation is pending completion asynchronously. For more information, see Remarks.</returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadFile(IntPtr hFile,
                                            byte[] lpBuffer,
                                            uint nNumberOfBytesToRead,
                                            ref uint lpNumberOfBytesRead,
                                            IntPtr lpOverlapped);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// If the application is running under a debugger, the function will throw an exception if it receives either a handle value that is not valid or a 
        /// pseudo-handle value. This can happen if you close a handle twice, or if you call CloseHandle on a handle returned by the FindFirstFile function 
        /// instead of calling the FindClose function.
        /// </returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/ms724211(v=vs.85).aspx </remarks>
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "CloseHandle")]
        internal static extern int CloseHandle(IntPtr hObject);

        /// <summary>
        /// Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.
        /// </summary>
        /// <param name="hDevice">A handle to the device on which the operation is to be performed. 
        /// The device is typically a volume, directory, file, or stream. To retrieve a device handle, use the CreateFile function. 
        /// For more information, see Remarks.</param>
        /// <param name="dwIoControlCode">The control code for the operation. This value identifies the specific operation to be performed 
        /// and the type of device on which to perform it.
        /// For a list of the control codes, see Remarks. The documentation for each control code provides usage details for the lpInBuffer, 
        /// nInBufferSize, lpOutBuffer, and nOutBufferSize parameters.
        /// </param>
        /// <param name="lpInBuffer">A pointer to the input buffer that contains the data required to perform the operation. The format of this data 
        /// depends on the value of the dwIoControlCode parameter.
        /// This parameter can be NULL if dwIoControlCode specifies an operation that does not require input data.
        /// </param>
        /// <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
        /// <param name="lpOutBuffer">A pointer to the output buffer that is to receive the data returned by the operation. The format of this data depends on 
        /// the value of the dwIoControlCode parameter.
        /// This parameter can be NULL if dwIoControlCode specifies an operation that does not return data.
        /// </param>
        /// <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
        /// <param name="lpBytesReturned">
        /// A pointer to a variable that receives the size of the data stored in the output buffer, in bytes.
        /// If the output buffer is too small to receive any data, the call fails, GetLastError returns ERROR_INSUFFICIENT_BUFFER, and lpBytesReturned is zero.
        /// If the output buffer is too small to hold all of the data but can hold some entries, some drivers will return as much data as fits. In this case, 
        /// the call fails, GetLastError returns ERROR_MORE_DATA, and lpBytesReturned indicates the amount of data received. Your application should call 
        /// DeviceIoControl again with the same operation, specifying a new starting point.
        /// If lpOverlapped is NULL, lpBytesReturned cannot be NULL. Even when an operation returns no output data and lpOutBuffer is NULL, DeviceIoControl 
        /// makes use of lpBytesReturned. After such an operation, the value of lpBytesReturned is meaningless.
        /// If lpOverlapped is not NULL, lpBytesReturned can be NULL. If this parameter is not NULL and the operation returns data, lpBytesReturned is meaningless 
        /// until the overlapped operation has completed. To retrieve the number of bytes returned, call GetOverlappedResult. If hDevice is associated with an 
        /// I/O completion port, you can retrieve the number of bytes returned by calling GetQueuedCompletionStatus.
        /// </param>
        /// <param name="lpOverlapped">A pointer to an OVERLAPPED structure.
        /// If hDevice was opened without specifying FILE_FLAG_OVERLAPPED, lpOverlapped is ignored.
        /// If hDevice was opened with the FILE_FLAG_OVERLAPPED flag, the operation is performed as an overlapped (asynchronous) operation. In this case, 
        /// lpOverlapped must point to a valid OVERLAPPED structure that contains a handle to an event object. Otherwise, the function fails in unpredictable ways.
        /// For overlapped operations, DeviceIoControl returns immediately, and the event object is signaled when the operation has been completed. Otherwise, 
        /// the function does not return until the operation has been completed or an error occurs.
        /// </param>
        /// <returns>If the operation completes successfully, the return value is nonzero.
        /// If the operation fails or is pending, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "DeviceIoControl")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeviceIoControl(IntPtr hDevice, 
                                                    IoControlCode dwIoControlCode,
                                                    IntPtr lpInBuffer, 
                                                    int nInBufferSize,
                                                    IntPtr lpOutBuffer, 
                                                    int nOutBufferSize,
                                                    ref int lpBytesReturned,
                                                    [In] ref NativeOverlapped lpOverlapped);

        /// <summary>
        /// Retrieves the name of a volume on a computer. FindFirstVolume is used to begin scanning the volumes of a computer.
        /// </summary>
        /// <param name="lpszVolumeName">A pointer to a buffer that receives a null-terminated string that specifies a volume GUID path for the first 
        /// volume that is found.</param>
        /// <param name="cchBufferLength">The length of the buffer to receive the volume GUID path, in TCHARs.</param>
        /// <returns>If the function succeeds, the return value is a search handle used in a subsequent call to the FindNextVolume and FindVolumeClose functions. 
        /// If the function fails to find any volumes, the return value is the INVALID_HANDLE_VALUE error code. To get extended error information, call 
        /// GetLastError.
        /// </returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364425(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "FindFirstVolume", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern SafeFileHandle FindFirstVolume(StringBuilder lpszVolumeName, uint cchBufferLength);

        /// <summary>
        /// Continues a volume search started by a call to the FindFirstVolume function. FindNextVolume finds one volume per call.
        /// </summary>
        /// <param name="hFindVolume">The volume search handle returned by a previous call to the FindFirstVolume function.</param>
        /// <param name="lpszVolumeName">A pointer to a string that receives the volume GUID path that is found.</param>
        /// <param name="cchBufferLength">The length of the buffer that receives the volume GUID path, in TCHARs.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. If no matching files can be found, 
        /// the GetLastError function returns the ERROR_NO_MORE_FILES error code. In that case, close the search with the FindVolumeClose function.
        /// </returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364431(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "FindNextVolume", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return:MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindNextVolume(SafeFileHandle hFindVolume, StringBuilder lpszVolumeName, uint cchBufferLength);

        /// <summary>
        /// Closes the specified volume search handle. The FindFirstVolume and FindNextVolume functions use this search handle to locate volumes.
        /// </summary>
        /// <param name="hFindVolume">The volume search handle to be closed. This handle must have been previously opened by the FindFirstVolume function.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364433(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "FindVolumeClose")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindVolumeClose(SafeFileHandle hFindVolume);

        /// <summary>
        /// The GetTokenInformation function retrieves a specified type of information about an access token. The calling process must have appropriate access rights to obtain the information.
        /// To determine if a user is a member of a specific group, use the CheckTokenMembership function. To determine group membership for app container tokens, use the CheckTokenMembershipEx function.
        /// </summary>
        /// <param name="tokenHandle">A handle to an access token from which information is retrieved. If TokenInformationClass specifies TokenSource, the handle must have TOKEN_QUERY_SOURCE access. For all other TokenInformationClass values, the handle must have TOKEN_QUERY access.</param>
        /// <param name="tokenInformationClass">Specifies a value from the TOKEN_INFORMATION_CLASS enumerated type to identify the type of information the function retrieves. Any callers who check the TokenIsAppContainer and have it return 0 should also verify that the caller token is not an identify level impersonation token. If the current token is not an app container but is an identity level token, you should return AccessDenied.</param>
        /// <param name="tokenInformation">A pointer to a buffer the function fills with the requested information. The structure put into this buffer depends upon the type of information specified by the TokenInformationClass parameter.</param>
        /// <param name="tokenInformationLength">Specifies the size, in bytes, of the buffer pointed to by the TokenInformation parameter. If TokenInformation is NULL, this parameter must be zero.</param>
        /// <param name="returnLength">A pointer to a variable that receives the number of bytes needed for the buffer pointed to by the TokenInformation parameter. If this value is larger than the value specified in the TokenInformationLength parameter, the function fails and stores no data in the buffer.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa446671(v=vs.85).aspx </remarks>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "GetTokenInformation")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetTokenInformation(IntPtr tokenHandle, TokenInformationClass tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);

        /// <summary>
        /// Retrieves information about MS-DOS device names. The function can obtain the current mapping for a particular MS-DOS device name. 
        /// The function can also obtain a list of all existing MS-DOS device names.
        /// MS-DOS device names are stored as junctions in the object namespace. The code that converts an MS-DOS path into a corresponding path uses these junctions 
        /// to map MS-DOS devices and drive letters. The QueryDosDevice function enables an application to query the names of the junctions used to implement 
        /// the MS-DOS device namespace as well as the value of each specific junction.
        /// </summary>
        /// <param name="lpDeviceName">An MS-DOS device name string specifying the target of the query. The device name cannot have a trailing backslash; 
        /// for example, use "C:", not "C:\".
        /// This parameter can be NULL. In that case, the QueryDosDevice function will store a list of all existing MS-DOS device names into the buffer pointed to 
        /// by lpTargetPath.</param>
        /// <param name="lpTargetPath">A pointer to a buffer that will receive the result of the query. The function fills this buffer with one or more null-terminated strings. The final null-terminated string is followed by an additional NULL.
        /// If lpDeviceName is non-NULL, the function retrieves information about the particular MS-DOS device specified by lpDeviceName. The first null-terminated 
        /// string stored into the buffer is the current mapping for the device. The other null-terminated strings represent undeleted prior mappings for the device.
        /// If lpDeviceName is NULL, the function retrieves a list of all existing MS-DOS device names. Each null-terminated string stored into the buffer is the name 
        /// of an existing MS-DOS device, for example, \Device\HarddiskVolume1 or \Device\Floppy0.</param>
        /// <param name="ucchMax">The maximum number of TCHARs that can be stored into the buffer pointed to by lpTargetPath.</param>
        /// <returns>
        /// If the function succeeds, the return value is the number of TCHARs stored into the buffer pointed to by lpTargetPath.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// If the buffer is too small, the function fails and the last error code is ERROR_INSUFFICIENT_BUFFER.
        /// </returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365461(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, EntryPoint = "QueryDosDevice")]
        internal static extern uint QueryDosDevice(string lpDeviceName, [Out] StringBuilder lpTargetPath, int ucchMax);

        /// <summary>
        /// Retrieves a list of drive letters and mounted folder paths for the specified volume.
        /// </summary>
        /// <param name="lpszVolumeName">A volume GUID path for the volume. A volume GUID path is of the form "\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\".</param>
        /// <param name="lpszVolumePathNames">A pointer to a buffer that receives the list of drive letters and mounted folder paths. The list is an array of null-terminated strings terminated by an additional NULL character. If the buffer is not large enough to hold the complete list, the buffer holds as much of the list as possible.</param>
        /// <param name="cchBuferLength">The length of the lpszVolumePathNames buffer, in TCHARs, including all NULL characters.</param>
        /// <param name="lpcchReturnLength">If the call is successful, this parameter is the number of TCHARs copied to the lpszVolumePathNames buffer. Otherwise, this parameter is the size of the buffer required to hold the complete list, in TCHARs.</param>
        /// <returns>If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. If the buffer is not large enough to hold the 
        /// complete list, the error code is ERROR_MORE_DATA and the lpcchReturnLength parameter receives the required buffer size.</returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364998(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, EntryPoint = "GetVolumePathNamesForVolumeNameW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetVolumePathNamesForVolumeName(
                                                                    [MarshalAs(UnmanagedType.LPWStr)] string lpszVolumeName,
                                                                    //[MarshalAs(UnmanagedType.LPWStr)] string lpszVolumePathNames,
                                                                    StringBuilder lpszVolumePathNames,
                                                                    uint cchBuferLength,
                                                                    ref UInt32 lpcchReturnLength);

        /// <summary>
        /// Retrieves a volume GUID path for the volume that is associated with the specified volume mount point ( drive letter, volume GUID path, or mounted folder).
        /// </summary>
        /// <param name="lpszVolumeMountPoint">A pointer to a string that contains the path of a mounted folder (for example, "Y:\MountX\") or a drive letter (for example, "X:\"). The string must end with a trailing backslash ('\').</param>
        /// <param name="lpszVolumeName">A pointer to a string that receives the volume GUID path. This path is of the form "\\?\Volume{GUID}\" where GUID is a GUID that identifies the volume. If there is more than one volume GUID path for the volume, only the first one in the mount manager's cache is returned.</param>
        /// <param name="cchBufferLength">The length of the output buffer, in TCHARs. A reasonable size for the buffer to accommodate the largest possible volume GUID path is 50 characters.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364994(v=vs.85).aspx </remarks>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetVolumeNameForVolumeMountPoint(string lpszVolumeMountPoint, 
                                                                    [Out] StringBuilder lpszVolumeName,
                                                                    uint cchBufferLength);

        /// <summary>
        /// Convert parameters to IOCTL code
        /// </summary>
        /// <param name="DeviceType">Type of the device.</param>
        /// <param name="Function">The function.</param>
        /// <param name="Method">The method.</param>
        /// <param name="Access">The access.</param>
        /// <returns>IOCTL id</returns>
        internal static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return ((DeviceType << 16) | (Access << 14) | (Function << 2) | Method);
        }

        /// <summary>
        /// Retrieves information about the file system and volume associated with the specified root directory.
        /// </summary>
        /// <param name="lpRootPathName">A pointer to a string that contains the root directory of the volume to be described.
        /// If this parameter is NULL, the root of the current directory is used. A trailing backslash is required. For example, you specify \\MyServer\MyShare as "\\MyServer\MyShare\", or the C drive as "C:\".</param>
        /// <param name="lpVolumeNameBuffer">A pointer to a buffer that receives the name of a specified volume. The buffer size is specified by the nVolumeNameSize parameter.</param>
        /// <param name="nVolumeNameSize">The length of a volume name buffer, in TCHARs. The maximum buffer size is MAX_PATH+1.
        /// This parameter is ignored if the volume name buffer is not supplied.</param>
        /// <param name="lpVolumeSerialNumber">A pointer to a variable that receives the volume serial number.
        /// This parameter can be NULL if the serial number is not required.
        /// This function returns the volume serial number that the operating system assigns when a hard disk is formatted. To programmatically obtain the hard disk's serial number that the manufacturer assigns, use the Windows Management Instrumentation (WMI) Win32_PhysicalMedia property SerialNumber.</param>
        /// <param name="lpMaximumComponentLength">A pointer to a variable that receives the maximum length, in TCHARs, of a file name component that a specified file system supports.
        /// A file name component is the portion of a file name between backslashes.
        /// The value that is stored in the variable that *lpMaximumComponentLength points to is used to indicate that a specified file system supports long names. For example, for a FAT file system that supports long names, the function stores the value 255, rather than the previous 8.3 indicator. Long names can also be supported on systems that use the NTFS file system.</param>
        /// <param name="lpFileSystemFlags">A pointer to a variable that receives flags associated with the specified file system.
        /// This parameter can be one or more of the following flags. However, FS_FILE_COMPRESSION and FS_VOL_IS_COMPRESSED are mutually exclusive.</param>
        /// <param name="lpFileSystemNameBuffer">A pointer to a buffer that receives the name of the file system, for example, the FAT file system or the NTFS file system. The buffer size is specified by the nFileSystemNameSize parameter.</param>
        /// <param name="nFileSystemNameSize">The length of the file system name buffer, in TCHARs. The maximum buffer size is MAX_PATH+1.
        /// This parameter is ignored if the file system name buffer is not supplied.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true, EntryPoint = "GetVolumeInformation")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetVolumeInformation(string lpRootPathName,
                                                StringBuilder lpVolumeNameBuffer,
                                                uint nVolumeNameSize,
                                                out uint lpVolumeSerialNumber,
                                                out uint lpMaximumComponentLength,
                                                out uint lpFileSystemFlags,
                                                StringBuilder lpFileSystemNameBuffer,
                                                uint nFileSystemNameSize);

        /// <summary>
        /// The SetupDiGetClassDevs function returns a handle to a device information set that contains requested device information elements for a local computer.
        /// </summary>
        /// <param name="ClassGuid">A pointer to the GUID for a device setup class or a device interface class. This pointer is optional and can be NULL. For more information about how to set ClassGuid, see the following Remarks section.</param>
        /// <param name="Enumerator">A pointer to a NULL-terminated string that specifies:
        /// An identifier (ID) of a Plug and Play (PnP) enumerator. This ID can either be the value's globally unique identifier (GUID) or symbolic name. For example, "PCI" can be used to specify the PCI PnP value. Other examples of symbolic names for PnP values include "USB," "PCMCIA," and "SCSI".
        /// A PnP device instance ID. When specifying a PnP device instance ID, DIGCF_DEVICEINTERFACE must be set in the Flags parameter.
        /// This pointer is optional and can be NULL. If an enumeration value is not used to select devices, set Enumerator to NULL</param>
        /// <param name="hwndParent">A handle to the top-level window to be used for a user interface that is associated with installing a device instance in the device information set. This handle is optional and can be NULL.</param>
        /// <param name="Flags">A variable of type DWORD that specifies control options that filter the device information elements that are added to the device information set. This parameter can be a bitwise OR of zero or more of the following flags. For more information about combining these flags, see the following Remarks section.
        /// DIGCF_ALLCLASSES = Return a list of installed devices for all device setup classes or all device interface classes.
        /// DIGCF_DEVICEINTERFACE = Return devices that support device interfaces for the specified device interface classes. This flag must be set in the Flags parameter if the Enumerator parameter specifies a device instance ID.
        /// DIGCF_DEFAULT = Return only the device that is associated with the system default device interface, if one is set, for the specified device interface classes.
        /// DIGCF_PRESENT = Return only devices that are currently present in a system.
        /// DIGCF_PROFILE = Return only devices that are a part of the current hardware profile.</param>
        /// <returns>If the operation succeeds, SetupDiGetClassDevs returns a handle to a device information set that contains all installed devices that matched the supplied parameters. If the operation fails, the function returns INVALID_HANDLE_VALUE. To get extended error information, call GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern SafeFileHandle SetupDiGetClassDevs(ref Guid ClassGuid,
                                                                    IntPtr Enumerator,
                                                                    IntPtr hwndParent,
                                                                    int Flags);

        /// <summary>
        /// The SetupDiGetDeviceInterfaceDetail function returns details about a device interface.
        /// </summary>
        /// <param name="hDevInfo">A pointer to the device information set that contains the interface for which to retrieve details. This handle is typically returned by SetupDiGetClassDevs.</param>
        /// <param name="deviceInterfaceData">A pointer to an SP_DEVICE_INTERFACE_DATA structure that specifies the interface in DeviceInfoSet for which to retrieve details. A pointer of this type is typically returned by SetupDiEnumDeviceInterfaces</param>
        /// <param name="deviceInterfaceDetailData">A pointer to an SP_DEVICE_INTERFACE_DETAIL_DATA structure to receive information about the specified interface. This parameter is optional and can be NULL. This parameter must be NULL if DeviceInterfaceDetailSize is zero. If this parameter is specified, the caller must set DeviceInterfaceDetailData.cbSize to sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA) before calling this function. The cbSize member always contains the size of the fixed part of the data structure, not a size reflecting the variable-length string at the end.</param>
        /// <param name="deviceInterfaceDetailDataSize">The size of the DeviceInterfaceDetailData buffer. The buffer must be at least (offsetof(SP_DEVICE_INTERFACE_DETAIL_DATA, DevicePath) + sizeof(TCHAR)) bytes, to contain the fixed part of the structure and a single NULL to terminate an empty MULTI_SZ string.
        /// This parameter must be zero if DeviceInterfaceDetailData is NULL.</param>
        /// <param name="requiredSize">A pointer to a variable of type DWORD that receives the required size of the DeviceInterfaceDetailData buffer. This size includes the size of the fixed part of the structure plus the number of bytes required for the variable-length device path string. This parameter is optional and can be NULL.</param>
        /// <param name="deviceInfoData">A pointer to a buffer that receives information about the device that supports the requested interface. The caller must set DeviceInfoData.cbSize to sizeof(SP_DEVINFO_DATA). This parameter is optional and can be NULL.</param>
        /// <returns>SetupDiGetDeviceInterfaceDetail returns TRUE if the function completed without error. If the function completed with an error, FALSE is returned and the error code for the failure can be retrieved by calling GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(SafeFileHandle hDevInfo,
                                                                       ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                                                                       ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
                                                                       UInt32 deviceInterfaceDetailDataSize,
                                                                       out UInt32 requiredSize,
                                                                       ref SP_DEVINFO_DATA deviceInfoData);

        /// <summary>
        /// The SetupDiEnumDeviceInterfaces function enumerates the device interfaces that are contained in a device information set
        /// </summary>
        /// <param name="hDevInfo">A pointer to a device information set that contains the device interfaces for which to return information. This handle is typically returned by SetupDiGetClassDevs.</param>
        /// <param name="devInfo">A pointer to an SP_DEVINFO_DATA structure that specifies a device information element in DeviceInfoSet. This parameter is optional and can be NULL. If this parameter is specified, SetupDiEnumDeviceInterfaces constrains the enumeration to the interfaces that are supported by the specified device. If this parameter is NULL, repeated calls to SetupDiEnumDeviceInterfaces return information about the interfaces that are associated with all the device information elements in DeviceInfoSet. This pointer is typically returned by SetupDiEnumDeviceInfo.</param>
        /// <param name="interfaceClassGuid">A pointer to a GUID that specifies the device interface class for the requested interface.</param>
        /// <param name="memberIndex">A zero-based index into the list of interfaces in the device information set. 
        /// The caller should call this function first with MemberIndex set to zero to obtain the first interface. Then, repeatedly increment MemberIndex and retrieve an interface until this function fails and GetLastError returns ERROR_NO_MORE_ITEMS.
        /// If DeviceInfoData specifies a particular device, the MemberIndex is relative to only the interfaces exposed by that device.</param>
        /// <param name="deviceInterfaceData">A pointer to a caller-allocated buffer that contains, on successful return, a completed SP_DEVICE_INTERFACE_DATA structure that identifies an interface that meets the search parameters. The caller must set DeviceInterfaceData.cbSize to sizeof(SP_DEVICE_INTERFACE_DATA) before calling this function.</param>
        /// <returns></returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInterfaces(SafeFileHandle hDevInfo,
                                                                    IntPtr devInfo,
                                                                    ref Guid interfaceClassGuid,
                                                                    UInt32 memberIndex,
                                                                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        /// <summary>
        /// The SetupDiDestroyDeviceInfoList function deletes a device information set and frees all associated memory.
        /// </summary>
        /// <param name="DeviceInfoSet">A handle to the device information set to delete.</param>
        /// <returns>The function returns TRUE if it is successful. Otherwise, it returns FALSE and the logged error can be retrieved with a call to GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiDestroyDeviceInfoList(SafeFileHandle DeviceInfoSet);

        /// <summary>
        /// The SetupDiGetDeviceRegistryProperty function retrieves the specified device property.
        /// This handle is typically returned by the SetupDiGetClassDevs or SetupDiGetClassDevsEx function.
        /// </summary>
        /// <param Name="DeviceInfoSet">Handle to the device information set that contains the interface and its underlying device.</param>
        /// <param Name="DeviceInfoData">Pointer to an SP_DEVINFO_DATA structure that defines the device instance.</param>
        /// <param Name="Property">Device property to be retrieved. SEE MSDN</param>
        /// <param Name="PropertyRegDataType">Pointer to a variable that receives the registry data Type. This parameter can be NULL.</param>
        /// <param Name="PropertyBuffer">Pointer to a buffer that receives the requested device property.</param>
        /// <param Name="PropertyBufferSize">Size of the buffer, in bytes.</param>
        /// <param Name="RequiredSize">Pointer to a variable that receives the required buffer size, in bytes. This parameter can be NULL.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(SafeFileHandle DeviceInfoSet,
                                                                    ref SP_DEVINFO_DATA DeviceInfoData,
                                                                    uint Property,
                                                                    out UInt32 PropertyRegDataType,
                                                                    IntPtr PropertyBuffer,
                                                                    uint PropertyBufferSize,
                                                                    out UInt32 RequiredSize);
    }
}
