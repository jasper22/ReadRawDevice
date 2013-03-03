ReadRawDevice
=============
	
 Library to run some low-level Win32 API functions in C# that could enumerate devices, volumes, get they sizes and eventually allow to read raw data from device.

 Project was build on Windows 7 32bit in Visual Studio 2012 in .NET Framework 4.5


ReadRawDevice.Gui
=================

 WPF project that use the ReadDeviceRaw project


ReadRawDevice.NUnit
===================

 xUnit test project for ReadRawDevice


Limitations
===========

* User must install the full .NET Framework 4.5 from Microsoft:  http://www.microsoft.com/en-us/download/details.aspx?id=30653 and then install additional updates from Windows Update service
* Application runs on .Net Framework 4.5 so it's not available on Windows XP. Actually it could be re-arranged to run on lower framework versions, may be in future release's...

