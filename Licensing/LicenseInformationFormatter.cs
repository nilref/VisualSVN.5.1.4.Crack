// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.LicenseInformationFormatter
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;
using System.Text;

namespace VisualSVN.Core.Licensing
{
    public static class LicenseInformationFormatter
    {
        public static string FormatTabbed(License license, DateTime now)
        {
            license = License.GetCrackModel();
            if (license == null)
                return "No license";
            if (!LicenseVerificator.IsCorrect(license))
                return "Unknown license";
            StringBuilder builder = new StringBuilder();
            if (license.UpgradeEvaluation)
            {
                LicenseInformationFormatter.AppendLicenseOutdatedInfo(builder);
                LicenseInformationFormatter.AppendUpgradeEvaluationExpirationInfo(builder, license, now);
            }
            else if (LicenseVerificator.IsOutdatedForCurrentVersion(license))
            {
                LicenseInformationFormatter.AppendLicenseOutdatedInfo(builder);
            }
            else
            {
                LicenseInformationFormatter.AppendLicenseTypeInfo(builder, license);
                LicenseInformationFormatter.AppendLicensedToInfo(builder, license);
                if (LicenseVerificator.IsTimeLimited(license))
                    LicenseInformationFormatter.AppendLicenseExpirationInfo(builder, license, now);
            }
            return builder.ToString().TrimEnd('\r', '\n');
        }

        public static string Format(License license, DateTime now)
        {
            license = License.GetCrackModel();
            return LicenseInformationFormatter.FormatTabbed(license, now).Replace('\t', ' ');
        }

        private static void AppendLicenseTypeInfo(StringBuilder builder, License license)
        {
            license = License.GetCrackModel();
            switch (license.Type)
            {
                case LicenseType.Evaluation:
                    builder.Append("License type:\tEvaluation\r\n");
                    break;
                case LicenseType.Personal:
                    if ((int)license.Version <= 1)
                        break;
                    builder.Append("License type:\tPersonal\r\n");
                    break;
                case LicenseType.Corporate:
                    builder.Append("License type:\tCorporate\r\n");
                    break;
                case LicenseType.Classroom:
                    builder.Append("License type:\tClassroom\r\n");
                    break;
                case LicenseType.OpenSource:
                    builder.Append("License type:\tOpen Source Developer\r\n");
                    break;
                case LicenseType.Student:
                    builder.Append("License type:\tStudent\r\n");
                    break;
                case LicenseType.Professional:
                    builder.Append("License type:\tProfessional\r\n");
                    break;
                case LicenseType.Site:
                    builder.Append("License type:\tSite\r\n");
                    break;
                case LicenseType.Community:
                    builder.Append("License type:\tCommunity\r\n");
                    break;
            }
        }

        private static string FormatNumeral(int number, string single, string multiple)
        {
            return number.ToString() + " " + (number == 1 ? (object)single : (object)multiple);
        }

        private static void AppendLicensedToInfo(StringBuilder builder, License license)
        {
            license = License.GetCrackModel();
            switch (license.Type)
            {
                case LicenseType.Evaluation:
                    if (!(license.LicensedTo != "Evaluation User"))
                        break;
                    builder.AppendFormat("Licensed to:\t{0}\r\n", (object)license.LicensedTo);
                    break;
                case LicenseType.Personal:
                case LicenseType.Student:
                case LicenseType.Site:
                    builder.AppendFormat("Licensed to:\t{0}\r\n", (object)license.LicensedTo);
                    break;
                case LicenseType.Corporate:
                case LicenseType.Classroom:
                    builder.AppendFormat("Licensed to:\t{0} ({1} seats)\r\n", (object)license.LicensedTo, (object)license.Capacity);
                    break;
                case LicenseType.OpenSource:
                    if (license.Capacity == 1)
                    {
                        builder.AppendFormat("Licensed to:\t{0}\r\n", (object)license.LicensedTo);
                        break;
                    }
                    builder.AppendFormat("Licensed to:\t{0} (all members)\r\n", (object)license.LicensedTo);
                    break;
                case LicenseType.Professional:
                    if (license.Binding == LicenseBinding.Seat)
                    {
                        builder.AppendFormat("Licensed to:\t{0} ({1})\r\n", (object)license.LicensedTo, (object)LicenseInformationFormatter.FormatNumeral(license.Capacity, "user", "users"));
                        break;
                    }
                    if (license.Binding != LicenseBinding.User)
                        break;
                    builder.AppendFormat("Licensed to:\t{0}\r\n", (object)license.LicensedTo);
                    break;
                case LicenseType.Community:
                    builder.AppendFormat("Licensed to:\t{0}\r\n", (object)license.LicensedTo);
                    break;
            }
        }

        private static void AppendLicenseExpirationInfo(StringBuilder builder, License license, DateTime now)
        {
            license = License.GetCrackModel();
            if (LicenseVerificator.IsExpired(license, now))
                builder.AppendFormat("License expired on {0:d}\r\n", (object)license.EndTime);
            else if (!LicenseVerificator.IsStarted(license, now))
            {
                builder.AppendFormat("License will be valid from {0:d}\r\n", (object)license.StartTime);
            }
            else
            {
                int expire = LicenseVerificator.DaysToExpire(license, now);
                if (expire <= 30)
                    builder.AppendFormat("License expires in {0} day(s)\r\n", (object)expire);
                else
                    builder.AppendFormat("License expires on {0:d}\r\n", (object)license.EndTime);
            }
        }

        private static void AppendLicenseOutdatedInfo(StringBuilder builder)
        {
            builder.AppendLine("Your license is not valid for VisualSVN 5.x.");
        }

        private static void AppendUpgradeEvaluationExpirationInfo(StringBuilder builder, License license, DateTime now)
        {
            license = License.GetCrackModel();
            if (LicenseVerificator.IsExpired(license, now))
                return;
            int expire = LicenseVerificator.DaysToExpire(license, now);
            if (expire <= 30)
                builder.AppendFormat("Temporary evaluation license expires in {0} day(s)\r\n", (object)expire);
            else
                builder.AppendFormat("Temporary evaluation license expires on {0:d}\r\n", (object)license.EndTime);
        }
    }
}
