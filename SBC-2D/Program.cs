using Dapper.FluentMap;
using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Device;
using SBC_2D.Infrastructures.Error;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Infrastructures.User;
using SBC_2D.Presenters;
using SBC_2D.Servicies;
using SBC_2D.Shared;
using SBC_2D.Views;
using SBC_2D.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SBC_2D.Shared.Enums;

namespace SBC_2D
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 

        [STAThread]

        static async Task Main()
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
            ErrorManager errorManager = new ErrorManager();

            //Dao應該配合工廠方法，切換不同資料庫連線
            //Dao生命週期不應該留這麼久
            UserDao userDao = new UserDao(iniStore.Setup.PathConfig.SqLiteFile);
            UserService userService = new UserService(userDao);

            RecipeDao recipeDao = new RecipeDao(iniStore.Setup.PathConfig.SqLiteFile);
            RecipeService recipeService = new RecipeService(recipeDao);

            DeviceManager deviceManager = new DeviceManager();
            deviceManager.Initialize(iniStore.Setup.DeviceConfig);
            int diStart = 0;
            int doStart = 0;
            List<(IIoDevice, int DiStart, int DoStart)> indexesMap
                = new List<(IIoDevice, int DiStart, int DoStart)>();
            foreach (var device in deviceManager.Devices.OfType<IIoDevice>())
            {
                indexesMap.Add((device, diStart, doStart));
                diStart = diStart + device.DiCount;
                doStart = doStart + device.DoCount;
            }
            SystemIo systemIo = new SystemIo(indexesMap);
            systemIo.Initialize();
            Machine machine = new Machine(errorManager, deviceManager, systemIo, iniStore.Setup);
            machine.Initialize();
            Form1 form1 = new Form1();
            Form2 form2 = new Form2();
            Form3 form3 = new Form3();
            Form4 form4 = new Form4();
            FormMain formMain = new FormMain(form1, form2, form3, form4);
            HomePagePresenter homePagePresenter = new HomePagePresenter(form1, machine, systemIo, errorManager);
            UserPresenter userPresenter = new UserPresenter(form4, userService);
            XmlDirSelectorPresenter xmlDirSelectorPresenter = new XmlDirSelectorPresenter(form4);
            RecipePresenter recipePresenter = new RecipePresenter(recipeService, form2);
            DevicePresenter devicePresenter = new DevicePresenter(form3, deviceManager, systemIo);
            FormMainPresenter formMainPresenter = new FormMainPresenter(formMain);
            machine.StatusChanged += (status) =>
            {
                formMain.SetMachineStatus(status.ToString());
            };
            userPresenter.UserChanged += (role, id) =>
            {
                formMain.SetUserRole(role.ToString());
                bool isEnabledEditMode = role != Role.Operater;
                form2.SetEditMode(isEnabledEditMode);
            };
            recipePresenter.OnRecipeChanged += (recipe) =>
            {
                machine.Recipe = recipe;
                formMain.SetRecipeName(recipe.Name);
            };
            formMain.AppStarted += () =>
            {
                userPresenter.Initialize();
                recipePresenter.Initialize();
                devicePresenter.Initialize();
                homePagePresenter.Initialize();
                _ = deviceManager.ConnectAllAsync();
            };
            Application.Run(formMain);
        }
    }
}
