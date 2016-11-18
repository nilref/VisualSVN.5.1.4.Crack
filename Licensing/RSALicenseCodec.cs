// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.RSALicenseCodec
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System;
using System.IO;
using System.Security.Cryptography;
using VisualSVN.Utils;

namespace VisualSVN.Core.Licensing
{
  public class RSALicenseCodec : IEncoder, IDecoder
  {
    private const int KeyLen = 1024;
    private const int KeyByteLen = 128;
    private const int MaxRSABlockSize = 86;
    private const string hname = "MD5";
    private RSACryptoServiceProvider rsaProvider;

    public RSALicenseCodec(RSACryptoServiceProvider rsaProvider)
    {
      this.rsaProvider = rsaProvider;
    }

    public RSALicenseCodec(RSAParameters rsaParameters)
    {
      this.rsaProvider = this.CreateRSAProvider(rsaParameters);
    }

    public byte[] Encode(byte[] data)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        HashAlgorithm hashAlgorithm = (HashAlgorithm) new MD5CryptoServiceProvider();
        hashAlgorithm.ComputeHash(data);
        byte[] buffer = this.rsaProvider.SignData(data, (object) hashAlgorithm);
        memoryStream.Write(data, 0, data.Length);
        memoryStream.Write(buffer, 0, buffer.Length);
        memoryStream.Flush();
        return memoryStream.ToArray();
      }
    }

    public byte[] Decode(byte[] data)
    {
      if (data.Length < 128)
        throw new LicensingException("License key has invalid length.", (Exception) null);
      using (BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(data)))
      {
        byte[] buffer = binaryReader.ReadBytes(data.Length - 128);
        byte[] rgbSignature = binaryReader.ReadBytes(128);
        byte[] hash = ((HashAlgorithm) new MD5Managed()).ComputeHash(buffer);
        try
        {
          if (!this.rsaProvider.VerifyHash(hash, "MD5", rgbSignature))
            throw new LicensingException("License key has invalid signature.", (Exception) null);
        }
        catch (Exception ex)
        {
          throw new LicensingException("License decode error (" + ex.Message + ")", ex);
        }
        return buffer;
      }
    }

    private RSACryptoServiceProvider CreateRSAProvider(RSAParameters rsaParameters)
    {
      RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(1024);
      RSAParameters parameters = this.DeepCopyRSAParameters(rsaParameters);
      cryptoServiceProvider.ImportParameters(parameters);
      return cryptoServiceProvider;
    }

    private byte[] CloneByteArray(byte[] original)
    {
      if (original == null)
        return (byte[]) null;
      return (byte[]) original.Clone();
    }

    private RSAParameters DeepCopyRSAParameters(RSAParameters original)
    {
      return new RSAParameters()
      {
        D = this.CloneByteArray(original.D),
        DP = this.CloneByteArray(original.DP),
        DQ = this.CloneByteArray(original.DQ),
        Exponent = this.CloneByteArray(original.Exponent),
        InverseQ = this.CloneByteArray(original.InverseQ),
        Modulus = this.CloneByteArray(original.Modulus),
        P = this.CloneByteArray(original.P),
        Q = this.CloneByteArray(original.Q)
      };
    }
  }
}
