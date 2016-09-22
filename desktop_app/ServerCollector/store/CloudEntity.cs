using JhpDataSystem;
using System;

namespace SyncManager.store
{
    public class BlobEntity
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string KindMetaData { get; set; }
        public string DataBlob { get; set; }
        //public DateTime DateAdded { get; set; }
        public string FormName { get; set; }
        public long EditDate { get; internal set; }
        public int EditDay { get; internal set; }
    }

    public class CloudEntity: BlobEntity
    {
    }

    public class LocalEntity: BlobEntity
    {
    }

    public class EntityConverter
    {
        public LocalEntity toLocalEntity(CloudEntity entity)
        {
            var decrypted = entity.DataBlob.Decrypt();


            return new LocalEntity()
            {
                Id = entity.Id,
                EntityId = entity.EntityId,
                DataBlob = decrypted.Value,
                EditDate = entity.EditDate,
                EditDay = entity.EditDay,
                FormName = entity.FormName,
                KindMetaData = entity.KindMetaData
            };
        }

        public CloudEntity toCloudEntity(LocalEntity entity)
        {
            var encrypted = entity.DataBlob.Encrypt();
            return new CloudEntity()
            {
                Id = entity.Id,
                EntityId = entity.EntityId,
                DataBlob = encrypted.Value,
                EditDate = entity.EditDate,
                EditDay = entity.EditDay,
                FormName = entity.FormName,
                KindMetaData = entity.KindMetaData
            };
        }
        //string encrypt(string textToEncrypt)
        //{
        //    return textToEncrypt;
        //}
        
        //string decrypt(string textToDecrypt)
        //{
        //    return textToDecrypt;
        //}
    }
}
