// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.NewLicenseSerializer
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;
using System.IO;
using System.Text;

namespace VisualSVN.Core.Licensing
{
    public static class NewLicenseSerializer
    {
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
                NewLicenseSerializer.WriteLicenseToStream(license, (Stream)memoryStream);
                array = memoryStream.ToArray();
            }
            return Base32Encoder.Encode(encoder.Encode(array));
        }

        private static void WriteLicenseToStream(License license, Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
            writer.Write(license.Version);
            writer.Write((byte)license.Type);
            writer.Write((byte)license.Binding);
            writer.Write(license.Capacity);
            NewLicenseSerializer.WriteBytePrefixedString(writer, license.LicensedTo);
            NewLicenseSerializer.WriteDateTime(writer, license.StartTime);
            NewLicenseSerializer.WriteDateTime(writer, license.EndTime);
            NewLicenseSerializer.WriteBytePrefixedString(writer, license.LicenseId.ToString());
            NewLicenseSerializer.WriteBytePrefixedString(writer, license.PurchaseId);
            NewLicenseSerializer.WriteDateTime(writer, license.PurchaseDate);
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
            if (str.Length > (int)byte.MaxValue)
                throw new ArgumentOutOfRangeException("str", (object)str, "String is too long.");
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
    }
}
