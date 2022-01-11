using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FaqPanel.Converters;
using Xamarin.Forms;

namespace FaqPanel.Models
{
    public class FaqItem : INotifyPropertyChanged
    {
        public FaqItem()
        {
        }

        public string Question
        {
            get => question;

            set
            {
                question = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Question"));
            }
        }

        public string Answer
        {
            get => answer;

            set
            {
                answer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Answer"));
            }
        }

        public bool IsExpanded
        {
            get => isExpanded;

            set
            {
                isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsExpanded"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string question;

        private string answer;

        private bool isExpanded = false;
    }
}
