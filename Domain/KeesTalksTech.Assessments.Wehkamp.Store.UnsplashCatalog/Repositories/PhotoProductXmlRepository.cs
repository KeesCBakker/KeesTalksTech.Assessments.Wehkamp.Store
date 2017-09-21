using KeesTalksTech.Assessments.Wehkamp.Store.Utilities.Serialization;
using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories
{
    public class PhotoProductXmlRepository : PhotoProductMemoryRepository
    {
        public PhotoProductXmlRepository(string xmlDataFilePath)
        {
            if (String.IsNullOrEmpty(xmlDataFilePath))
            {
                throw new ArgumentNullException(nameof(xmlDataFilePath));
            }

            var data = XmlSerializationHelper.DeserializeFromFile<PhotoProduct[]>(xmlDataFilePath);
            foreach (var products in data)
            {
                Add(products);
            }
        }
    }
}
