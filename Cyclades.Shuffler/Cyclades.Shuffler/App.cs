using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cyclades.Shuffler.Domain;
using Cyclades.Shuffler.Helpers;
using Cyclades.Shuffler.ViewModels;
using Cyclades.Shuffler.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Cyclades.Shuffler
{
    public class App : Application
    {
        
        public App()
        {
            // First time initialization
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

      var navigationService = new NavigationService();
            navigationService.Configure(ViewModelLocator.StartPageKey, typeof(StartPage));
            navigationService.Configure(ViewModelLocator.GamePageKey, typeof(GamePage));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            var dialogService = new DialogService();
            SimpleIoc.Default.Register<IDialogService>(() => dialogService);

            SimpleIoc.Default.Register<StartPageViewModel>();
            SimpleIoc.Default.Register<GamePageViewModel>();

            var navPage = new NavigationPage(new StartPage());

            navigationService.Initialize(navPage);
            dialogService.Initialize(navPage);

            // The root page of your application
            MainPage = navPage;

            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    
}
