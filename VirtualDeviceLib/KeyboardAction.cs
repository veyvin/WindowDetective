// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.KeyboardAction
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualDeviceLib
{
  [Serializable]
  public sealed class KeyboardAction : VirtualDeviceAction
  {
    private EnumKeyboardKey m_keyboardKey;

    public EnumKeyboardKey KeyboardKey
    {
      get
      {
        return this.m_keyboardKey;
      }
      set
      {
        this.m_keyboardKey = value;
      }
    }

    public KeyboardAction(EnumVirtualDeviceActionType action_type, EnumKeyboardKey keyboardKey)
      : base(action_type)
    {
      this.m_keyboardKey = keyboardKey;
    }

    public KeyboardAction()
      : base(EnumVirtualDeviceActionType.KeyPress)
    {
      this.m_keyboardKey = EnumKeyboardKey.Z;
    }

    public override string ToString()
    {
      if (this.m_actionType != EnumVirtualDeviceActionType.KeyDown && this.m_actionType != EnumVirtualDeviceActionType.KeyUp && this.m_actionType != EnumVirtualDeviceActionType.KeyPress)
        return "{VirtualDeviceException:KeyboardAction的实例被赋予了{" + (object) this.m_actionType + "}的动作类型}";
      return "{" + base.ToString() + ", KeyboardKey:" + (object) this.m_keyboardKey + "}";
    }

    public override void Execute()
    {
      if (this.m_actionType == EnumVirtualDeviceActionType.KeyDown)
        WindowsAPI.keybd_event(this.m_keyboardKey, WindowsAPI.MapVirtualKey(this.m_keyboardKey, 0), 0U, 0U);
      else if (this.m_actionType == EnumVirtualDeviceActionType.KeyUp)
      {
        WindowsAPI.keybd_event(this.m_keyboardKey, WindowsAPI.MapVirtualKey(this.m_keyboardKey, 0), 2U, 0U);
      }
      else
      {
        if (this.m_actionType != EnumVirtualDeviceActionType.KeyPress)
          throw new VirtualDeviceLibException("KeyboardAction的实例被赋予了{" + (object) this.m_actionType + "}的动作类型");
        WindowsAPI.keybd_event(this.m_keyboardKey, WindowsAPI.MapVirtualKey(this.m_keyboardKey, 0), 0U, 0U);
        WindowsAPI.keybd_event(this.m_keyboardKey, WindowsAPI.MapVirtualKey(this.m_keyboardKey, 0), 2U, 0U);
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
      throw new NotImplementedException();
    }
  }
}
