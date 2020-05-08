using Presentation_Layar.Service;
using System.Windows;

namespace Presentation_Layar.ViewModel.BaseNavigation
{
    class ViewModelBase : ModelPropertyChanged 
    {
        public RootVM Root { get; private set; }
        public object Owner { get; private set; }

        public ViewModelBase(RootVM root, object owner)
        {
            Root = root;
            Owner = owner;
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
