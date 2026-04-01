using Dapper.FluentMap;
using SBC_2D.Domain.Servicies;
using SBC_2D.Events;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Infrastructures.User;
using SBC_2D.Presenters;
using SBC_2D.Servicies;
using SBC_2D.Views;
using SBC_2D.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SBC_2D
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 

        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new RecipeMap());
                config.AddMap(new UserMap());
            });
            IniStore iniStore = new IniStore();
            IniService iniService = new IniService(iniStore);
            iniStore.Setup = IniService.GetSetup();

            //Dao應該配合工廠方法，切換不同資料庫連線
            //Dao生命週期不應該留這麼久
            UserDao userDao = new UserDao(iniStore.Setup.PathConfig.SqLiteFile);
            UserService userService = new UserService(userDao);

            RecipeDao recipeDao = new RecipeDao(iniStore.Setup.PathConfig.SqLiteFile);
            RecipeService recipeService = new RecipeService(recipeDao);

            DeviceManager deviceManager = new DeviceManager();
            deviceManager.Initialize(iniStore.Setup.DeviceConfig);

            Form1 form1 = new Form1();
            Form2 form2 = new Form2();
            Form3 form3 = new Form3();
            Form4 form4 = new Form4();
            FormMain formMain = new FormMain(form1, form2 , form3, form4);
            PresenterEventBus presenterEventBus = new PresenterEventBus();
            UserPresenter userPresenter = new UserPresenter(form4, userService, presenterEventBus);
            XmlDirSelectorPresenter xmlDirSelectorPresenter = new XmlDirSelectorPresenter(form4, presenterEventBus);
            RecipePresenter recipePresenter = new RecipePresenter(recipeService, form2, presenterEventBus);
            DevicePresenter devicePresenter = new DevicePresenter(form3, deviceManager);
            //FormMainPresenter formMainPresenter = new FormMainPresenter(formMain, devicePresenter, recipePresenter, userPresenter, presenterEventBus);
            FormMainPresenter formMainPresenter = new FormMainPresenter(formMain, presenterEventBus);
            userPresenter.Initialize();
            recipePresenter.Initialize();
            devicePresenter.Initialize();
            _ = devicePresenter.ConnectAllAsync();
            Application.Run(formMain);
        }
    }
}
