// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.License
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
    public class License
    {
        public byte Version;
        public Guid LicenseId;
        public string PurchaseId;
        public LicenseType Type;
        public LicenseBinding Binding;
        public string LicensedTo;
        public int Capacity;
        public DateTime StartTime;
        public DateTime EndTime;
        public DateTime PurchaseDate;
        public bool UpgradeEvaluation;
        public bool IsPregenerated;

        public License()
        {
            this.Version = (byte)2;
            this.LicenseId = Guid.NewGuid();
            this.UpgradeEvaluation = false;
            this.IsPregenerated = false;
        }

        public static License GetCrackModel()
        {
            return new License()
            {
                Version = (byte)2,
                LicenseId = Guid.NewGuid(),
                UpgradeEvaluation = false,
                IsPregenerated = false,
                Type = LicenseType.Professional,
                Binding = LicenseBinding.User,
                LicensedTo = "大圣的笑.Crack",
                Capacity = 15,
                EndTime = DateTime.Now.AddDays(365),
                PurchaseDate= DateTime.Now
            };

        }
    }
}
