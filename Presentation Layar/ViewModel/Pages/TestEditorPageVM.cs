using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using Presentation_Layar.ViewModel.Windows;
using Service_Layar;

namespace Presentation_Layar.ViewModel.Pages
{
    class TestEditorPageVM : ViewModelBase
    {
        #region Variables
        private DataService _dataService = DataService.GetInstance();
        private Settings _settings = Settings.GetInstance();
        #endregion

        #region Constructors
        public TestEditorPageVM(RootVM root, object owner) : base(root, owner) 
        {
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
            TestModes = new List<string>();
            TestModes.Add(Settings.GROUP_MODE);
            TestModes.Add(Settings.PERSONAL_MODE);
            SelectedTestMode = Settings.DEFAULT_SELECTED_MODE;
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

        public string SelectedTestMode { get; set; }
        public List<string> TestModes { get; set; }

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
            TestEditPageVM editPage = new TestEditPageVM(Root, this, null);
            Root.CurrentVM = new PasswordPageVM(Root, this, editPage, _settings.Password);
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

            TestEditPageVM editPage = new TestEditPageVM(Root, this, SelectedTest);
            Root.CurrentVM = new PasswordPageVM(Root, this, editPage, _settings.Password);
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
            PasswordPageVM passwordPage = new PasswordPageVM(Root, Owner, this, _settings.Password);
            if ( passwordPage.PasswordIsTrue )
            {
                if ( !removeResult ) Error.Show("Возникла ошибка при удалении теста");
                Info.Show("Тест успешно удалён");
            }
        }));

        private RelayCommand _exportToPDF;
        public RelayCommand ExportToPDF => _exportToPDF ?? ( _exportToPDF = new RelayCommand(obj =>
        {
            if ( SelectedTest == null ) return;
            PDFConverterSettings pdfWindow = new PDFConverterSettings();
            pdfWindow.DataContext = new PDFConverterSettingsVM(SelectedTest);
            pdfWindow.Show();
        }));

        private RelayCommand _goToTesting;
        public RelayCommand GoToTesting => _goToTesting ?? ( _goToTesting = new RelayCommand(obj =>
        {
            if ( SelectedTest == null || SelectedTestMode == null) return;
            if ( _settings.GetAmountQuestions(SelectedTestMode) > SelectedTest.Questions.Count )
            {
                MessageBox.Show("Тест содержит меньше вопросов (" + SelectedTest.Questions.Count + ") , чем выбранно в настройках (" + _settings.GetAmountQuestions(SelectedTestMode) + "). Количество тестов в настройках изменено на: " + SelectedTest.Questions.Count);
                switch ( SelectedTestMode )
                {
                    case Settings.GROUP_MODE: _settings.GroupAmountQuestions = SelectedTest.Questions.Count; break;
                    case Settings.PERSONAL_MODE: _settings.PersonalAmountQuestions = SelectedTest.Questions.Count; break;
                }
            }
            TestManager manager = new TestManager(Root, this, SelectedTestMode, SelectedTest);
        }));

        private RelayCommand _goToTestingSettings;
        public RelayCommand GoToTestingSettings => _goToTestingSettings ?? ( _goToTestingSettings = new RelayCommand(obj =>
        {
            TestingSettings settings = new TestingSettings();
            settings.DataContext = new TestingSettingsVM(settings);
            settings.Show();
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
