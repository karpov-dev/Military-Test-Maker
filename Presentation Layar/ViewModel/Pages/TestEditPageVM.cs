using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;

namespace Presentation_Layar.ViewModel.Pages
{
    class TestEditPageVM : ViewModelBase 
    {
        #region Variables
        private DataService _dataService = DataService.GetInstance();
        private Test _oldVersion;
        #endregion

        #region Constructors
        public TestEditPageVM(RootVM root, object owner, Test test) : base(root, owner)
        {
            _oldVersion = test;
            if ( test == null ) test = new Test();
            Test = (Test)test.Clone();

            Title = new InputTextVM();
            Title.UseValidation = true;
            Title.Text = Test.Title;

            Author = new InputTextVM();
            Author.UseValidation = true;
            Author.Text = Test.Author;

            Description = new InputTextVM();
            Description.UseValidation = true;
            Description.Text = Test.Description;

            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();

        }
        #endregion

        #region Properties
        public bool ButtonsEnabled
        {
            get
            {
                if ( SelectedQuestion != null ) return true;
                return false;
            }
        }
        public Test Test { get; set; }
        //public ObservableCollection<Question> Questions => new ObservableCollection<Question>(Test.Questions);
        public InputTextVM Title { get; set; }
        public InputTextVM Author { get; set; }
        public InputTextVM Description { get; set; }
        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }

        private Question _selectedQuestion;
        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
                OnPropertyChanged("ButtonsEnabled");
            }
        }
        #endregion

        #region Relay Command
        private RelayCommand _saveTest;
        public RelayCommand SaveTest => _saveTest ?? ( _saveTest = new RelayCommand(obj =>
        {
            if ( CanSave() )
            {
                Test.Title = Title.Text;
                Test.Description = Description.Text;
                Test.Author = Author.Text;
                //Test.Questions = new List<Question>(Questions);
                _dataService.UpsertTest(_oldVersion, Test);
                Root.CurrentVM = Owner;
            }
        }));

        private RelayCommand _cancalCommand;
        public RelayCommand CancalCommand => _cancalCommand ?? ( _cancalCommand = new RelayCommand(obj =>
        {
            if ( HasChanges() )
            {
                MessageBoxResult result = MessageBox.Show("В тесте есть несохранённые изменения. Вы действительно хотите выйти без сохранения?", "Обнаружены несохранённые данные", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes) Root.CurrentVM = Owner;
            } else Root.CurrentVM = Owner;
        }));
        #endregion

        #region Methods
        private void UpdateComponent()
        {
            OnPropertyChanged("Test");
            OnPropertyChanged("SelectedQuestion");
            OnPropertyChanged("Title");
            OnPropertyChanged("Author");
            OnPropertyChanged("Description");
            OnPropertyChanged("Questions");
            OnPropertyChanged("ButtonsEnabled");
        }
        private bool CanSave()
        {
           bool titleSaveResult = Title.CanSave(),
                authorSaveResult = Author.CanSave(),
                descriptionSaveResult = Description.CanSave(),
                amountTestsCanSave = Test.Questions.Count > 2;
            foreach(Question question in Test.Questions )
            {
                if ( string.IsNullOrWhiteSpace(question.Queston) || string.IsNullOrWhiteSpace(question.RightAnswer) )
                {
                    Error.Show("Заполните полностью вопросы");
                    return false;
                }
                foreach(string answer in question.Answers )
                {
                    if ( string.IsNullOrWhiteSpace(answer) ) return false;
                    Error.Show("Вопрос " + question.Queston + " содержит незаполненные поля ответов");
                }
            }
            if ( !amountTestsCanSave ) Error.Show("Тест должен содержать как минимум три вопроса");
            if ( titleSaveResult && authorSaveResult && descriptionSaveResult && amountTestsCanSave ) return true;
            return false;
        }
        private bool HasChanges()
        {
            if ( _oldVersion == null ) return true;
            if ( _oldVersion.Title != Test.Title ||
                _oldVersion.Description != Test.Description ||
                _oldVersion.Author != Test.Author ||
                _oldVersion.Questions.Count != Test.Questions.Count ) return true;
            return false;
        }
        #endregion
    }
}
