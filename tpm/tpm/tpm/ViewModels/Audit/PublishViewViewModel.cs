using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.Validtion;

namespace tpm.ViewModels {
    public sealed class PublishViewViewModel : ViewModelBase, IViewModel {

        private PublishValidation _publishValidation = new PublishValidation();
        private string _emailAddress;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PublishViewViewModel() {
            //
            // TODO: remove after testiong
            //
            EmailAddress = "emanueltestemail@gmail.com";

            HomeCommand = new Command(() => {
                BaseSingleton<AuditScopeObserver>.Instance.OnGoHome();
            });

            PublishCommand = new Command(async () => {
                //
                // TODO: validate email... send form PDF, send mail.
                //
                if (EmailAddress != null) {
                    string validationOutput;

                    if (_publishValidation.IsValidEmail(EmailAddress, out validationOutput)) {
                        BaseSingleton<AuditScopeObserver>.Instance.OnPublish(EmailAddress);
                        //
                        // TODO: uncoment after testing
                        //
                        //EmailAddress = string.Empty;
                    }
                    else {
                        await DisplayAlert("Publish", validationOutput);
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand HomeCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand PublishCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress {
            get => _emailAddress;
            set => SetProperty<string>(ref _emailAddress, value);
        }
    }
}


//PDF GENERATION AND SENDING
/*
/// <summary>
/// TODO: just TEST
/// </summary>
public ICommand GeneratePDFCommand {
    get;
    private set;
}

/// <summary>
/// TODO: just TEST
/// </summary>
public ICommand SendMailCommand {
    get;
    private set;
}

/// <summary>
/// TODO: just TEST
/// </summary>
private void TEST_GEN_PDF() {
    //
    // Take first 3 questions
    //
    List<Question> questions =
        QuestionsList[0]
        .Take(3)
        .Select<QuestionViewModel, Question>(qVM => qVM.Question)
        .ToList<Question>();

    PdfFixedDocument document = new PdfFixedDocument();

    PdfPen pen_12 = new PdfPen(PdfRgbColor.Black, 1.2);
    PdfPen pen_12_blue = new PdfPen(PdfRgbColor.Blue, 1.2);
    PdfPen pen_7 = new PdfPen(PdfRgbColor.Black, .7);
    PdfStandardFont font_h_18 = new PdfStandardFont(PdfStandardFontFace.Helvetica, 18);
    PdfStandardFont font_h_14 = new PdfStandardFont(PdfStandardFontFace.Helvetica, 14);
    PdfBrush brush = new PdfBrush();

    foreach (Question question in questions) {
        PdfPage page = document.Pages.Add();

        int yOffset = 34;

        TEST_DRAW_TEXT_LINE(page, font_h_18, pen_12, brush, 0, yOffset, question.QuestionMessage, false);
        yOffset += 22;

        TEST_DRAW_TEXT_LINE(page, font_h_14, pen_12_blue, brush, 0, yOffset, string.Format("Answer is - {0}", question.Answer.Value.ToString()), false);
        yOffset += 16;

        TEST_DRAW_TEXT_LINE(page, font_h_14, pen_12_blue, brush, 0, yOffset, "Comments:", false);
        yOffset += 16;

        foreach (AttachedComment comment in question.Comments) {
            TEST_DRAW_TEXT_LINE(page, font_h_14, pen_12_blue, brush, 0, yOffset, string.Format("{0} - {1:dd MMM H:mm}", comment.CommentMessage, comment.PublishDate), false);
            yOffset += 16;
        }

        foreach (AttachedImage image in question.Images) {
            byte[] butes = System.Convert.FromBase64String(image.Base64Image);
            MemoryStream imageStream = new MemoryStream(butes);
            imageStream.Flush();
            //imageStream.Dispose();
            PdfPngImage pdfImage = new PdfPngImage(imageStream);

            TEST_DRAW_IMAGE(page, pdfImage, 0, yOffset, 300, 300);
        }
    }

    Stream pdfStream = DependencyService.Get<IFileHelper>().GetFileStreamForPDF();

    document.Save(pdfStream);
    pdfStream.Flush();
    pdfStream.Dispose();



    DependencyService.Get<IFileHelper>().IntentToDisplayPDF();
}

/// <summary>
/// TODO: just TEST.
/// </summary>
private void TEST_SEND_MAIL() {
    DependencyService.Get<IFileHelper>().IntentToSentMailWithPDF();
}

/// <summary>
/// TODO: just TEST.
/// </summary>
private void TEST_DRAW_TEXT_LINE(PdfPage page, PdfStandardFont font, PdfPen pen, PdfBrush brush, int XOffset, int YOffset, string text, bool drawUnderLine) {
    page.Graphics.DrawString(text, font, brush, XOffset, YOffset);
}

/// <summary>
/// TODO: just TEST.
/// </summary>
private void TEST_DRAW_IMAGE(PdfPage page, PdfPngImage image, int xOffset, int yOffset, int width, int height) {
    page.Graphics.DrawImage(image, xOffset, yOffset, width, height);
}
*/
