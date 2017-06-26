// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.Scene
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VirtualDeviceLib
{
  [Serializable]
  public class Scene
  {
    private const string configPath = "sceneconfig\\";
    private static XmlSerializer xmlS;

    public EnumSceneName Name { get; set; }

    public List<GameRole> GameRoles { get; set; }

    public List<Scene> ReachableScenes { get; set; }

    public override string ToString()
    {
      string str1 = string.Empty + "{Name:" + (object) this.Name;
      if (this.GameRoles.Count > 0)
        str1 = str1 + ",GameRoles:[" + (object) this.GameRoles[0];
      for (int index = 1; index < this.GameRoles.Count; ++index)
        str1 = str1 + "," + (object) this.GameRoles[index];
      if (this.GameRoles.Count > 0)
        str1 += "]";
      string str2 = str1 + "]";
      if (this.ReachableScenes.Count > 0)
        str2 = str2 + ",ReachableScenes:[{" + (object) this.ReachableScenes[0].Name + "}";
      for (int index = 1; index < this.ReachableScenes.Count; ++index)
        str2 = str2 + ",{" + (object) this.ReachableScenes[index].Name + "}";
      if (this.ReachableScenes.Count > 0)
        str2 += "]";
      return str2 + "}";
    }

    public static Scene GetScene(EnumSceneName scene_name)
    {
      if (Scene.xmlS == null)
        Scene.xmlS = new XmlSerializer(typeof (Scene));
      Scene scene = (Scene) null;
      try
      {
        using (FileStream fileStream = new FileStream("sceneconfig\\" + (object) scene_name + ".xml", FileMode.Open))
          scene = Scene.xmlS.Deserialize((Stream) fileStream) as Scene;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      return scene;
    }
  }
}
