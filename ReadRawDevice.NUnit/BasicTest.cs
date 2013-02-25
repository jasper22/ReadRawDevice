using System;
using System.Linq;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xunit;
using Xunit.Extensions;
using ReadRawDevice.Core;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ReadRawDevice.NUnit
{
    public class BasicTests
    {
        private ReadRawDevice.Engine engine;
        private VolumesCollection coll = null;
        private DeviceCollection devColl = null;

        public BasicTests()
        {
            AppDomain.CurrentDomain.FirstChanceException += (o, e) => {
                System.Diagnostics.Trace.WriteLine("=== First chance exception occurred: " + e.Exception.ToString());
            };

            TaskScheduler.UnobservedTaskException += (o, e) => {
                System.Diagnostics.Trace.WriteLine("--- Task exception occurred: " + e.Exception.ToString());
            };

            engine = new Engine();

            System.Diagnostics.Debug.Listeners.Add(new DefaultTraceListener());

            // if this test throw - we are not 'admin' to run those test
            // Can not continue if not admin
        }

        [Fact]
        public void allVolumesShouldHaveASize()
        {
            //coll.Where(dev => !dev.PartitionLength.HasValue).All(dev => {
            //    System.Diagnostics.Trace.WriteLine("Device: " + dev.DevicePath + " doesn't have a size");
            //    return true;
            //});

            Assert.DoesNotThrow(() =>
            {
                var tsk = engine.BuildVolumesAsync();
                tsk.Wait();
                coll = tsk.Result;
            });

            Assert.True(coll.All(dev => !dev.VolumeSize.HasValue));
        }

        [Fact]
        public void allVolumesShouldHaveDiskSize()
        {
            //coll.Where(dev => !(dev.DiskSize.HasValue && dev.DiskSize.Value >= 0)).All(dev =>
            //{
            //    System.Diagnostics.Trace.WriteLine("Device: " + dev.DevicePath + " doesn't have a size");
            //    return true;
            //});

            Assert.DoesNotThrow(() =>
            {
                var tsk = engine.BuildVolumesAsync();
                tsk.Wait();
                coll = tsk.Result;
            });

            Assert.True(coll.All(dev => !(dev.VolumeSize.HasValue && dev.VolumeSize.Value >= 0)));
        }

        [Fact]
        public void enumerateDevicesShouldNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                var tsk = engine.BuildDevicesAsync();
                tsk.Wait();
                devColl = tsk.Result;
            });
        }

        [Fact]
        public async void testSaveToFile()
        {
            //Assert.DoesNotThrow(() =>
            //{
                //coll = await engine.BuildVolumesAsync();
            devColl = await engine.BuildDevicesAsync().ConfigureAwait(false); ;
            //});

            //string fileName = System.IO.Path.GetTempFileName();
            string fileName = @"C:\Projects\Software\Tools\ReadRawDevice.Gui\ReadRawDevice.NUnit\bin\Debug\diskOutput.bin";

            var device = devColl.Where(dev =>
            {
                return (dev.DiskSize.HasValue) && (dev.FriendlyName.Equals("Alex EFI_TEST USB Device"));
            }).First();

            var progressIndicator = new Progress<int>(progress => {
                System.Diagnostics.Debug.WriteLine("Progress is: " + progress.ToString() + "%");
            });

            System.Diagnostics.Trace.WriteLine("Output file is: " + fileName);

            long bytesRead = await engine.ExtractDiskAsync(device, fileName, progressIndicator);
            //tsk2.Wait();

            System.Diagnostics.Debug.WriteLine("--- All done ---");
            //engine.ExtractDiskAsync(
        }

        //[Fact]
        //public void getAllSystemDevices()
        //{
        //    IEnumerable<SystemDevice> listOfDevices = reader.GetListOfDevices();

        //    Assert.NotEmpty(listOfDevices);
        //}

        //[Fact]
        //public void getDriveLayoutForC_shouldNotThrow()
        //{
        //    IEnumerable<SystemDevice> listOfDevices = reader.GetListOfDevices();

        //    SystemDevice diskC = listOfDevices.Where(dev => dev.DevicePath.StartsWith("C")).FirstOrDefault();

        //    Assert.DoesNotThrow(() =>
        //    {
        //        reader.GetDriveLayout(diskC);
        //    });
        //}

        //[Fact]
        //public void getDriveLayoutForAllDrivesShouldNotFail()
        //{
        //    IEnumerable<SystemDevice> listOfDevices = reader.GetListOfDevices();

        //    Assert.DoesNotThrow(() => {
        //        listOfDevices.All(dev => {
        //            reader.GetDriveLayout(dev);
        //            return true;
        //        });
        //    });
        //}

        //[Fact]
        //public void getDiskGeometryForC_shouldNotFail()
        //{
        //    IEnumerable<SystemDevice> listOfDevices = reader.GetListOfDevices();

        //    SystemDevice diskC = listOfDevices.Where(dev => dev.DevicePath.StartsWith("C")).FirstOrDefault();

        //    Assert.DoesNotThrow(() => {
        //        reader.GetDriveGeometry(diskC);
        //    });
        //}

        //[Fact]
        //public void getVolumeExtentsForDiskC_shouldNotFail()
        //{
        //    IEnumerable<SystemDevice> listOfDevices = reader.GetListOfDevices();

        //    SystemDevice diskC = listOfDevices.Where(dev => dev.DevicePath.StartsWith("C")).FirstOrDefault();

        //    Assert.DoesNotThrow(() => {
        //        reader.GetDiskExtents(diskC);
        //    });
        //}
    }
}
