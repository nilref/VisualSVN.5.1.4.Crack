// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.LicenseVerificator
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
    public static class LicenseVerificator
    {
        public static bool IsValid(License license, DateTime now)
        {
            return true;
        }

        public static bool IsCorrect(License license)
        {
            return true;
        }

        public static bool IsExpired(License license, DateTime now)
        {
            return false;
        }

        public static bool IsOutdatedForLicensingMilestone(License license, int licensingMilestone)
        {
            return false;//test
        }

        public static bool IsOutdatedForCurrentVersion(License license)
        {
            return false;//test
        }

        public static bool IsExpiringSoon(License license, DateTime now)
        {
            return false;
        }

        public static int DaysToExpire(License license, DateTime now)
        {
            return 999;
        }

        public static bool IsStarted(License license, DateTime now)
        {   
            return true;
        }

        public static bool IsTimeLimited(License license)
        {
            return false;
        }

        public static bool IsPregenerated(License license)
        {
            return false;
        }
    }
}
