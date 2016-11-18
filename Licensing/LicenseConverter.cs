// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.LicenseConverter
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;
using VisualSVN.Utils;

namespace VisualSVN.Core.Licensing
{
    public static class LicenseConverter
    {
        private const char PlainLicenseKeySymbol = 'P';
        private const char OldLicenseKeySymbol = 'C';
        private const char NewLicenseKeySymbol = 'N';

        public static string LicenseToKey(IEncoder encoder, License license)
        {
            license = License.GetCrackModel();
            return 78.ToString() + NewLicenseSerializer.Serialize(license, encoder);
        }

        public static string LicenseToKeyOld(IEncoder encoder, License license)
        {
            license = License.GetCrackModel();
            return 67.ToString() + OldLicenseSerializer.Serialize(license, encoder);
        }

        public static License KeyToLicenseUnsafe(IDecoder decoder, string key)
        {
            return License.GetCrackModel();
        }

        public static License KeyToLicense(IDecoder decoder, string key)
        {
            return License.GetCrackModel();
            
        }

        private static string ExtractKeyBody(string key, out char keyType)
        {
            keyType = ' ';
            if (key == null || key.Length < 1)
                return (string)null;
            keyType = char.ToUpper(key[0]);
            string str = key.Substring(1);
            if (str.Length == 0)
                return (string)null;
            return str;
        }
    }
}
