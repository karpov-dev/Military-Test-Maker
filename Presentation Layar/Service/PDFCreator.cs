using Data_Layer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Presentation_Layar.Service
{
    static class PDFCreator
    {
        #region Variables
        private static float yPosition = 0;
        #endregion

        #region Constants
        private const int MAX_TITLE_FONT_SIZE = 18;
        private const int MAX_BIG_FONT_SIZE = 16;
        private const int MAX_MAIN_FONT_SIZE = 14;
        private const int MAX_SMALL_FONT_SIZE = 12;
        private const int MIN_FONT_SIZE = 6;

        private const float MAX_LINE_SPACING = 2f;
        private const float MIN_LINE_SPACING = 2f;

        private const float MAX_WORD_SPACING = 3f;
        private const float MIN_WORD_SPACING = 1f;

        private const int MAX_QUESTIONS_OFFSET = 60;
        private const int MIN_QUESTIONS_OFFSET = 0;

        private const int SYMBOLS_IN_LINE = 90;
        private const int PAGE_WIDTH = 450;
        #endregion

        #region Properties
        private static int _titleFontSize = 15;
        public static int TitleFontSize
        {
            get => _titleFontSize;
            set
            {
                if(value < MAX_TITLE_FONT_SIZE && value >= MAX_BIG_FONT_SIZE)
                {
                    _titleFontSize = value;
                }
            }
        }

        private static int _bigFontSize = 12;
        public static int BigFontSize
        {
            get => _bigFontSize;
            set
            {
                if(value < MAX_BIG_FONT_SIZE && value >= MAX_MAIN_FONT_SIZE)
                {
                    _bigFontSize = value;
                }
            }
        }

        private static int _mainFontSize = 10;
        public static int MainFontSize
        {
            get => _mainFontSize;
            set
            {
                if(value < MAX_MAIN_FONT_SIZE && value >= MAX_SMALL_FONT_SIZE)
                {
                    _mainFontSize = value;
                }
            }
        }

        private static int _smallFontSize = 8;
        public static int SmallFontSize
        {
            get => _smallFontSize;
            set
            {
                if(value < MAX_SMALL_FONT_SIZE && value >= MIN_FONT_SIZE)
                {
                    _smallFontSize = value;
                }
            }
        }

        private static float _lineSpacing = 1.1f;
        public static float LineSpacing
        {
            get => _lineSpacing;
            set
            {
                if(value < MAX_LINE_SPACING && value >= MIN_LINE_SPACING)
                {
                    _lineSpacing = value;
                }
            }
        }

        private static float _wordSpacing = 1.5f;
        public static float WordSpacing
        {
            get => _wordSpacing;
            set
            {
                if(value < MAX_WORD_SPACING && value >= MIN_WORD_SPACING )
                {

                }
            }
        }

        private static int _questionsOffset = 10;
        public static int QuestionOffset
        {
            get => _questionsOffset;
            set
            {
                if(value < MAX_QUESTIONS_OFFSET && value > MIN_QUESTIONS_OFFSET )
                {
                    _questionsOffset = value;
                }
            }
        }
        #endregion

        #region Methods
        public static bool CreatePDF(string savePath, Test test)
        {
            if ( test == null || string.IsNullOrWhiteSpace(savePath) ) return false;

            #region Documet Page Graphics
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            yPosition = 0;
            #endregion

            #region Fonts
            PdfFont TitleFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", TitleFontSize), true);
            PdfFont BigFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", BigFontSize), true);
            PdfFont MainFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", MainFontSize), true);
            PdfFont SmallFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", SmallFontSize), true);
            #endregion

            WriteString(page, "Тема:" + " \"" + test.Title + "\" ", TitleFont, PdfBrushes.Black);
            WriteString(page, "Дисциплина:" + " \"" + test.Description + "\" ", SmallFont, PdfBrushes.Black);
            WriteString(page, "Составил:" + " \"" + test.Author + "\" ", SmallFont, PdfBrushes.Black);
            DrawLine(page, new PdfPen(Color.Black, 2));
            
            for(int i = 0; i < test.Questions.Count; i++ )
            {
                if ( yPosition > page.Size.Height )
                {
                    page = document.Pages.Add();
                    yPosition = 0;
                }
                WriteString(page, "Задание " + ( i + 1 ).ToString() + ". ", MainFont, PdfBrushes.Black);
                WriteString(page, test.Questions[i].Queston, MainFont, PdfBrushes.Black);
                for ( int j = 0; j < test.Questions[i].Answers.Count; j++ )
                {
                    WriteString(page, j + ") " + test.Questions[i].Answers[j], MainFont, PdfBrushes.Black);
                }
                WriteString(page, "Ответ: ____", MainFont, PdfBrushes.Black);
                WriteString(page, " ", MainFont, PdfBrushes.Black);
            }

            try
            {
                document.Save(savePath + "\\Test.pdf");
                document.Close(true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static float GetNeedHeight(PdfFont font, int stringLength)
        {
            int needLines = 1;
            while(stringLength > SYMBOLS_IN_LINE )
            {
                needLines++;
                stringLength -= SYMBOLS_IN_LINE;
            }
            return needLines * font.Height;
        }
        private static void WriteString(PdfPage page, string str, PdfFont font, PdfBrush brush, int offset = 0)
        {

            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Middle;
            format.CharacterSpacing = 1;
            format.LineSpacing = LineSpacing;
            format.ParagraphIndent = 2.1f;
            format.WordSpacing = WordSpacing;
            format.WordWrap = PdfWordWrapType.Character;

            PdfGraphics graphics = page.Graphics;
            float needArea = GetNeedHeight(font, str.Length);
            graphics.DrawString(str, font, brush, new RectangleF(offset, yPosition, PAGE_WIDTH, needArea));
            yPosition += needArea;
        }
        private static void DrawLine(PdfPage page, PdfPen pen)
        {
            PdfGraphics graphics = page.Graphics;
            yPosition += LineSpacing;
            graphics.DrawLine(pen, new PointF(0, yPosition), new PointF(page.Size.Width, yPosition));
            yPosition += LineSpacing;
        }
        #endregion
    }
}
