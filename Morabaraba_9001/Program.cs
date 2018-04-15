using Morabaraba_9001.Classes;

namespace Morabaraba_9001
{
    class Program
    {
        private static IGameSession gameSession;

        static void Main(string[] args)
        {
            gameSession = new GameSession();
        }
    }
}
