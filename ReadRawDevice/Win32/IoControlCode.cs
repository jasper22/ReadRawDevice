
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// Enumerable values that enter into <c>DeviceIoControl</c> function as <c>dwIoControlCode</c> parameter
    /// </summary>
    /// <remarks>Some of the code was taken from: http://blogs.msdn.com/b/codedebate/archive/2007/12/18/6797175.aspx </remarks>
    [Flags]
    internal enum IoControlCode : uint
    {
        ////
        //// STORAGE

        /// <summary>
        /// Base 'Storage' definition
        /// </summary>
        StorageBase = FileDeviceType.FILE_DEVICE_MASS_STORAGE,

        /// <summary>
        /// Determines whether the media has changed on a removable-media device that the caller has opened for read or write access. If read or write access to the device is not necessary, the caller can improve performance by opening the device with FILE_READ_ATTRIBUTES and issuing anIOCTL_STORAGE_CHECK_VERIFY2 request instead.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560535(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_CHECK_VERIFY = (StorageBase << 16) | (0x0200 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Determines whether the media has changed on a removable-media device - the caller has opened with FILE_READ_ATTRIBUTES. Because no file system is mounted when a device is opened in this way, this request can be processed much more quickly than an IOCTL_STORAGE_CHECK_VERIFY request.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560538(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_CHECK_VERIFY2 = (StorageBase << 16) | (0x0200 << 2) | IoMethod.Buffered | (0 << 14), // System.IO.FileAccess.Any
        
        /// <summary>
        /// Locks the device to prevent removal of the media. If the driver can prevent the media from being removed while the drive is in use, it disables or enables the mechanism that ejects media on a device - the caller has opened for read or write access.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560579(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_MEDIA_REMOVAL = (StorageBase << 16) | (0x0201 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Causes the device to eject the media if the device supports ejection capabilities.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560542(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_EJECT_MEDIA = (StorageBase << 16) | (0x0202 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Causes media to be loaded in a device that the caller has opened for read or write access. If read or write access to the device is not necessary, the caller can improve performance by opening the device with FILE_READ_ATTRIBUTES and issuing an IOCTL_STORAGE_LOAD_MEDIA2 request instead.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560567(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_LOAD_MEDIA = (StorageBase << 16) | (0x0203 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Causes media to be loaded in a device that the caller has opened with FILE_READ_ATTRIBUTES. Because no file system is mounted when a device is opened in this way, this request can be processed much more quickly than an IOCTL_STORAGE_LOAD_MEDIA request.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560570(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_LOAD_MEDIA2 = (StorageBase << 16) | (0x0203 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Claims a device for the exclusive use of the caller on a bus that supports multiple initiators and the concept of reserving a device, such as a SCSI bus.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560597(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_RESERVE = (StorageBase << 16) | (0x0204 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Releases a device previously reserved for the exclusive use of the caller on a bus that supports multiple initiators and the concept of reserving a device, such as a SCSI bus.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560593(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_RELEASE = (StorageBase << 16) | (0x0205 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Determines whether another device that the driver supports has been connected to the I/O bus, either since the system was booted or since 
        /// the driver last processed this request.
        /// This IOCTL is obsolete in the Plug and Play environment. Plug and Play class drivers handle this request by calling IoInvalidateDeviceRelations 
        /// with the device relations type BusRelations. If a new device is found, the class driver's AddDevice routine will be called.
        /// Legacy class drivers can continue to handle this IOCTL without modifications. If a new device is found, the driver sets up any necessary 
        /// system objects and resources to handle I/O requests for its new device. It also initializes the device on receipt of this request 
        /// dynamically, that is, without requiring the machine to be rebooted. Such a driver is assumed to support devices connected on a dynamically 
        /// configurable I/O bus.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560546(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_FIND_NEW_DEVICES = (StorageBase << 16) | (0x0206 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Locks the device to prevent removal of the media. If the driver can prevent the media from being removed while the drive is in use, 
        /// the driver disables or enables the mechanism that ejects media, thereby locking the drive. A caller must open the device with 
        /// FILE_READ_ATTRIBUTES to send this request.
        /// Unlike IOCTL_STORAGE_MEDIA_REMOVAL, the driver tracks IOCTL_STORAGE_EJECTION_CONTROL requests by caller and ignores unlock requests for 
        /// which it has not received a lock request from the same caller, thereby preventing other callers from unlocking the drive.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560540(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_EJECTION_CONTROL = (StorageBase << 16) | (0x0250 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Temporarily enables or disables delivery of the custom PnP events GUID_IO_MEDIA_ARRIVAL and GUID_IO_MEDIA_REMOVAL on a removable-media device. 
        /// This, in turn, enables or disables media change detection (AutoPlay) for the device if the caller has opened the device with 
        /// FILE_READ_ATTRIBUTES access and if the device has AutoPlay enabled in the registry. The caller must not open the device for read or write 
        /// access or the IOCTL operation will fail. This IOCTL has no effect on the AutoPlay setting in the registry.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560577(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_MCN_CONTROL = (StorageBase << 16) | (0x0251 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Returns information about the geometry of floppy drives.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560559(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_GET_MEDIA_TYPES = (StorageBase << 16) | (0x0300 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Returns information about the types of media supported by a device. A storage class driver must handle this IOCTL to control devices to be accessed by the removable storage manager (RSM) either as stand-alone devices or as data transfer elements (drives) in a media library or changer device.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560563(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_GET_MEDIA_TYPES_EX = (StorageBase << 16) | (0x0301 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Resets an I/O bus and, indirectly, each device on the bus. Resetting the bus clears all device reservations and transfer speed settings, which must then be renegotiated, making it a time-consuming operation that should be used very rarely. The caller requires only read access to issue a bus reset.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560600(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_STORAGE_RESET_BUS = (StorageBase << 16) | (0x0400 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// If possible, resets a non-SCSI storage device without affecting other devices on the bus. Device reset for SCSI devices is not supported. The caller requires only read access to issue a device reset and, to comply, the device must be capable of responding to I/O requests. If the device reset succeeds, pending I/O requests are canceled.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560603(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_STORAGE_RESET_DEVICE = (StorageBase << 16) | (0x0401 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Returns a STORAGE_DEVICE_NUMBER structure that contains the FILE_DEVICE_XXX type, device number, and, for a partitionable device, the partition number assigned to a device by the driver when the device is started. This request is usually issued by a fault-tolerant disk driver.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560551(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_GET_DEVICE_NUMBER = (StorageBase << 16) | (0x0420 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Polls for a prediction of device failure. This request works with the IDE disk drives that support self-monitoring analysis and reporting technology (SMART). If the drive is a SCSI drive, the class driver attempts to verify if the SCSI disk supports the equivalent IDE SMART technology by check the inquiry information on the Information Exception Control Page, X3T10/94-190 Rev 4.
        /// If the device supports prediction failure, the disk class driver queries the device for failure prediction status and reports the results. If the disk class driver assigns a nonzero value to the PredictFailure member of STORAGE_PREDICT_FAILURE in the output buffer at Irp->AssociatedIrp.SystemBuffer, the disk has bad sectors and is predicting a failure. The storage stack returns 512 bytes of vendor-specific information about the failure prediction in the VendorSpecific member of STORAGE_PREDICT_FAILURE.
        /// If the PredictFailure member contains a value of zero, the disk is not predicting a failure.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560587(v=vs.85).aspx </remarks>
        IOCTL_STORAGE_PREDICT_FAILURE = (StorageBase << 16) | (0x0440 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Base value for 'volume'
        /// </summary>
        VolumeBase = 0x00000056,

        /// <summary>
        /// Retrieves the physical location of a specified volume on one or more disks.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa365194(v=vs.85).aspx </remarks>
        IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS = (VolumeBase << 16) | (0 << 2) |  IoMethod.Buffered | (0 << 14),

        ////
        //// DISK

        /// <summary>
        /// Base 'Disk' definition
        /// </summary>
        DiskBase = FileDeviceType.FILE_DEVICE_DISK,

        /// <summary>
        /// Returns information about the physical disk's geometry (media type, number of cylinders, tracks per cylinder, sectors per track, and bytes per sector).
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560357(v=vs.85).aspx </remarks>
        IOCTL_DISK_GET_DRIVE_GEOMETRY = (DiskBase << 16) | (0x0000 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Returns information about the physical disk's geometry (media type, number of cylinders, tracks per cylinder, sectors per track, and bytes per sector).
        /// The difference between IOCTL_DISK_GET_DRIVE_GEOMETRY_EX and the older IOCTL_DISK_GET_DRIVE_GEOMETRY request is that 
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX can retrieve information from both Master Boot Record (MBR) and GUID Partition Table (GPT)-type 
        /// partitioned media, whereas IOCTL_DISK_GET_DRIVE_GEOMETRY can only read MBR-style media.
        /// </summary>
        /// <example>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560359(v=vs.85).aspx </example>
        IOCTL_DISK_GET_DRIVE_GEOMETRY_EX = (DiskBase << 16) | (0x0028 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Returns information about the type, size, and nature of a disk partition. (Floppy drivers need not handle this request.)
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560373(v=vs.85).aspx </remarks>
        IOCTL_DISK_GET_PARTITION_INFO = (DiskBase << 16) | (0x0001 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Changes the partition type of the specified disk partition. (Floppy drivers need not handle this request.)
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560413(v=vs.85).aspx </remarks>
        IOCTL_DISK_SET_PARTITION_INFO = (DiskBase << 16) | (0x0002 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Returns information about the number of partitions, disk signature, and features of each partition on a disk. (Floppy drivers need not handle this request.)
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560361(v=vs.85).aspx </remarks>
        IOCTL_DISK_GET_DRIVE_LAYOUT = (DiskBase << 16) | (0x0003 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Contains extended information about a drive's partitions.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/aa364001(v=vs.85).aspx </remarks>
        IOCTL_DISK_GET_DRIVE_LAYOUT_EX = (DiskBase << 16) | (0x0014 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Repartitions a disk as specified. (Floppy drivers need not handle this request.)
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560408(v=vs.85).aspx </remarks>
        IOCTL_DISK_SET_DRIVE_LAYOUT = (DiskBase << 16) | (0x0004 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Performs a logical format of a specified extent on a disk.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560420(v=vs.85).aspx </remarks>
        IOCTL_DISK_VERIFY = (DiskBase << 16) | (0x0005 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Formats the specified set of contiguous tracks on the disk.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff559447(v=vs.85).aspx </remarks>
        IOCTL_DISK_FORMAT_TRACKS = (DiskBase << 16) | (0x0006 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Maps defective blocks to new location on disk. This request instructs the device to reassign the bad block address to a good block from its spare-block pool.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560398(v=vs.85).aspx </remarks>
        IOCTL_DISK_REASSIGN_BLOCKS = (DiskBase << 16) | (0x0007 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Increments a reference counter that enables the collection of disk performance statistics, such as the numbers of bytes read and written since the driver last processed this request, for a corresponding disk monitoring application. In Microsoft Windows 2000 this IOCTL is handled by the filter driver diskperf. In Windows XP and later operating systems, the partition manager handles this request for disks and ftdisk.sys and dmio.sys handle this request for volumes.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560388(v=vs.85).aspx </remarks>
        IOCTL_DISK_PERFORMANCE = (DiskBase << 16) | (0x0008 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Determines whether a disk is writable.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560384(v=vs.85).aspx</remarks>
        IOCTL_DISK_IS_WRITABLE = (DiskBase << 16) | (0x0009 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// 
        /// </summary>
        DiskLogging = (DiskBase << 16) | (0x000a << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Is similar to IOCTL_DISK_FORMAT_TRACKS, except that it allows the caller to specify several more parameters. The additional extended parameters are the format gap length, the number of sectors per track, and an array whose element size is equal to the number of sectors per track. This array represents the track layout.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff559448(v=vs.85).aspx </remarks>
        IOCTL_DISK_FORMAT_TRACKS_EX = (DiskBase << 16) | (0x000b << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Do not use
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363979(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_DISK_HISTOGRAM_STRUCTURE = (DiskBase << 16) | (0x000c << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Do not use
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363979(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_DISK_HISTOGRAM_DATA = (DiskBase << 16) | (0x000d << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Do not use
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363979(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_DISK_HISTOGRAM_RESET = (DiskBase << 16) | (0x000e << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Do not use
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363979(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_DISK_REQUEST_STRUCTURE = (DiskBase << 16) | (0x000f << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Do not use
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363979(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_DISK_REQUEST_DATA = (DiskBase << 16) | (0x0010 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Do not use
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363979(v=vs.85).aspx </remarks>
        [Obsolete]
        IOCTL_DISK_CONTROLLER_NUMBER = (DiskBase << 16) | (0x0011 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Returns version information, a capabilities mask, and a bitmask for the device. This IOCTL must be handled by drivers that support Self-Monitoring Analysis and Reporting Technology (SMART).
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff566202(v=vs.85).aspx</remarks>
        SMART_GET_VERSION = (DiskBase << 16) | (0x0020 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Sends one of the following Self-Monitoring Analysis and Reporting Technology (SMART) commands to the device:
        /// * Enable or disable reporting on the device
        /// * Enable or disable autosaving of attributes
        /// * Save current attributes now
        /// * Execute offline diagnostics
        /// * Get current SMART status
        /// * Write to SMART log
        /// This IOCTL must be handled by drivers that support SMART.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff566206(v=vs.85).aspx </remarks>
        SMART_SEND_DRIVE_COMMAND = (DiskBase << 16) | (0x0021 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Returns the ATA-2 identify data, the Self-Monitoring Analysis and Reporting Technology (SMART) thresholds, or the SMART attributes for the device. This IOCTL must be handled by drivers that support SMART.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff566204(v=vs.85).aspx </remarks>
        SMART_RCV_DRIVE_DATA = (DiskBase << 16) | (0x0022 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Updates device extension with drive size information for current media.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560419(v=vs.85).aspx </remarks>
        IOCTL_DISK_UPDATE_DRIVE_SIZE = (DiskBase << 16) | (0x0032 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Increases the size of an existing partition. It is used in conjunction with IOCTL_DISK_UPDATE_DRIVE_SIZE to extend a disk, so that it will contain a new free space area, and then to extend an existing partition on the disk into the newly attached free space. It takes a DISK_GROW_PARTITION structure as the only parameter. For this operation to work, the space after the specified partition must be free. A partition cannot be extended over another existing partition.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560376(v=vs.85).aspx </remarks>
        IOCTL_DISK_GROW_PARTITION = (DiskBase << 16) | (0x0034 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),

        /// <summary>
        /// Returns disk cache configuration data.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff559451(v=vs.85).aspx </remarks>
        IOCTL_DISK_GET_CACHE_INFORMATION = (DiskBase << 16) | (0x0035 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Sets disk cache configuration data.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560405(v=vs.85).aspx </remarks>
        IOCTL_DISK_SET_CACHE_INFORMATION = (DiskBase << 16) | (0x0036 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),

        /// <summary>
        /// Removes partition information from the disk. If the partition style of the disk is Master Boot Record (MBR), sector 0 of the disk is wiped clean except for the bootstrap code. All signatures, such as the AA55 boot signature and the NTFT disk signature, will be removed. If the partition style of the disk is GUID Partition Table (GPT), the primary partition table header in sector 1 and the backup partition table in the last sector of the disk are wiped clean. This operation can be used to generate so-called "superfloppies" that contain a file system starting at the first sector of the disk rather than in a partition on the disk.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff559439(v=vs.85).aspx </remarks>
        IOCTL_DISK_DELETE_DRIVE_LAYOUT = (DiskBase << 16) | (0x0040 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// 
        /// </summary>
        DiskSenseDevice = (DiskBase << 16) | (0x00f8 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// In Microsoft Windows 2000 and later operating systems, this IOCTL is replaced by IOCTL_STORAGE_CHECK_VERIFY. The only difference between the two IOCTLs is the base value.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff559429(v=vs.85).aspx </remarks>
        IOCTL_DISK_CHECK_VERIFY = (DiskBase << 16) | (0x0200 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// In Microsoft Windows 2000 and later operating systems, this IOCTL is replaced by IOCTL_STORAGE_FIND_NEW_DEVICES. The only difference between the two IOCTLs is the base value.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff559442(v=vs.85).aspx </remarks>
        IOCTL_DISK_FIND_NEW_DEVICES = (DiskBase << 16) | (0x0206 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// In Microsoft Windows 2000 and later operating systems, this IOCTL is replaced by IOCTL_STORAGE_GET_MEDIA_TYPES. The only difference between the two IOCTLs is the base value.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff560371(v=vs.85).aspx </remarks>
        IOCTL_DISK_GET_MEDIA_TYPES = (DiskBase << 16) | (0x0300 << 2) | IoMethod.Buffered | (0 << 14),
        
        ////
        //// CHANGER

        /// <summary>
        /// Define base 'Changer' value
        /// </summary>
        ChangerBase = FileDeviceType.FILE_DEVICE_CHANGER,
        
        /// <summary>
        /// Retrieves the parameters of the specified device.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363394(v=vs.85).aspx</remarks>
        IOCTL_CHANGER_GET_PARAMETERS = (ChangerBase << 16) | (0x0000 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Retrieves the current status of the specified device.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363396(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_GET_STATUS = (ChangerBase << 16) | (0x0001 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Retrieves the product data for the specified device.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363395(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_GET_PRODUCT_DATA = (ChangerBase << 16) | (0x0002 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Sets the state of the device's insert/eject port, door, or keypad.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363401(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_SET_ACCESS = (ChangerBase << 16) | (0x0004 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Retrieves the status of all elements or a specified number of elements of a particular type.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363393(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_GET_ELEMENT_STATUS = (ChangerBase << 16) | (0x0005 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Initializes the status of all elements or the specified elements of a particular type
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363397(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_INITIALIZE_ELEMENT_STATUS = (ChangerBase << 16) | (0x0006 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Sets the changer's robotic transport mechanism to the specified element address. This optimizes moving or exchanging media by positioning the transport beforehand.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363402(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_SET_POSITION = (ChangerBase << 16) | (0x0007 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Moves a piece of media from a source element to one destination, and the piece of media originally in the first destination to a second destination.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363392(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_EXCHANGE_MEDIUM = (ChangerBase << 16) | (0x0008 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),

        /// <summary>
        /// Moves a piece of media to a destination.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363398(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_MOVE_MEDIUM = (ChangerBase << 16) | (0x0009 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Physically recalibrates a transport element. Recalibration may involve returning the transport to its home position.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363400(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_REINITIALIZE_TRANSPORT = (ChangerBase << 16) | (0x000A << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Retrieves the volume tag information for the specified elements.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363399(v=vs.85).aspx </remarks>
        IOCTL_CHANGER_QUERY_VOLUME_TAGS = (ChangerBase << 16) | (0x000B << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        ////
        //// FILESYSTEM

        /// <summary>
        /// Requests a level 1 opportunistic lock on a file.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364590(v=vs.85).aspx</remarks>
        FSCTL_REQUEST_OPLOCK_LEVEL_1 = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (0 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Requests a level 2 opportunistic lock on a file.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364591(v=vs.85).aspx </remarks>
        FSCTL_REQUEST_OPLOCK_LEVEL_2 = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (1 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Requests a batch opportunistic lock on a file.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364588(v=vs.85).aspx </remarks>
        FSCTL_REQUEST_BATCH_OPLOCK = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (2 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Responds to notification that an exclusive opportunistic lock on a file is about to be broken. Use this operation to indicate that the file should receive a level 2 opportunistic lock.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364579(v=vs.85).aspx </remarks>
        FSCTL_OPLOCK_BREAK_ACKNOWLEDGE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (3 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Notifies a server that a client application is ready to close a file. Use this operation after notification that an opportunistic lock on a file is ready to be broken.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364578(v=vs.85).aspx </remarks>
        FSCTL_OPBATCH_ACK_CLOSE_PENDING = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (4 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Enables the calling application to wait for completion of an opportunistic lock break.
        /// This operation is not useful to application developers and is documented here only for completeness. CreateFile handles the problem that this operation was designed to handle.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364581(v=vs.85).aspx</remarks>
        FSCTL_OPLOCK_BREAK_NOTIFY = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (5 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Locks a volume if it is not in use. A locked volume can be accessed only through handles to the file object (*hDevice) that locks the volume. For more information, see the Remarks section.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364575(v=vs.85).aspx </remarks>
        FSCTL_LOCK_VOLUME = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (6 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Unlocks a volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364814(v=vs.85).aspx </remarks>
        FSCTL_UNLOCK_VOLUME = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (7 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Dismounts a volume regardless of whether or not the volume is currently in use. For more information, see the Remarks section.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364562(v=vs.85).aspx </remarks>
        FSCTL_DISMOUNT_VOLUME = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (8 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Determines whether the specified volume is mounted, or if the specified file or directory is on a mounted volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364574(v=vs.85).aspx </remarks>
        FSCTL_IS_VOLUME_MOUNTED = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (10 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_IS_PATHNAME_VALID control code performs static analysis of the supplied pathname and returns a status value that indicates 
        /// whether the pathname is well formed (for example, no illegal characters, acceptable path length, and so on). Because this analysis does 
        /// not consider the content of the volume, it sometimes gives "false positives." In other words, the analysis might indicate that the 
        /// pathname is well formed, even when it is not. Negative results are more reliable, but are not guaranteed to be correct.
        /// This control code is not supported with fast FAT file systems, and it is not a meaningful operation in NTFS or UDFS. NTFS and UDFS support 
        /// such a wide variety of codesets that any string is potentially a valid pathname.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545431(v=vs.85).aspx </remarks>
        FSCTL_IS_PATHNAME_VALID = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (11 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_MARK_VOLUME_DIRTY control code marks a specified volume as dirty, which triggers Autochk.exe to run on the volume during the next system restart.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff728860(v=vs.85).aspx </remarks>
        FSCTL_MARK_VOLUME_DIRTY = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (12 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_QUERY_RETRIEVAL_POINTERS control code retrieves a mapping between virtual cluster numbers (VCN, offsets within the file/stream 
        /// space) and logical cluster numbers (LCN, offsets within the volume space), starting at the beginning of the file up to the map size 
        /// specified in InputBuffer.
        /// FSCTL_QUERY_RETRIEVAL_POINTERS is similar to FSCTL_GET_RETRIEVAL_POINTERS. However, FSCTL_QUERY_RETRIEVAL_POINTERS only works in kernel 
        /// mode on local paging files or the system hives. The paging file is guaranteed to have a one-to-one mapping from the VCN in a volume to the 
        /// LCN that refer more directly to the underlying physical storage. You must not use FSCTL_QUERY_RETRIEVAL_POINTERS with files other than 
        /// the page file, because they might reside on volumes, such as mirrored volumes, that have one-to-many mappings of VCNs to LCNs.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545507(v=vs.85).aspx </remarks>
        FSCTL_QUERY_RETRIEVAL_POINTERS = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (14 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Retrieves the current compression state of a file or directory on a volume whose file system supports per-stream compression.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364567(v=vs.85).aspx </remarks>
        FSCTL_GET_COMPRESSION = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (15 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Sets the compression state of a file or directory on a volume whose file system supports per-file and per-directory compression. You can use FSCTL_SET_COMPRESSION to compress or uncompress a file or directory on such a volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364592(v=vs.85).aspx </remarks>
        FSCTL_SET_COMPRESSION = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (16 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// The FSCTL_MARK_AS_SYSTEM_HIVE control code informs the file system that the specified file contains the registry's system hive. The file 
        /// system must flush system hive data to disk at just the right moment to avoid deadlocks and to ensure data integrity. Do not use this file 
        /// system control code with any file other than the file that contains the registry's system hive. This control code does not work with a 
        /// directory or volume handle. File system redirectors that access files on remote machines treat this control code as a no-op.
        /// Only kernel-level components can use this filesystem control code.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545455(v=vs.85).aspx </remarks>
        FSCTL_MARK_AS_SYSTEM_HIVE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (19 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// The FSCTL_OPLOCK_BREAK_ACK_NO_2 control code responds to notification that an exclusive (level 1, batch, or filter) opportunistic lock 
        /// (oplock) on a file has been broken.
        /// A client application sends this control code to indicate that it acknowledges the oplock break and that, if the oplock is a level 1 oplock 
        /// that was broken to level 2, it does not want the level 2 oplock.
        /// To process this control code, a minifilter calls FltOplockFsctrl with the following parameters. A file system or legacy filter driver 
        /// calls FsRtlOplockFsctrl.
        /// For more information about opportunistic locking and about the FSCTL_OPLOCK_BREAK_ACK_NO_2 control code, see the Microsoft Windows SDK 
        /// documentation.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545476(v=vs.85).aspx </remarks>
        FSCTL_OPLOCK_BREAK_ACK_NO_2 = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (20 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// The FSCTL_INVALIDATE_VOLUMES control code finds and removes all the volumes mounted on the device represented by the specified file object 
        /// or handle.
        /// To perform this operation, minifilter drivers call FltFsControlFile, and file systems, redirectors, and legacy file system filter drivers 
        /// call ZwFsControlFile, using the following parameters.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff934688(v=vs.85).aspx </remarks>
        FSCTL_INVALIDATE_VOLUMES = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (21 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/dd414538.aspx</remarks>
        FSCTL_QUERY_FAT_BPB = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (22 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_REQUEST_FILTER_OPLOCK control code requests a filter opportunistic lock (oplock) on a file.
        /// To process this control code, a minifilter calls FltOplockFsctrl with the following parameters. A file system or legacy filter driver 
        /// calls FsRtlOplockFsctrl.
        /// For more information about opportunistic locking and about the FSCTL_REQUEST_FILTER_OPLOCK control code, see the Microsoft Windows SDK 
        /// documentation.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545518(v=vs.85).aspx </remarks>
        FSCTL_REQUEST_FILTER_OPLOCK = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (23 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Retrieves the information from various file system performance counters.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364565(v=vs.85).aspx </remarks>
        FSCTL_FILESYSTEM_GET_STATISTICS = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (24 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Retrieves information about the specified NTFS file system volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364569(v=vs.85).aspx </remarks>
        FSCTL_GET_NTFS_VOLUME_DATA = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (25 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Retrieves the first file record that is in use and is of a lesser than or equal ordinal value to the requested file reference number.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364568(v=vs.85).aspx </remarks>
        FSCTL_GET_NTFS_FILE_RECORD = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (26 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Retrieves a bitmap of occupied and available clusters on a volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364573(v=vs.85).aspx </remarks>
        FSCTL_GET_VOLUME_BITMAP = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (27 << 2) | IoMethod.Neither | (0 << 14),
        
        /// <summary>
        /// Given a file handle, retrieves a data structure that describes the allocation and location on disk of a specific file, or, given a volume handle, the locations of bad clusters on a volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364572(v=vs.85).aspx </remarks>
        FSCTL_GET_RETRIEVAL_POINTERS = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (28 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Relocates one or more virtual clusters of a file from one logical cluster to another within the same volume. This operation is used during defragmentation. 
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364577(v=vs.85).aspx </remarks>
        FSCTL_MOVE_FILE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (29 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_IS_VOLUME_DIRTY control code determines whether the specified volume is dirty.
        /// If the volume information file is corrupted, NTFS will return STATUS_FILE_CORRUPT_ERROR.
        /// To perform this operation, minifilter drivers call FltFsControlFile with the following parameters, and file systems, redirectors, and 
        /// legacy file system filter drivers call ZwFsControlFile with the following parameters.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545443(v=vs.85).aspx </remarks>
        FSCTL_IS_VOLUME_DIRTY = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (30 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// 
        /// </summary>
        FsctlGetHfsInformation = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (31 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Signals the file system driver not to perform any I/O boundary checks on partition read or write calls. Instead, boundary checks are performed by the device driver.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364556(v=vs.85).aspx </remarks>
        FSCTL_ALLOW_EXTENDED_DASD_IO = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (32 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// 
        /// </summary>
        FsctlReadPropertyData = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (33 << 2) | IoMethod.Neither | (0 << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlWritePropertyData = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (34 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// 
        /// </summary>
        FsctlDumpPropertyData = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (37 << 2) | IoMethod.Neither | (0 << 14),
        
        /// <summary>
        /// Searches a directory for a file whose creator owner matches the specified SID.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364566(v=vs.85).aspx </remarks>
        FSCTL_FIND_FILES_BY_SID = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (35 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Sets the object identifier for the specified file or directory.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364593(v=vs.85).aspx </remarks>
        FSCTL_SET_OBJECT_ID = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (38 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Retrieves the object identifier for the specified file or directory.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364570(v=vs.85).aspx </remarks>
        FSCTL_GET_OBJECT_ID = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (39 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Removes the object identifier from a specified file or directory. The underlying object is not deleted.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364559(v=vs.85).aspx </remarks>
        FSCTL_DELETE_OBJECT_ID = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (40 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_SET_REPARSE_POINT control code sets a reparse point on a file or directory.
        /// To perform this operation, call ZwFsControlFile with the following parameters.
        /// Minifilters should use FltTagFile instead of FSCTL_SET_REPARSE_POINT to set a reparse point.
        /// For more information about reparse points and the FSCTL_SET_REPARSE_POINT control code, see the Microsoft Windows SDK documentation.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff545568(v=vs.85).aspx </remarks>
        FSCTL_SET_REPARSE_POINT = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (41 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_GET_REPARSE_POINT control code retrieves the reparse point data associated with the specified file or directory.
        /// To perform this operation, call FltFsControlFile or ZwFsControlFile with the following parameters.
        /// For more information about reparse points and the FSCTL_GET_REPARSE_POINT control code, see the Microsoft Windows SDK documentation.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff544836(v=vs.85).aspx </remarks>
        FSCTL_GET_REPARSE_POINT = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (42 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_DELETE_REPARSE_POINT control code deletes a reparse point from the specified file or directory. 
        /// Using FSCTL_DELETE_REPARSE_POINT does not delete the file or directory.
        /// To perform this operation, call ZwFsControlFile with the following parameters.
        /// Minifilters should use FltUntagFile instead of FSCTL_DELETE_REPARSE_POINT to delete a reparse point.
        /// For more information about reparse points and the FSCTL_DELETE_REPARSE_POINT control code, see the Microsoft Windows SDK documentation.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff544828(v=vs.85).aspx </remarks>
        FSCTL_DELETE_REPARSE_POINT = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (43 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Enumerates the update sequence number (USN) data between two specified boundaries to obtain master file table (MFT) records.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364563(v=vs.85).aspx </remarks>
        FSCTL_ENUM_USN_DATA = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (44 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// The FSCTL_SECURITY_ID_CHECK control code performs a bulk security check of the security identifiers exposed through USN records.
        /// </summary>
        /// <remarks>http://blog.yezhucn.com/fileio/fsctl_security_id_check.htm</remarks>
        FSCTL_SECURITY_ID_CHECK = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (45 << 2) | IoMethod.Neither | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// Retrieves the set of update sequence number (USN) change journal records between two specified USN values.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364586(v=vs.85).aspx </remarks>
        FSCTL_READ_USN_JOURNAL = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (46 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Modifies user data associated with the object identifier for the specified file or directory.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364594(v=vs.85).aspx </remarks>
        FSCTL_SET_OBJECT_ID_EXTENDED = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (47 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Retrieves the object identifier for the specified file or directory. If no object identifier exists, using FSCTL_CREATE_OR_GET_OBJECT_ID creates one.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364557(v=vs.85).aspx</remarks>
        FSCTL_CREATE_OR_GET_OBJECT_ID = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (48 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// Marks the indicated file as sparse or not sparse. In a sparse file, large ranges of zeros may not require disk allocation. Space for nonzero data will be allocated as needed as the file is written.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364596(v=vs.85).aspx </remarks>
        FSCTL_SET_SPARSE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (49 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Fills a specified range of a file with zeros (0). If the file is sparse or compressed, the NTFS file system may deallocate disk space in the file. This sets the range of bytes to zeros (0) without extending the file size.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364597(v=vs.85).aspx </remarks>
        FSCTL_SET_ZERO_DATA = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (50 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Write << 14),
        
        /// <summary>
        /// Scans a file or alternate stream looking for ranges that may contain nonzero data. Only compressed or sparse files can have zeroed ranges known to the operating system. For other files, the output buffer will contain only a single entry that contains the starting point and the length requested.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364582(v=vs.85).aspx </remarks>
        FSCTL_QUERY_ALLOCATED_RANGES = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (51 << 2) | IoMethod.Neither | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlEnableUpgrade = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (52 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Write << 14),
        
        /// <summary>
        /// The FSCTL_SET_ENCRYPTION request sets the encryption for the file or directory associated with the given handle.
        /// The message contains an ENCRYPTION_BUFFER structure that indicates whether to encrypt/decrypt a file or an individual stream.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/cc980038.aspx </remarks>
        FSCTL_SET_ENCRYPTION = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (53 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// 
        /// </summary>
        FsctlEncryptionFsctlIo = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (54 << 2) | IoMethod.Neither | (0 << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlWriteRawEncrypted = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (55 << 2) | IoMethod.Neither | (0 << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlReadRawEncrypted = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (56 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Creates an update sequence number (USN) change journal stream on a target volume, or modifies an existing change journal stream.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364558(v=vs.85).aspx </remarks>
        FSCTL_CREATE_USN_JOURNAL = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (57 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Retrieves the update sequence number (USN) change-journal information for the specified file or directory.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364584(v=vs.85).aspx </remarks>
        FSCTL_READ_FILE_USN_DATA = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (58 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Generates a record in the update sequence number (USN) change journal stream for the input file. This record will have the USN_REASON_CLOSE flag.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364816(v=vs.85).aspx </remarks>
        FSCTL_WRITE_USN_CLOSE_RECORD = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (59 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// Increases the size of a mounted volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364564(v=vs.85).aspx </remarks>
        FSCTL_EXTEND_VOLUME = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (60 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Queries for information on the current update sequence number (USN) change journal, its records, and its capacity.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364583(v=vs.85).aspx </remarks>
        FSCTL_QUERY_USN_JOURNAL = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (61 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Deletes the update sequence number (USN) change journal on a volume, or waits for notification of change journal deletion.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364561(v=vs.85).aspx </remarks>
        FSCTL_DELETE_USN_JOURNAL = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (62 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// Marks a specified file or directory and its change journal record with information about changes to that file or directory.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364576(v=vs.85).aspx </remarks>
        FSCTL_MARK_HANDLE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (63 << 2) | IoMethod.Buffered | (0 << 14),

        /// <summary>
        /// The FSCTL_SIS_COPYFILE request message requests that the server use the single-instance storage (SIS)filter to copy a file. 
        /// The message contains an SI_COPYFILE data element. For more information about single-instance storage, see [SIS].
        /// If the SIS filter is installed on the server, it will attempt to copy the specified source file to the specified destination 
        /// file by creating an SIS link instead of actually copying the file data. If necessary and allowed, the source file is placed under 
        /// SIS control before the destination file is created.
        /// This FSCTL can be issued against either a file or directory handle. The source and destination files MUST reside on the volume 
        /// associated with the given handle.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/cc232055.aspx</remarks>
        FSCTL_SIS_COPYFILE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (64 << 2) | IoMethod.Buffered | (0 << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlSisLinkFiles = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (65 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlHsmMsg = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (66 << 2) | IoMethod.Buffered | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlNssControl = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (67 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Write << 14),

        /// <summary>
        /// 
        /// </summary>
        FsctlNssRcontrol = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (70 << 2) | IoMethod.Buffered | (System.IO.FileAccess.Read << 14),
        
        /// <summary>
        /// 
        /// </summary>
        FsctlHsmData = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (68 << 2) | IoMethod.Neither | ((System.IO.FileAccess.Read | System.IO.FileAccess.Write) << 14),
        
        /// <summary>
        /// Recalls a file from storage media that Remote Storage manages, which is the hierarchical storage management software.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/desktop/aa364587(v=vs.85).aspx</remarks>
        FSCTL_RECALL_FILE = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (69 << 2) | IoMethod.Neither | (0 << 14),

        /// <summary>
        /// The FSCTL_GET_BOOT_AREA_INFO control code retrieves the locations of boot sectors for a volume.
        /// </summary>
        /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/windows/hardware/ff728858(v=vs.85).aspx </remarks>
        FSCTL_GET_BOOT_AREA_INFO = (FileDeviceType.FILE_DEVICE_FILE_SYSTEM << 16) | (140 << 2) | IoMethod.Buffered | (0 << 14),

        
        // VIDEO
        VideoQuerySupportedBrightness = (FileDeviceType.FILE_DEVICE_VIDEO << 16) | (0x0125 << 2) | IoMethod.Buffered | (0 << 14),
        VideoQueryDisplayBrightness = (FileDeviceType.FILE_DEVICE_VIDEO << 16) | (0x0126 << 2) | IoMethod.Buffered | (0 << 14),
        VideoSetDisplayBrightness = (FileDeviceType.FILE_DEVICE_VIDEO << 16) | (0x0127 << 2) | IoMethod.Buffered | (0 << 14)
    }
}
