using MobileCollector.model;
using ServerCollector;
using ServerCollector.store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerCollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var value = new Google.Apis.Datastore.v1.Data.Value() {
            //    TimestampValue=DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)
            //};
            //var ser = Newtonsoft.Json.JsonConvert.SerializeObject(value);

            AppInstance.Instance.InitialiseAppResources(null,null);
            //we have loaded the app
            this.menuServerSync.Click += MenuServerSync_Click;
            //this.menuServerSyncOldData.Click += MenuServerSync_Click;
            this.menuConfigure.Click += MenuConfigure_Click;
            this.menuAllData.Click += MenuAllData_Click;
            this.menuSmmaries.Click += MenuSmmaries_Click;
            this.menuRefreshLocal.Click += menuRefreshLocal_Click;
        }

        void setProgressValue(int value)
        {
            pbarProgress.Value = value;
        }

        private void menuRefreshLocal_Click(object sender, RoutedEventArgs e)
        {
            AppInstance.Instance.CloudDbInstance.RefreshLocalEntities(setProgressValue);
        }

        private void MenuSmmaries_Click(object sender, RoutedEventArgs e)
        {
            var addRecordsFromTablet = false;
            if (addRecordsFromTablet)
            {
                //we have records that couldn't be synced from the tablet
                var toSave = new List<CloudEntity>();
                var txt = File.ReadAllText("Assets\\unsyncd.txt");
                var outEntities = txt.DecompressFromBase64String();

                var now = DateTime.Now.AddDays(-8);
                var asBinary = now.ToBinary();

                var processors = new Dictionary<string, List<CloudEntity>>();
                foreach (var outEntity in outEntities)
                {
                    var ppdataset = DbSaveableEntity.fromJson<GeneralEntityDataset>(new KindItem(outEntity.DataBlob));
                    var saveable = new DbSaveableEntity(ppdataset)
                    {
                        kindName = new KindName(ppdataset.FormName)
                    };
                    if (!processors.ContainsKey(ppdataset.FormName))
                    {
                        processors[ppdataset.FormName] = new List<CloudEntity>();
                    }
                    var cloudEntity = new CloudEntity()
                    {
                        Id = saveable.Id.Value,
                        EntityId = saveable.EntityId.Value,
                        EditDay = now.toYMDInt(),
                        EditDate = asBinary,
                        DataBlob = saveable
                        .getJson()
                        .Encrypt()
                        .Value,
                        FormName = ppdataset.FormName,
                        KindMetaData = saveable.Entity.KindMetaData ?? string.Empty
                    };
                    processors[ppdataset.FormName].Add(cloudEntity);
                }
                foreach (var item in processors)
                {
                    new KindDataProcessor()
                        .addToProcessingQueue(item.Key, item.Value);
                }
                //we save them to local
                AppInstance.Instance.CloudDbInstance.RefreshLocalEntities(setProgressValue);
            }

            MessageBox.Show("Menu item clicked ");
        }

        private void MenuAllData_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu item clicked ");
        }

        private void MenuConfigure_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu item clicked ");
        }

        private async void MenuServerSync_Click(object sender, RoutedEventArgs e)
        {
            //we test the connection
            var isConnected = await CloudDb.checkConnection();
            if(!isConnected)
            {
                MessageBox.Show("No internet connection detected");
                return;
            }

            var dialog = 
                MessageBox.Show("Do you want to start data download from the server", "Please confirm action", 
                MessageBoxButton.OKCancel);

            if (dialog != MessageBoxResult.OK)
                return;
            
            //we get list of files to download
            var syncOldData = this.menuServerSyncOldData == sender;

            var res = await AppInstance.Instance.CloudDbInstance
                .EnsureServerSync(setProgressValue, syncOldData);

            //for each file, we download
            AppInstance.Instance.CloudDbInstance.RefreshLocalEntities(setProgressValue);
            //we decrypt

            //we deidentify

            //and save to the main datastore
            MessageBox.Show("Menu item clicked ");
        }
    }
}
