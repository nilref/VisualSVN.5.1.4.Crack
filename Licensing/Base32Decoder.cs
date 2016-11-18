// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.Base32Decoder
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
  public class Base32Decoder
  {
    public static byte[] Decode(string str)
    {
      return Base32Decoder.ConvertFromKey(str, (byte) 5);
    }

    private static byte[] ConvertFromKey(string key, byte base2)
    {
      if (Base32Decoder.IsEmpty(key))
        return (byte[]) null;
      uint num1 = (uint) Math.Floor((double) base2 / 8.0 * (double) key.Length);
      byte[] numArray = new byte[(int) num1];
      if (Base32Decoder.IsEmpty((Array) numArray))
        return (byte[]) null;
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (byte) 0;
      int num2 = 0;
      int num3 = key.Length - 1;
      byte num4 = 0;
      while (num3 >= 0 && (long) num2 < (long) (num1 * 8U))
      {
        byte val = Base32Decoder.CharToVal(key[num3--]);
        int num5 = (int) numArray[num2 / 8] + ((int) val << num2 % 8) + (int) num4;
        num4 = (byte) (num5 / 256);
        numArray[num2 / 8] = (byte) (num5 % 256);
        num2 += (int) base2;
      }
      return numArray;
    }

    private static bool IsEmpty(string value)
    {
      if (value != null)
        return value.Length == 0;
      return true;
    }

    private static bool IsEmpty(Array value)
    {
      if (value != null)
        return value.Length == 0;
      return true;
    }

    private static byte CharToVal(char c)
    {
      c = char.ToLower(c);
      if (48 > (int) c || (int) c > 57)
        return (byte) Base32Decoder.TrimNegative((int) (byte) ((int) c - 97 + 10));
      return (byte) Base32Decoder.TrimNegative((int) (byte) ((uint) c - 48U));
    }

    private static int TrimNegative(int x)
    {
      if (x < 0)
        return 0;
      return x;
    }
  }
}
