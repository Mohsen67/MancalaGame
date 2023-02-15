using MancalaAssessment.Models;

namespace MancalaAssessment.ViewModels;

public class MainWindowViewModel
{
    private readonly IGameEngine gameEngine;
    private IGame game;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MainWindowViewModel(IGameEngine gameEngine)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        this.gameEngine = gameEngine;
        NewGameCommand = new RelayCommand(NewGame, CanStartNewGame);
    }

    public RelayCommand NewGameCommand { get; init; }

    public void NewGame(object? parameter)
    {
        game = gameEngine.NewGame();
    }

    private bool CanStartNewGame(object obj)
    {
        return true;
    }
}