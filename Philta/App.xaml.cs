using Philta.Models;
using Philta.ViewModels;
using Philta.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace Philta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPhiltaViewModel, PhiltaViewModel>();
            container.RegisterType<IPhiltaModel, PhiltaModel>();

            var window = container.Resolve<PhiltaView>();
            window.Show();
        }

    }
}
