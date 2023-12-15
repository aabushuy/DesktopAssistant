using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ScenarioTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        }

        public void DoSafeAction(Action action)
        {
            if (Dispatcher.Thread == Thread.CurrentThread)
            {
                action.Invoke();
            }
            else
            {
                Dispatcher.Invoke(action);
            }
        }
    }
}
