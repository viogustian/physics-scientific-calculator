// ================================================================
// ENTRY POINT  –  .NET 10
// ================================================================
namespace PhysicsCalculator;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}
