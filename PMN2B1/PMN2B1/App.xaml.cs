using PMN2B1.Repositories;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMN2B1
{
    public partial class App : Application
    {
        static CattleRepository cattleRepository;

        public static CattleRepository CattleRepository
        {
            get
            {
                if (cattleRepository == null)
                {
                    cattleRepository = new CattleRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cattle.db3"));
                }
                return cattleRepository;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
