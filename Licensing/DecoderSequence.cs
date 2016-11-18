// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.DecoderSequence
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

namespace VisualSVN.Core.Licensing
{
  public class DecoderSequence : IDecoder
  {
    private IDecoder[] decoders;

    public DecoderSequence(params IDecoder[] decoders)
    {
      this.decoders = decoders;
    }

    public byte[] Decode(byte[] data)
    {
      byte[] data1 = data;
      for (int index = this.decoders.Length - 1; index >= 0; --index)
        data1 = this.decoders[index].Decode(data1);
      return data1;
    }
  }
}
