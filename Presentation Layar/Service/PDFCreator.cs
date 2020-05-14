using Data_Layer;
using Presentation_Layar.Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Presentation_Layar.Service
{
    static class PDFCreator
    {
        #region Variables
        private static float yPosition = 0;
        private static PdfPage page;
        private static PdfDocument document;
        private static PdfGraphics graphics;
        #endregion

        #region Constants
        private const int MAX_TITLE_FONT_SIZE = 18;
        private const int MAX_BIG_FONT_SIZE = 16;
        private const int MAX_MAIN_FONT_SIZE = 14;
        private const int MAX_SMALL_FONT_SIZE = 12;
        private const int MIN_FONT_SIZE = 6;

        private const float MAX_LINE_SPACING = 3f;
        private const float MIN_LINE_SPACING = 1f;

        private const float MAX_WORD_SPACING = 4f;
        private const float MIN_WORD_SPACING = 1f;

        private const int MAX_QUESTIONS_OFFSET = 60;
        private const int MIN_QUESTIONS_OFFSET = 0;

        private const int SYMBOLS_IN_LINE = 90;
        private const int PAGE_WIDTH = 450;

        private const int PDF_IMAGE_HEIGHT = 100;
        private const int PDF_IMAGE_MARGIN = 5;
        private const int QUESTION_NUMBER_MARGIN = 14;
        #endregion

        #region Properties
        private static int _titleFontSize = 14;
        public static int TitleFontSize
        {
            get => _titleFontSize;
            set
            {
                if(value < MAX_TITLE_FONT_SIZE && value >= MIN_FONT_SIZE)
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
                if(value < MAX_BIG_FONT_SIZE && value >= MIN_FONT_SIZE)
                {
                    _bigFontSize = value;
                }
            }
        }

        private static int _mainFontSize = 9;
        public static int MainFontSize
        {
            get => _mainFontSize;
            set
            {
                if(value < MAX_MAIN_FONT_SIZE && value >= MIN_FONT_SIZE)
                {
                    _mainFontSize = value;
                }
            }
        }

        private static int _smallFontSize = 6;
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

        private static string _filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string FilePath
        {
            get => _filePath;
            set
            {
                if( !string.IsNullOrWhiteSpace(value) )
                {
                    _filePath = value;
                }
            }
        }

        private static string _fileName = "TestForStudents";
        public static string FileName
        {
            get => _fileName;
            set
            {
                if ( !string.IsNullOrWhiteSpace(value) )
                {
                    _fileName = value;
                }
            }
        }
        #endregion

        #region Methods
        public static bool CreatePDF(Test test)
        {
            if ( test == null) return false;

            #region Documet Page Graphics
            document = new PdfDocument();
            page = document.Pages.Add();
            graphics = page.Graphics;
            yPosition = 0;
            #endregion

            #region Fonts
            PdfFont TitleFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", TitleFontSize), true);
            PdfFont BigFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", BigFontSize), true);
            PdfFont MainFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", MainFontSize), true);
            PdfFont SmallFont = new PdfTrueTypeFont(new Font("Arial Unicode MS", SmallFontSize), true);
            #endregion

            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Left;
            format.LineAlignment = PdfVerticalAlignment.Middle;
            format.CharacterSpacing = 1;
            format.LineSpacing = LineSpacing;
            format.ParagraphIndent = 2.1f;
            format.WordSpacing = WordSpacing;
            format.WordWrap = PdfWordWrapType.Character;

            WriteString("Тема:" + " \"" + test.Title + "\" ", TitleFont, PdfBrushes.Black);
            WriteString("Дисциплина:" + " \"" + test.Description + "\" ", SmallFont, PdfBrushes.Black);
            WriteString("Составил:" + " \"" + test.Author + "\" ", SmallFont, PdfBrushes.Black);
            DrawLine(new PdfPen(Color.Black, 2));

            for ( int i = 0; i < test.Questions.Count; i++ )
            {
                CheckPageEnd();
                WriteString("Задание " + ( i + 1 ).ToString() + ". ", MainFont, PdfBrushes.Black);
                WriteString(test.Questions[i].Queston, MainFont, PdfBrushes.Black);
                List<string> answers = new List<string>();
                DrawObjectsInLine(test.Questions[i].Answers, 2, MainFont, format);
                WriteString("Ответ: ____", MainFont, PdfBrushes.Black);
                WriteString(" ", MainFont, PdfBrushes.Black);
            }

            try
            {
                document.Save(FilePath + "\\" + FileName + ".pdf");
                document.Close(true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static float GetNeedHeight(PdfFont font, int stringLength, int symbolsInLine)
        {
            int needLines = 1;
            while(stringLength > symbolsInLine)
            {
                needLines++;
                stringLength -= symbolsInLine;
            }
            return needLines * font.Height;
        }
        private static void WriteString(string str, PdfFont font, PdfBrush brush, int offset = 0)
        {
            float needArea = GetNeedHeight(font, str.Length, SYMBOLS_IN_LINE);
            CheckPageEnd(needArea);
            graphics.DrawString(str, font, brush, new RectangleF(offset, yPosition, PAGE_WIDTH, needArea));
            yPosition += needArea;
        }
        private static void DrawLine(PdfPen pen)
        {
            yPosition += LineSpacing;
            graphics.DrawLine(pen, new PointF(0, yPosition), new PointF(page.Size.Width, yPosition));
            yPosition += LineSpacing;
            CheckPageEnd();
        }
        private static string GetLongestString(List<string> strings)
        {
            string longestString = strings[0];
            foreach(string str in strings )
            {
                if(str.Length > longestString.Length )
                {
                    longestString = str;
                }
            }
            return longestString;
        }


        private static void DrawObjectsInLine(List<string> strings, int elementsInLine, PdfFont font, PdfStringFormat format)
        {
            List<List<string>> elementsInGrid = new List<List<string>>();
            List<string> oneLine = new List<string>();
            //разбиение на строки элементов
            for (int i = 0; i < strings.Count; i++ )
            {
                oneLine.Add(strings[i]);
                if(oneLine.Count == elementsInLine )
                {
                    elementsInGrid.Add(oneLine);
                    oneLine = new List<string>();
                }
            }
            int answerNumber = 1;
            //проход по всем элементам будущего Grid
            for (int i = 0; i < elementsInGrid.Count; i++ )
            {
                List<float> heights = new List<float>();

                List<string> elements = new List<string>(elementsInGrid[i]);
                for ( int k = 0; k < elements.Count; k++ )
                {
                    heights.Add(GetNeedHeight(font, elements[k], SYMBOLS_IN_LINE / elementsInLine));
                }
                float needHeight = GetMoreNumber(heights); //получаем высоту, которая необходима строке

                //при необходимости переходим на другую страницу
                CheckPageEnd(needHeight);

                float availableWidth = PAGE_WIDTH / elements.Count - 10; //досутпная ширина на один элемент
                int xPosition = 0;
                for ( int k = 0; k < elements.Count; k++ )
                {
                    
                    if ( FileWorker.IsImage(elements[k]) )
                    {
                        try
                        {
                            graphics.DrawString(answerNumber.ToString() + ") ", font, PdfBrushes.Black, new RectangleF(xPosition, yPosition, 13, QUESTION_NUMBER_MARGIN + LineSpacing), format);
                            xPosition += QUESTION_NUMBER_MARGIN;
                            graphics.DrawImage(new PdfBitmap(elements[k]), new RectangleF(xPosition, yPosition, availableWidth, needHeight));
                            xPosition += PDF_IMAGE_MARGIN;
                            yPosition += PDF_IMAGE_MARGIN;
                        }
                        catch( System.Exception e )
                        {
                            
                        }
                    }
                    else
                    {
                        graphics.DrawString(answerNumber.ToString() + ") " + elements[k], font, PdfBrushes.Black, new RectangleF(xPosition, yPosition, availableWidth, needHeight), format);
                    }
                    xPosition += (int) availableWidth + 5;
                    answerNumber++;
                }
                yPosition += needHeight + 5;
            }
        }
        private static float GetNeedHeight(PdfFont font, string str, int symbolsInLine)
        {
            if ( FileWorker.IsImage(str) ) return PDF_IMAGE_HEIGHT + PDF_IMAGE_MARGIN;
            int needLines = 1,
                strLength = str.Length;
            while(strLength > symbolsInLine )
            {
                needLines++;
                strLength -= symbolsInLine;
            }
            return needLines * font.Height + LineSpacing;
        }
        private static float GetMoreNumber(List<float> numbers)
        {
            float moreNumber = numbers[0];
            for(int i = 0; i < numbers.Count; i++ )
            {
                if(moreNumber < numbers[i] )
                {
                    moreNumber = numbers[i];
                }
            }
            return moreNumber;
        }
        private static bool CheckPageEnd(float needArea = 0)
        {
            if ( yPosition + needArea + 20 >= page.Size.Height )
            {
                page = document.Pages.Add();
                graphics = page.Graphics;
                yPosition = 0;
                return true;
            }
            return false;
        }
        #endregion
    }
}
