using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.DependencyServices {
    public interface IFileHelper {
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool Exists(string fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        void WriteText(string fileName, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string ReadText(string fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetFiles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        void Delete(string fileName);
        */

        /// <summary>
        ///
        /// </summary>
        string GeneratedPdfFileName { get; }

        /// <summary>
        /// 
        /// </summary>
        string DownloadedPdfFileName { get; }

        /// <summary>
        /// Returns FileStream for PDF
        /// </summary>
        /// <param name="fileName"></param>
        Stream GetFileStreamForPDF();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        //void DownloadSource(string src);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        Task<bool> DownloadSourceAsync(string src);
    }
}
