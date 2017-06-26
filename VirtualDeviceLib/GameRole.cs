// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.GameRole
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;

namespace VirtualDeviceLib
{
  [Serializable]
  public class GameRole
  {
    public string Name { get; set; }

    public bool IsAttackable { get; set; }

    public override string ToString()
    {
      return string.Format("{{Name:{0},IsAttackable:{1}}}", (object) this.Name, (object) this.IsAttackable);
    }
  }
}
