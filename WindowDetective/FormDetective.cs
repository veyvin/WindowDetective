// Decompiled with JetBrains decompiler
// Type: WindowDetective.FormDetective
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowDetective
{
  public class FormDetective : Form
  {
    private readonly VirtualDeviceLib.WindowDetective m_windowDetective = new VirtualDeviceLib.WindowDetective();
    private IContainer components;
    private Point ptMouse;
    private Timer tmrDetect;
    private TextBox txtWorkReport;
    private int waitCount;

    public FormDetective()
    {
      this.InitializeComponent();
    }

    private void tmrDetect_Tick(object sender, EventArgs e)
    {
      if (this.ptMouse == this.m_windowDetective.MousePosition)
      {
        this.waitCount = this.waitCount + 1;
        if (this.waitCount < 10)
        {
          this.Text = this.Text + ".";
          return;
        }
        this.waitCount = 0;
        this.Text = "窗口侦探";
      }
      this.ptMouse = this.m_windowDetective.MousePosition;
      this.txtWorkReport.Text = this.m_windowDetective.Result().ToString();
    }

    private void txtWorkReport_MouseEnter(object sender, EventArgs e)
    {
      this.tmrDetect.Stop();
    }

    private void txtWorkReport_MouseLeave(object sender, EventArgs e)
    {
      this.tmrDetect.Start();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDetective));
      this.txtWorkReport = new TextBox();
      this.tmrDetect = new Timer(this.components);
      this.SuspendLayout();
      this.txtWorkReport.Dock = DockStyle.Fill;
      this.txtWorkReport.Location = new Point(0, 0);
      this.txtWorkReport.Margin = new Padding(0);
      this.txtWorkReport.Multiline = true;
      this.txtWorkReport.Name = "txtWorkReport";
      this.txtWorkReport.Size = new Size(284, 122);
      this.txtWorkReport.TabIndex = 0;
      this.txtWorkReport.MouseEnter += new EventHandler(this.txtWorkReport_MouseEnter);
      this.txtWorkReport.MouseLeave += new EventHandler(this.txtWorkReport_MouseLeave);
      this.tmrDetect.Enabled = true;
      this.tmrDetect.Tick += new EventHandler(this.tmrDetect_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 122);
      this.Controls.Add((Control) this.txtWorkReport);
      this.MaximizeBox = false;
      this.Name = "FormDetective";
      this.Text = "窗口侦探";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
