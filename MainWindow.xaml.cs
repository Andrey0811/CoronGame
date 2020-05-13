using System.Windows;
using CoronGame.Logic;
using CoronGame.Maps;

namespace CoronGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var engine = new Engine(new Map(), 
                new Render(GameCanvas));
            engine.InitGame();
            engine.DrawObj();
        }
    }
}
