// Decompiled with JetBrains decompiler
// Type: InputDeviceLib.WaitDeclaration
// Assembly: WindowDetective, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64CDF6FC-7220-4ED6-9C43-AF047670C6C0
// Assembly location: C:\Users\veyvin\Desktop\WindowDetective.exe

using System;
using System.Drawing;
using System.Threading;

namespace InputDeviceLib
{
  internal class WaitDeclaration
  {
    public static void WaitMillisecond(int milliseconds)
    {
      if (milliseconds <= 0)
      {
        Console.WriteLine("等待时间不超过0，视为不等待...");
      }
      else
      {
        Console.CursorVisible = false;
        Console.Write("程序将在等待【");
        Point point1 = new Point(Console.CursorLeft, Console.CursorTop);
        Console.WriteLine("{0}秒】后继续进行", (object) ((double) milliseconds / 1000.0));
        Console.Write("[");
        Point point2 = new Point(Console.CursorLeft, Console.CursorTop);
        Console.Write("....,....;....,....;....,....;....,....;....,....");
        Point point3 = new Point(Console.CursorLeft, Console.CursorTop);
        Console.Write(";]");
        int num = milliseconds;
        do
        {
          Thread.Sleep(1);
          Console.SetCursorPosition(point1.X, point1.Y);
          Console.Write("{0}秒】后继续进行              ", (object) ((double) num / 1000.0 - 0.001));
          --num;
          Console.SetCursorPosition(point2.X, point2.Y);
          for (; ((double) point3.X - 0.0) / (double) point2.X >= (double) milliseconds / ((double) (milliseconds - num) - 0.0); ++point2.X)
            Console.Write("#");
        }
        while (num > 0);
        Console.SetCursorPosition(0, point3.Y + 1);
        if (point3.Y > Console.BufferHeight - 10)
        {
          Console.WriteLine("缓冲区即将溢出，10秒后将清空缓冲区。");
          Console.Beep();
          for (int index = 9; index >= 0; --index)
          {
            Thread.Sleep(1000);
            Console.Write(index);
            Console.SetCursorPosition(0, Console.CursorTop);
          }
          Console.Clear();
        }
        Console.CursorVisible = true;
      }
      Console.WriteLine();
    }

    public static void WaitSeconds(int seconds)
    {
      if (seconds <= 0)
      {
        Console.WriteLine("等待时间不超过0，视为不等待...");
      }
      else
      {
        Console.Write("程序将在等待【{0}秒】后继续进行", (object) seconds);
        Console.WriteLine("   ( ***** ***** : {0}秒 )", (object) 5.0);
        for (int index = 0; index < seconds * 2; ++index)
        {
          Thread.Sleep(500);
          if (index % 5 == 0)
            Console.Write(" *");
          else
            Console.Write("*");
          if (index % 50 == 49)
            Console.WriteLine("  剩余【{0}秒】", (object) ((seconds * 2 - index - 1) / 2));
        }
        Console.WriteLine("等待完毕，程序继续进行...");
      }
    }
  }
}
