using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;


namespace tpm.Pdf {
    public sealed class UserPdfDrawingScheme : PdfKreator<User> {

        /// <summary>
        /// PdfKreator<User> implementation
        /// </summary>
        public override User Essence {
            get; protected set;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void BeginDrawing() {
            DrawUserInfo();
            DrawQuestions();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawUserInfo() {
            SizeF neededSpace = _fontHelvetica16.MeasureString("Name:", _pdfStringFormat);
            ResolvePageInnerCapacity(neededSpace.Height);

            PdfLayoutResult pdfLayoutResult;

            //
            // User name
            //
            pdfLayoutResult = InitTextElement("Name:", _fontHelvetica16, _grayBrush, _pdfStringFormat, 0)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset += pdfLayoutResult.Bounds.Width;

            pdfLayoutResult = InitTextElement(Essence.UserName, _fontHelvetica16, _blackBrush, _pdfStringFormat, 3)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset = 0;
            _yOffset += pdfLayoutResult.Bounds.Height;

            ResolvePageInnerCapacity(neededSpace.Height);

            //
            // Company name
            //
            pdfLayoutResult = InitTextElement("Company name:", _fontHelvetica16, _grayBrush, _pdfStringFormat, 0)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset += pdfLayoutResult.Bounds.Width;

            pdfLayoutResult = InitTextElement(Essence.CompanyName, _fontHelvetica16, _blackBrush, _pdfStringFormat, 3)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset = 0;
            _yOffset += pdfLayoutResult.Bounds.Height;

            ResolvePageInnerCapacity(neededSpace.Height);

            //
            // Company address
            //
            pdfLayoutResult = InitTextElement("Company address:", _fontHelvetica16, _grayBrush, _pdfStringFormat, 0)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset += pdfLayoutResult.Bounds.Width;

            pdfLayoutResult = InitTextElement(Essence.CompanyAddress, _fontHelvetica16, _blackBrush, _pdfStringFormat, 3)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset = 0;
            _yOffset += pdfLayoutResult.Bounds.Height;

            ResolvePageInnerCapacity(neededSpace.Height);

            //
            // Company city state zip
            //
            pdfLayoutResult = InitTextElement("City state zip:", _fontHelvetica16, _grayBrush, _pdfStringFormat, 0)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset += pdfLayoutResult.Bounds.Width;

            pdfLayoutResult = InitTextElement(Essence.CompanyCityStateZip, _fontHelvetica16, _blackBrush, _pdfStringFormat, 3)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset = 0;
            _yOffset += pdfLayoutResult.Bounds.Height;

            ResolvePageInnerCapacity(neededSpace.Height);

            //
            // Company specific location
            //
            pdfLayoutResult = InitTextElement("City specific location:", _fontHelvetica16, _grayBrush, _pdfStringFormat, 0)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset += pdfLayoutResult.Bounds.Width;

            pdfLayoutResult = InitTextElement(Essence.CompanySpecificLocation, _fontHelvetica16, _blackBrush, _pdfStringFormat, 3)
                .Draw(_currentPage, _xOffset, _yOffset);

            _xOffset = 0;
            _yOffset += pdfLayoutResult.Bounds.Height;

            ResolvePageInnerCapacity(neededSpace.Height);

            _yOffset += 21;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawQuestions() {
            float margin = 21;
            float workingWidth = _currentPage.GetClientSize().Width;
            PdfLayoutFormat pdfLayoutFormat = new PdfLayoutFormat();
            SizeF neededSpace;
            PdfLayoutResult pdfLayoutResult;

            for (int i = 0; i < Essence.Questions.Count; i++) {
                //
                // Write question message and answer
                //
                string questionMessageString =
                    string.Format("{0}. {1} -{2}.", (i + 1), Essence.Questions[i].QuestionMessage, Essence.Questions[i].Answer);

                neededSpace =
                    _fontHelvetica14.MeasureString(questionMessageString, _pdfStringFormat);
                ResolvePageInnerCapacity(neededSpace.Height + margin);

                DrawDividerLine();
                _yOffset += margin;

                pdfLayoutResult =
                    InitTextElement(questionMessageString, _fontHelvetica14, _blackBrush, _pdfStringFormat, 0)
                        .Draw(_currentPage, new PointF(_xOffset, _yOffset), workingWidth, pdfLayoutFormat);

                _yOffset += (pdfLayoutResult.Bounds.Height + margin);

                //
                // Go through comments
                //
                if (Essence.Questions[i].Comments.Any()) {
                    float commentMargin = 13;
                    string questionCommentsCountString =
                        string.Format("Comments ({0}):", Essence.Questions[i].Comments.Count);

                    neededSpace =
                        _fontHelvetica14.MeasureString(questionCommentsCountString, _pdfStringFormat);
                    ResolvePageInnerCapacity(neededSpace.Height + margin);

                    pdfLayoutResult =
                        InitTextElement(questionCommentsCountString, _fontHelvetica14, _blackBrush, _pdfStringFormat, 0)
                            .Draw(_currentPage, new PointF(_xOffset, _yOffset), workingWidth, pdfLayoutFormat);

                    _yOffset += (pdfLayoutResult.Bounds.Height + commentMargin);
                    _xOffset = commentMargin;

                    foreach (AttachedComment comment in Essence.Questions[i].Comments) {
                        DrawComment(comment, commentMargin);
                    }
                }

                _xOffset = 0;

                //
                // Go through images
                //
                if (Essence.Questions[i].Images.Any()) {
                    float commentMargin = 13;
                    string questionImagesCountString =
                        string.Format("Images ({0}):", Essence.Questions[i].Images.Count);

                    neededSpace =
                        _fontHelvetica14.MeasureString(questionImagesCountString, _pdfStringFormat);
                    ResolvePageInnerCapacity(neededSpace.Height + margin);

                    pdfLayoutResult =
                        InitTextElement(questionImagesCountString, _fontHelvetica14, _blackBrush, _pdfStringFormat, 0)
                            .Draw(_currentPage, new PointF(_xOffset, _yOffset), workingWidth, pdfLayoutFormat);

                    _yOffset += (pdfLayoutResult.Bounds.Height + commentMargin);
                    _xOffset = commentMargin;
                    _xOffset = 0;

                    foreach (AttachedImage image in Essence.Questions[i].Images) {
                        DrawImage(image, commentMargin);
                    }
                }

                _xOffset = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        private void DrawComment(AttachedComment comment, float marginForComments) {
            float margin = marginForComments;
            float workingWidth = _currentPage.GetClientSize().Width;
            PdfLayoutFormat pdfLayoutFormat = new PdfLayoutFormat();
            PdfLayoutResult pdfLayoutResult;

            string commentString =
                string.Format("{0}", comment.CommentMessage);
            string commentPublishDate =
                string.Format("Published: {0:dd MMM H:mm}", comment.PublishDate);

            ResolvePageInnerCapacity(
                _fontHelvetica14.MeasureString(commentString, _pdfStringFormat).Height +
                _fontHelvetica14.MeasureString(commentString, _pdfStringFormat).Height +
                margin);

            pdfLayoutResult =
                InitTextElement(commentString, _fontHelvetica14, _blackBrush, _pdfStringFormat, 0)
                    .Draw(_currentPage, new PointF(_xOffset, _yOffset), workingWidth, pdfLayoutFormat);

            _yOffset += pdfLayoutResult.Bounds.Height;

            pdfLayoutResult =
                InitTextElement(commentPublishDate, _fontHelvetica14, _blackBrush, _pdfStringFormat, 0)
                    .Draw(_currentPage, new PointF(_xOffset, _yOffset), workingWidth, pdfLayoutFormat);

            _yOffset += (pdfLayoutResult.Bounds.Height + margin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        private void DrawImage(AttachedImage image, float marginForImages) {
            float margin = marginForImages;
            
            PdfBitmap pdfBitmap = InitPdfBitmap(image.Base64Image);

            ResolvePageInnerCapacity(pdfBitmap.Height);

            float PageWidth = _currentPage.Graphics.ClientSize.Width;
            float PageHeight = _currentPage.Graphics.ClientSize.Height - _yOffset;
            float myWidth = pdfBitmap.Width;
            float myHeight = pdfBitmap.Height;

            float shrinkFactor;

            if (myWidth > PageWidth) {
                shrinkFactor = myWidth / PageWidth;
                myWidth = PageWidth;
                myHeight = myHeight / shrinkFactor;
            }

            if (myHeight > PageHeight) {
                shrinkFactor = myHeight / PageHeight;
                myHeight = PageHeight;
                myWidth = myWidth / shrinkFactor;
            }

            float XPosition = (PageWidth - myWidth) / 2;
            float YPosition = ((PageHeight - myHeight) / 2)+_yOffset;

            _currentPage.Graphics.DrawImage(pdfBitmap, XPosition, YPosition, myWidth, myHeight);

            _yOffset = _currentPage.GetClientSize().Height;
        }

//        float PageWidth = _currentPage.Graphics.ClientSize.Width;
//        float PageHeight = _currentPage.Graphics.ClientSize.Height;
//        float myWidth = pdfBitmap.Width;
//        float myHeight = pdfBitmap.Height;

//        float shrinkFactor;

//            if (myWidth > PageWidth) {
//                shrinkFactor = myWidth / PageWidth;
//                myWidth = PageWidth;
//                myHeight = myHeight / shrinkFactor;
//            }

//            if (myHeight > PageHeight) {
//                shrinkFactor = myHeight / PageHeight;
//                myHeight = PageHeight;
//                myWidth = myWidth / shrinkFactor;
//            }

//float XPosition = (PageWidth - myWidth) / 2;
//float YPosition = (PageHeight - myHeight) / 2;

//            ResolvePageInnerCapacity(pdfBitmap.Height);

//_currentPage.Graphics.DrawImage(pdfBitmap, XPosition, YPosition, myWidth, myHeight);

//            _yOffset = _currentPage.GetClientSize().Height;
    }
}