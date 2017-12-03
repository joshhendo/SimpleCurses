using System;
using System.Linq;
using SimpleCurses.Handlers;
using SimpleCurses.Interfaces;
using SimpleCurses.Rendering;

namespace SimpleCurses.Views
{
    public class MultipleQuestionsView: IRenderable
    {
        private string[] questions;
        private int currentQuestion;
        
        private string[] answers;

        public event ViewEventHandler Finished;

        public MultipleQuestionsView(string[] questions)
        {
            this.questions = questions;
            this.answers = new string[questions.Length];
            this.currentQuestion = 0;
        }
        
        public void HandleKeyPress(ConsoleKeyInfo key)
        {
            if (currentQuestion >= this.answers.Length)
            {
                return;
            }
            
            if (this.answers[currentQuestion] == null)
            {
                this.answers[currentQuestion] = "";
            }
            
            // TODO: The inner logic here needs much improvement. String manipulation like this == bad!
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    currentQuestion++;
                    if (currentQuestion >= this.questions.Length)
                    {
                        Finished?.Invoke(this, this.answers);
                    }
                    break;
                case ConsoleKey.Backspace:
                    if (this.answers[currentQuestion].Length > 0)
                    {
                        this.answers[currentQuestion] = this.answers[currentQuestion].Substring(0, this.answers[currentQuestion].Length - 1);
                    }
                    break;
                case ConsoleKey.Escape:
                    Finished?.Invoke(this, null);
                    break;
                default:
                    this.answers[currentQuestion] += key.KeyChar;
                    break;
            }
        }

        public RenderableDot[][] GetRenderable()
        {
            var generator = new RenderableDotGenerator();

            var maxQuestionLength = questions.Max().Length;
            for (var i = 0; i < questions.Length; i++)
            {
                generator.SetPosition(5, 3 + i);
                generator.Write(questions[i]);
                
                generator.SetPosition(maxQuestionLength + 4, 3 + i);
                
                generator.Write(": ");
                generator.Write((answers[i] ?? ""));

                if (i == currentQuestion)
                {
                    generator.Write("▌");
                }
            }
            
            return generator.GetRenderableDots();
        }
  
    }
}