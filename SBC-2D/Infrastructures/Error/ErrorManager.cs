using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Infrastructures.Error
{
    public class ErrorManager
    {
        private readonly ConcurrentDictionary<ErrorCode, ErrorEntry> _activeErrors
            = new ConcurrentDictionary<ErrorCode, ErrorEntry>();

        public event Action<ErrorEntry> ErrorRaised;
        public event Action<ErrorCode> ErrorCleared;

        public IReadOnlyCollection<ErrorEntry> ActiveErrors => _activeErrors.Values.ToList().AsReadOnly();

        public void Report(ErrorCode code, string message)
        {
            if (code == ErrorCode.NoError) return;
            var entry = new ErrorEntry(code, message ?? string.Empty);
            _activeErrors[code] = entry;
            try { ErrorRaised?.Invoke(entry); } catch { }
        }

        public void Clear(ErrorCode code)
        {
            if (_activeErrors.TryRemove(code, out _))
            {
                try { ErrorCleared?.Invoke(code); } catch { }
            }
        }

        public void ClearAll()
        {
            var keys = _activeErrors.Keys.ToArray();
            foreach (var k in keys) Clear(k);
        }

        public bool HasError(ErrorCode code)
            => _activeErrors.ContainsKey(code);
    }
}