using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Tools
{
    public class InIFileControler
    {

        private string iniPath = "";
        private string keyValueSeparator = "";
        private bool useEncryption = false;

        private string encKey = "";
        public InIFileControler(string _iniPath, string _keyValueSeparator = "=", bool encrypt = false, string psk = "1234567891011121314151617")
        {
            iniPath = _iniPath;
            keyValueSeparator = _keyValueSeparator;
            useEncryption = encrypt;
            encKey = psk;
        }

        // Returns the path without file name, if the complete path with file name is provided.
        // ie:
        // Provided: c:\somefolder\somefolder\somefilename.exe
        // Returned: c:\somefolder\somefolder\
        private string GetFoldersFromPath(string completePath, bool removeTrailingBackslash = false)
        {

            string[] parts = null;
            parts = Regex.Split(completePath, @"\\");

            if (removeTrailingBackslash)
            {
                return completePath.Substring(0, completePath.Length - (parts[parts.Length - 1].Length + 1));
            }
            else
            {
                return completePath.Substring(0, completePath.Length - parts[parts.Length - 1].Length);
            }

        }

        private bool CreateFolders(string _path, bool pathIncludesFilename = false, string errorMessage = "")
        {

            if (pathIncludesFilename)
            {
                _path = GetFoldersFromPath(_path);
            }

            try
            {
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

            return true;
        }


        private void PauseWithoutBlockingUI(double Milliseconds)
        {
            DateTime timeOut = DateTime.Now.AddMilliseconds(Milliseconds);

            do
            {
                Thread.Sleep(1);
                //Application.DoEvents()
            } while (!(DateTime.Now > timeOut));

        }

        public bool ClearINI()
        {
            String errMsg = "";
            return ClearINI(ref errMsg);
        }

        public bool ClearINI(ref String errorMessage)
        {

            try
            {
                File.Delete(iniPath);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

            try
            {
                FileStream newIni = new FileStream(iniPath, FileMode.Create);
                newIni.Close();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

            if (useEncryption)
                MarkEncryptedFile();
            return true;

        }

        private void MarkEncryptedFile()
        {
            StreamWriter writer = new StreamWriter(iniPath, false);
            string mark = "SYSTEM_ENCRYPTION_VALIDATOR" + keyValueSeparator + "ENCRYPTION_OK";

            mark = EncryptionTools.Encrypt(mark, encKey, true);
            writer.WriteLine(mark);
            writer.Close();
        }

        public bool ValidateEncryptedIniFile()
        {
            String thisLine = "";
            String[] parts = null;
            StreamReader reader = null;
            bool validated = false;

            try
            {
                // Read the ini file into an arraylist...
                reader = new StreamReader(iniPath);

                //Until eof

                while (reader.Peek() != -1)
                {
                    thisLine = reader.ReadLine();
                    thisLine = EncryptionTools.Decrypt(thisLine, encKey, true);

                    if (thisLine.Contains(keyValueSeparator))
                    {
                        parts = Regex.Split(thisLine, keyValueSeparator);
                        if (parts[0].Equals("SYSTEM_ENCRYPTION_VALIDATOR") && parts[1].Equals("ENCRYPTION_OK"))
                        {
                            validated = true;
                            break; // TODO: might not be correct. Was : Exit While
                        }
                    }

                }

                // The file's empty.
            }
            catch (Exception) { }

            try
            {
                reader.Close();
            }
            catch (Exception) { }

            return validated;
        }

        public bool ReadAllEntries(ref Dictionary<String, String> iniEntries, ref string errorMessage)
        {
            String thisLine = "";
            String[] parts = null;
            StreamReader reader = default(StreamReader);

            if (!File.Exists(iniPath))
            {
                errorMessage = "Ini file not found.";
                return false;
            }

            if (useEncryption)
            {
                if (!ValidateEncryptedIniFile())
                {
                    errorMessage = "This file is not an encrypted ini file created by this class, or decryption failed / invalid key.";
                    return false;
                }
            }

            try
            {
                // Read the ini file into an arraylist...
                reader = new StreamReader(iniPath);
                iniEntries.Clear();

                //Until eof
                while (reader.Peek() != -1)
                {
                    thisLine = reader.ReadLine();

                    if (useEncryption)
                    {
                        thisLine = EncryptionTools.Decrypt(thisLine, encKey, true);
                    }

                    if (thisLine.Contains(keyValueSeparator))
                    {
                        parts = Regex.Split(thisLine, keyValueSeparator);

                        if (useEncryption)
                        {
                            if (!(parts[0].Equals("SYSTEM_ENCRYPTION_VALIDATOR") && parts[1].Equals("ENCRYPTION_OK")))
                            {
                                iniEntries.Add(parts[0], parts[1]);
                            }
                        }
                        else
                        {
                            iniEntries.Add(parts[0], parts[1]);
                        }
                    }
                }

                reader.Close();
                // The file's empty.
            }
            catch (Exception) { }

            return true;

        }

        public bool ReadAllEntries(ref ArrayList iniEntries)
        {
            String errMsg = "";
            return ReadAllEntries(ref iniEntries, ref errMsg);
        }

        // Get the contents of the ini file.
        public bool ReadAllEntries(ref ArrayList iniEntries, ref string errorMessage)
        {

            if (!File.Exists(iniPath))
            {
                errorMessage = "Ini file not found.";
                return false;
            }

            if (useEncryption)
            {
                if (!ValidateEncryptedIniFile())
                {
                    errorMessage = "This file is not an encrypted ini file created by this class, or decryption failed / invalid key.";
                    return false;
                }
            }

            try
            {
                // Read the ini file into an arraylist...
                StreamReader reader = new StreamReader(iniPath);
                string thisLine = "";
                String[] parts;
                iniEntries.Clear();

                //Until eof
                while (!(reader.Peek() == -1))
                {
                    thisLine = reader.ReadLine();

                    if (useEncryption)
                    {
                        thisLine = EncryptionTools.Decrypt(thisLine, encKey, true);
                        parts = Regex.Split(thisLine, keyValueSeparator);

                        if (!(parts[0].Equals("SYSTEM_ENCRYPTION_VALIDATOR") && parts[1].Equals("ENCRYPTION_OK")))
                        {
                            iniEntries.Add(thisLine);
                        }
                    }
                    else
                    {
                        iniEntries.Add(thisLine);
                    }
                }

                reader.Close();

            }
            catch (Exception)
            {
                // The file's empty.
            }

            return true;

        }

        public bool GetValue(string theKey, ref string theValue)
        {
            String errMsg = "";
            return GetValue(theKey, ref theValue, ref errMsg);
        }

        public bool GetValue(string theKey, ref string theValue, ref string errMsg)
        {

            if (!File.Exists(iniPath))
            {
                errMsg = "Ini file not found.";
                return false;
            }

            if (useEncryption)
            {
                if (!ValidateEncryptedIniFile())
                {
                    errMsg = "This file is not an encrypted ini file created by this class, or decryption failed / invalid key.";
                    return false;
                }
            }

            bool valueFound = false;

            try
            {
                StreamReader reader = new StreamReader(iniPath);
                string thisLine = "";
                String[] parts;

                while (!(reader.Peek() == -1)) //Until eof
                {
                    thisLine = reader.ReadLine();

                    if (useEncryption)
                    {
                        thisLine = EncryptionTools.Decrypt(thisLine, encKey, true);
                        parts = Regex.Split(thisLine, keyValueSeparator);

                        if (!(parts[0].Equals("SYSTEM_ENCRYPTION_VALIDATOR") && parts[1].Equals("ENCRYPTION_OK")))
                        {
                            if (parts[0].Equals(theKey))
                            {
                                theValue = parts[1];
                                valueFound = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        parts = Regex.Split(thisLine, keyValueSeparator);
                        if (parts[0].Equals(theKey))
                        {
                            theValue = parts[1];
                            valueFound = true;
                            break;
                        }
                    }
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            if (valueFound) { return true; }
            return false;

            //ArrayList iniEntries = new ArrayList();
            //Int32 count = 0;
            //string[] parts = null;
            //string tmp = "";
            //string tmpKey = "";
            //string tmpValue = "";

            //// Get the contents of the ini file.
            //if (!ReadAllEntries(ref iniEntries, ref tmp))
            //{
            //    errMsg = tmp;
            //    return false;
            //}

            //// search for the key...
            //if (iniEntries.Count > 0)
            //{
            //    for (count = 0; count <= iniEntries.Count - 1; count++)
            //    {
            //        tmp = Convert.ToString(iniEntries[count]);
            //        parts = Regex.Split(tmp, keyValueSeparator);
            //        tmpKey = parts[0];
            //        tmpValue = parts[1];

            //        // Have we found it?
            //        if (theKey.ToLower().Trim() == tmpKey.ToLower().Trim())
            //        {
            //            theValue = tmpValue;
            //            return true;
            //        }
            //    }

            //    theValue = "";
            //}
            //else
            //{
            //    theValue = "No entries found in the ini file.";
            //}

            //return false;

        }

        public bool RemoveEntry(string theKey)
        {
            String errMsg = "";
            return RemoveEntry(theKey, ref errMsg);
        }

        public bool RemoveEntry(string theKey, ref string errorMessage)
        {

            ArrayList iniEntries = new ArrayList();
            Int32 count = 0;
            string tmp = "";
            string[] parts = null;
            string tmpKey = "";
            string tmpValue = "";
            bool entryRemoved = false;

            // Get the contents of the ini file, and
            // report an error reading from it, if any.
            if (!ReadAllEntries(ref iniEntries, ref tmp))
            {
                errorMessage = tmp;
                return false;
            }

            // Delete the original ini file.
            try
            {
                File.Delete(iniPath);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

            // Wait for it to be deleted...
            while (File.Exists(iniPath))
            {
                PauseWithoutBlockingUI(10);
            }

            try
            {
                StreamWriter writer = new StreamWriter(iniPath, false);

                if (iniEntries.Count < 1)
                {
                    errorMessage = "Entry does not exist.";
                    return false;
                }
                else
                {
                    for (count = 0; count <= iniEntries.Count - 1; count++)
                    {
                        tmp = Convert.ToString(iniEntries[count]);
                        parts = Regex.Split(tmp, keyValueSeparator);
                        tmpKey = parts[0];
                        tmpValue = parts[1];

                        // If we find the entry,
                        if (theKey.ToLower().Trim() == tmpKey.ToLower().Trim())
                        {
                            // Leave it out.
                            entryRemoved = true;
                        }
                        else
                        {
                            if ((useEncryption))
                                tmp = EncryptionTools.Encrypt(tmp, encKey, true);
                            writer.WriteLine(tmp);
                        }
                    }

                }

                writer.Close();

            }
            catch (Exception ex)
            {
                errorMessage = "Error writing ini file.\n\n" + ex.Message;
                return false;
            }

            if (!entryRemoved)
                errorMessage = "Entry does not exist.";
            return entryRemoved;

        }

        public bool WriteEntry(String theKey, String theValue)
        {
            String errMsg = "";
            return WriteEntry(theKey, theValue, ref errMsg);
        }

        public bool WriteEntry(string theKey, string theValue, ref string errorMessage)
        {

            ArrayList iniEntries = new ArrayList();
            Int32 count = 0;
            string tmp = "";
            string[] parts = null;
            string tmpKey = "";
            string tmpValue = "";
            bool valueWritten = false;
            StringBuilder theEntry = new StringBuilder();

            // If ini file doesn't exist, create it.
            if (!File.Exists(iniPath))
            {
                try
                {
                    CreateFolders(GetFoldersFromPath(iniPath));
                    FileStream fs = new FileStream(iniPath, FileMode.Create);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    errorMessage = "Could not create ini file: " + iniPath + ". (" + ex.Message + ")";
                    return false;
                }
            }

            // Get the contents of the ini file, and
            // report an error reading from it, if any.
            if (!ReadAllEntries(ref iniEntries, ref tmp))
            {
                errorMessage = tmp;
                return false;
            }

            // Delete the original ini file.
            if (!ClearINI(ref errorMessage))
            {
                return false;
            }

            theEntry.Append(theKey);
            theEntry.Append(keyValueSeparator);
            theEntry.Append(theValue);

            try
            {
                StreamWriter writer = new StreamWriter(iniPath, useEncryption);

                if (iniEntries.Count < 1)
                {
                    tmp = theEntry.ToString();
                    if ((useEncryption))
                        tmp = EncryptionTools.Encrypt(tmp, encKey, true);

                    writer.WriteLine(tmp);
                    valueWritten = true;
                }
                else
                {
                    for (count = 0; count <= iniEntries.Count - 1; count++)
                    {
                        tmp = Convert.ToString(iniEntries[count]);
                        parts = Regex.Split(tmp, keyValueSeparator);
                        tmpKey = parts[0];
                        tmpValue = parts[1];

                        // Modify ini file if the key already exists.
                        if (theKey.ToLower().Trim() == tmpKey.ToLower().Trim())
                        {
                            tmp = theEntry.ToString();
                            if ((useEncryption))
                                tmp = EncryptionTools.Encrypt(tmp, encKey, true);

                            writer.WriteLine(tmp);
                            valueWritten = true;
                        }
                        else
                        {
                            if ((useEncryption))
                                tmp = EncryptionTools.Encrypt(tmp, encKey, true);
                            writer.WriteLine(tmp);
                        }
                    }

                    // Add it to the ini file if the key didn't exist in it already
                    if (!valueWritten)
                    {
                        tmp = theEntry.ToString();
                        if ((useEncryption))
                            tmp = EncryptionTools.Encrypt(tmp, encKey, true);

                        writer.WriteLine(tmp);
                        valueWritten = true;
                    }

                }

                writer.Close();

            }
            catch (Exception ex)
            {
                errorMessage = "Error writing ini file.\n\n" + ex.Message;
                return false;
            }

            return valueWritten;
        }

        private class EncryptionTools
        {
            public static String Encrypt(String toEncrypt, String key, bool useHashing)
            {
                byte[] keyArray = null;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                //If hashing use get hashcode regards to your key
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice

                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                //transform the specified region of bytes array to resultArray
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                //Release resources held by TripleDes Encryptor
                tdes.Clear();
                //Return the encrypted data into unreadable string format
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }

            public static Byte[] Encrypt(Byte[] toEncryptArray, Int32 count, String key, bool useHashing)
            {
                byte[] keyArray = null;

                //System.Windows.Forms.MessageBox.Show(key);
                //If hashing use get hashcode regards to your key
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice

                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)

                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                //transform the specified region of bytes array to resultArray
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, count);
                //Release resources held by TripleDes Encryptor
                tdes.Clear();
                //Return the encrypted data into unreadable string format
                return resultArray;
            }

            public static String Decrypt(String cipherString, String key, bool useHashing)
            {
                byte[] keyArray = null;
                //get the byte code of the string

                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                if (useHashing)
                {
                    //if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //release any resource held by the MD5CryptoServiceProvider

                    hashmd5.Clear();
                }
                else
                {
                    //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)

                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                //Release resources held by TripleDes Encryptor                
                tdes.Clear();
                //return the Clear decrypted TEXT
                return UTF8Encoding.UTF8.GetString(resultArray);
            }

            // toDecryptArray = Convert.FromBase64String()
            // to: utf8 string. (System.Text.UTF8Encoding.UTF8.GetBytes())
            public static bool Decrypt(Byte[] arrayToDecrypt, ref Byte[] resultArray, Int32 inputCount, String key, bool useHashing, ref String errMsg)
            {
                byte[] keyArray = null;
                bool results = true;

                //get the byte code of the string
                if (useHashing)
                {
                    //if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    //release any resource held by the MD5CryptoServiceProvider

                    hashmd5.Clear();
                }
                else
                {
                    //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)

                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                try
                {
                    resultArray = cTransform.TransformFinalBlock(arrayToDecrypt, 0, inputCount);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    results = false;
                }

                //Release resources held by TripleDes Encryptor                
                tdes.Clear();
                //return the Clear decrypted TEXT
                return results;
            }

        }
    }
}
