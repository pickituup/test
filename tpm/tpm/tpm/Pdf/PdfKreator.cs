using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.DependencyServices;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.Pdf {
    public abstract class PdfKreator<T> where T : IPdfCanBeDrawed {

        protected PdfDocument _document;
        protected PdfPage _currentPage;

        protected PdfStringFormat _pdfStringFormat;
        protected PdfFont _fontHelvetica14,
            _fontHelvetica16,
            _fontHelvetica18,
            _fontHelvetica20;
        protected PdfBrush _blackBrush,
            _blueBrush,
            _grayBrush;

        protected float _yOffset;
        protected float _xOffset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public Task DrawAsync(T essence) {
            return Task.Run(() => {
                Essence = essence;

                PrepareDrawing();
                BeginDrawing();
                EndDrawing();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract T Essence { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        protected abstract void BeginDrawing();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="stringFormat"></param>
        /// <param name="position"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        protected PdfTextElement InitTextElement(string text, PdfFont font, PdfBrush brush, PdfStringFormat stringFormat, int space) {
            string prefixSpace = new string(' ', space);

            PdfTextElement textElement = new PdfTextElement(string.Format("{0}{1}", prefixSpace, text)) {
                Font = font,
                Brush = brush,
                StringFormat = stringFormat
            };

            return textElement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected PdfBitmap InitPdfBitmap(string base64) {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64));
            PdfBitmap pdfBitmap = new PdfBitmap(stream);

            return pdfBitmap;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void DrawDividerLine() {
            _currentPage.Graphics.DrawLine(new PdfPen(_grayBrush) { Width = 1 }, 0, _yOffset, _currentPage.GetClientSize().Width, _yOffset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neededSpace"></param>
        protected void ResolvePageInnerCapacity(float neededSpace) {
            float freeSpace = _currentPage.GetClientSize().Height - _yOffset;

            if (freeSpace < neededSpace) {
                AddNewPageToTheDocument();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrepareDrawing() {
            //Create a new PDF document.

            _document = new PdfDocument();
            _document.PageSettings.Size = PdfPageSize.A4;

            AddNewPageToTheDocument();

            _fontHelvetica14 = new PdfStandardFont(PdfFontFamily.Helvetica, 14);
            _fontHelvetica16 = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            _fontHelvetica18 = new PdfStandardFont(PdfFontFamily.Helvetica, 18);
            _fontHelvetica20 = new PdfStandardFont(PdfFontFamily.Helvetica, 28);

            _blackBrush = PdfBrushes.Black;
            _grayBrush = PdfBrushes.LightGray;
            _blueBrush = PdfBrushes.RoyalBlue;

            _pdfStringFormat = new PdfStringFormat();
            _pdfStringFormat.Alignment = PdfTextAlignment.Left;
            _pdfStringFormat.WordWrap = PdfWordWrapType.Word;
        }

        /// <summary>
        /// 
        /// </summary>
        private void EndDrawing() {
            Stream pdfStream = DependencyService.Get<IFileHelper>().GetFileStreamForPDF();

            _document.Save(pdfStream);
            _document.Close(true);
            _document.Dispose();
            
            pdfStream.Close();
            pdfStream.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddNewPageToTheDocument() {
            _currentPage = _document.Pages.Add();

            _yOffset = 0;
        }
    }
}