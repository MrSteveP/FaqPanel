using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaqPanel.Views.Controls;
using Xamarin.Forms;

namespace FaqPanel
{
    public partial class MainPage : ContentPage
    {
        public List<FaqItem> FaqList { get; set; } = new List<FaqItem>();

        public MainPage()
        {
            InitializeComponent();

            FaqList.Add(new FaqItem() { Question = "Question 1", Answer = "Testing 123" });
            FaqList.Add(new FaqItem() { Question = "Question 2", Answer = "Testing 456" });
            FaqList.Add(new FaqItem() { Question = "Question 3", Answer = "Testing 789" });

            BindingContext = this;
        }

        private void QuestionTapped(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Question Tapped");
            Debug.WriteLine(sender);

            if (sender != null && sender is StackLayout)
            {
                FaqItem currentItem = null;

                StackLayout questionBar = (StackLayout)sender;
                Debug.WriteLine(questionBar.BindingContext);

                if (questionBar.BindingContext != null)
                {
                    currentItem = (FaqItem)questionBar.BindingContext;
                    bool currentlyExpanded = currentItem.IsExpanded;

                    foreach (FaqItem item in FaqList)
                    {
                        item.IsExpanded = false;
                    }

                    currentItem.IsExpanded = !currentlyExpanded;
                }
            }
        }
    }
}
