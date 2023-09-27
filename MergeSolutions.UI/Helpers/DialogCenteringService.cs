using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace MergeSolutions.UI.Helpers
{
    public class DialogCenteringService : IDisposable
    {
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_CALLWNDPROCRET = 12;
        private readonly IntPtr _hHook;
        private readonly IWin32Window _owner;

        public DialogCenteringService(IWin32Window owner)
        {
            _owner = owner;
            _hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, DialogHookProc, IntPtr.Zero, GetCurrentThreadId());
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr idHook);

        public void Dispose()
        {
            UnhookWindowsHookEx(_hHook);
        }

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            SetWindowPosFlags uFlags);

        private void CenterWindow(IntPtr hChildWnd)
        {
            var recChild = new Rectangle(0, 0, 0, 0);
            var success = GetWindowRect(hChildWnd, ref recChild);

            if (!success)
            {
                return;
            }

            var width = recChild.Width - recChild.X;
            var height = recChild.Height - recChild.Y;

            var recParent = new Rectangle(0, 0, 0, 0);
            success = GetWindowRect(_owner.Handle, ref recParent);

            if (!success)
            {
                return;
            }

            var ptCenter = new Point
            {
                X = recParent.X + (recParent.Width - recParent.X) / 2,
                Y = recParent.Y + (recParent.Height - recParent.Y) / 2
            };


            var ptStart = new Point
            {
                X = ptCenter.X - width / 2,
                Y = ptCenter.Y - height / 2
            };

            Task.Factory.StartNew(() => SetWindowPos(hChildWnd, (IntPtr) 0, ptStart.X, ptStart.Y, width, height,
                SetWindowPosFlags.SWP_ASYNCWINDOWPOS | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOACTIVATE |
                SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_NOZORDER));
        }

        private IntPtr DialogHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(_hHook, nCode, wParam, lParam);
            }

            var msg = (CWPRETSTRUCT) Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT))!;

            if (msg.message is not (int) CbtHookAction.HCBT_ACTIVATE)
            {
                return CallNextHookEx(_hHook, nCode, wParam, lParam);
            }

            try
            {
                CenterWindow(msg.hwnd);
            }
            finally
            {
                UnhookWindowsHookEx(_hHook);
            }

            return CallNextHookEx(_hHook, nCode, wParam, lParam);
        }

        #region Nested Type: CbtHookAction

        private enum CbtHookAction
        {
            HCBT_ACTIVATE = 5,
        }

        #endregion

        #region Nested Type: CWPRETSTRUCT

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };

        #endregion
    }

    [Flags]
    public enum SetWindowPosFlags : uint
    {
        SWP_ASYNCWINDOWPOS = 0x4000,
        SWP_NOACTIVATE = 0x0010,
        SWP_NOOWNERZORDER = 0x0200,
        SWP_NOSIZE = 0x0001,
        SWP_NOZORDER = 0x0004,
    }
}