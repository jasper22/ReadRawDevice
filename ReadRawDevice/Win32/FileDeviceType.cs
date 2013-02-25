
namespace ReadRawDevice.Win32
{
    /// <summary>
    /// The type of device. Values from 0 through 32,767 are reserved for use by Microsoft. Values from 32,768 through 65,535 are reserved for use by other vendors.
    /// </summary>
    /// <remarks>MSDN: http://msdn.microsoft.com/en-us/library/bb968801(VS.85).aspx</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.Documentation", "SA1602:EnumerationItemsMustBeDocumented", Target = "ReadRawDevice.Win32.FileDeviceType", Scope = "namespace", Justification = "MSDN documented. Not really important here")]
    internal enum FileDeviceType
    {
        /// <summary>
        /// FILE_DEVICE_BEEP parameter
        /// </summary>
        FILE_DEVICE_BEEP = 0x00000001,

        /// <summary>
        /// FILE_DEVICE_CD_ROM parameter
        /// </summary>
        FILE_DEVICE_CD_ROM = 0x00000002,

        /// <summary>
        /// FILE_DEVICE_CD_ROM_FILE_SYSTEM parameter
        /// </summary>
        FILE_DEVICE_CD_ROM_FILE_SYSTEM = 0x00000003,

        /// <summary>
        /// FILE_DEVICE_CONTROLLER parameter
        /// </summary>
        FILE_DEVICE_CONTROLLER = 0x00000004,

        /// <summary>
        /// FILE_DEVICE_DATALINK parameter
        /// </summary>
        FILE_DEVICE_DATALINK = 0x00000005,

        /// <summary>
        /// FILE_DEVICE_DFS parameter
        /// </summary>
        FILE_DEVICE_DFS = 0x00000006,

        /// <summary>
        /// FILE_DEVICE_DISK parameter
        /// </summary>
        FILE_DEVICE_DISK = 0x00000007,

        /// <summary>
        /// FILE_DEVICE_DISK_FILE_SYSTEM parameter
        /// </summary>
        FILE_DEVICE_DISK_FILE_SYSTEM = 0x00000008,

        /// <summary>
        /// FILE_DEVICE_FILE_SYSTEM parameter
        /// </summary>
        FILE_DEVICE_FILE_SYSTEM = 0x00000009,

        /// <summary>
        /// FILE_DEVICE_INPORT_PORT parameter
        /// </summary>
        FILE_DEVICE_INPORT_PORT = 0x0000000a,

        /// <summary>
        /// FILE_DEVICE_KEYBOARD parameter
        /// </summary>
        FILE_DEVICE_KEYBOARD = 0x0000000b,

        /// <summary>
        /// FILE_DEVICE_MAILSLOT parameter
        /// </summary>
        FILE_DEVICE_MAILSLOT = 0x0000000c,

        /// <summary>
        /// FILE_DEVICE_MIDI_IN parameter
        /// </summary>
        FILE_DEVICE_MIDI_IN = 0x0000000d,

        /// <summary>
        /// FILE_DEVICE_MIDI_OUT parameter
        /// </summary>
        FILE_DEVICE_MIDI_OUT = 0x0000000e,

        /// <summary>
        /// FILE_DEVICE_MOUSE parameter
        /// </summary>
        FILE_DEVICE_MOUSE = 0x0000000f,

        /// <summary>
        /// FILE_DEVICE_MULTI_UNC_PROVIDER parameter
        /// </summary>
        FILE_DEVICE_MULTI_UNC_PROVIDER = 0x00000010,

        /// <summary>
        /// FILE_DEVICE_NAMED_PIPE parameter
        /// </summary>
        FILE_DEVICE_NAMED_PIPE = 0x00000011,

        /// <summary>
        /// FILE_DEVICE_NETWORK parameter
        /// </summary>
        FILE_DEVICE_NETWORK = 0x00000012,

        /// <summary>
        /// FILE_DEVICE_NETWORK_BROWSER parameter
        /// </summary>
        FILE_DEVICE_NETWORK_BROWSER = 0x00000013,

        /// <summary>
        /// FILE_DEVICE_NETWORK_FILE_SYSTEM parameter
        /// </summary>
        FILE_DEVICE_NETWORK_FILE_SYSTEM = 0x00000014,

        /// <summary>
        /// FILE_DEVICE_NULL parameter
        /// </summary>
        FILE_DEVICE_NULL = 0x00000015,

        /// <summary>
        /// FILE_DEVICE_PARALLEL_PORT parameter
        /// </summary>
        FILE_DEVICE_PARALLEL_PORT = 0x00000016,

        /// <summary>
        /// FILE_DEVICE_PHYSICAL_NETCARD parameter
        /// </summary>
        FILE_DEVICE_PHYSICAL_NETCARD = 0x00000017,

        /// <summary>
        /// FILE_DEVICE_PRINTER parameter
        /// </summary>
        FILE_DEVICE_PRINTER = 0x00000018,

        /// <summary>
        /// FILE_DEVICE_SCANNER parameter
        /// </summary>
        FILE_DEVICE_SCANNER = 0x00000019,

        /// <summary>
        /// FILE_DEVICE_SERIAL_MOUSE_PORT parameter
        /// </summary>
        FILE_DEVICE_SERIAL_MOUSE_PORT = 0x0000001a,

        /// <summary>
        /// FILE_DEVICE_SERIAL_PORT parameter
        /// </summary>
        FILE_DEVICE_SERIAL_PORT = 0x0000001b,

        /// <summary>
        /// FILE_DEVICE_SCREEN parameter
        /// </summary>
        FILE_DEVICE_SCREEN = 0x0000001c,

        /// <summary>
        /// FILE_DEVICE_SOUND parameter
        /// </summary>
        FILE_DEVICE_SOUND = 0x0000001d,

        /// <summary>
        /// FILE_DEVICE_STREAMS parameter
        /// </summary>
        FILE_DEVICE_STREAMS = 0x0000001e,

        /// <summary>
        /// FILE_DEVICE_TAPE parameter
        /// </summary>
        FILE_DEVICE_TAPE = 0x0000001f,

        /// <summary>
        /// FILE_DEVICE_TAPE_FILE_SYSTEM parameter
        /// </summary>
        FILE_DEVICE_TAPE_FILE_SYSTEM = 0x00000020,

        /// <summary>
        /// FILE_DEVICE_TRANSPORT parameter
        /// </summary>
        FILE_DEVICE_TRANSPORT = 0x00000021,

        /// <summary>
        /// FILE_DEVICE_UNKNOWN parameter
        /// </summary>
        FILE_DEVICE_UNKNOWN = 0x00000022,

        /// <summary>
        /// FILE_DEVICE_VIDEO parameter
        /// </summary>
        FILE_DEVICE_VIDEO = 0x00000023,

        /// <summary>
        /// FILE_DEVICE_VIRTUAL_DISK parameter
        /// </summary>
        FILE_DEVICE_VIRTUAL_DISK = 0x00000024,

        /// <summary>
        /// FILE_DEVICE_WAVE_IN parameter
        /// </summary>
        FILE_DEVICE_WAVE_IN = 0x00000025,

        /// <summary>
        /// FILE_DEVICE_WAVE_OUT parameter
        /// </summary>
        FILE_DEVICE_WAVE_OUT = 0x00000026,

        /// <summary>
        /// FILE_DEVICE_8042_PORT parameter
        /// </summary>
        FILE_DEVICE_8042_PORT = 0x00000027,

        /// <summary>
        /// FILE_DEVICE_NETWORK_REDIRECTOR parameter
        /// </summary>
        FILE_DEVICE_NETWORK_REDIRECTOR = 0x00000028,

        /// <summary>
        /// FILE_DEVICE_BATTERY parameter
        /// </summary>
        FILE_DEVICE_BATTERY = 0x00000029,

        /// <summary>
        /// FILE_DEVICE_BUS_EXTENDER parameter
        /// </summary>
        FILE_DEVICE_BUS_EXTENDER = 0x0000002a,

        /// <summary>
        /// FILE_DEVICE_MODEM parameter
        /// </summary>
        FILE_DEVICE_MODEM = 0x0000002b,

        /// <summary>
        /// FILE_DEVICE_VDM parameter
        /// </summary>
        FILE_DEVICE_VDM = 0x0000002c,

        /// <summary>
        /// FILE_DEVICE_MASS_STORAGE parameter
        /// </summary>
        FILE_DEVICE_MASS_STORAGE = 0x0000002d,

        /// <summary>
        /// FILE_DEVICE_SMB parameter
        /// </summary>
        FILE_DEVICE_SMB = 0x0000002e,

        /// <summary>
        /// FILE_DEVICE_KS parameter
        /// </summary>
        FILE_DEVICE_KS = 0x0000002f,

        /// <summary>
        /// FILE_DEVICE_CHANGER parameter
        /// </summary>
        FILE_DEVICE_CHANGER = 0x00000030,

        /// <summary>
        /// FILE_DEVICE_SMARTCARD parameter
        /// </summary>
        FILE_DEVICE_SMARTCARD = 0x00000031,

        /// <summary>
        /// FILE_DEVICE_ACPI parameter
        /// </summary>
        FILE_DEVICE_ACPI = 0x00000032,

        /// <summary>
        /// FILE_DEVICE_DVD parameter
        /// </summary>
        FILE_DEVICE_DVD = 0x00000033,

        /// <summary>
        /// FILE_DEVICE_FULLSCREEN_VIDEO parameter
        /// </summary>
        FILE_DEVICE_FULLSCREEN_VIDEO = 0x00000034,

        /// <summary>
        /// FILE_DEVICE_DFS_FILE_SYSTEM parameter
        /// </summary>
        FILE_DEVICE_DFS_FILE_SYSTEM = 0x00000035,

        /// <summary>
        /// FILE_DEVICE_DFS_VOLUME parameter
        /// </summary>
        FILE_DEVICE_DFS_VOLUME = 0x00000036,

        /// <summary>
        /// FILE_DEVICE_SERENUM parameter
        /// </summary>
        FILE_DEVICE_SERENUM = 0x00000037,

        /// <summary>
        /// FILE_DEVICE_TERMSRV parameter
        /// </summary>
        FILE_DEVICE_TERMSRV = 0x00000038,

        /// <summary>
        /// FILE_DEVICE_KSEC parameter
        /// </summary>
        FILE_DEVICE_KSEC = 0x00000039,

        /// <summary>
        /// FILE_DEVICE_FIPS parameter
        /// </summary>
        FILE_DEVICE_FIPS = 0x0000003A,

        /// <summary>
        /// FILE_DEVICE_INFINIBAND parameter
        /// </summary>
        FILE_DEVICE_INFINIBAND = 0x0000003B,

        /// <summary>
        /// FILE_DEVICE_VMBUS parameter
        /// </summary>
        FILE_DEVICE_VMBUS = 0x0000003E,

        /// <summary>
        /// FILE_DEVICE_CRYPT_PROVIDER parameter
        /// </summary>
        FILE_DEVICE_CRYPT_PROVIDER = 0x0000003F,

        /// <summary>
        /// FILE_DEVICE_WPD parameter
        /// </summary>
        FILE_DEVICE_WPD = 0x00000040,

        /// <summary>
        /// FILE_DEVICE_BLUETOOTH parameter
        /// </summary>
        FILE_DEVICE_BLUETOOTH = 0x00000041,

        /// <summary>
        /// FILE_DEVICE_MT_COMPOSITE parameter
        /// </summary>
        FILE_DEVICE_MT_COMPOSITE = 0x00000042,

        /// <summary>
        /// FILE_DEVICE_MT_TRANSPORT parameter
        /// </summary>
        FILE_DEVICE_MT_TRANSPORT = 0x00000043,

        /// <summary>
        /// FILE_DEVICE_BIOMETRIC parameter
        /// </summary>
        FILE_DEVICE_BIOMETRIC = 0x00000044,

        /// <summary>
        /// FILE_DEVICE_PMI parameter
        /// </summary>
        FILE_DEVICE_PMI = 0x00000045,

        /// <summary>
        /// FILE_DEVICE_EHSTOR parameter
        /// </summary>
        FILE_DEVICE_EHSTOR = 0x00000046,

        /// <summary>
        /// FILE_DEVICE_DEVAPI parameter
        /// </summary>
        FILE_DEVICE_DEVAPI = 0x00000047,

        /// <summary>
        /// FILE_DEVICE_GPIO parameter
        /// </summary>
        FILE_DEVICE_GPIO = 0x00000048,

        /// <summary>
        /// FILE_DEVICE_USBEX parameter
        /// </summary>
        FILE_DEVICE_USBEX = 0x00000049,

        /// <summary>
        /// FILE_DEVICE_CONSOLE parameter
        /// </summary>
        FILE_DEVICE_CONSOLE = 0x00000050,

        /// <summary>
        /// FILE_DEVICE_NFP parameter
        /// </summary>
        FILE_DEVICE_NFP = 0x00000051,

        /// <summary>
        /// FILE_DEVICE_SYSENV parameter
        /// </summary>
        FILE_DEVICE_SYSENV = 0x00000052
    }
}
