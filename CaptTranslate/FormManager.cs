using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptTranslate
{
    internal abstract class FormManager
    {
        private static Form _instance;
        public static Action AfterClose;
        private static void FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
            AfterClose?.Invoke();
        }

        public static void OpenForm<T>() where T : Form, new()
        {
            if(AfterClose != null)
            {
                AfterClose -= AfterClose;
            }
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new T();
                _instance.FormClosed += FormClosed;
                _instance.Show();
            }
            else
            {
                _instance.BringToFront();
            }
        }
    }
}
