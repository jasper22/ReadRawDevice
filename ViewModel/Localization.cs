
namespace ReadRawDevice.Gui.ViewModel
{
    using System.Windows;

    /// <summary>
    /// Object will provide runtime localization
    /// Redirect to correct ResourceDictionary file
    /// </summary>
    internal class Localization
    {
        private static ResourceDictionary localizedResource = null;

        /// <summary>
        /// Gets the localized resource.
        /// </summary>
        /// <value>
        /// The localized resource.
        /// </value>
        /// <remarks>Currently only en-US is supported</remarks>
        internal static ResourceDictionary LocalizedResource
        {
            get
            {
                if (localizedResource == null)
                {
                    localizedResource = new ResourceDictionary();

                    switch (System.Globalization.CultureInfo.CurrentUICulture.Name)
                    {
                        default:
                            localizedResource.Source = new System.Uri("/ReadRawDevice.Gui;component/Assests/Localization-en_US.xaml", System.UriKind.RelativeOrAbsolute);
                            break;
                    }
                }

                return localizedResource;
            }
        }
    }
}
