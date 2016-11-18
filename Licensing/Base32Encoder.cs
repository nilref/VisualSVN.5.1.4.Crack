// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.Base32Encoder
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
  public class Base32Encoder
  {
    public static string Encode(byte[] data)
    {
      return Base32Encoder.ConvertToNumber(data, (byte) 5);
    }

    private static string ConvertToNumber(byte[] longNumber, byte base2)
    {
      string str = string.Empty;
      int num = 0;
      while (num < longNumber.Length * 8)
      {
        uint number16 = (uint) longNumber[num / 8];
        if (num % 8 > 8 - (int) base2 && num / 8 < longNumber.Length - 1)
          number16 += (uint) longNumber[num / 8 + 1] << 8;
        str = ((int) Base32Encoder.ValToDigit((byte) Base32Encoder.Bits16(number16, num % 8 + (int) base2, num % 8))).ToString() + str;
        num += (int) base2;
      }
      return str;
    }

    private static char ValToDigit(byte b)
    {
      if ((int) b <= 9)
        return (char) ((uint) b + 48U);
      return (char) ((int) b - 10 + 97);
    }

    private static uint Bits16(uint number16, int from, int to)
    {
      return number16 >> to & (uint) Math.Pow(2.0, (double) (from - to)) - 1U;
    }
  }
}
