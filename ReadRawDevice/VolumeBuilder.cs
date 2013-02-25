
namespace ReadRawDevice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ReadRawDevice.Core;
    using ReadRawDevice.Win32;

    /// <summary>
    /// Internal object responsible for 'building' <see cref="SystemVolume"/> objects
    /// </summary>
    internal class VolumeBuilder : Native
    {
        private CancellationToken token = CancellationToken.None;

        /// <summary>
        /// Fully builds this list of <see cref="SystemVolume"/> objects
        /// </summary>
        /// <param name="token">Cancellation token</param>
        /// <returns>Collection of <see cref="SystemVolume"/> objects</returns>
        internal VolumesCollection Build(CancellationToken token)
        {
            IEnumerable<SystemVolume> listOfDevices = GetListOfDevices();

            token.ThrowIfCancellationRequested();

            var tmpListOfDevices = AnalyzeDevices(listOfDevices, token);

            token.ThrowIfCancellationRequested();

            return new VolumesCollection(tmpListOfDevices);
        }

        /// <summary>
        /// Gets the list of devices.
        /// </summary>
        /// <returns>List of system devices</returns>
        internal IEnumerable<SystemVolume> GetListOfDevices()
        {
            IEnumerable<string> volumeNames = base.GetVolumesNames();
            IDictionary<string, string> devicesNames = base.GetDeviceNames(volumeNames);
            IDictionary<string, string> volumePaths = base.GetVolumesNamesPaths(volumeNames);

            var listOfDevices = from volName in volumeNames.ToList()
                                select new SystemVolume(volName, devicesNames[volName], volumePaths[volName]);
            return listOfDevices;
        }


        /// <summary>
        /// Deeply analyzes the devices and set apropiate data on them
        /// </summary>
        /// <param name="listOfDevices">The list of devices.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>List of fully initialized devices</returns>
        internal IList<SystemVolume> AnalyzeDevices(IEnumerable<SystemVolume> listOfDevices, CancellationToken token)
        {
            List<SystemVolume> tmpListOfDevices = new List<SystemVolume>(listOfDevices);
            Task[] listOfTask = new Task[3];

            foreach (SystemVolume singleDevice in tmpListOfDevices)
            {
                listOfTask[0] = GetVolumeDiskExtents(singleDevice, token)
                    .ContinueWith(tsk => {
                        singleDevice.SetDiskExtentInfo(tsk.Result);
                    }
                    , token,  TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);


                listOfTask[1] = GetDriveLayout(singleDevice, token)
                    .ContinueWith(tsk => {
                        singleDevice.SetDriveLayoutInfo(tsk.Result);
                    }
                    , token, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);

                //listOfTask[2] = GetDriveGeometry(singleDevice, token)
                //    .ContinueWith(tsk =>{
                //        singleDevice.SetDriveGeometry(tsk.Result);
                //    }
                //    , token, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);

                listOfTask[2] = SetVolumeInformation(singleDevice, token);

                Task.WaitAll(listOfTask, token);
            }

            return tmpListOfDevices;
        }

        /// <summary>
        /// Task will query provided device for 'Volume extents' function
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Task that result in Nullable <see cref="VOLUME_DISK_EXTENTS"/> structure</returns>
        protected Task<Nullable<VOLUME_DISK_EXTENTS>> GetVolumeDiskExtents(SystemVolume device, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.Factory.StartNew<Nullable<VOLUME_DISK_EXTENTS>>(() =>
            {
                VOLUME_DISK_EXTENTS? diskExtents;
                try
                {
                    diskExtents = base.GetDiskExtendsForVolume(device);
                    return diskExtents;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            , token);
        }

        /// <summary>
        /// Task will query provided device for 'Drive layout' function
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Task that result in Nullable <see cref="DRIVE_LAYOUT_INFORMATION_EX"/> structure</returns>
        protected Task<Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]>> GetDriveLayout(SystemVolume device, CancellationToken token)
        {
            return Task.Factory.StartNew<Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]>>(() =>
            {
                Tuple<DRIVE_LAYOUT_INFORMATION_EX, PARTITION_INFORMATION_EX[]> driveLayout;

                token.ThrowIfCancellationRequested();

                try
                {
                    driveLayout = base.GetDriveLayout(device);
                    return driveLayout;
                }
                catch(Exception )
                {
                    return null;
                }
            }
            , token);
        }

        ///// <summary>
        ///// Task will query provided device for 'Drive geometry' function
        ///// </summary>
        ///// <param name="device"><see cref="SystemDevice"/> to query</param>
        ///// <param name="token">Cancellation token</param>
        ///// <returns>Task that result in Nullable <see cref="DISK_GEOMETRY_EX"/> structure</returns>
        //protected Task<Nullable<DISK_GEOMETRY_EX>> GetDriveGeometry(SystemDevice device, CancellationToken token)
        //{
        //    return Task.Factory.StartNew<Nullable<DISK_GEOMETRY_EX>>(() =>
        //    {
        //        DISK_GEOMETRY_EX diskGeometry;

        //        token.ThrowIfCancellationRequested();

        //        try
        //        {
        //            diskGeometry = base.GetDriveGeometry(device);
        //            return diskGeometry;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //    , token);
        //}


        /// <summary>
        /// Sets the volume information.
        /// </summary>
        /// <param name="device"><see cref="SystemVolume"/> to query</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Task the will do the job</returns>
        protected Task SetVolumeInformation(SystemVolume device, CancellationToken token)
        {
            return Task.Factory.StartNew(() => {
                
                token.ThrowIfCancellationRequested();

                base.SetVolumeInformation(device);
            }
            , token);
        }
    }
}
