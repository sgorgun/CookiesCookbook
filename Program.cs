var cookiesRecipesApp = new CookiesRecipesApp();
cookiesRecipesApp.Run();

public class CookiesRecipesApp
{
    private readonly RecpesRepository _recpesRepository;
    private readonly RecpesUserInteraction _recpesUserInteraction;

    /// <summary>
    /// Inicialise filds in class.
    /// </summary>
    /// <param name="recpesRepository">Recepies storage.</param>
    /// <param name="recpesUserInteraction">Ineraction with recepies. Like display, print and so on.</param>
    public CookiesRecipesApp(RecpesRepository recpesRepository, RecpesUserInteraction recpesUserInteraction)
    {
        _recpesRepository = recpesRepository;
        _recpesUserInteraction = recpesUserInteraction;
    }

    public void Run()
    {
        throw new NotImplementedException();
    }
}