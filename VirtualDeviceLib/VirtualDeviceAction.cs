// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.VirtualDeviceAction
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualDeviceLib
{
  [Serializable]
  public abstract class VirtualDeviceAction
  {
    protected EnumVirtualDeviceActionType m_actionType;

    public EnumVirtualDeviceActionType ActionType
    {
      get
      {
        return this.m_actionType;
      }
      set
      {
        this.m_actionType = value;
      }
    }

    protected VirtualDeviceAction(EnumVirtualDeviceActionType action_type)
    {
      this.m_actionType = action_type;
    }

    public override string ToString()
    {
      return "ActionType:" + (object) this.ActionType;
    }

    public abstract void Execute();

    public bool Save(string fileName)
    {
      if (fileName == null || fileName == string.Empty)
        return false;
      return this.Save(fileName, EnumSaveType.BINARY);
    }

    public abstract bool Save(string fileName, EnumSaveType save_type);

    public bool Save(Stream stream)
    {
      return this.Save(stream, EnumSaveType.BINARY);
    }

    public abstract bool Save(Stream stream, EnumSaveType save_type);

    public static object FromFile(string fileName)
    {
      if (fileName == null || fileName == string.Empty)
        return (object) null;
      return VirtualDeviceAction.FromFile(fileName, EnumSaveType.BINARY);
    }

    public static object FromFile(string fileName, EnumSaveType save_type)
    {
      try
      {
        object obj = (object) null;
        if ((uint) save_type > 0U)
          return (object) null;
        if (!fileName.ToLower().EndsWith(".vdaction"))
          fileName += ".vdaction";
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
          obj = new BinaryFormatter().Deserialize((Stream) fileStream);
        return obj;
      }
      catch (VirtualDeviceLibException ex)
      {
        return (object) null;
      }
    }

    public static object FromFile(Stream stream)
    {
      return VirtualDeviceAction.FromFile(stream, EnumSaveType.BINARY);
    }

    public static object FromFile(Stream stream, EnumSaveType save_type)
    {
      try
      {
        if (save_type == EnumSaveType.BINARY)
          return new BinaryFormatter().Deserialize(stream);
        return (object) null;
      }
      catch (VirtualDeviceLibException ex)
      {
        return (object) null;
      }
    }

    public void Execute(bool hideExceptions)
    {
      if (hideExceptions)
      {
        try
        {
          this.Execute();
        }
        catch (VirtualDeviceLibException ex)
        {
        }
      }
      else
        this.Execute();
    }

    public static VirtualDeviceAction CreateAction(EnumVirtualDeviceActionType action_type, int ptX, int ptY)
    {
      if (action_type == EnumVirtualDeviceActionType.Delay || action_type == EnumVirtualDeviceActionType.KeyDown || (action_type == EnumVirtualDeviceActionType.KeyUp || action_type == EnumVirtualDeviceActionType.KeyPress) || action_type == EnumVirtualDeviceActionType.Scroll)
        throw new VirtualDeviceLibException("不匹配的动作类型CreateAction(action_type:" + (object) action_type + ", ptX:" + (object) ptX + ", ptY:" + (object) ptY + ")");
      return (VirtualDeviceAction) new MouseAction(action_type, ptX, ptY);
    }

    public static VirtualDeviceAction CreateAction(EnumVirtualDeviceActionType action_type, int interval)
    {
      if (action_type == EnumVirtualDeviceActionType.Delay)
        return (VirtualDeviceAction) new DelayAction(interval);
      if (action_type == EnumVirtualDeviceActionType.Scroll)
        return (VirtualDeviceAction) new MouseAction(interval);
      throw new VirtualDeviceLibException("不匹配的动作类型CreateAction(action_type:" + (object) action_type + ", interval:" + (object) interval + ")");
    }

    public static VirtualDeviceAction CreateAction(EnumVirtualDeviceActionType action_type, EnumKeyboardKey keyboardKey)
    {
      if (action_type == EnumVirtualDeviceActionType.KeyDown || action_type == EnumVirtualDeviceActionType.KeyUp || action_type == EnumVirtualDeviceActionType.KeyPress)
        return (VirtualDeviceAction) new KeyboardAction(action_type, keyboardKey);
      throw new VirtualDeviceLibException("不匹配的动作类型CreateAction(action_type:" + (object) action_type + ", keyboardKey:" + (object) keyboardKey + ")");
    }
  }
}
