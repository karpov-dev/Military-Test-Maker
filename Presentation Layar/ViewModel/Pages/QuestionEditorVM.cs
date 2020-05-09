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
        private DataService _dataService = DataService.GetInstance();
        private List<InputAnswerVM> _answers;
        #endregion

        #region Constructors
        public QuestionEditorVM(RootVM root, object owner, Test test, Question question) : base(root, owner)
        {
            if ( question == null ) question = new Question();
            Question = question;
            Test = test;
            Error = new ErrorMessageVM();
            _answers = new List<InputAnswerVM>();

            QuestionInpit = new InputTextVM();
            QuestionInpit.Text = Question.Queston;

            OneAnswer = new InputAnswerVM(1);
            OneAnswer.Text = Question.Answers[0];
            OneAnswer.IsCheceked = true;
            _answers.Add(OneAnswer);

            TwoAnswer = new InputAnswerVM(2);
            TwoAnswer.Text = Question.Answers[1];
            _answers.Add(TwoAnswer);

            ThreeAnswer = new InputAnswerVM(3);
            ThreeAnswer.Text = Question.Answers[2];
            _answers.Add(ThreeAnswer);

            FourAnswer = new InputAnswerVM(4);
            FourAnswer.Text = Question.Answers[3];
            _answers.Add(FourAnswer);
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
            List<bool> canSaveList = new List<bool>();
            canSaveList.Add(QuestionInpit.CanSave());
            foreach(InputAnswerVM answerVM in _answers )
            {
                canSaveList.Add(answerVM.CanSave());
            }
            foreach(bool canSave in canSaveList )
            {
                if ( !canSave ) return false;
            }
            if ( string.IsNullOrWhiteSpace(GetRightAnswer())){
                Error.Show("Выберите правильный ответ");
                return false;
            }
            return true;
        }
        private void SaveQuestion()
        {
            Question.Queston = QuestionInpit.Text;
            Question.Answers = new List<string>();
            foreach(InputAnswerVM answerVM in _answers ) Question.Answers.Add(answerVM.Text);
            Question.RightAnswer = GetRightAnswer();
        }
        private string GetRightAnswer()
        {
            foreach(InputAnswerVM answerVM in _answers )
                if ( answerVM.IsCheceked ) return answerVM.Text;
            return null;
        }
        #endregion
    }
}
