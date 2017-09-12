using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.DependencyServices;
using tpm.Models;
using Xamarin.Forms;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using Xfinium.Pdf.Graphics.FormattedContent;

namespace tpm.Pdf {
    public sealed class PdfDrawing {

        private static readonly int _LEFT_RESTRICT = 20;
        private static readonly int _TOP_RESTRICT = 40;
        private static readonly int _RIGHT_RESTRICT = 20;
        private static readonly int _BOTTOM_RESTRICT = 720;
        private static readonly PdfRgbColor _secondaryGrayColor = new PdfRgbColor(215, 215, 224);
        private static readonly PdfRgbColor _mainBlackColor = new PdfRgbColor(32, 33, 37);
        private static readonly PdfRgbColor _greenColor = new PdfRgbColor(38, 201, 95);
        private static readonly PdfRgbColor _redColor = new PdfRgbColor(246, 84, 84);

        private PdfFixedDocument _document;
        private PdfStandardFont _fontHelvetica21 = new PdfStandardFont(PdfStandardFontFace.Helvetica, 21);
        private PdfStandardFont _fontHelvetica28 = new PdfStandardFont(PdfStandardFontFace.Helvetica, 28);
        private PdfStandardFont _fontHelvetica36 = new PdfStandardFont(PdfStandardFontFace.Helvetica, 36);

        private PdfPage _curentPage;
        private int _xOffset;
        private int _yOffset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public Task DrawAsync(User user) {
            return Task.Run(() => {
                _document = new PdfFixedDocument();

                TEST_GEN_PDF(user);

                _document = null;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        private void TEST_GEN_PDF(User user) {
            CreateNewPdfPage();

            Draw(user);

            Stream pdfStream = DependencyService.Get<IFileHelper>().GetFileStreamForPDF();

            _document.Save(pdfStream);
            pdfStream.Flush();
            pdfStream.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private void Draw(User user) {
            _xOffset = _RIGHT_RESTRICT;

            DrawUserInfo(user);
            DrawQuestions(user);
        }

        /// <summary>
        /// User info drawing schema
        /// </summary>
        /// <param name="user"></param>
        private void DrawUserInfo(User user) {
            PdfFormattedContent userInfoBlock = CreateFormatedContent(
                CreateParagraph(
                    0, 20,
                    new Tuple<string, PdfStandardFont, PdfColor>("User information: ", _fontHelvetica21, _mainBlackColor)
                ),
                CreateParagraph(
                    0, 20,
                    new Tuple<string, PdfStandardFont, PdfColor>("Name: ", _fontHelvetica21, _secondaryGrayColor),
                    new Tuple<string, PdfStandardFont, PdfColor>(user.UserName, _fontHelvetica28, _mainBlackColor)
                ),
                CreateParagraph(
                    0, 20,
                    new Tuple<string, PdfStandardFont, PdfColor>("Company information: ", _fontHelvetica21, _mainBlackColor)
                ),
                CreateParagraph(
                    0, 0,
                    new Tuple<string, PdfStandardFont, PdfColor>("Name: ", _fontHelvetica21, _secondaryGrayColor),
                    new Tuple<string, PdfStandardFont, PdfColor>(user.CompanyName, _fontHelvetica28, _mainBlackColor)
                ),
                CreateParagraph(
                    0, 0,
                    new Tuple<string, PdfStandardFont, PdfColor>("Address: ", _fontHelvetica21, _secondaryGrayColor),
                    new Tuple<string, PdfStandardFont, PdfColor>(user.CompanyAddress, _fontHelvetica28, _mainBlackColor)
                ),
                CreateParagraph(
                    0, 0,
                    new Tuple<string, PdfStandardFont, PdfColor>("Sity, state, zip: ", _fontHelvetica21, _secondaryGrayColor),
                    new Tuple<string, PdfStandardFont, PdfColor>(user.CompanyCityStateZip, _fontHelvetica28, _mainBlackColor)
                ),
                CreateParagraph(
                    0, 20,
                    new Tuple<string, PdfStandardFont, PdfColor>("Specific location: ", _fontHelvetica21, _secondaryGrayColor),
                    new Tuple<string, PdfStandardFont, PdfColor>(user.CompanyCityStateZip, _fontHelvetica28, _mainBlackColor)
                )
            );
            userInfoBlock.Wrap(_curentPage.Width - _RIGHT_RESTRICT);
            _curentPage.Graphics.DrawFormattedContent(userInfoBlock, _xOffset, _yOffset);
            _yOffset += (int)userInfoBlock.Height;
        }

        /// <summary>
        /// Questions drawing schema
        /// </summary>
        /// <param name="user"></param>
        private void DrawQuestions(User user) {
            //
            // Question indexes will be messed. Because they also was grouped... TODO: remove index from question model and use only index in QuestionViewModel which can change its own number.
            //
            int index = 0;

            foreach (Question question in user.Questions) {
                index++;

                PdfFormattedContent questionMessage =
                    CreateFormatedContent(
                        CreateParagraph(
                            20, 20,
                            new Tuple<string, PdfStandardFont, PdfColor>(string.Format("{0}. {1}", index, question.QuestionMessage), _fontHelvetica21, _mainBlackColor),
                            new Tuple<string, PdfStandardFont, PdfColor>(string.Format(" - {0}", question.Answer.Value ? "Yes" : "No"), _fontHelvetica28, _mainBlackColor)
                        )
                    );
                questionMessage.Wrap(_curentPage.Width - _RIGHT_RESTRICT);

                ResolvePageInnerCapacity((int)questionMessage.Height);

                DrawLine(_secondaryGrayColor, 3, new PdfPoint(0, _yOffset), new PdfPoint(_curentPage.Width - _RIGHT_RESTRICT, _yOffset));
                _yOffset += 3;

                _curentPage.Graphics.DrawFormattedContent(questionMessage, _xOffset, _yOffset);
                _yOffset += (int)questionMessage.Height;

                DrawComments(question);
                DrawImages(question);
            }
        }

        /// <summary>
        /// Comments drawing schema
        /// </summary>
        /// <param name="user"></param>
        private void DrawComments(Question question) {
            if (!(question.Comments.Any())) {
                return;
            }

            PdfFormattedContent commentsHeader =
                CreateFormatedContent(
                    CreateParagraph(
                        0, 20,
                        new Tuple<string, PdfStandardFont, PdfColor>(string.Format("Attached comments ({0}):", question.Comments.Count), _fontHelvetica21, _mainBlackColor)
                    )
                );

            commentsHeader.Wrap(_curentPage.Width - _RIGHT_RESTRICT);
            ResolvePageInnerCapacity((int)commentsHeader.Height);
            _curentPage.Graphics.DrawFormattedContent(commentsHeader, _xOffset, _yOffset);
            _yOffset += (int)commentsHeader.Height;

            foreach (AttachedComment comment in question.Comments) {
                PdfFormattedContent commentText =
                    CreateFormatedContent(
                        CreateParagraph(
                            5, 5,
                            new Tuple<string, PdfStandardFont, PdfColor>(string.Format(" - {0}", comment.CommentMessage), _fontHelvetica21, _mainBlackColor),
                            new Tuple<string, PdfStandardFont, PdfColor>(string.Format(" {0:dd MMM H:mm}", comment.PublishDate), _fontHelvetica21, _secondaryGrayColor)
                        )
                    );
                commentText.Wrap(_curentPage.Width - _RIGHT_RESTRICT);
                ResolvePageInnerCapacity((int)commentText.Height);
                _curentPage.Graphics.DrawFormattedContent(commentText, _xOffset, _yOffset);
                _yOffset += (int)commentText.Height;
            }

            _yOffset += 20;
        }

        /// <summary>
        /// Images drawing schema
        /// </summary>
        /// <param name="question"></param>
        private void DrawImages(Question question) {
            if (!(question.Images.Any())) {
                return;
            }

            PdfFormattedContent imagesHeader =
            CreateFormatedContent(
                CreateParagraph(
                    0, 20,
                    new Tuple<string, PdfStandardFont, PdfColor>(string.Format("Attached images ({0}):", question.Images.Count), _fontHelvetica21, _mainBlackColor)
                )
            );

            imagesHeader.Wrap(_curentPage.Width - _RIGHT_RESTRICT);
            ResolvePageInnerCapacity((int)imagesHeader.Height);
            _curentPage.Graphics.DrawFormattedContent(imagesHeader, _xOffset, _yOffset);
            _yOffset += (int)imagesHeader.Height;


            foreach (AttachedImage image in question.Images) {
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(image.Base64Image));
                PdfPngImage pngImage = new PdfPngImage(stream);

                int srcHeight = pngImage.Height;
                int srcWidth = pngImage.Width;
                GetScaledSize(ref srcWidth, ref srcHeight);

                ResolvePageInnerCapacity(srcHeight);
                _curentPage.Graphics.DrawImage(pngImage, _xOffset, _yOffset, srcWidth, srcHeight);
                _yOffset += (int)srcHeight;
                _yOffset += 10;
            }

            _yOffset += 20;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        private void DrawLine(PdfColor color, double width, PdfPoint point1, PdfPoint point2) {
            ResolvePageInnerCapacity((int)width);

            _curentPage.Graphics.DrawLine(new PdfPen(color, width), point1, point2);
            _yOffset += (int)width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private PdfFormattedParagraph CreateParagraph(PdfStandardFont font, params string[] text) {
            PdfFormattedParagraph paragraph = new PdfFormattedParagraph();
            paragraph.HorizontalAlign = PdfStringHorizontalAlign.Left;
            paragraph.LineSpacingMode = PdfFormattedParagraphLineSpacing.Single;

            foreach (string textBlock in text) {
                paragraph.Blocks.Add(new PdfFormattedTextBlock(textBlock, font));
            }

            return paragraph;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private PdfFormattedParagraph CreateParagraph(double spacingBefore, double spacingAfter, params Tuple<string, PdfStandardFont, PdfColor>[] tupleTextBloks) {
            PdfFormattedParagraph paragraph = new PdfFormattedParagraph();
            paragraph.HorizontalAlign = PdfStringHorizontalAlign.Left;
            paragraph.LineSpacingMode = PdfFormattedParagraphLineSpacing.Single;
            paragraph.SpacingBefore = spacingBefore;
            paragraph.SpacingAfter = spacingAfter;

            foreach (Tuple<string, PdfStandardFont, PdfColor> tupleBlok in tupleTextBloks) {
                PdfFormattedTextBlock formatedBlock = new PdfFormattedTextBlock(tupleBlok.Item1, tupleBlok.Item2);
                formatedBlock.TextColor = new PdfBrush(tupleBlok.Item3);

                paragraph.Blocks.Add(formatedBlock);
            }

            return paragraph;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paragraphs"></param>
        /// <returns></returns>
        private PdfFormattedContent CreateFormatedContent(params PdfFormattedParagraph[] paragraphs) {
            PdfFormattedContent formattedContent = new PdfFormattedContent();

            foreach (PdfFormattedParagraph paragraph in paragraphs) {
                formattedContent.Paragraphs.Add(paragraph);
            }

            return formattedContent;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateNewPdfPage() {
            _curentPage = _document.Pages.Add();
            _yOffset = _TOP_RESTRICT;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResolvePageInnerCapacity(int neededSpace) {
            int freeSpace = _BOTTOM_RESTRICT - _yOffset;

            if (freeSpace < neededSpace) {
                CreateNewPdfPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcSize"></param>
        private void GetScaledSize(ref int srcWidth, ref int srcHeight) {
            double inSampleSize = 1;

            while (srcWidth >= _curentPage.Width || srcHeight >= _curentPage.Height) {
                inSampleSize++;

                srcWidth = (int)(srcWidth / inSampleSize);
                srcHeight = (int)(srcHeight / inSampleSize);
            }
        }
    }
}