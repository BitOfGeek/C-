using Beverages.View;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beverages.Mediator
{
    class MessageMediator
    {
        public static Action CloseAction { get; set; }
        ProductView productView;

        public MessageMediator()
        {
            Messenger.Default.Register<NotificationMessage>(this, ActivateWindow);
        }

        private void ActivateWindow(NotificationMessage message)
        {

            if (message.Notification == "activate")
            {
                productView = new ProductView();
                productView.Show();
            }

            if (message.Notification == "close")
            {
                MessageBox.Show("Restart application for changes to appear");
                productView.Close();
            }
        }
    }
}
