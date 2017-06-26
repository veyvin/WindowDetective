// Decompiled with JetBrains decompiler
// Type: VirtualDeviceLib.Eye
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace VirtualDeviceLib
{
  public class Eye
  {
    private int m_horizontalGranularity;
    private int m_verticalGranularity;
    private Thread thrWatching;

    public int HorizontalGranularity
    {
      get
      {
        return this.m_horizontalGranularity;
      }
      set
      {
        if (0 >= value)
          return;
        this.m_horizontalGranularity = value;
      }
    }

    public int VerticalGranularity
    {
      get
      {
        return this.m_verticalGranularity;
      }
      set
      {
        if (0 >= value)
          return;
        this.m_verticalGranularity = value;
      }
    }

    public int WatchCycle { get; set; }

    public bool Started { get; protected set; }

    public Bitmap SignedPicture { get; set; }

    public Point RelativeOriginalPoint { get; set; }

    public string Title { get; set; }

    public Scene CurrentScene { get; protected set; }

    public Scene PreScene { get; protected set; }

    private void DoWatching()
    {
    }

    public bool StartWatching()
    {
      if (!this.Started)
      {
        this.thrWatching.Start();
        this.Started = true;
      }
      return this.Started;
    }

    public bool StopWatching()
    {
      if (this.Started)
      {
        this.thrWatching.Suspend();
        this.Started = false;
      }
      return this.Started;
    }

    public static Rectangle FindPicture(Bitmap bmpTarget)
    {
      return Eye.FindPicture(bmpTarget, SystemInformation.VirtualScreen, 0.1);
    }

    public static Rectangle FindPicture(Bitmap bmpTarget, Rectangle searchArea)
    {
      return Eye.FindPicture(bmpTarget, searchArea, 0.1);
    }

    public static Rectangle FindPicture(Bitmap bmpTarget, double tolerance)
    {
      return Eye.FindPicture(bmpTarget, SystemInformation.VirtualScreen, tolerance);
    }

    public static Rectangle FindPicture(Bitmap bmpTarget, Rectangle searchArea, double tolerance)
    {
      if (bmpTarget == null || 0.0 > tolerance || (tolerance > 1.0 || bmpTarget.Width <= 0) || (bmpTarget.Height <= 0 || bmpTarget.Width > searchArea.Width) || bmpTarget.Height > searchArea.Height)
        return Rectangle.Empty;
      Bitmap desktop = WindowsAPI.GetDesktop();
      if (desktop == null)
        return Rectangle.Empty;
      return Eye.searchBitmap(bmpTarget, desktop, tolerance);
    }

    public static Color GetPixelColor(int x, int y)
    {
      IntPtr dc = WindowsAPI.GetDC(IntPtr.Zero);
      uint pixel = WindowsAPI.GetPixel(dc, x, y);
      WindowsAPI.ReleaseDC(IntPtr.Zero, dc);
      return Color.FromArgb((int) pixel & (int) byte.MaxValue, ((int) pixel & 65280) >> 8, ((int) pixel & 16711680) >> 16);
    }

    private static unsafe Rectangle searchBitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
    {
      BitmapData bitmapdata1 = smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
      BitmapData bitmapdata2 = bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
      int stride1 = bitmapdata1.Stride;
      int stride2 = bitmapdata2.Stride;
      int width = bigBmp.Width;
      int num1 = bigBmp.Height - smallBmp.Height + 1;
      int num2 = smallBmp.Width * 3;
      int height = smallBmp.Height;
      Rectangle empty = Rectangle.Empty;
      int int32 = Convert.ToInt32((double) byte.MaxValue * tolerance);
      byte* numPtr1 = (byte*) (void*) bitmapdata1.Scan0;
      byte* numPtr2 = (byte*) (void*) bitmapdata2.Scan0;
      int num3 = stride1 - smallBmp.Width * 3;
      int num4 = stride2 - bigBmp.Width * 3;
      bool flag = true;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        for (int index2 = 0; index2 < width; ++index2)
        {
          byte* numPtr3 = numPtr2;
          byte* numPtr4 = numPtr1;
          for (int index3 = 0; index3 < height; ++index3)
          {
            flag = true;
            for (int index4 = 0; index4 < num2; ++index4)
            {
              int num5 = (int) *numPtr2 - int32;
              if ((int) *numPtr2 + int32 < (int) *numPtr1 || num5 > (int) *numPtr1)
              {
                flag = false;
                break;
              }
              ++numPtr2;
              ++numPtr1;
            }
            if (flag)
            {
              byte* numPtr5 = numPtr4;
              byte* numPtr6 = numPtr3;
              numPtr1 = numPtr5 + stride1 * (1 + index3);
              numPtr2 = numPtr6 + stride2 * (1 + index3);
            }
            else
              break;
          }
          if (flag)
          {
            empty.X = index2;
            empty.Y = index1;
            empty.Width = smallBmp.Width;
            empty.Height = smallBmp.Height;
            break;
          }
          byte* numPtr7 = numPtr3;
          numPtr1 = numPtr4;
          numPtr2 = numPtr7 + 3;
        }
        if (!flag)
          numPtr2 += num4;
        else
          break;
      }
      bigBmp.UnlockBits(bitmapdata2);
      smallBmp.UnlockBits(bitmapdata1);
      return empty;
    }
  }
}
