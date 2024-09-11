using BackupCSLib.Service;
using System.Windows;
using System.Windows.Controls;

namespace BackupCSApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            if (fvi != null && fvi.FileVersion != null)
            {
                string version = fvi.FileVersion;
                Title = $"{Title} {version}";
            }

            Actions actions = new()
            {
                SetProjectName = SetProjectName,
                SetActionName = SetActionName,
                InitProgressBar = InitProgressBar,
                SetProgressBar = SetProgressBar,
                InitDuraion = InitDuraion,
                SetDuration = SetDuration,
                AddTotalLine = AddTotalLine
            };

            Backup backup = new() { TheActionsactions = actions };
            backup.Run();
        }

        private void SetProjectName(string projectName)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                ProjectName.Text = projectName;
            }));
        }

        private void SetActionName(string actionName)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                ActionName.Text = actionName;
            }));
        }

        private void InitProgressBar(int start, int finish)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                ProgressBar.Minimum = start;
                ProgressBar.Maximum = finish;
                ProgressBar.Value = start;
            }));
        }

        private void SetProgressBar(int value)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                ProgressBar.Value = value;
            }));
        }

        private void InitDuraion()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Duration.Text = "";
            }));
        }

        private void SetDuration(string duration)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Duration.Text = duration;
            }));
        }

        private void AddTotalLine(string totalLine)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Total.Text += totalLine + "\n";
            }));
        }
    }
}