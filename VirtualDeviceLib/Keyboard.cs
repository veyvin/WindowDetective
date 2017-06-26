// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.Keyboard
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Collections.Generic;
using System.Threading;

namespace VirtualDeviceLib
{
  public static class Keyboard
  {
    private static readonly List<EnumKeyboardKey> m_downKeys = new List<EnumKeyboardKey>();
    private const int WM_KeyDown = 256;
    private const int WM_KeyUp = 257;
    private const int WM_SysKeyDown = 260;
    private const int WM_SysKeyUp = 261;

    public static List<EnumKeyboardKey> DownKeys
    {
      get
      {
        if (Keyboard.m_downKeys == null)
          throw new VirtualDeviceLibException("m_downKeys为null，疑为编码错误。");
        return Keyboard.m_downKeys;
      }
    }

    public static bool IsKeyDown(EnumKeyboardKey key)
    {
      return Keyboard.m_downKeys.Contains(key);
    }

    public static void KeyPress(IntPtr hWnd, EnumKeyboardKey key)
    {
      WindowsAPI.PostMessage(hWnd, 256, (IntPtr) ((long) key), (IntPtr) Keyboard.MakeKeyLparam(key, true));
      Thread.Sleep(80);
      WindowsAPI.PostMessage(hWnd, 257, (IntPtr) ((long) key), (IntPtr) Keyboard.MakeKeyLparam(key, false));
    }

    public static void KeyPress(IntPtr hWnd, EnumKeyboardKey key, int interval)
    {
      WindowsAPI.PostMessage(hWnd, 256, (IntPtr) ((long) key), (IntPtr) Keyboard.MakeKeyLparam(key, true));
      Thread.Sleep(interval);
      WindowsAPI.PostMessage(hWnd, 257, (IntPtr) ((long) key), (IntPtr) Keyboard.MakeKeyLparam(key, false));
    }

    public static void KeyDown(IntPtr hWnd, EnumKeyboardKey key)
    {
      WindowsAPI.PostMessage(hWnd, 256, (IntPtr) ((long) key), (IntPtr) Keyboard.MakeKeyLparam(key, true));
      if (!Keyboard.m_downKeys.Contains(key))
        return;
      Keyboard.m_downKeys.Add(key);
    }

    public static void KeyUp(IntPtr hWnd, EnumKeyboardKey key)
    {
      WindowsAPI.PostMessage(hWnd, 257, (IntPtr) ((long) key), (IntPtr) Keyboard.MakeKeyLparam(key, false));
      if (!Keyboard.m_downKeys.Contains(key))
        return;
      Keyboard.m_downKeys.Remove(key);
    }

    private static int MakeKeyLparam(EnumKeyboardKey VirtualKey, bool keyDown)
    {
      string empty1 = string.Empty;
      string str1 = !keyDown ? 192.ToString() : "00";
      int num = (int) WindowsAPI.MapVirtualKey(VirtualKey, 0);
      string empty2 = string.Empty;
      string str2 = "00" + (object) num;
      string str3 = str2.Substring(str2.Length - 2, 2);
      string s = str1 + str3 + "0001";
      int result = 0;
      int.TryParse(s, out result);
      return result;
    }
  }
}
