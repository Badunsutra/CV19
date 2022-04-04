using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CV19.ViewModels
{
    internal class DirectoryViewModel : Base.ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                        .EnumerateDirectories()
                        .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                        .EnumerateFiles()
                        .Select(file_info => new FileViewModel(file_info.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<FileViewModel>();
            }
        }

        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories
                        .Cast<object>()
                        .Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }

        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string Path) => _DirectoryInfo = new DirectoryInfo(Path);
    }

    internal class FileViewModel : Base.ViewModel
    {
        private readonly FileInfo _FileInfo;

        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreationTime => _FileInfo.CreationTime;

        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);
    }
}
