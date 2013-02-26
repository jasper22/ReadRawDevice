
namespace ReadRawDevice.Gui.View
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    /// <summary>
    /// Represent a standard <see cref="RichTextBox"/>  the 'Document' property could be binded
    /// </summary>
    /// <remarks>Code from Codeproject: http://www.codeproject.com/Articles/137209/Binding-and-styling-text-to-a-RichTextBox-in-WPF</remarks>
    public class BindableRichTextBox : RichTextBox
    {
        /// <summary>
        /// DP property for 'Document' binding
        /// </summary>
        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", 
                                                                    typeof(FlowDocument),
                                                                    typeof(BindableRichTextBox), 
                                                                    new FrameworkPropertyMetadata (null, new PropertyChangedCallback(OnDocumentChanged)));

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Documents.FlowDocument" /> that represents the contents of the <see cref="T:System.Windows.Controls.RichTextBox" />.
        /// </summary>
        /// <returns>A <see cref="T:System.Windows.Documents.FlowDocument" /> object that represents the contents of the <see cref="T:System.Windows.Controls.RichTextBox" />.By default, this property is set to an empty <see cref="T:System.Windows.Documents.FlowDocument" />.  Specifically, the empty <see cref="T:System.Windows.Documents.FlowDocument" /> contains a single <see cref="T:System.Windows.Documents.Paragraph" />, which contains a single <see cref="T:System.Windows.Documents.Run" /> which contains no text.</returns>
        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        /// <summary>
        /// Called when [document changed].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void OnDocumentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            RichTextBox rtb = (RichTextBox)obj;
            rtb.Document = (FlowDocument)args.NewValue;
        }
    }
}
