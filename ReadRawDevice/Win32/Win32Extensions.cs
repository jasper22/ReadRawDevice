
namespace ReadRawDevice.Win32
{
    internal static class Win32Extensions
    {
        /// <summary>
        /// Converts <see cref="PARTITION_STYLE"/> enumerator to string.
        /// </summary>
        /// <param name="partitionStyle">The partition style as <see cref="PARTITION_STYLE"/></param>
        /// <returns>String representation of current enumerator</returns>
        internal static string ConvertToString(this PARTITION_STYLE partitionStyle)
        {
            string style = string.Empty;
            switch (partitionStyle)
            {
                case PARTITION_STYLE.PARTITION_STYLE_GPT:
                    style = "GPT";
                    break;
                case PARTITION_STYLE.PARTITION_STYLE_MBR:
                    style = "MBR";
                    break;
                case PARTITION_STYLE.PARTITION_STYLE_RAW:
                    style = "RAW";
                    break;
                default:
                    style = "Unknown 'PARTITION_STYLE'. Raw value: " + ((uint)partitionStyle).ToString();
                    break;
            }

            return style;
        }

        /// <summary>
        /// Converts <see cref="DiskPartitionType"/> to string.
        /// </summary>
        /// <param name="diskPartitionType">Value of <see cref="DiskPartitionType"/> enumerator</param>
        /// <returns>String representation of enum</returns>
        internal static string ConvertToString(this DiskPartitionType diskPartitionType)
        {
            string partType = string.Empty;
            switch (diskPartitionType)
            {
                case DiskPartitionType.PARTITION_ENTRY_UNUSED:
                    partType = "Unused";
                    break;
                case DiskPartitionType.PARTITION_EXTENDED:
                    partType = "Extended";
                    break;
                case DiskPartitionType.PARTITION_FAT_12:
                    partType = "FAT-12";
                    break;
                case DiskPartitionType.PARTITION_FAT_16:
                    partType = "FAT-16";
                    break;
                case DiskPartitionType.PARTITION_FAT32:
                    partType = "FAT-32";
                    break;
                case DiskPartitionType.PARTITION_HUGE:
                    partType = "Huge";
                    break;
                case DiskPartitionType.PARTITION_IFS:
                    partType = "IFS (Installable File System)";
                    break;
                case DiskPartitionType.PARTITION_LDM:
                    partType = "LDM";
                    break;
                case DiskPartitionType.PARTITION_NTFT:
                    partType = "NTFT";
                    break;
                case DiskPartitionType.PARTITION_PREP:
                    partType = "PREP (Power PC Reference Platform)";
                    break;
                case DiskPartitionType.PARTITION_UNIX:
                    partType = "Unix";
                    break;
                case DiskPartitionType.PARTITION_XENIX_1:
                    partType = "Xenix 1";
                    break;
                case DiskPartitionType.PARTITION_XENIX_2:
                    partType = "Xenix 2";
                    break;
                case DiskPartitionType.PARTITION_XINT13:
                    partType = "XINT13";
                    break;
                case DiskPartitionType.PARTITION_XINT13_EXTENDED:
                    partType = "XINT13 Extended";
                    break;
                case DiskPartitionType.VALID_NTFT:
                    partType = "Valid NTFT";
                    break;
                default:
                    partType = "Unknown partition type. Raw value is: " + ((byte)diskPartitionType).ToString();
                    break;
            }

            return partType;
        }
    }
}
