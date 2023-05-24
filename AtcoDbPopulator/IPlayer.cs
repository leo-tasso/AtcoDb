namespace AtcoDbPopulator;

/// <summary>
/// Interface to model a player class, a class that makes the traffic situation evolve over time.
/// </summary>
public interface IPlayer
{
    /// <summary>
    /// Method to set the starting point.
    /// </summary>
    /// <param name="selectedDateTime">The starting point Date.</param>
    void UpdateTill(DateTime selectedDateTime);

    /// <summary>
    /// Method to play the situation.
    /// </summary>
    /// <param name="speed">The speed Rate.</param>
    void Play(int speed);

    /// <summary>
    /// Method to pause the situation.
    /// </summary>
    void Pause();
}