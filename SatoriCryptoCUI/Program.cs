using LibSatoriCrypto;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SatoriCryptoCUI
{
    internal static class Program
    {
        static Translate tr = new Translate(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "i18n"),
            CultureInfo.CurrentUICulture.Name);

        static void Main(string[] args)
        {
#if DEBUG
            if (args.Length <= 0)
            {
                Console.WriteLine(tr.t("[D] コマンドラインの引数を入力してください。"));
                args = Console.ReadLine().Split(" ");
                Console.Clear();
            }
#endif
            var argsInfo = AnalyzeArgs(args);

            if (argsInfo.RequiredShowHelp)
            {
                ShowHelp();
                return;
            }
            if (string.IsNullOrEmpty(argsInfo.FilePath))
            {
                TWriteLine("Encryption or decryption target file is not specified. See '--help' for how to use.");
                return;
            }
            if (!File.Exists(argsInfo.FilePath))
            {
                TWriteLine("Encryption or decryption target file is not exists. Please specification other file.");
                return;
            }

            if (argsInfo.RequiredDecrypt)
            {
                TWriteLine("Start decrypt...");
                var newFileName = Path.GetFileName(argsInfo.FilePath).EndsWith(".sef") 
                    ? argsInfo.FilePath[0..^4] : argsInfo.FilePath;

                if (!ConfirmAllowOverwrite(newFileName)) return;

                try
                {
                    SatoriCrypto.DecryptFile(argsInfo.FilePath, newFileName, argsInfo.Password, new CryptoProgress(tr));
                    TWriteLine("Saved decrypted file at {0}", newFileName);
                }
                catch (Exception ex)
                {
                    TWriteLine("[X] Error occured when decrypting.");
                    TWriteLine("[X] Error message: {0}", ex.Message);
                    TWriteLine("[X] If you are decrypting an encrypted file contains an AES crypto, make sure the password is correct.");
                }
            }
            else
            {
                TWriteLine("Superencryption mode: {0}", argsInfo.SuperencryptionMode.ToString());
                TWriteLine("Start encrypt...");

                if (!ConfirmAllowOverwrite(argsInfo.FilePath + ".sef")) return;
                
                SatoriCrypto.EncryptFile(argsInfo.FilePath, argsInfo.FilePath + ".sef", argsInfo.Password, argsInfo.SuperencryptionMode, new CryptoProgress(tr));
                TWriteLine("Saved encrypted file at {0}", argsInfo.FilePath + ".sef");
            }
        }

        static void ShowHelp()
        {
            TWriteLine("SatoriCrypto Console");
            Console.WriteLine("Usage: {0} [--password <password>] [--superenc-mode <superenc-mode>] [--help] [--decrypt] [--version] <file>", Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName));
            Console.WriteLine();
            TWriteLine("Parameter reference:");
            TWriteKeyAndValue("  --password (-p)", "Set the encryption password. This password is used for encryption or decryption.");
            TWriteKeyAndValue("  --superenc-mode (-s)", "Set the super-encryption mode (avaliable only encrypt mode). Please see 'Superencryption mode' section for more.");
            TWriteKeyAndValue("  --decrypt (-d)", "If you want to decrypt the file, set this parameter.");
            TWriteKeyAndValue("  --help (-h)", "Show help and exit.");
            TWriteKeyAndValue("  --version (-v)", "Show version info and exit.");
            TWriteKeyAndValue("  <file>", "Set the you want to encrypt or decrypt file.");
            Console.WriteLine();
            TWriteLine("Superencryption mode:");
            TWriteKeyAndValue("  none", "XOR password and file.");
            TWriteKeyAndValue("  pxf2key", "XOR key(XOR Password and filename) and file.");
            TWriteKeyAndValue("  aes", "Use AES crypto.");
            TWriteKeyAndValue("  px2aes", "XOR Password and AES encrypted file.");
        }

        static AnalyzedArgsInfo AnalyzeArgs(string[] args)
        {
            var argsInfo = new AnalyzedArgsInfo();

            string previousArg = null;
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-h":
                    case "--help":
                        argsInfo.RequiredShowHelp = true;
                        break;
                    
                    case "-d":
                    case "--decrypt":
                        argsInfo.RequiredDecrypt = true;
                        break;
                    
                    case "-s":
                    case "--superenc-mode":
                        argsInfo.SuperencryptionMode = arg switch
                        {
                            "none" => SuperencryptionMode.None,
                            "pxf2key" => SuperencryptionMode.PxF2Key,
                            "aes" => SuperencryptionMode.AES,
                            "pxaes" => SuperencryptionMode.PxAES,
                            _ => argsInfo.SuperencryptionMode
                        };
                        break;
                    
                    case "-p":
                    case "--password":
                        argsInfo.Password = arg;
                        break;
                }
                if (!arg.StartsWith("-")) argsInfo.FilePath = arg;

                previousArg = arg;
            }

            return argsInfo;
        }

        private static bool ConfirmAllowOverwrite(string filename)
        {
            if (File.Exists(filename)) return TUI.YesNo(tr.t("[?] '{0}' is already exists. Do you want to overwrite?", filename));
            return true;
        }

        private static void TWriteLine(string translate) => Console.WriteLine(tr.t(translate));
        private static void TWriteLine(string baseText, params object[] args) => Console.WriteLine(tr.t(baseText, args));
        private static void TWriteKeyAndValue(string key, string tValue) => Console.WriteLine(key + ": " + tr.t(tValue));
    }

    public struct AnalyzedArgsInfo
    {
        public string FilePath { get; set; } = "";
        public bool RequiredShowHelp { get; set; } = false;
        public SuperencryptionMode SuperencryptionMode { get; set; } = SuperencryptionMode.None;
        public string Password { get; set; } = "";
        public bool RequiredDecrypt = false;

        public AnalyzedArgsInfo()
        {
        }
    }
}