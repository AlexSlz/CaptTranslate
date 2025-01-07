using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaptTranslate
{
    internal class HotKeyManager
    {
        // Импортируем функции из Windows API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        public static extern int UnregisterHotKey(IntPtr hWnd, int id);

        public const int MOD_CTRL = 0x0002;  // Ctrl
        public const int MOD_SHIFT = 0x0004; // Shift
        public const int MOD_ALT = 0x0001;   // Alt

        private readonly Dictionary<int, Action> _actions;

        private IntPtr _handle;
        public HotKeyManager(IntPtr handle)
        {
            _actions = new Dictionary<int, Action>();
            _handle = handle;
        }
        public void AddHotKey(int modifier, int key, Action action)
        {
            int id = _actions.Count + 1;
            if(Register(id, modifier, key))
            {
                _actions.Add(id, action);
            }
            else
            {
                MessageBox.Show("Reg error.");
            }
        }

        public void UpdateHotKey(int modifier, int key, Action action)
        {
            int id = _actions.First(item => item.Value == action).Key;
            Unregister(id);
            Register(id, modifier, key);
        }

        public void ProcessHotKey(int hotKeyId)
        {
            if (_actions.ContainsKey(hotKeyId))
            {
                _actions[hotKeyId]?.Invoke();
            }
        }

        public bool Register(int id, int modifier, int key)
        {
            return RegisterHotKey(_handle, id, modifier, key) != 0;
        }
        public bool Unregister(int id)
        {
            return UnregisterHotKey(_handle, id) != 0;
        }
        public void Dispose()
        {
            foreach (var hotKey in _actions)
            {
                Unregister(hotKey.Key);
            }
        }
    }
}
