// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "ReadRawDevice", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Scope = "namespace", Justification = "Will be done later")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.Documentation", "SA1602:EnumerationItemsMustBeDocumented", Target = "ReadRawDevice.Win32.FileDeviceType", Scope = "namespace", Justification = "MSDN documented. Not really important here")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "ReadRawDevice.Win32")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetupDi", Scope = "member", Target = "ReadRawDevice.DeviceBuilder.#GetListOfDevices(System.Threading.CancellationToken)", Justification = "General name")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "INFO", Scope = "type", Target = "ReadRawDevice.Win32.DISK_EX_INT13_INFO", Justification = "Following MSDN name declaration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "type", Target = "ReadRawDevice.Win32.DISK_EX_INT13_INFO", Justification = "Following MSDN name declaration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "INT", Scope = "type", Target = "ReadRawDevice.Win32.DISK_EX_INT13_INFO", Justification = "Following MSDN name declaration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DISK", Scope = "type", Target = "ReadRawDevice.Win32.DISK_EX_INT13_INFO", Justification = "Following MSDN name declaration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.DiskInt13Union.#Int13", Justification = "This is a nature of this struct")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.DiskInt13Union.#ExInt13", Justification = "This is nature of this struct")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "type", Target = "ReadRawDevice.Win32.PARTITION_INFORMATION_GPT", Justification = "Following MSDN name declaration")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_CDROM", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_PARTITION", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_TAPE", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_WRITEONCEDISK", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_VOLUME", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_MEDIUMCHANGER", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_FLOPPY", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_CDCHANGER", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_STORAGEPORT", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_VMLUN", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_SES", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_HIDDEN_VOLUME", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_COMPORT", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "ReadRawDevice.Win32.SetupDiInterfacesGuid.#GUID_DEVINTERFACE_SERENUM_BUS_ENUMERATOR", Justification = "By design")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "ReadRawDevice.Win32.UnsafeNativeMethods.#GetVolumeNameForVolumeMountPoint(System.String,System.Text.StringBuilder,System.UInt32)", Justification = "Listed for future use")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "ReadRawDevice.Win32.UnsafeNativeMethods.#CTL_CODE(System.UInt32,System.UInt32,System.UInt32,System.UInt32)", Justification = "This is natural Win32 function to receive CTL code. Listed here for future use")]
