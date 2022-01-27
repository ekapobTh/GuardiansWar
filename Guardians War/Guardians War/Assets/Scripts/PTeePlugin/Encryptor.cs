using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System;

public class Encryptor {

	public RijndaelManaged GetRijndaelManaged(String secretKey)
	{
		var keyBytes = new byte[16];
		var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
		Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
		return new RijndaelManaged
		{
			Mode = CipherMode.CBC,
			Padding = PaddingMode.PKCS7,
			KeySize = 128,
			BlockSize = 128,
			Key = keyBytes,
			IV = keyBytes
		};
	}
	
	public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
	{
		return rijndaelManaged.CreateEncryptor()
			.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
	}
	
	public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
	{
		return rijndaelManaged.CreateDecryptor()
			.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
	}

	public String Encrypt(String plainText, String key)
	{
		var plainBytes = Encoding.UTF8.GetBytes(plainText);
		return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
	}

	public String Decrypt(String encryptedText, String key)
	{
		var encryptedBytes = Convert.FromBase64String(encryptedText);
		return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
	}
}
