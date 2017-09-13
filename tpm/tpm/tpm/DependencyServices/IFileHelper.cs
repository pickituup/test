using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.DependencyServices {
    public interface IFileHelper {

        /// <summary>
        ///
        /// </summary>
        string GeneratedPdfFileName { get; }

        /// <summary>
        /// 
        /// </summary>
        string DownloadedPdfFileName { get; }

        /// <summary>
        /// 
        /// </summary>
        string TpmDictionaryPath { get; }

        /// <summary>
        /// Returns FileStream for PDF
        /// </summary>
        /// <param name="fileName"></param>
        Stream GetFileStreamForPDF();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        Task<bool> DownloadSourceAsync(string src);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        bool IsFileExists(string fullFilePath);
    }
}
