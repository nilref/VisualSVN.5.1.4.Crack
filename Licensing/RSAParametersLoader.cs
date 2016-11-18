// Decompiled with JetBrains decompiler
// Type: VisualSVN.Core.Licensing.RSAParametersLoader
// Assembly: VisualSVN.Core.L, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC2B0911-F358-4C87-AC4A-B042E9B55069
// Assembly location: C:\Users\10100744\Desktop\VisualSVN.Core.L.dll

using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace VisualSVN.Core.Licensing
{
  public class RSAParametersLoader
  {
    public const string CSP_CONTAINER_NAME = "OSTY";

    public static RSAParameters LoadFromResource(Assembly assembly, string resourceName, string password)
    {
      string name = assembly.GetName().Name;
      return RSAParametersLoader.DeserializeRSAKeyFromStream(assembly.GetManifestResourceStream(name + "." + resourceName), password);
    }

    public static RSAParameters LoadFromFile(string fileName, string password)
    {
      return RSAParametersLoader.DeserializeRSAKeyFromStream((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read), password);
    }

    private static RSAParameters DeserializeRSAKeyFromStream(Stream stream, string password)
    {
      RSAParameters rsaParameters = new RSAParameters();
      using (stream)
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        rsaParameters.Exponent = (byte[]) binaryFormatter.Deserialize(stream);
        rsaParameters.Modulus = (byte[]) binaryFormatter.Deserialize(stream);
        if (password != null)
        {
          long length = stream.Length - stream.Position;
          byte[] numArray = new byte[length];
          long num = (long) stream.Read(numArray, 0, (int) length);
          PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, (byte[]) null);
          passwordDeriveBytes.HashName = "SHA256";
          Rijndael rijndael = (Rijndael) new RijndaelManaged();
          rijndael.KeySize = 256;
          rijndael.Key = passwordDeriveBytes.GetBytes(32);
          rijndael.IV = new byte[rijndael.BlockSize / 8];
          byte[] buffer = rijndael.CreateDecryptor().TransformFinalBlock(numArray, 0, (int) num);
          rijndael.Clear();
          MemoryStream memoryStream = new MemoryStream(buffer, false);
          rsaParameters.P = (byte[]) binaryFormatter.Deserialize((Stream) memoryStream);
          rsaParameters.Q = (byte[]) binaryFormatter.Deserialize((Stream) memoryStream);
          rsaParameters.D = (byte[]) binaryFormatter.Deserialize((Stream) memoryStream);
          rsaParameters.InverseQ = (byte[]) binaryFormatter.Deserialize((Stream) memoryStream);
          rsaParameters.DP = (byte[]) binaryFormatter.Deserialize((Stream) memoryStream);
          rsaParameters.DQ = (byte[]) binaryFormatter.Deserialize((Stream) memoryStream);
          memoryStream.Close();
        }
        stream.Close();
        return rsaParameters;
      }
    }
  }
}
