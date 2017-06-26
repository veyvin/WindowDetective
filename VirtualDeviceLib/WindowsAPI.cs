// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.WindowsAPI
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VirtualDeviceLib
{
  public class WindowsAPI
  {
    public const uint KEYEVENTF_KEYUP = 2;
    public const uint KEYEVENTF_KEYDOWN = 0;
    private const int STRINGBUFFER_SizeConst = 512;

    [DllImport("gdi32.dll")]
    private static extern IntPtr DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobjBm);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    public static Bitmap GetDesktop()
    {
      IntPtr dc = WindowsAPI.GetDC(WindowsAPI.GetDesktopWindow());
      IntPtr compatibleDc = WindowsAPI.CreateCompatibleDC(dc);
      int systemMetrics1 = WindowsAPI.GetSystemMetrics(0);
      int systemMetrics2 = WindowsAPI.GetSystemMetrics(1);
      IntPtr compatibleBitmap = WindowsAPI.CreateCompatibleBitmap(dc, systemMetrics1, systemMetrics2);
      if (!(compatibleBitmap != IntPtr.Zero))
        return (Bitmap) null;
      IntPtr hgdiobjBm = WindowsAPI.SelectObject(compatibleDc, compatibleBitmap);
      WindowsAPI.BitBlt(compatibleDc, 0, 0, systemMetrics1, systemMetrics2, dc, 0, 0, 13369376);
      WindowsAPI.SelectObject(compatibleDc, hgdiobjBm);
      WindowsAPI.DeleteDC(compatibleDc);
      WindowsAPI.ReleaseDC(WindowsAPI.GetDesktopWindow(), dc);
      Bitmap bitmap = Image.FromHbitmap(compatibleBitmap);
      WindowsAPI.DeleteObject(compatibleBitmap);
      GC.Collect();
      return bitmap;
    }

    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

    [DllImport("user32.dll")]
    public static extern void keybd_event(EnumKeyboardKey bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern byte MapVirtualKey(EnumKeyboardKey wCode, int wMap);

    public static void KeyDown(EnumKeyboardKey key)
    {
      WindowsAPI.keybd_event(key, WindowsAPI.MapVirtualKey(key, 0), 0U, 0U);
    }

    public static void KeyUp(EnumKeyboardKey key)
    {
      WindowsAPI.keybd_event(key, WindowsAPI.MapVirtualKey(key, 0), 2U, 0U);
    }

    public static void KeyPress(EnumKeyboardKey key)
    {
      WindowsAPI.keybd_event(key, WindowsAPI.MapVirtualKey(key, 0), 0U, 0U);
      Thread.Sleep(0);
      WindowsAPI.keybd_event(key, WindowsAPI.MapVirtualKey(key, 0), 2U, 0U);
    }

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32")]
    public static extern void mouse_event(EnumVirtualDeviceActionType dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, StringBuilder lpszClass, StringBuilder lpszWindow);

    [DllImport("user32.dll")]
    public static extern int EnumWindows(WindowsAPI.EnumWindowsProc ewp, int lParam);

    [DllImport("user32.dll")]
    public static extern int EnumChildWindows(IntPtr hWndParent, WindowsAPI.CallBack lpfn, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetWindowText(IntPtr hWnd, out WindowsAPI.STRINGBUFFER text, int nMaxCount);

    public static int GetWindowText(IntPtr hWnd, out string text)
    {
      WindowsAPI.STRINGBUFFER text1 = new WindowsAPI.STRINGBUFFER();
      int windowText = WindowsAPI.GetWindowText(hWnd, out text1, 512);
      text = text1.szText;
      return windowText;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetClassName(IntPtr hWnd, out WindowsAPI.STRINGBUFFER lpString, int nMaxCount);

    public static int GetClassName(IntPtr hWnd, out string lpString)
    {
      WindowsAPI.STRINGBUFFER lpString1 = new WindowsAPI.STRINGBUFFER();
      int className = WindowsAPI.GetClassName(hWnd, out lpString1, 512);
      lpString = lpString1.szText;
      return className;
    }

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out WindowsAPI.Rect lpRect);

    public static bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect)
    {
      WindowsAPI.Rect lpRect1 = new WindowsAPI.Rect();
      WindowsAPI.GetWindowRect(hWnd, out lpRect1);
      lpRect = new Rectangle(lpRect1.Left, lpRect1.Top, lpRect1.Right - lpRect1.Left, lpRect1.Down - lpRect1.Top);
      return true;
    }

    [DllImport("user32.dll")]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessId);

    [DllImport("user32.dll")]
    public static extern int GetWindow(int hwnd, int wCmd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    public static extern int SetParent(int hWndChild, int hWndNewParent);

    [DllImport("user32.dll")]
    private static extern int GetCursorPos(ref WindowsAPI.POINT lpPoint);

    public static int GetCursorPos(ref Point lpPoint)
    {
      WindowsAPI.POINT lpPoint1 = new WindowsAPI.POINT();
      int cursorPos = WindowsAPI.GetCursorPos(ref lpPoint1);
      lpPoint.X = lpPoint1.X;
      lpPoint.Y = lpPoint1.Y;
      return cursorPos;
    }

    [DllImport("user32.dll")]
    public static extern bool SetCurorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(Point Point);

    [DllImport("user32.dll")]
    private static extern IntPtr GetCursor();

    [DllImport("user32.dll")]
    private static extern bool GetCursorInfo(out WindowsAPI.CURSORINFO pci);

    public static string GetCursorShape()
    {
      try
      {
        WindowsAPI.CURSORINFO pci = new WindowsAPI.CURSORINFO();
        pci.cbSize = Marshal.SizeOf<WindowsAPI.CURSORINFO>(pci);
        WindowsAPI.GetCursorInfo(out pci);
        if (pci.flags == 1)
          return new Cursor(pci.hCursor).ToString();
        return "(Hidden)";
      }
      catch (Exception ex)
      {
        return "(Failure)";
      }
    }

    public delegate bool CallBack(IntPtr pwnd, int lParam);

    public delegate bool EnumWindowsProc(IntPtr pHandle, int p_Param);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct STRINGBUFFER
    {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
      public readonly string szText;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct Rect
    {
      public readonly int Left;
      public readonly int Top;
      public readonly int Right;
      public readonly int Down;

      public override string ToString()
      {
        return "{Left:" + (object) this.Left + ", Up:" + (object) this.Top + ", RightPP:" + (object) this.Right + "DownPP:" + (object) this.Down + "}";
      }
    }

    public struct POINT
    {
      public int X;
      public int Y;
    }

    private struct CURSORINFO
    {
      public int cbSize;
      public int flags;
      public IntPtr hCursor;
      public WindowsAPI.POINT ptScreenPos;
    }
  }
}
