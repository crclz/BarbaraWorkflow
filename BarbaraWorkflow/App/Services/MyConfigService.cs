using BarbaraWorkflow.Domain.Models;
using BarbaraWorkflow.Infra;
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
    public class MyConfigService : IDisposable
    {
        private DirectoryInfo store = new DirectoryInfo(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/BarbaraWorkflow");

        private Subject<int> disposeSS = new Subject<int>();
        private bool disposedValue;

        private IObservable<(ApplicationSetting, string)> settingUnchecked { get; }
        private IObservable<ApplicationSetting> setting { get; }
        public IObservable<string> SettingMessageSS { get; } = new Subject<string>();
        public IObservable<MainLabelStyle> MainLabelStyleSS { get; }

        public MyConfigService()
        {
            settingUnchecked = new FileContentWatcher(GetSettingFile().FullName)
                .Select(p => ReadApplicationSetting(p))
                .Replay(1).RefCount();

            setting = settingUnchecked.Where(p => string.IsNullOrEmpty(p.Item2)).Select(p => p.Item1);

            MainLabelStyleSS = setting.Select(p => p.MainLabelStyle).DistinctUntilChanged();

            SettingMessageSS = settingUnchecked.Select(p =>
            {
                if (string.IsNullOrEmpty(p.Item2))
                {
                    return "配置文件有效";
                }
                else
                {
                    return $"配置获取失败，尝试删除配置文件。Message：{p.Item2}";
                }
            });
        }

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

        public (ApplicationSetting, string) ReadApplicationSetting(string content)
        {
            try
            {
                var st = JsonConvert.DeserializeObject<ApplicationSetting>(content)!;
                if (st == null)
                {
                    throw new Exception("Setting is null");
                }
                return (st, "");
            }
            catch (Exception ex)
            {
                return (new ApplicationSetting(), ex.Message);
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    disposeSS.OnNext(1);
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
