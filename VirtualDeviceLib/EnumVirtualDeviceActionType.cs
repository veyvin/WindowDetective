// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.EnumVirtualDeviceActionType
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

namespace VirtualDeviceLib
{
  public enum EnumVirtualDeviceActionType
  {
    Move = 1,
    LeftDown = 2,
    LeftUp = 4,
    LeftClick = 6,
    RightDown = 8,
    RightUp = 16,
    RightClick = 24,
    MiddleDown = 32,
    MiddleUp = 64,
    MiddleClick = 96,
    Scroll = 2048,
    KeyPress = 30467,
    MoveTo = 32769,
    Delay = 33024,
    KeyDown = 33280,
    KeyUp = 33536,
  }
}
