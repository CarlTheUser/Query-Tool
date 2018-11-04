using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using QueryTool.Data.Model;

namespace QueryTool.Data.Provider.XML
{
    internal class XMLSqlConfigurationModelPersistence : SqlConfigurationModelPersistence
    {
        #region Constants

        private static readonly string XML_FILENAME = "SqlConfigurations.xml";

        private static readonly string MODEL_ELEMENT_NAME = "Configuration";

        private static readonly string MODEL_ELEMENT_COLLECTION_NAME = "Configurations";

        private static readonly string XML_DOCUMENT_LOCATION = $@"{Configuration.Instance.AppDataFolderPath}\{XML_FILENAME}";

        #endregion

        public XMLSqlConfigurationModelPersistence()
        {
            XDocument document;

            if (!File.Exists(XML_DOCUMENT_LOCATION))
            {
                document = new XDocument(new XElement(MODEL_ELEMENT_COLLECTION_NAME));
                document.Save(XML_DOCUMENT_LOCATION);
            }
        }

        private int GetNextIdentity()
        {
            int returnValue = 0;

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            try
            {
                returnValue = (from query in document
                               .Element(MODEL_ELEMENT_COLLECTION_NAME)
                               .Descendants(MODEL_ELEMENT_NAME)
                               select query).Max(x => (int)x.Attribute("Id")) + 1;
            }
            catch
            {
                returnValue = 1;
            }

            return returnValue;
        }
        
        public override int Save(SqlConfiguration model)
        {
            if (model == null) throw new ArgumentNullException("model");

            int id = 0;

            try
            {

                XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

                XElement sqlConfigurations = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

                id = GetNextIdentity();

                XElement element = new XElement
                (
                    MODEL_ELEMENT_NAME,
                    new XAttribute("Id", model.Id = id),
                    new XAttribute("Name", model.Name),
                    new XAttribute("ProviderType", model.ProviderType),
                    new XElement("Connection", model.ConnectionString)
                );

                sqlConfigurations.AddFirst(element);

                document.Save(XML_DOCUMENT_LOCATION);
            }
            catch
            {
                id = 0;

                throw;
            }

            return id;
        }

        public override void Edit(int id, SqlConfiguration updatedModel)
        {
            if (updatedModel == null) throw new ArgumentNullException("updatedModel");

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement sqlConfigurations = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            XElement query = (from configuration in sqlConfigurations.Elements()
                              where configuration.Attribute("Id").Value == id.ToString()
                              select configuration).FirstOrDefault();

            query.SetElementValue("Name", updatedModel.Name);
            query.SetAttributeValue("ProviderType", updatedModel.ProviderType);

            query.Element("Connection").SetValue(updatedModel.ConnectionString);

            document.Save(XML_DOCUMENT_LOCATION);
        }

        public override void Delete(int id)
        {
            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            document.Element(MODEL_ELEMENT_COLLECTION_NAME)
                .Elements(MODEL_ELEMENT_NAME)
                .Where(x => x.Attribute("Id").Value == id.ToString())
                .Remove();

            document.Save(XML_DOCUMENT_LOCATION);
        }

        public override SqlConfiguration GetById(int id)
        {
            SqlConfiguration returnValue = null;

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement configurations = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            XElement configuration = (from config in configurations.Elements()
                              where config.Attribute("Id").Value == id.ToString()
                              select config).FirstOrDefault();

            if (configuration != null)
            {
                returnValue = new SqlConfiguration();

                returnValue.Id = int.Parse(configuration.Attribute("Id").Value);
                returnValue.Name = configuration.Attribute("Name").Value;
                returnValue.ProviderType = (string)configuration.Attribute("ProviderType");
                returnValue.ConnectionString = configuration.Element("Connection").Value;

            }

            return returnValue;
        }

        public override IEnumerable<SqlConfiguration> GetAll()
        {
            List<SqlConfiguration> configurationsList = new List<SqlConfiguration>();

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement sqlConfigurations = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            configurationsList = (from config in sqlConfigurations.Elements(MODEL_ELEMENT_NAME)

                           let id = (int)config.Attribute("Id")
                           let name = config.Attribute("Name").Value
                           let provider = (string)config.Attribute("ProviderType")
                           let connectionString = config.Element("Connection").Value

                           select new SqlConfiguration
                           {
                               Id = id,
                               Name = name,
                               ConnectionString = connectionString,
                               ProviderType = provider
                           }).ToList();

            return configurationsList;
        }
        
    }
}
