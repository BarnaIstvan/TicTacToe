using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X'; //Jelöli az aktuális játékost
        private char[,] gridArray = new char[3, 3]; //3x3-as kétdimenziós tömb
        private bool gameFinished = false; //Jelzi a játék végét

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
            //Sorok és oszlopok ellenőrzése
            for (int i = 0; i < 3; i++)
            {
                if (gridArray[i, 0] == playerValue && gridArray[i, 1] == playerValue && gridArray[i, 2] == playerValue)
                    return true;
                if (gridArray[0, i] == playerValue && gridArray[1, i] == playerValue && gridArray[2, i] == playerValue)
                    return true;
            }
            //Átlók ellenőrzése
            if (gridArray[0, 0] == playerValue && gridArray[1, 1] == playerValue && gridArray[2, 2] == playerValue)
                return true;
            if (gridArray[0, 2] == playerValue && gridArray[1, 1] == playerValue && gridArray[2, 0] == playerValue)
                return true;
            return false;
        }

        private bool IsGameFinished() //Ellenőrzi hogy vab-e még üres mező
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

        private void Button_Click(object sender, RoutedEventArgs e) //Eseménykezelő a gomb megnyomásakor
        {
            Button btn = (Button)sender;
            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            if (!gameFinished && btn.Content == null)
            {
                //Beállítja a gomb tartalmát az aktuális játékos jelével
                btn.Content = currentPlayer;
                btn.IsEnabled = false;
                SolidColorBrush bgBrush = new SolidColorBrush();
                if (currentPlayer == 'X')
                    bgBrush.Color = Colors.Red;
                else
                    bgBrush.Color = Colors.Green;
                btn.Background = bgBrush;

                gridArray[row, col] = currentPlayer;

                //Ha az egyik játékos nyert
                if (PlayerWins(currentPlayer))
                {
                    MessageBox.Show($"{currentPlayer} has WON", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    gameFinished = true;
                    Close();
                }
                //Ha döntetlen
                else if (IsGameFinished())
                {
                    MessageBox.Show("It's a TIE!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    gameFinished = true;
                    Close();
                }
                //Ha a játék folytatódik a másikra vált
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
