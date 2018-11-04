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
    internal class XMLQueryModelPersistence : QueryModelPersistence
    {

        #region Constants

        private static readonly string XML_FILENAME = "SavedQueries.xml";

        private static readonly string MODEL_ELEMENT_NAME = "Query";

        private static readonly string MODEL_ELEMENT_COLLECTION_NAME = "Queries";

        private static readonly string XML_DOCUMENT_LOCATION = $@"{Configuration.Instance.AppDataFolderPath}\{XML_FILENAME}";

        #endregion

        public XMLQueryModelPersistence()
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

        public override int Save(Query model)
        {
            if (model == null) throw new ArgumentNullException("model");

            int id = 0;

            try
            {

                XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

                XElement queries = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

                id = GetNextIdentity();

                XElement element = new XElement
                (
                    MODEL_ELEMENT_NAME,
                    new XAttribute("Id", model.Id = id),
                    new XAttribute("Name", model.Name),
                    new XAttribute("Provider", model.Provider),
                    new XElement("Value", model.Value)
                );

                queries.AddFirst(element);
                document.Save(XML_DOCUMENT_LOCATION);
            }
            catch
            {
                id = 0;

                throw;
            }

            return id;
        }

        public override void Edit(int id, Query updatedModel)
        {
            if (updatedModel == null) throw new ArgumentNullException("updatedModel");

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement queries = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            XElement query = (from image in queries.Elements()
                                  where image.Attribute("Id").Value == id.ToString()
                                  select image).FirstOrDefault();

            query.SetElementValue("Name", updatedModel.Name);
            query.SetAttributeValue("Provider", updatedModel.Provider);

            query.Element("Value").SetValue(updatedModel.Value);

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

        public override Query GetById(int id)
        {
            Query returnValue = null;

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement queries = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            XElement query = (from q in queries.Elements()
                                  where q.Attribute("Id").Value == id.ToString()
                                  select q).FirstOrDefault();

            if (query != null)
            {
                returnValue = new Query();

                returnValue.Id = int.Parse(query.Attribute("Id").Value);
                returnValue.Name = query.Element("Name").Value;
                returnValue.Provider = (string)query.Attribute("Provider");
                returnValue.Value = query.Element("Value").Value;
                
            }

            return returnValue;
        }

        public override IEnumerable<Query> GetAll()
        {
            List<Query> queriesList = new List<Query>();

            XDocument document = XDocument.Load(XML_DOCUMENT_LOCATION);

            XElement queries = document.Element(MODEL_ELEMENT_COLLECTION_NAME);

            queriesList = (from query in queries.Elements(MODEL_ELEMENT_NAME)

                           let id = (int)query.Attribute("Id")
                           let name = query.Element("Name").Value
                           let provider = (string)query.Attribute("Provider")
                           let value = query.Element("Value").Value

                           select new Query
                           {
                               Id = id,
                               Name = name,
                               Value = value,
                               Provider = provider
                           }).ToList();

            return queriesList;
        }

        
    }
}
