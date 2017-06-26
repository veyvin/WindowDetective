// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.WindowDetective
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VirtualDeviceLib
{
  public class WindowDetective
  {
    private readonly StringBuilder sbForFormDetectiveResult = new StringBuilder();
    private Rectangle m_area = Rectangle.Empty;
    private Point m_mousePosition = new Point(-10000, -20000);
    private Point oldPosition = Point.Empty;
    private string m_className;
    private string m_title;

    public IntPtr FormAtMouse { get; set; } = IntPtr.Zero;

    public string Title
    {
      get
      {
        return this.m_title;
      }
      set
      {
        this.m_title = value;
      }
    }

    public string ClassName
    {
      get
      {
        return this.m_className;
      }
      set
      {
        this.m_className = value;
      }
    }

    public Rectangle Area
    {
      get
      {
        return this.m_area;
      }
      set
      {
        this.m_area = value;
      }
    }

    public Point MousePosition
    {
      get
      {
        WindowsAPI.GetCursorPos(ref this.m_mousePosition);
        return this.m_mousePosition;
      }
      set
      {
        this.m_mousePosition = value;
      }
    }

    public Color ColorAtMouse { get; set; }

    public void Refresh()
    {
      WindowsAPI.GetCursorPos(ref this.m_mousePosition);
      this.oldPosition = this.m_mousePosition;
      this.FormAtMouse = WindowsAPI.WindowFromPoint(this.m_mousePosition);
      WindowsAPI.GetWindowText(this.FormAtMouse, out this.m_title);
      WindowsAPI.GetClassName(this.FormAtMouse, out this.m_className);
      WindowsAPI.GetWindowRect(this.FormAtMouse, out this.m_area);
      this.ColorAtMouse = Eye.GetPixelColor(this.m_mousePosition.X, this.m_mousePosition.Y);
    }

    public StringBuilder Result(bool refresh)
    {
      if (refresh)
        this.Refresh();
      this.sbForFormDetectiveResult.Remove(0, this.sbForFormDetectiveResult.Length);
      this.sbForFormDetectiveResult.Append("位置:" + (object) Control.MousePosition + " 颜色:{" + string.Format("R:{0} G:{1} B:{2}", (object) this.ColorAtMouse.R, (object) this.ColorAtMouse.G, (object) this.ColorAtMouse.B) + "}" + Environment.NewLine);
      this.sbForFormDetectiveResult.Append("标题:{" + this.m_title + "}" + Environment.NewLine);
      this.sbForFormDetectiveResult.Append("句柄:{" + this.FormAtMouse.ToString() + "}" + Environment.NewLine);
      this.sbForFormDetectiveResult.Append("类别:{" + this.m_className + "}" + Environment.NewLine);
      this.sbForFormDetectiveResult.Append("区域:" + (object) this.m_area + Environment.NewLine);
      this.sbForFormDetectiveResult.Append("鼠标形状:{" + WindowsAPI.GetCursorShape() + "}" + Environment.NewLine);
      return this.sbForFormDetectiveResult;
    }

    public StringBuilder Result()
    {
      return this.Result(true);
    }
  }
}
