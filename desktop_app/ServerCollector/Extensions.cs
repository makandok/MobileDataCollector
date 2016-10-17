using MobileCollector;
using MobileCollector.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerCollector
{
    internal static class Extensions
    {        
        internal static KindKey toKindKey(this string kindKey)
        {
            if (string.IsNullOrWhiteSpace(kindKey))
                throw new ArgumentNullException(
                    "string.toKindKey requires a non zero length string");
            return new KindKey() { Value = kindKey };
        }

        internal static KindName toKind(this string kindName)
        {
            if (string.IsNullOrWhiteSpace(kindName))
                throw new ArgumentNullException(
                    "string.toKind requires a non zero length string");
            return new KindName() {Value = kindName };
        }

        internal static List<KindName> toKinds(this List<string> kindNames)
        {
            return (from kind in kindNames select kind.toKind()).ToList();
        }

        public static int toYMDInt(this DateTime dateValue)
        {
            var timeofday = dateValue.ToString("yyyyMMdd");
            return Convert.ToInt32(timeofday);
        }

        internal static List<OutEntity> DecompressFromBase64String(this string compressedString)
        {
            //var compressedString = File.ReadAllText("Assets\\unsyncd.txt");
            var barray = System.Convert.FromBase64String(compressedString);

            var toReturn = new MemoryStream();

            var jsonString = string.Empty;
            using (var outStream = new System.IO.MemoryStream(barray))
            using (var deflateStream = new System.IO.Compression
        .DeflateStream(outStream,
        System.IO.Compression.CompressionMode.Decompress))
            {
                deflateStream.CopyTo(toReturn);
                deflateStream.Close();
                jsonString = System.Text.Encoding.UTF8.GetString(toReturn.ToArray());
            }
            var js = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<MobileCollector.model.OutEntity>>(jsonString);
            return js;
        }

        internal static string CompressToBase64String(this List<OutEntity> unsyncdRecs)
        {
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(unsyncdRecs);
            var bytes = System.Text.Encoding.UTF8.GetBytes(js);

            var b64 = string.Empty;
            using (var input = new System.IO.MemoryStream(bytes))
            {
                using (var outStream = new System.IO.MemoryStream())
                using (var deflateStream = new System.IO.Compression
            .DeflateStream(outStream,
            System.IO.Compression.CompressionMode.Compress))
                {
                    input.CopyTo(deflateStream);
                    deflateStream.Close();
                    b64 = System.Convert.ToBase64String(outStream.ToArray());
                }
            }
            return b64;
        }

        //internal static long toSafeDate(this DateTime dateValue)
        //{            
        //    return (dateValue.Year * 100 + dateValue.Month) * 100 + dateValue.Day;
        //}

        //internal static DateTime fromSafeDate(this int safeDate)
        //{
        //    var asString = safeDate.ToString();
        //    var day = Convert.ToInt16(asString.Substring(5, 2));
        //    var month = Convert.ToInt16(asString.Substring(3, 2));
        //    var year = Convert.ToInt16(asString.Substring(0, 4));
        //    return new DateTime(year, month, day);
        //}

        //internal static T GetDataView<T>(this FieldItem field, Android.App.Activity context) where T : Android.Views.View
        //{
        //    //we convert these into int Ids
        //    var fieldName = 
        //        (field.dataType == Constants.DATEPICKER || field.dataType == Constants.TIMEPICKER)
        //        ? 
        //        Constants.DATE_TEXT_PREFIX + field.name :
        //        field.name;

        //    int resourceId = context.Resources.GetIdentifier(
        //        fieldName, "id", context.PackageName);
        //    T view = null;
        //    view = context.FindViewById<T>(resourceId);
        //    return view;
        //}

        internal static string toText(this System.IO.Stream stream)
        {
            //var buffer = new byte[length];
            //prepexFieldsStream.Read(buffer, 0, Convert.ToInt32(length));
            //var asString = Convert.ToString(buffer);

            var mstream = new System.IO.MemoryStream();
            stream.CopyTo(mstream);
            var bytes = mstream.ToArray();
            return System.Text.Encoding.Default.GetString(bytes);
        }

        internal static byte[] toByteArray(this System.IO.Stream stream)
        {
            var mstream = new System.IO.MemoryStream();
            stream.CopyTo(mstream);
            var bytes = mstream.ToArray();
            return bytes;
        }

        //this System.Json.JsonValue
        internal static string decryptAndGetApiSetting(this Dictionary<string,string> jsonObject, string settingString)
        {
            var value = jsonObject[settingString];
            if (Constants.ENCRYPTED_ASSETS.Contains(settingString))
            {
                //we decrypt
                return JhpSecurity.Decrypt(value);
            }
            return value;
        }
    }
}