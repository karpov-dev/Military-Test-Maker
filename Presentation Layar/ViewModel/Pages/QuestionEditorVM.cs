using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;
using System.Collections.Generic;

namespace Presentation_Layar.ViewModel.Pages
{
    class QuestionEditorVM : ViewModelBase
    {
        #region Variables
        DataService _dataService = DataService.GetInstance(); 
        #endregion

        #region Constructors
        public QuestionEditorVM(RootVM root, object owner, Test test, Question question) : base(root, owner)
        {
            if ( question == null ) question = new Question();
            Question = question;
            Test = test;
            Error = new ErrorMessageVM();

            QuestionInpit = new InputTextVM();
            QuestionInpit.Text = Question.Queston;

            OneAnswer = new InputAnswerVM(1);
            OneAnswer.Text = Question.Answers[0];

            TwoAnswer = new InputAnswerVM(2);
            TwoAnswer.Text = Question.Answers[1];

            ThreeAnswer = new InputAnswerVM(3);
            ThreeAnswer.Text = Question.Answers[2];

            FourAnswer = new InputAnswerVM(4);
            FourAnswer.Text = Question.Answers[3];
        }
        #endregion

        #region Properties
        public Test Test { get; set; }
        public Question Question { get; set; }
        public InputTextVM QuestionInpit { get; set; }
        public InputAnswerVM OneAnswer { get; set; }
        public InputAnswerVM TwoAnswer { get; set; }
        public InputAnswerVM ThreeAnswer { get; set; }
        public InputAnswerVM FourAnswer { get; set; }
        public ErrorMessageVM Error { get; set; }
        #endregion

        #region Relay Command
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }));

        private RelayCommand _addAndCancel;
        public RelayCommand AddAndCancel => _addAndCancel ?? ( _addAndCancel = new RelayCommand(obj =>
        {
            if ( CanSave() )
            {
                SaveQuestion();
                Test.Questions.Add(Question);
                Root.CurrentVM = Owner;
            }
        }));

        private RelayCommand _addAndNew;
        public RelayCommand AddAndNew => _addAndNew ?? ( _addAndNew = new RelayCommand(obj =>
        {
            if ( CanSave() )
            {
                SaveQuestion();
                Test.Questions.Add(Question);
                Root.CurrentVM = new QuestionEditorVM(Root, Owner, Test, null);
            }
        }));
        #endregion

        #region Methods
        private bool CanSave()
        {
            bool questionCanSave = QuestionInpit.CanSave(),
                 oneAnswerCanSave = OneAnswer.CanSave(),
                 twoAnswerCanSave = TwoAnswer.CanSave(),
                 threeAnswerCanSave = ThreeAnswer.CanSave(),
                 fourCanSave = FourAnswer.CanSave();
            if ( questionCanSave && oneAnswerCanSave && twoAnswerCanSave && threeAnswerCanSave && fourCanSave ) return true;
            return false;
        }
        private void SaveQuestion()
        {
            Question.Queston = QuestionInpit.Text;
            Question.Answers = new List<string>();
            Question.Answers.Add(OneAnswer.Text);
            Question.Answers.Add(TwoAnswer.Text);
            Question.Answers.Add(ThreeAnswer.Text);
            Question.Answers.Add(FourAnswer.Text);
        }
        #endregion
    }
}
