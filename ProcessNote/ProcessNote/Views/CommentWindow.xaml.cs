using System;
using System.Windows;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        private int _processId;

        public CommentWindow(int processId)
        {
            InitializeComponent();
            _processId = processId;
        }

        private void SaveComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomProcess.Notes.Count < 1) CustomProcess.Notes.Add(_processId, Comment.Text);
                else CustomProcess.Notes[_processId] = Comment.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private void CloseComment_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}