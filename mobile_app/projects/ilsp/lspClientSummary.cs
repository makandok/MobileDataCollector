using System;
using System.Collections.Generic;
using System.Linq;
using MobileCollector.model;
using System.Globalization;

namespace MobileCollector.projects.ilsp
{
    [SQLite.Table(Constants.KIND_DERIVED_LSP_CLIENTSUMMARY)]
    public class lspClientSummary : ILocalDbEntity
    {
        public static string KindName { get { return Constants.KIND_DERIVED_LSP_CLIENTSUMMARY; } }
        private long _itemid = -1L;
        [SQLite.Ignore]
        public long itemId { get { return _itemid; } set { _itemid = value; } }
        public long getItemId()
        {
            if (_itemid == -1L && Id != null)
            {
                _itemid = Id.Value.GetHashCode();
            }
            return _itemid;
        }

        public int FormSerial { get; set; }

        [SQLite.PrimaryKey]
        public string KindKey { get; set; }

        [SQLite.Ignore]
        public KindKey Id { get; set; }
        [SQLite.Ignore]
        public KindKey EntityId { get; set; }
        [SQLite.Ignore]
        public string KindMetaData { get; set; }

        public DateTime CoreActivityDate { get; set; }
        public ISaveableEntity build()
        {
            Id = new KindKey(KindKey); EntityId = new KindKey(KindKey);
            return this;
        }
        
        public string BeneficiaryName { get; set; }
        public DateTime InterviewDate { get; set; }
        public string BeneficiaryAge { get; set; }
        public string NameofGroup { get; set; }
        public string NameOfVillage { get; set; }
        public string BeneficiarySex { get; set; }

        public List<NameValuePair> ToValuesList()
        {
            var toReturn = new List<NameValuePair>()
            {
                new NameValuePair() {Name=Constants.FIELD_ENTITYID,Value = EntityId.Value },
                new NameValuePair() {Name=Constants.FIELD_ID,Value = Id.Value },

                new NameValuePair() {Name=Constants.FIELD_LSP_DATEOFVISIT,Value = CoreActivityDate.ToString(CultureInfo.InvariantCulture) },
                new NameValuePair() {Name=Constants.FIELD_LSP_BENEFICIARY,
                    Value = BeneficiaryName },
                new NameValuePair() {Name=Constants.FIELD_LSP_BENEFICIARYAGE,
                    Value = BeneficiaryAge },
                new NameValuePair() {Name=Constants.FIELD_LSP_GROUPNAME,
                    Value = NameofGroup },
               new NameValuePair() {Name=Constants.FIELD_LSP_VILLAGE,
                    Value = NameOfVillage },
               new NameValuePair() {Name=Constants.FIELD_LSP_BENEFICIARY_SEX,
                    Value = BeneficiarySex },
            };
            return toReturn;
        }

        public ILocalDbEntity Load(GeneralEntityDataset lookupEntry)
        {
            var expectedFields = Constants.LSP_IndexedFieldNames;
            var fieldValues = lookupEntry.FieldValues;
            var allFields = (from field in lookupEntry.FieldValues
                             where expectedFields.Contains(field.Name)
                             select field).ToDictionary(x => x.Name, y => y.Value);
            foreach (var field in expectedFields)
            {
                if (!allFields.ContainsKey(field))
                {
                    allFields[field] = "";
                }
            }

            this.KindKey = lookupEntry.Id.Value;
            this.EntityId = new KindKey(KindKey);
            this.Id = new KindKey(KindKey);

            this.KindMetaData = lookupEntry.KindMetaData;

            var dateStr = allFields[Constants.FIELD_LSP_DATEOFVISIT];
            DateTime coreActivityDate;
            if (DateTime.TryParse(dateStr, out coreActivityDate))
            {
                this.CoreActivityDate = coreActivityDate;
            }
            this.InterviewDate = coreActivityDate;

            this.BeneficiarySex = allFields[Constants.FIELD_LSP_BENEFICIARY_SEX];
            this.BeneficiaryName = allFields[Constants.FIELD_LSP_BENEFICIARY];
            this.BeneficiaryAge = allFields[Constants.FIELD_LSP_BENEFICIARYAGE];
            this.NameofGroup = allFields[Constants.FIELD_LSP_GROUPNAME];
            this.NameOfVillage = allFields[Constants.FIELD_LSP_VILLAGE];            
            return this;
        }
    }
}