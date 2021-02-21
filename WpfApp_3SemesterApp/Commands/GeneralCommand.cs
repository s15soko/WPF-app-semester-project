using System;
using System.Windows.Input;

namespace WpfApp_3SemesterApp.Commands
{
    public class GeneralCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action DoWork;

        public GeneralCommand(Action work)
        {
            DoWork = work;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoWork();
        }
    }
}
