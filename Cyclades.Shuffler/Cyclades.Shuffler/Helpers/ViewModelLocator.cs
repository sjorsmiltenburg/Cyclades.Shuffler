using System.Diagnostics.CodeAnalysis;
using Cyclades.Shuffler.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Cyclades.Shuffler.Helpers
{
    public class ViewModelLocator
    {
        public const string StartPageKey = "StartPage";
        public const string GamePageKey = "GamePage";

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",Justification = "This non-static member is needed for data binding purposes.")]
        public StartPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartPageViewModel>();
            }
        }

        static ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                //if (!SimpleIoc.Default.IsRegistered<GalaSoft.MvvmLight.Views.INavigationService>())
                //{
                //    SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.INavigationService, DesignNavigationService>();
                //}

                //SimpleIoc.Default.Register<IFlowersService, DesignFlowersService>();
            }
            else
            {
                //SimpleIoc.Default.Register<IFlowersService, FlowersService>();
            }

       
        }
    }
}
