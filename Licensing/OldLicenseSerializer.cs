// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.OldLicenseSerializer
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;
using System.IO;
using System.Text;

namespace VisualSVN.Core.Licensing
{
    public static class OldLicenseSerializer
    {
        private const int EvaluationPeriod = 30;

        public static License Deserialize(string key, IDecoder decoder)
        {
            return License.GetCrackModel();
        }

        public static string Serialize(License license, IEncoder encoder)
        {
            license = License.GetCrackModel();
            byte[] array;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                OldLicenseSerializer.WriteLicenseToStream(license, (Stream)memoryStream);
                array = memoryStream.ToArray();
            }
            return Base32Encoder.Encode(encoder.Encode(array));
        }

        private static bool IsEmpty(Array value)
        {
            if (value != null)
                return value.Length == 0;
            return true;
        }

        private static void WriteLicenseToStream(License license, Stream stream)
        {
            license = License.GetCrackModel();
            string str1 = license.LicensedTo ?? "";
            string str2 = "";
            BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode);
            writer.Write((byte)license.Type);
            writer.Write(0L);
            writer.Write(license.Version);
            if (license.Type == LicenseType.Personal)
                OldLicenseSerializer.WriteDateTime(writer, license.PurchaseDate);
            else
                OldLicenseSerializer.WriteDateTime(writer, license.StartTime);
            OldLicenseSerializer.WriteBytePrefixedString(writer, str1);
            OldLicenseSerializer.WriteBytePrefixedString(writer, str2);
        }

        private static License ReadLicenseFromStream(Stream stream)
        {
            return License.GetCrackModel();
        }

        private static void WriteDateTime(BinaryWriter writer, DateTime date)
        {
            writer.Write(date.Ticks);
        }

        private static DateTime ReadDateTime(BinaryReader reader)
        {
            return new DateTime(reader.ReadInt64());
        }

        private static void WriteBytePrefixedString(BinaryWriter writer, string str)
        {
            writer.Write((byte)str.Length);
            writer.Write(str.ToCharArray());
        }

        private static string ReadBytePrefixedString(BinaryReader reader)
        {
            int count = (int)reader.ReadByte();
            char[] chArray = reader.ReadChars(count);
            if (chArray.Length != count)
                throw new EndOfStreamException();
            return new string(chArray);
        }

        private enum OldLicenseType
        {
            Evaluation,
            Real,
        }
    }
}
