// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.VirtualDeviceLibException
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;

namespace VirtualDeviceLib
{
  public class VirtualDeviceLibException : Exception
  {
    private const string LibError = "{VirtualDeviceLibException:";

    public override string Message
    {
      get
      {
        return "{VirtualDeviceLibException:" + base.Message + "}";
      }
    }

    public VirtualDeviceLibException(string message)
      : base(message)
    {
    }
  }
}
