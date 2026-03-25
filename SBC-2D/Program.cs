using Dapper.FluentMap;
using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Presenters;
using SBC_2D.Servicies;
using SBC_2D.Views;
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
            });
            IniStore iniStore = new IniStore();
            IniService iniService = new IniService(iniStore);
            iniStore.Setup = iniService.GetSetup();

            RecipeDao recipeDao = new RecipeDao(iniStore.Setup.PathConfig.SqLiteFile);
            RecipeService recipeService = new RecipeService(iniService, recipeDao);

            DevicesStore devicesStore = new DevicesStore();
            DeviceManager deviceManager = new DeviceManager();
            DeviceService deviceService = new DeviceService(deviceManager, iniStore.Setup.DeviceConfig, devicesStore, iniService);
            List<IDevice> devices = DeviceFactory.CreateDevices(iniStore.Setup.DeviceConfig);
            devicesStore.Devices.Clear();
            devicesStore.Devices.AddRange(devices);
            List<IoDeviceContext> iodcs = DeviceFactory.CreateIoDeviceContexts(devices.OfType<IIoDevice>());
            devicesStore.IoDeviceContext.Clear();
            devicesStore.IoDeviceContext.AddRange(iodcs);
            //deviceManager.Initialize(devicesStore, iniStore.Setup.DeviceConfig);

            Form2 form2 = new Form2();
            Form3 form3 = new Form3();
            FormMain formMain = new FormMain(form2 , form3);
            RecipePresenter recipePresenter = new RecipePresenter(recipeService, iniService, form2);
            DevicePresenter devicePresenter = new DevicePresenter(form3, deviceService, iniService);
            FormMainPresenter formMainPresenter = new FormMainPresenter(formMain, devicePresenter, recipePresenter);
            formMainPresenter.Initialize();
            Application.Run(formMain);
        }
    }
}
