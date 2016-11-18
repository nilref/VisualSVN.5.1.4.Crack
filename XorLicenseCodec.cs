// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.XorLicenseCodec
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

namespace VisualSVN.Core
{
  public class XorLicenseCodec : IEncoder, IDecoder
  {
    private byte[] xorKey = new byte[5]
    {
      (byte) 20,
      (byte) 177,
      (byte) 126,
      (byte) 47,
      (byte) 49
    };

    public XorLicenseCodec(byte[] xorKey)
    {
      this.xorKey = xorKey;
    }

    public XorLicenseCodec()
    {
    }

    public byte[] Encode(byte[] data)
    {
      byte[] numArray = new byte[data.Length];
      for (int index = 0; index < data.Length; ++index)
        numArray[index] = (byte) ((uint) data[index] ^ (uint) this.xorKey[index % this.xorKey.Length]);
      return numArray;
    }

    public byte[] Decode(byte[] data)
    {
      return this.Encode(data);
    }
  }
}
