// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.LicensingMilestone
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
  public static class LicensingMilestone
  {
    private static readonly DateTime VisualStudio2010Beta2ReleaseDate = new DateTime(2009, 10, 15);
    private static readonly DateTime VisualStudio11DeveloperPreviewReleaseDate = new DateTime(2011, 9, 16);
    private static readonly DateTime VisualStudio2013PreviewReleaseDate = new DateTime(2012, 12, 1);
    private static readonly DateTime Milestone5GracePeriod = new DateTime(2014, 6, 3);
    private static readonly DateTime[] GracePeriodMilestoneDates = new DateTime[4]
    {
      LicensingMilestone.VisualStudio2010Beta2ReleaseDate,
      LicensingMilestone.VisualStudio11DeveloperPreviewReleaseDate,
      LicensingMilestone.VisualStudio2013PreviewReleaseDate,
      LicensingMilestone.Milestone5GracePeriod
    };
    public const int CurrentMilestone = 5;
    public const int PreviousMilestone = 4;

    public static DateTime GetGracePeriodDate(int licensingMilestone)
    {
      int index = licensingMilestone - 2;
      if (index < 0)
        throw new ArgumentOutOfRangeException("licensingMilestone");
      if (index >= LicensingMilestone.GracePeriodMilestoneDates.Length)
        return DateTime.MaxValue;
      return LicensingMilestone.GracePeriodMilestoneDates[index];
    }
  }
}
