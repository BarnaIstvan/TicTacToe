using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private char[,] gridArray = new char[3, 3];
        private bool gameFinished = false;

        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    gridArray[row, col] = ' ';
                }
            }
        }

        private bool PlayerWins(char playerValue)
        {
            for (int i = 0; i < 3; i++)
            {
                if (gridArray[i, 0] == playerValue && gridArray[i, 1] == playerValue && gridArray[i, 2] == playerValue)
                    return true;
                if (gridArray[0, i] == playerValue && gridArray[1, i] == playerValue && gridArray[2, i] == playerValue)
                    return true;
            }
            if (gridArray[0, 0] == playerValue && gridArray[1, 1] == playerValue && gridArray[2, 2] == playerValue)
                return true;
            if (gridArray[0, 2] == playerValue && gridArray[1, 1] == playerValue && gridArray[2, 0] == playerValue)
                return true;
            return false;
        }

        private bool IsGameFinished()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (gridArray[row, col] == ' ')
                        return false;
                }
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            if (!gameFinished && btn.Content == null)
            {
                btn.Content = currentPlayer;
                btn.IsEnabled = false;
                SolidColorBrush bgBrush = new SolidColorBrush();
                if (currentPlayer == 'X')
                    bgBrush.Color = Colors.Red;
                else
                    bgBrush.Color = Colors.Green;
                btn.Background = bgBrush;

                gridArray[row, col] = currentPlayer;

                if (PlayerWins(currentPlayer))
                {
                    MessageBox.Show($"{currentPlayer} Has Won", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    gameFinished = true;
                    Close();
                }
                else if (IsGameFinished())
                {
                    MessageBox.Show("It's a tie!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    gameFinished = true;
                    Close();
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
            else if (gameFinished)
            {
                MessageBox.Show("The game is already finished!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
