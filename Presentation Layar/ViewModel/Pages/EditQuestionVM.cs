﻿using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class EditQuestionVM : ViewModelBase 
    {
        #region Constructors
        public EditQuestionVM(RootVM root, object owner, Question question) : base(root, owner)
        {
            Question = question;
            OldQuestion = (Question)question.Clone();
            QuestionInput = new InputTextVM();
            QuestionInput.Text = Question.Queston;

            Answers = new ObservableCollection<InputAnswerVM>();
            for(int i = 0; i < question.Answers.Count; i++ )
            {
                InputAnswerVM answerVM = new InputAnswerVM(i + 1);
                answerVM.Text = question.Answers[i];
                if ( question.Answers[i] == question.RightAnswer )
                    answerVM.IsCheceked = true;
                Answers.Add(answerVM);
            }
        }
        #endregion

        #region Properties
        public Question Question { get; set; }
        public Question OldQuestion { get; set; }
        public InputTextVM QuestionInput { get; set; }
        public ObservableCollection<InputAnswerVM> Answers { get; set; }
        #endregion

        #region Relay Command
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            Question = OldQuestion;
            Root.CurrentVM = Owner;
        }));

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? ( _saveCommand = new RelayCommand(obj =>
        {
            if ( CanSave() )
            {
                Question.Queston = QuestionInput.Text;
                UpdateAnswers();
                SetRightAnswer();

                Root.CurrentVM = Owner;
            }
        }));
        #endregion

        #region Methods 
        private bool CanSave()
        {
            List<bool> cmpCanSave = new List<bool>()
            {
                QuestionInput.CanSave()
            };
            foreach(InputAnswerVM inputVM in Answers )
            {
                cmpCanSave.Add(inputVM.CanSave());
            }
            foreach(bool canSave in cmpCanSave )
            {
                if ( !canSave ) return false;
            }
            return true;
        }
        private void UpdateAnswers()
        {
            for ( int i = 0; i < Answers.Count; i++ )
            {
                Question.Answers[i] = Answers[i].Text;
            }
        }
        private void SetRightAnswer()
        {
            foreach(InputAnswerVM answerVM in Answers )
            {
                if ( answerVM.IsCheceked )
                    Question.RightAnswer = answerVM.Text;
            }
        }
        #endregion
    }
}
