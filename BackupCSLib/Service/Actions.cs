using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCSLib.Service
{
    public struct Actions
    {
        public required Action<string> SetProjectName { get; init; }
        public required Action<string> SetActionName { get; init; }
        public required Action<int, int> InitProgressBar { get; init; }
        public required Action<int> SetProgressBar { get; init; }
        public required Action InitDuraion { get; init; }
        public required Action<string> SetDuration { get; init; }
        public required Action<string> AddTotalLine { get; init; }
    }
}
