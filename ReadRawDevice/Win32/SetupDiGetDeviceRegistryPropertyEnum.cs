
namespace ReadRawDevice.Win32
{
    using System;

    /// <summary>
    /// Flags for SetupDiGetDeviceRegistryProperty().
    /// </summary>
    /// <remarks>MSND: http://msdn.microsoft.com/en-us/library/windows/hardware/ff551967(v=vs.85).aspx </remarks>
    [Flags]
    internal enum SetupDiGetDeviceRegistryPropertyEnum : uint
    {
        /// <summary>
        /// The function retrieves a REG_SZ string that contains the description of a device.
        /// </summary>
        SPDRP_DEVICEDESC = 0x00000000,

        /// <summary>
        /// The function retrieves a REG_MULTI_SZ string that contains the list of hardware IDs for a device. For information about hardware IDs, see Device Identification Strings.
        /// </summary>
        SPDRP_HARDWAREID = 0x00000001,

        /// <summary>
        /// The function retrieves a REG_MULTI_SZ string that contains the list of compatible IDs for a device. For information about compatible IDs, see Device Identification Strings.
        /// </summary>
        SPDRP_COMPATIBLEIDS = 0x00000002,

        /// <summary>
        /// Unused
        /// </summary>
        SPDRP_UNUSED0 = 0x00000003,

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the service name for a device.
        /// </summary>
        SPDRP_SERVICE = 0x00000004,

        /// <summary>
        /// Unused
        /// </summary>
        SPDRP_UNUSED1 = 0x00000005,

        /// <summary>
        /// Unused
        /// </summary>
        SPDRP_UNUSED2 = 0x00000006,
        
        /// <summary>
        /// The function retrieves a REG_SZ string that contains the device setup class of a device
        /// </summary>
        SPDRP_CLASS = 0x00000007,

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the GUID that represents the device setup class of a device.
        /// </summary>
        SPDRP_CLASSGUID = 0x00000008,

        /// <summary>
        /// The function retrieves a string that identifies the device's software key (sometimes called the driver key). For more information about driver keys, see Registry Trees and Keys for Devices and Drivers.
        /// </summary>
        SPDRP_DRIVER = 0x00000009,

        /// <summary>
        /// The function retrieves a bitwise OR of a device's configuration flags in a DWORD value. The configuration flags are represented by the CONFIGFLAG_Xxx bitmasks that are defined in Regstr.h.
        /// </summary>
        SPDRP_CONFIGFLAGS = 0x0000000A,
        
        /// <summary>
        /// The function retrieves a REG_SZ string that contains the name of the device manufacturer.
        /// </summary>
        SPDRP_MFG = 0x0000000B,

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the friendly name of a device.
        /// </summary>
        SPDRP_FRIENDLYNAME = 0x0000000C, 

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the hardware location of a device.
        /// </summary>
        SPDRP_LOCATION_INFORMATION = 0x0000000D,

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the name that is associated with the device's PDO. For more information, see IoCreateDevice.
        /// </summary>
        SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,

        /// <summary>
        /// The function retrieves a bitwise OR of the following CM_DEVCAP_Xxx flags in a DWORD. The device capabilities that are represented by these flags correspond to the device capabilities that are represented by the members of the DEVICE_CAPABILITIES structure. The CM_DEVCAP_Xxx constants are defined in Cfgmgr32.h.
        /// </summary>
        SPDRP_CAPABILITIES = 0x0000000F,

        /// <summary>
        /// The function retrieves a DWORD value set to the value of the UINumber member of the device's DEVICE_CAPABILITIES structure.
        /// </summary>
        SPDRP_UI_NUMBER = 0x00000010,

        /// <summary>
        /// The function retrieves a REG_MULTI_SZ string that contains the names of a device's upper filter drivers.
        /// </summary>
        SPDRP_UPPERFILTERS = 0x00000011,

        /// <summary>
        /// The function retrieves a REG_MULTI_SZ string that contains the names of a device's lower-filter drivers.
        /// </summary>
        SPDRP_LOWERFILTERS = 0x00000012,

        /// <summary>
        /// The function retrieves the GUID for the device's bus type
        /// </summary>
        SPDRP_BUSTYPEGUID = 0x00000013,

        /// <summary>
        /// The function retrieves the device's legacy bus type as an INTERFACE_TYPE value (defined in Wdm.h and Ntddk.h).
        /// </summary>
        SPDRP_LEGACYBUSTYPE = 0x00000014,

        /// <summary>
        /// The function retrieves the device's bus number.
        /// </summary>
        SPDRP_BUSNUMBER = 0x00000015,

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the name of the device's enumerator.
        /// </summary>
        SPDRP_ENUMERATOR_NAME = 0x00000016,

        /// <summary>
        /// The function retrieves a SECURITY_DESCRIPTOR structure for a device.
        /// </summary>
        SPDRP_SECURITY = 0x00000017,

        /// <summary>
        /// The function retrieves a REG_SZ string that contains the device's security descriptor. The format of security descriptor strings is described in Microsoft Windows SDK documentation.
        /// </summary>
        SPDRP_SECURITY_SDS = 0x00000018,

        /// <summary>
        /// The function retrieves a DWORD value that represents the device's type. For more information, see Specifying Device Types.
        /// </summary>
        SPDRP_DEVTYPE = 0x00000019,

        /// <summary>
        /// The function retrieves a DWORD value that indicates whether a user can obtain exclusive use of the device. The returned value is one if exclusive use is allowed, or zero otherwise. For more information, see IoCreateDevice.
        /// </summary>
        SPDRP_EXCLUSIVE = 0x0000001A,

        /// <summary>
        /// The function retrieves a bitwise OR of a device's characteristics flags in a DWORD. For a description of these flags, which are defined in Wdm.h and Ntddk.h, see the DeviceCharacteristics parameter of the IoCreateDevice function.
        /// </summary>
        SPDRP_CHARACTERISTICS = 0x0000001B,

        /// <summary>
        /// The function retrieves the device's address.
        /// </summary>
        SPDRP_ADDRESS = 0x0000001C,

        /// <summary>
        /// The function retrieves a format string (REG_SZ) used to display the UINumber value.
        /// </summary>
        SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D,

        /// <summary>
        /// (Windows XP and later) The function retrieves a CM_POWER_DATA structure that contains the device's power management information.
        /// </summary>
        SPDRP_DEVICE_POWER_DATA = 0x0000001E,

        /// <summary>
        /// (Windows XP and later) The function retrieves the device's current removal policy as a DWORD that contains one of the CM_REMOVAL_POLICY_Xxx values that are defined in Cfgmgr32.h.
        /// </summary>
        SPDRP_REMOVAL_POLICY = 0x0000001F,

        /// <summary>
        /// (Windows XP and later) The function retrieves the device's hardware-specified default removal policy as a DWORD that contains one of the CM_REMOVAL_POLICY_Xxx values that are defined in Cfgmgr32.h.
        /// </summary>
        SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020,

        /// <summary>
        /// (Windows XP and later) The function retrieves the device's override removal policy (if it exists) from the registry, as a DWORD that contains one of the CM_REMOVAL_POLICY_Xxx values that are defined in Cfgmgr32.h.
        /// </summary>
        SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021,

        /// <summary>
        /// (Windows XP and later) The function retrieves a DWORD value that indicates the installation state of a device. The installation state is represented by one of the CM_INSTALL_STATE_Xxx values that are defined in Cfgmgr32.h. The CM_INSTALL_STATE_Xxx values correspond to the DEVICE_INSTALL_STATE enumeration values.
        /// </summary>
        SPDRP_INSTALL_STATE = 0x00000022,

        /// <summary>
        /// (Windows Server 2003 and later) The function retrieves a REG_MULTI_SZ string that represents the location of the device in the device tree.
        /// </summary>
        SPDRP_LOCATION_PATHS = 0x00000023,

        /// <summary>
        /// Base ContainerID
        /// </summary>
        SPDRP_BASE_CONTAINERID = 0x00000024
    }
}
