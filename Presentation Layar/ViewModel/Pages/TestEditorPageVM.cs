using System.Collections.ObjectModel;
using Business_Layar;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;

namespace Presentation_Layar.ViewModel.Pages
{
    class TestEditorPageVM : ViewModelBase
    {
        #region Variables
        private DataService _dataService = DataService.GetInstance();
        #endregion

        #region Constructors
        public TestEditorPageVM(RootVM root, object owner) : base(root, owner) 
        {
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
        }
        #endregion

        #region Properties
        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }
        public ObservableCollection<Test> Tests => new ObservableCollection<Test>(_dataService.GetTests());
        public bool ButtonsEnabled
        {
            get
            {
                if ( SelectedTest != null ) return true;
                return false;
            }
        }

        private Test _selectedTest;
        public Test SelectedTest
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;
                OnPropertyChanged();
                HideNotifications();
                OnPropertyChanged("ButtonsEnabled");
            }
        }
        #endregion

        #region Ralay Command
        private RelayCommand _addTest;
        public RelayCommand AddTest => _addTest ?? ( _addTest = new RelayCommand(obj =>
        {
            HideNotifications();
            Root.CurrentVM = new TestEditPageVM(Root, this, null);
        }));

        private RelayCommand _editTest;
        public RelayCommand EditTest => _editTest ?? ( _editTest = new RelayCommand(obj =>
        {
            HideNotifications();
            if ( SelectedTest == null )
            {
                Error.Show("Выберите тест для редактирования");
                return;
            }
            Root.CurrentVM = new TestEditPageVM(Root, this, SelectedTest);
        }));

        private RelayCommand _deleteTest;
        public RelayCommand DeleteTest => _deleteTest ?? ( _deleteTest = new RelayCommand(obj =>
        {
            HideNotifications();
            if ( SelectedTest == null )
            {
                Error.Show("Выберите тест для удаления");
                return;
            }
            bool removeResult = _dataService.RemoveTest(SelectedTest);
            UpdateComponent();
            if ( !removeResult ) Error.Show("Возникла ошибка при удалении теста");
            Info.Show("Тест успешно удалён");
            
        }));

        private RelayCommand _cancel;
        public RelayCommand Cancel => _cancel ?? ( _cancel = new RelayCommand(obj =>
        {
            HideNotifications();
            Root.CurrentVM = Owner;
        }));
        #endregion

        #region Methods
        private void UpdateComponent()
        {
            OnPropertyChanged("Tests");
            OnPropertyChanged("SelectedTest");
            OnPropertyChanged("ButtonsEnabled");
            HideNotifications();
        }
        private void HideNotifications()
        {
            Error.Hide();
            Info.Hide();
        }
        #endregion
    }
}
