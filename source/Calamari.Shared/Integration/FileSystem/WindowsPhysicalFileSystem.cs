﻿using System.Runtime.InteropServices;

namespace Calamari.Integration.FileSystem
{
    public class WindowsPhysicalFileSystem : CalamariPhysicalFileSystem
    {
        public WindowsPhysicalFileSystem()
        {
#if USE_ALPHAFS_FOR_LONG_FILE_PATH_SUPPORT
            File = new LongPathsFile();
            Directory = new LongPathsDirectory();
#endif
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        protected override bool GetDiskFreeSpace(string directoryPath, out ulong totalNumberOfFreeBytes)
        {
            ulong freeBytesAvailable;
            ulong totalNumberOfBytes;

            return GetDiskFreeSpaceEx(directoryPath, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
        }
    }
}
