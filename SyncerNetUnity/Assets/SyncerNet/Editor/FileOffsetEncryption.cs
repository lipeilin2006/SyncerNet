using System;
using System.IO;
using YooAsset;

/// <summary>
/// �ļ�ƫ�Ƽ��ܷ�ʽ
/// </summary>
public class FileOffsetEncryption : IEncryptionServices
{
    public EncryptResult Encrypt(EncryptFileInfo fileInfo)
    {
        int offset = 32;
        byte[] fileData = File.ReadAllBytes(fileInfo.FileLoadPath);
        var encryptedData = new byte[fileData.Length + offset];
        Buffer.BlockCopy(fileData, 0, encryptedData, offset, fileData.Length);

        EncryptResult result = new EncryptResult();
        result.Encrypted = true;
        result.EncryptedData = encryptedData;
        return result;
    }
}
