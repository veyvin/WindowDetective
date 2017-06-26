// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.MouseAction
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualDeviceLib
{
  [Serializable]
  public sealed class MouseAction : VirtualDeviceAction
  {
    private int x;
    private int y;

    public int PointX
    {
      get
      {
        return this.x;
      }
      set
      {
        this.x = value;
      }
    }

    public int PointY
    {
      get
      {
        return this.y;
      }
      set
      {
        this.y = value;
      }
    }

    public int ScrollInterval
    {
      get
      {
        return this.x;
      }
      set
      {
        this.x = value;
      }
    }

    public MouseAction(EnumVirtualDeviceActionType action_type, int ptX, int ptY)
      : base(action_type)
    {
      if (action_type != EnumVirtualDeviceActionType.Move && action_type != EnumVirtualDeviceActionType.MoveTo)
        return;
      this.x = ptX;
      this.y = ptY;
    }

    public MouseAction()
      : base(EnumVirtualDeviceActionType.LeftClick)
    {
      this.x = 0;
      this.y = 0;
    }

    public MouseAction(int scroll_interval)
      : base(EnumVirtualDeviceActionType.Scroll)
    {
      this.x = scroll_interval;
    }

    public override string ToString()
    {
      if (this.m_actionType == EnumVirtualDeviceActionType.Scroll)
        return "{" + base.ToString() + ", ScrollInterval:" + (object) this.x + "}";
      if (this.m_actionType == EnumVirtualDeviceActionType.Move || this.m_actionType == EnumVirtualDeviceActionType.MoveTo)
        return "{" + base.ToString() + ", PointX:" + (object) this.x + ", PointY:" + (object) this.y + "}";
      if (this.m_actionType == EnumVirtualDeviceActionType.LeftClick || this.m_actionType == EnumVirtualDeviceActionType.LeftDown || (this.m_actionType == EnumVirtualDeviceActionType.LeftUp || this.m_actionType == EnumVirtualDeviceActionType.RightClick) || (this.m_actionType == EnumVirtualDeviceActionType.RightDown || this.m_actionType == EnumVirtualDeviceActionType.RightUp || (this.m_actionType == EnumVirtualDeviceActionType.MiddleClick || this.m_actionType == EnumVirtualDeviceActionType.MiddleDown)) || this.m_actionType == EnumVirtualDeviceActionType.MiddleUp)
        return "{" + base.ToString() + ", PointX:Current, PointY:Current}";
      return "{VirtualDeviceException:MouseAction的实例被赋予了{" + (object) this.m_actionType + "}的动作类型}";
    }

    public override void Execute()
    {
      if (this.m_actionType == EnumVirtualDeviceActionType.Scroll)
        WindowsAPI.mouse_event(this.m_actionType, 0, 0, this.x, UIntPtr.Zero);
      else if (this.m_actionType == EnumVirtualDeviceActionType.MoveTo || this.m_actionType == EnumVirtualDeviceActionType.Move)
      {
        WindowsAPI.mouse_event(this.m_actionType, this.x, this.y, 0, UIntPtr.Zero);
      }
      else
      {
        if (this.m_actionType == EnumVirtualDeviceActionType.KeyDown || this.m_actionType == EnumVirtualDeviceActionType.KeyUp || this.m_actionType == EnumVirtualDeviceActionType.KeyPress)
          throw new VirtualDeviceLibException("一个鼠标动作MouseAction的实例被赋予了其他动作{" + (object) this.m_actionType + "}的类型");
        WindowsAPI.mouse_event(this.m_actionType, 0, 0, 0, UIntPtr.Zero);
      }
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
      try
      {
        if (save_type == EnumSaveType.BINARY)
          new BinaryFormatter().Serialize(stream, (object) this);
        return true;
      }
      catch (VirtualDeviceLibException ex)
      {
        return false;
      }
    }
  }
}
