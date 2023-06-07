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

    /// <summary>
    /// Main program metod.
    /// </summary>
    public void Run()
    {
        var allRecipes = _recpesRepository.Read(filePath); // Step 1: Reading all recipes. TODO: make filePath.
        _recpesUserInteraction.PrintExistingRecipes(allRecipes); // Step 2: Display on the screen all recepies.
        _recpesUserInteraction.PromtToCreateRecipe(); // Step 3: Prompting the user to create recipy.
        var ingredients = _recpesUserInteraction.ReadIngredientsFromUser(); //Step 4: // Reading ingredirnts from user.

        if (ingredients.Count > 0) // Check: is user selected any ingredients?
        {
            var recepes = new Recipy(ingredients); // Using ingrediens for creating recipe.
            allRecipes.Add(recepe); // Add recepe in recipes list.
            _recpesRepository.Write(filePath, allRecipes) // Write recipes to file.
            _recpesUserInteraction.ShowMessage("Recipe added."); //Show messade about adding.
            _recpesUserInteraction.ShowMessage(recepe.ToString()); // Show recepe.
        }
        else // Message if recepies are not added.
        {
            _recpesUserInteraction.SowMessage("No ingredients has been selected. Recipe will not be saved.")
        }

        _recpesUserInteraction.Exit();       
    }   
}