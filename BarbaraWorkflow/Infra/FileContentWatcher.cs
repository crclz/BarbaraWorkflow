using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbaraWorkflow.Infra
{
    class FileContentWatcher : IObservable<string>
    {
        private Dictionary<string, IObserver<string>> Subscribers = new Dictionary<string, IObserver<string>>();
        private readonly FileSystemWatcher watcher;

        public string Filename { get; }

        public FileContentWatcher(string filename)
        {
            watcher = new FileSystemWatcher(Path.GetDirectoryName(filename)!);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            //watcher.Changed += OnChanged;
            //watcher.Created += OnCreated;
            //watcher.Deleted += OnDeleted;
            //watcher.Renamed += OnRenamed;
            //watcher.Error += OnError;

            watcher.Changed += Watcher_Changed;

            watcher.Filter = Path.GetFileName(filename);
            watcher.IncludeSubdirectories = false;
            Filename = filename;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(Filename))
            {
                EmitChange("");
            }

            var content = "";

            var retries = 3;

            for (var i = 0; i < retries; i++)
            {
                Thread.Sleep(25);

                try
                {
                    content = File.ReadAllText(Filename);
                    break;
                }
                catch (IOException ex) when (ex.Message.Contains("access") && i != retries - 1)
                {
                    continue;
                }

            }

            EmitChange(content);
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            var id = Guid.NewGuid().ToString();
            Subscribers.Add(id, observer);

            Refresh();

            return new UnsubscribeHandler(() => Unsubscribe(id));
        }

        public void Unsubscribe(string id)
        {
            Subscribers.Remove(id);
            Refresh();
        }

        private void Refresh()
        {
            watcher.EnableRaisingEvents = Subscribers.Count > 0;
        }

        private void EmitChange(string content)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.Value.OnNext(content);
            }
        }
    }

    class UnsubscribeHandler : IDisposable
    {
        private readonly Action dispose;

        public UnsubscribeHandler(Action dispose)
        {
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose();
        }
    }
}
