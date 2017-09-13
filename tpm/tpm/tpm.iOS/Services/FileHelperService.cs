﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using tpm.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.iOS.Services.FileHelperService))]
namespace tpm.iOS.Services {
    public sealed class FileHelperService : IFileHelper {

        public static readonly string GENERATED_PDF_FILE_NAME = "tpm_own_collected_data.pdf";
        public static readonly string DOWNLOADED_PDF_FILE_NAME = "WalkWork_Surfaces.pdf";
        public static readonly string TPM_EXTERNAL_DICTIONARY_NAME = "TPM";

        /// <summary>
        /// 
        /// </summary>
        public string GeneratedPdfFileName {
            get => GENERATED_PDF_FILE_NAME;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DownloadedPdfFileName {
            get => DOWNLOADED_PDF_FILE_NAME;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TpmDictionaryPath =>
            GetInternalTpmDirPath();

        /// <summary>
        /// Get path to the TMP internal Documents directory.
        /// </summary>
        /// <returns></returns>
        public static string GetInternalTpmDirPath() {
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string targetDir = System.IO.Path.Combine(dirPath, TPM_EXTERNAL_DICTIONARY_NAME);

            bool isDirExist = Directory.Exists(targetDir);

            if (isDirExist) {
                return targetDir;
            }
            else {
                Directory.CreateDirectory(targetDir);
            }

            return targetDir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public Task<bool> DownloadSourceAsync(string src) {
            return Task<bool>.Run(() => {
                //return TEST_1(src);
                return TEST_2(src);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Stream GetFileStreamForPDF() {
            string docsPath = GetInternalTpmDirPath();
            string fullFilePath = System.IO.Path.Combine(docsPath, GENERATED_PDF_FILE_NAME);

            FileStream fileStream = File.Open(fullFilePath, FileMode.Create);
            //
            // Clear all data from that file.
            //
            fileStream.SetLength(0);

            return fileStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public bool IsFileExists(string fullFilePath) =>
            File.Exists(fullFilePath);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private bool TEST_1(string src) {
            try {
                HttpWebRequest request = new HttpWebRequest(new Uri(src));

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (FileStream fileStream = System.IO.File.Open(System.IO.Path.Combine(GetInternalTpmDirPath(), DOWNLOADED_PDF_FILE_NAME), FileMode.Create)) {
                    //
                    // Clear all data from that file.
                    //
                    fileStream.SetLength(0);
                    stream.CopyTo(fileStream);
                }

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private bool TEST_2(string src) {
            try {
                string fullFilePath =
                    System.IO.Path.Combine(GetInternalTpmDirPath(), DOWNLOADED_PDF_FILE_NAME);

                WebClient webClient = new WebClient();
                webClient.DownloadFile(new Uri(src), fullFilePath);

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}