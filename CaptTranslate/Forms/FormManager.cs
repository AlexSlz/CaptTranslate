using System;
using System.Windows.Forms;

namespace CaptTranslate;

internal static class FormManager
{
    private static Form _instance;
    public static Action AfterClose;
    private static void FormClosed(object sender, FormClosedEventArgs e)
    {
        if (sender is Form form)
        {
            form.FormClosed -= FormClosed;
            form.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        _instance = null;
        AfterClose?.Invoke();
        AfterClose = null;
    }

    public static void OpenForm<T>() where T : Form, new()
    {
        if (_instance == null || _instance.IsDisposed)
        {
            _instance = new T();
            _instance.FormClosed += FormClosed;
            _instance.Show();
        }
        _instance.BringToFront();            
        _instance.Activate();
    }
}

