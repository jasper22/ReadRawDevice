
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// Represent device interface class for SetupDi functions
    /// </summary>
    /// <remarks>Defined in winioctl.h</remarks>
    internal class SetupDiInterfacesGuid
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="SetupDiInterfacesGuid"/> class from being created.
        /// </summary>
        private SetupDiInterfacesGuid()
        {
        }

        /// <summary>
        /// Disk
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_DISK = Guid.Parse("53f56307-b6bf-11d0-94f2-00a0c91efb8b");

        /// <summary>
        /// CDRom
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_CDROM = Guid.Parse("53F56308-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// Partition
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_PARTITION = Guid.Parse("53F5630A-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// Tape
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_TAPE = Guid.Parse("53F5630B-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// WriteOnce disk
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_WRITEONCEDISK = Guid.Parse("53F5630C-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// Volume
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_VOLUME = Guid.Parse("53F5630D-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// Medium changer
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_MEDIUMCHANGER = Guid.Parse("53F56310-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// Floppy
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_FLOPPY = Guid.Parse("53F56311-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// CD-Changer
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_CDCHANGER = Guid.Parse("53F56312-B6BF-11D0-94F2-00A0C91EFB8B");

        /// <summary>
        /// Storage port
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_STORAGEPORT = Guid.Parse("2ACCFE60-C130-11D2-B082-00A0C91EFB8B");

        /// <summary>
        /// VM lun
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_VMLUN = Guid.Parse("6f416619-9f29-42a5-b20b-37e219ca02b0");

        /// <summary>
        /// SES
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_SES = Guid.Parse("e9f2d03a-747c-41c2-bb9a-02c62b6d5fcb");

        /// <summary>
        /// Hidden volume
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_HIDDEN_VOLUME = Guid.Parse("7f108a28-9833-4b3b-b780-2c6b5fa5c062");

        /// <summary>
        /// COM port
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_COMPORT = Guid.Parse("86e0d1e0-8089-11d0-9ce4-08003e301f73");

        /// <summary>
        /// Serenum bus enumerator
        /// </summary>
        internal static readonly Guid GUID_DEVINTERFACE_SERENUM_BUS_ENUMERATOR = Guid.Parse("4D36E978-E325-11CE-BFC1-08002BE10318");
    }
}
