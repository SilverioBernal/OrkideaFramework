using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.Azure
{
    public class Storage
    {
        public string connString { get; set; }
        public string destinyContainer { get; set; }

        public Storage()
        {

        }

        public Storage(string ConnString, string DestinyContainer)
        {
            connString = ConnString;
            destinyContainer = DestinyContainer;
        }

        public bool uploadFile(Stream fileStream, string fileName)
        {
            bool res = false;

            try
            {
                if (string.IsNullOrEmpty(connString))
                    throw new Exception("The attribute 'connString' is mandatory");

                if (string.IsNullOrEmpty(destinyContainer))
                    throw new Exception("The attribute 'destinyContainer' is mandatory");

                //Get a reference to the storage account
                CloudStorageAccount sa = CloudStorageAccount.Parse(connString);
                CloudBlobClient bc = sa.CreateCloudBlobClient();

                //Get a reference to the container
                CloudBlobContainer container = bc.GetContainerReference(destinyContainer);

                CloudBlockBlob cbb = container.GetBlockBlobReference(fileName);

                cbb.UploadFromStream(fileStream);

                res = true;
            }
            catch (Exception)
            {
                res = false;
            }

            return res;
        }

        public MemoryStream getFile(string fileName)
        {
            if (string.IsNullOrEmpty(connString))
                throw new Exception("The attribute 'connString' is mandatory");

            if (string.IsNullOrEmpty(destinyContainer))
                throw new Exception("The attribute 'destinyContainer' is mandatory");

            MemoryStream res = new MemoryStream();

            try
            {
                //Get a reference to the storage account
                CloudStorageAccount sa = CloudStorageAccount.Parse(connString);
                CloudBlobClient bc = sa.CreateCloudBlobClient();

                //Get a reference to the container
                CloudBlobContainer container = bc.GetContainerReference(destinyContainer);

                CloudBlockBlob cbb = container.GetBlockBlobReference(fileName);

                cbb.DownloadToStream(res);
            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }

        public bool deleteFile(string fileName)
        {
            bool res = false;

            try
            {
                if (string.IsNullOrEmpty(connString))
                    throw new Exception("The attribute 'connString' is mandatory");

                if (string.IsNullOrEmpty(destinyContainer))
                    throw new Exception("The attribute 'destinyContainer' is mandatory");

                //Get a reference to the storage account
                CloudStorageAccount sa = CloudStorageAccount.Parse(connString);
                CloudBlobClient bc = sa.CreateCloudBlobClient();

                //Get a reference to the container
                CloudBlobContainer container = bc.GetContainerReference(destinyContainer);

                CloudBlockBlob cbb = container.GetBlockBlobReference(fileName);

                cbb.DeleteIfExists();

                res = true;
            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }
    }
}
