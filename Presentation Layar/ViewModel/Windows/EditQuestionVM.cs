using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Presentation_Layar.ViewModel.Windows
{
    class EditQuestionVM : ModelPropertyChanged 
    {
        #region Constructors
        public EditQuestionVM(EditQuestion window, Question question)
        {
            Window = window;
            Question = question;
            OldQuestion = (Question)question.Clone();
            QuestionInput = new InputTextVM();
            QuestionInput.Text = Question.Queston;

            Answers = new ObservableCollection<InputAnswerVM>();
            for(int i = 0; i < question.Answers.Count; i++ )
            {
                InputAnswerVM answerVM = new InputAnswerVM(i + 1);
                answerVM.Text = question.Answers[i];
                Answers.Add(answerVM);
            }
        }
        #endregion

        #region Properties
        public Question Question { get; set; }
        public Question OldQuestion { get; set; }
        public EditQuestion Window { get; set; }
        public InputTextVM QuestionInput { get; set; }
        public ObservableCollection<InputAnswerVM> Answers { get; set; }
        #endregion

        #region Relay Command
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            Question = OldQuestion;
            Window.Hide();
        }));

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? ( _saveCommand = new RelayCommand(obj =>
        {
            Question.Queston = QuestionInput.Text;
            DataService dataService = DataService.GetInstance();
            for(int i = 0; i < Answers.Count; i++ )
            {
                Question.Answers[i] = Answers[i].Text;
            }
            
            Window.Hide();
        }));
        #endregion
    }
}
