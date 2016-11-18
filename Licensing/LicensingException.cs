// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.LicensingException
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
  public class LicensingException : Exception
  {
    public LicensingException(string message, Exception inner)
      : base(message, inner)
    {
    }

    public LicensingException(string message)
      : base(message)
    {
    }
  }
}
