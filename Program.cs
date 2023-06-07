var cookiesRecipesApp = new CookiesRecipesApp();
cookiesRecipesApp.Run();

class CookiesRecipesApp
{
    private readonly RecipesRepository _recipesRepository;
    private readonly RecipesUserInteraction _recipesUserInteraction;


    /// <summary>
    /// Inicialise filds in class.
    /// </summary>
    /// <param name="recpesRepository">Recepies storage.</param>
    /// <param name="recpesUserInteraction">Ineraction with recepies. Like display, print and so on.</param>
    public CookiesRecipesApp(RecipesRepository recipesRepository, RecipesUserInteraction recipesUserInteraction)
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }

    /// <summary>
    /// Main program metod.
    /// </summary>
    public void Run()
    {
        var allRecipes = _recipesRepository.Read(filePath); // Step 1: Reading all recipes. TODO: make filePath.
        _recipesUserInteraction.PrintExistingRecipes(allRecipes); // Step 2: Display on the screen all recepies.
        _recipesUserInteraction.PromtToCreateRecipe(); // Step 3: Prompting the user to create recipy.
        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser(); //Step 4: // Reading ingredirnts from user.

        if (ingredients.Count > 0) // Check: is user selected any ingredients?
        {
            var recipes = new Recipy(ingredients); // Using ingrediens for creating recipe.
            allRecipes.Add(recipe); // Add recepe in recipes list.
            _recipesRepository.Write(filePath, allRecipes); // Write recipes to file.
            _recipesUserInteraction.ShowMessage("Recipe added."); //Show messade about adding.
            _recipesUserInteraction.ShowMessage(recipe.ToString()); // Show recepe.
        }
        else // Message if recepies are not added.
        {
            _recipesUserInteraction.SowMessage("No ingredients has been selected. Recipe will not be saved.");
        }

        _recipesUserInteraction.Exit(); //Exit       
    }   
}

public class RecipesUserInteraction
{
}

internal class RecipesRepository
{
}