using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToolWell.Interfaces;

namespace ToolWell.Services
{
    public class MessageBoxService : IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public bool ShowConfirmation(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}
