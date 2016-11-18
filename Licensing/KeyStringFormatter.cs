// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.KeyStringFormatter
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System.Text;

namespace VisualSVN.Core.Licensing
{
  public class KeyStringFormatter
  {
    public static readonly string OpeningKeySymbols = new string('-', 50);
    public static readonly string ClosingKeySymbols = new string('-', 50);
    public static readonly char[] ReplaceSymbols = new char[5]
    {
      '\n',
      '\r',
      '\t',
      ' ',
      '-'
    };
    public const int KeyLineWidth = 50;

    public static string FormatKey(string pureKey)
    {
      StringBuilder stringBuilder = new StringBuilder(pureKey.Length);
      stringBuilder.Append(KeyStringFormatter.OpeningKeySymbols);
      stringBuilder.Append("\r\n");
      int startIndex = 0;
      while (startIndex + 50 <= pureKey.Length)
      {
        stringBuilder.Append(pureKey.Substring(startIndex, 50));
        stringBuilder.Append("\r\n");
        startIndex += 50;
      }
      stringBuilder.Append(pureKey.Substring(startIndex, pureKey.Length % 50));
      stringBuilder.Append("\r\n");
      stringBuilder.Append(KeyStringFormatter.ClosingKeySymbols);
      return stringBuilder.ToString();
    }

    private static string RemoveChars(string str, char[] charsToRemove)
    {
      StringBuilder stringBuilder = new StringBuilder(str);
      foreach (char ch in charsToRemove)
        stringBuilder.Replace(ch.ToString(), "");
      return stringBuilder.ToString();
    }

    public static string ParseKey(string formattedKey)
    {
      return KeyStringFormatter.RemoveChars(formattedKey, KeyStringFormatter.ReplaceSymbols);
    }
  }
}
