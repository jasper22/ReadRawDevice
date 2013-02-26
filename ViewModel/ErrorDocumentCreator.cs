
namespace ReadRawDevice.Gui.ViewModel
{
    using System;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// Represent an object that could convert <see cref="Exception"/> to/from <see cref="FlowDocument"/>
    /// </summary>
    /// <remarks>FlowDocument MSDN: http://msdn.microsoft.com/en-us/library/aa970909.aspx </remarks>
    internal class ErrorDocumentCreator
    {
        /// <summary>
        /// Creates the specified exception to convert.
        /// </summary>
        /// <param name="exceptionToConvert">The exception to convert.</param>
        /// <returns><see cref="FlowDocument"/> that describe the exception</returns>
        internal static FlowDocument Create(Exception exceptionToConvert)
        {
            Paragraph paraTitle = new Paragraph(new Bold(new Run("Error occurred")));
            paraTitle.FontSize = 16;
            paraTitle.Foreground = System.Windows.SystemColors.HighlightBrush;

            Paragraph paraLineBreak = new Paragraph(new LineBreak());

            Section errorInfoSection = new Section();

            Paragraph paraErrorInfo = new Paragraph(new Run(exceptionToConvert.ToString()));
            paraErrorInfo.TextIndent = 20;

            errorInfoSection.Blocks.Add(paraErrorInfo);

            if (exceptionToConvert.InnerException != null)
            {
                Paragraph paraInnerException = new Paragraph(new Run(exceptionToConvert.InnerException.ToString()));
                paraInnerException.TextIndent = 40;
                errorInfoSection.Blocks.Add(paraInnerException);
            }


            FlowDocument mainDocument = new FlowDocument();
            mainDocument.Blocks.Add(paraTitle);
            mainDocument.Blocks.Add(paraLineBreak);
            mainDocument.Blocks.Add(errorInfoSection);

            return mainDocument;
        }
    }
}
