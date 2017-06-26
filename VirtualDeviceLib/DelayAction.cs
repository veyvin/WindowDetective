// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.DelayAction
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace VirtualDeviceLib
{
  [Serializable]
  public sealed class DelayAction : VirtualDeviceAction
  {
    private int m_delayInterval;

    public int DelayInterval
    {
      get
      {
        if (this.m_delayInterval < 0)
          return 0;
        return this.m_delayInterval;
      }
      set
      {
        if (value < 0)
          return;
        this.m_delayInterval = value;
      }
    }

    public DelayAction(int delay_interval)
      : base(EnumVirtualDeviceActionType.Delay)
    {
      this.DelayInterval = delay_interval;
    }

    public DelayAction()
      : base(EnumVirtualDeviceActionType.Delay)
    {
      this.m_delayInterval = 1000;
    }

    public override string ToString()
    {
      if (this.m_actionType != EnumVirtualDeviceActionType.Delay)
        return "{VirtualDeviceException:DelayAction的实例被赋予了{" + (object) this.m_actionType + "}的动作类型}";
      return "{" + base.ToString() + ", DelayInterval:" + (object) this.DelayInterval + "}";
    }

    public override void Execute()
    {
      if (this.m_actionType != EnumVirtualDeviceActionType.Delay)
        throw new VirtualDeviceLibException("DelayAction的实例被赋予了{" + (object) this.m_actionType + "}的动作类型");
      Thread.Sleep(this.DelayInterval);
    }

    public override bool Save(string fileName, EnumSaveType save_type)
    {
      try
      {
        if (save_type == EnumSaveType.BINARY)
        {
          if (!fileName.ToLower().EndsWith(".vdaction"))
            fileName += ".vdaction";
          using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            new BinaryFormatter().Serialize((Stream) fileStream, (object) this);
        }
        return true;
      }
      catch (VirtualDeviceLibException ex)
      {
        return false;
      }
    }

    public override bool Save(Stream stream, EnumSaveType save_type)
    {
      throw new NotImplementedException();
    }
  }
}
