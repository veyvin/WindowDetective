// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.Mouse
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Windows.Forms;

namespace VirtualDeviceLib
{
  public static class Mouse
  {
    private static readonly object synchObj = new object();
    private const int absoluteBound = 65535;

    public static bool IsLeftDown { get; private set; }

    public static bool IsRightDown { get; private set; }

    public static bool IsMiddleDown { get; private set; }

    public static bool MoveTo(int x, int y)
    {
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.MoveTo, x * (int) ushort.MaxValue / Screen.PrimaryScreen.Bounds.Width, y * (int) ushort.MaxValue / Screen.PrimaryScreen.Bounds.Height, 0, UIntPtr.Zero);
      return true;
    }

    public static void LeftClick()
    {
      if (Mouse.IsLeftDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.LeftClick, 0, 0, 0, UIntPtr.Zero);
    }

    public static void LeftDown()
    {
      if (Mouse.IsLeftDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.LeftDown, 0, 0, 0, UIntPtr.Zero);
      lock (Mouse.synchObj)
        Mouse.IsLeftDown = true;
    }

    public static void LeftUp()
    {
      if (!Mouse.IsLeftDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.LeftUp, 0, 0, 0, UIntPtr.Zero);
      lock (Mouse.synchObj)
        Mouse.IsLeftDown = false;
    }

    public static void RightClick()
    {
      if (Mouse.IsRightDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.RightClick, 0, 0, 0, UIntPtr.Zero);
    }

    public static void RightDown()
    {
      if (Mouse.IsRightDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.RightDown, 0, 0, 0, UIntPtr.Zero);
      lock (Mouse.synchObj)
        Mouse.IsRightDown = true;
    }

    public static void RightUp()
    {
      if (!Mouse.IsRightDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.RightUp, 0, 0, 0, UIntPtr.Zero);
      lock (Mouse.synchObj)
        Mouse.IsRightDown = false;
    }

    public static void MiddleClick()
    {
      if (Mouse.IsMiddleDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.MiddleClick, 0, 0, 0, UIntPtr.Zero);
    }

    public static void MiddleDown()
    {
      if (Mouse.IsMiddleDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.MiddleDown, 0, 0, 0, UIntPtr.Zero);
      Mouse.IsMiddleDown = true;
      lock (Mouse.synchObj)
        Mouse.IsMiddleDown = true;
    }

    public static void MiddleUp()
    {
      if (Mouse.IsMiddleDown)
        return;
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.MiddleUp, 0, 0, 0, UIntPtr.Zero);
      lock (Mouse.synchObj)
        Mouse.IsMiddleDown = false;
    }

    public static void Scroll(int interval)
    {
      WindowsAPI.mouse_event(EnumVirtualDeviceActionType.Scroll, 0, 0, interval, UIntPtr.Zero);
    }
  }
}
