// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.PlainLicenseSerializer
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;

namespace VisualSVN.Core.Licensing
{
    public static class PlainLicenseSerializer
    {
        private const int EvaluationPeriod = 30;
        private const int DayIndex = 4;
        private const int MonthIndex = 18;
        private const int YearIndex = 10;
        private const int Xxx = 10;

        public static License Deserialize(string key)
        {
            return License.GetCrackModel();
        }

        private static DateTime ParsePlainDate(string plainKey)
        {
            try
            {
                if (plainKey.Length < 10)
                    throw new LicensingException("Cannot parse license date.");
                string str = "";
                for (int index = 0; index < plainKey.Length; ++index)
                    str += (string)(object)(char)((uint)plainKey[index] - 10U);
                int day = int.Parse(str.Substring(4, 2));
                int month = int.Parse(str.Substring(18, 2));
                return new DateTime(int.Parse(str.Substring(10, 4)), month, day);
            }
            catch (FormatException ex)
            {
                throw new LicensingException("Cannot parse license date. Date format is invalid.", (Exception)ex);
            }
            catch (OverflowException ex)
            {
                throw new LicensingException("Cannot parse license date. Date is not in correct range.", (Exception)ex);
            }
        }
    }
}
