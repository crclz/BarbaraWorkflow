using BarbaraWorkflow.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BarbaraWorkflow.App.Services
{
    public class MyConfigService
    {
        private DirectoryInfo store = new DirectoryInfo(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/BarbaraWorkflow");

        private BehaviorSubject<ApplicationSetting> setting { get; } = new BehaviorSubject<ApplicationSetting>(new ApplicationSetting());
        public Subject<string> SettingMessageSS { get; } = new Subject<string>();
        public IObservable<MainLabelStyle> MainLabelStyleSS { get; }

        public MyConfigService()
        {
            LoadApplicationSetting();

            MainLabelStyleSS = setting.Select(p => p.MainLabelStyle).DistinctUntilChanged();

            var th = new Thread(() =>
            {
                while (true)
                {
                    LoadApplicationSetting();
                    Thread.Sleep(1000);
                }
            });

            th.IsBackground = true;

            th.Start();
        }

        // TODO: descructor timer

        public static MyConfigService Singleton { get; } = initSingleton();

        private static MyConfigService initSingleton()
        {
            return new MyConfigService();
        }




        public FileInfo GetSettingFile()
        {
            if (!store.Exists)
            {
                store.Create();
            }

            var settingStore = new DirectoryInfo(store.FullName + "/settings");
            if (!settingStore.Exists)
            {
                settingStore.Create();
            }

            var file = new FileInfo(settingStore.FullName + "/settings.json");

            if (!file.Exists)
            {
                var defaultSetting = new ApplicationSetting();
                File.WriteAllText(file.FullName, JsonConvert.SerializeObject(defaultSetting, Formatting.Indented));
            }

            return file;
        }

        public void LoadApplicationSetting()
        {
            try
            {
                var text = File.ReadAllText(GetSettingFile().FullName);
                var sett = JsonConvert.DeserializeObject<ApplicationSetting>(text)!;
                setting.OnNext(sett);
                SettingMessageSS.OnNext("配置文件有效");
            }
            catch (Exception e)
            {
                SettingMessageSS.OnNext($"配置获取失败，尝试删除配置文件。错误：{e.Message}");
            }
        }
    }
}
