// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.EncoderSequence
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

namespace VisualSVN.Core.Licensing
{
  public class EncoderSequence : IEncoder
  {
    private IEncoder[] encoders;

    public EncoderSequence(params IEncoder[] encoders)
    {
      this.encoders = encoders;
    }

    public byte[] Encode(byte[] data)
    {
      byte[] data1 = data;
      for (int index = 0; index < this.encoders.Length; ++index)
        data1 = this.encoders[index].Encode(data1);
      return data1;
    }
  }
}
