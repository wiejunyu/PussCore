using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Puss.Encrypt
{
    public interface IEncryptService
    {

        #region DES对称加密解密

        /// <summary> 
        /// 加密字符串
        /// </summary> 
        /// <param name="strText">需被加密的字符串</param> 
        /// <param name="strEncrKey">密钥</param> 
        /// <returns></returns> 
        string DesEncrypt(string strText, string strEncrKey);

        /// <summary> 
        /// 解密字符串
        /// </summary> 
        /// <param name="strText">需被解密的字符串</param> 
        /// <param name="sDecrKey">密钥</param> 
        /// <returns></returns> 
        string DesDecrypt(string strText, string sDecrKey);

        /// <summary> 
        /// 加密文件
        /// </summary> 
        /// <param name="m_InFilePath">原路径</param> 
        /// <param name="m_OutFilePath">加密后的文件路径</param> 
        /// <param name="strEncrKey">密钥</param> 
        void DesEncryptFile(string m_InFilePath, string m_OutFilePath, string strEncrKey);

        /// <summary> 
        /// 解密文件
        /// </summary> 
        /// <param name="m_InFilePath">被解密路径</param> 
        /// <param name="m_OutFilePath">解密后的路径</param> 
        /// <param name="sDecrKey">密钥</param> 
        void DesDecryptFile(string m_InFilePath, string m_OutFilePath, string sDecrKey);
        #endregion

        #region 3DES对称加密解密
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="originalValue">加密数据</param>
        /// <param name="key">24位字符的密钥字符串</param>
        /// <param name="IV">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        string DESEncrypt(string originalValue, string key, string IV);

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">24位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        string DESDecrypst(string data, string key, string IV);
        #endregion

        #region AES RijndaelManaged加密解密
        string AES_Encrypt(string encryptString);

        string AES_Decrypt(string decryptString);

        #region AES(CBC)有向量（IV）
        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <returns>加密结果字符串</returns>
        string AES_Encrypt(string encryptString, string encryptKey);

        /// <summary> 
        /// 对称加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        string AES_Decrypt(string decryptString, string decryptKey);
        #endregion

        #region AES(ECB)无向量（IV）
        /// <summary>  
        /// AES加密(无向量)  
        /// </summary>  
        /// <param name="plainBytes">被加密的明文</param>  
        /// <param name="key">密钥 32 </param>  
        /// <returns>密文</returns>  
        string AESEncryptECB(string encryptString, string encryptKey);

        /// <summary>  
        /// AES解密(无向量)  
        /// </summary>  
        /// <param name="decryptString">被加密的明文</param>  
        /// <param name="decryptKey">密钥</param>  
        /// <returns>明文</returns>  
        string AESDecryptECB(string decryptString, string decryptKey);
        #endregion
        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        CryptoStream AES_EncryptStrream(FileStream fs, string decryptKey);

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        CryptoStream AES_DecryptStream(FileStream fs, string decryptKey);

        /// <summary>
        /// 对指定文件加密
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="OutputFile"></param>
        /// <returns></returns>
        bool AES_EncryptFile(string InputFile, string OutputFile);

        /// <summary>
        /// 对指定的文件解压缩
        /// </summary>
        /// <param name="InputFile"></param>
        /// <param name="OutputFile"></param>
        /// <returns></returns>
        bool AES_DecryptFile(string InputFile, string OutputFile);

        #endregion

        #region RSA加密 解密

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>密文字符串</returns>
        string EncryptByRSA(string plaintext, string publicKey);

        /// <summary> 
        ///RSA解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>明文字符串</returns>
        string DecryptByRSA(string ciphertext, string privateKey);

        /// <summary>
        /// 生成RSA加密 解密的 密钥,生成的key就是 方法EncryptByRSA与DecryptByRSA用的key了
        /// </summary>
        void GetRSAKey();
        #endregion

        #region Base64加密解密

        /// <summary>
        /// Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
        /// 這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
        /// 這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
        /// 系統中而不同。
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string Base64Encrypt(string str);

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string Base64Decrypt(string str);
        #endregion

        #region MD5
        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetMD5_32(string input);

        /// <summary>
        /// 获得16位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetMD5_16(string input);

        /// <summary>
        /// 获得8位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetMD5_8(string input);

        /// <summary>
        /// 获得4位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetMD5_4(string input);

        string MD5EncryptHash(String input);
        #endregion

        #region MD5签名验证

        /// <summary>
        /// 对给定文件路径的文件加上标签
        /// </summary>
        /// <param name="path">要加密的文件的路径</param>
        /// <returns>标签的值</returns>
        bool AddMD5(string path);

        /// <summary>
        /// 对给定路径的文件进行验证
        /// </summary>
        /// <param name="path"></param>
        /// <returns>是否加了标签或是否标签值与内容值一致</returns>
        bool CheckMD5(string path);
        #endregion

        #region  SHA256加密算法
        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
        string SHA256(string str);
        #endregion

        #region RC4加密 解密
        /// <summary>
        /// RC4加密算法 返回进过rc4加密过的字符
        /// </summary>
        /// <param name="str">被加密的字符</param>
        /// <param name="ckey">密钥</param>
        string EncryptRC4wq(string str, string ckey);

        /// <summary>
        /// RC4解密算法 返回进过rc4解密过的字符
        /// </summary>
        /// <param name="str">被解密的字符</param>
        /// <param name="ckey">密钥</param>
        string DecryptRC4wq(string str, string ckey);
        #endregion
    }
}
