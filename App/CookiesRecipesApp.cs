using CookiesCookbook.Recipes;

namespace CookiesCookbook.App;

public class CookiesRecipesApp
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    /// <summary>
    /// Inicialise filds in class.
    /// </summary>
    /// <param name="recpesRepository">Recepies storage.</param>
    /// <param name="recpesUserInteraction">Ineraction with recepies. Like display, print and so on.</param>
    public CookiesRecipesApp(IRecipesRepository recipesRepository, IRecipesUserInteraction recipesUserInteraction) // It will be used when we create an instance of the class.
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction; // Creating deppendency injection. Class is given the dependencies it need. It doesn't create them itself.
    }

    /// <summary>
    /// Main program method.
    /// </summary>
    public void Run(string filePath)
    {
        var allRecipes = _recipesRepository.Read(filePath); // Step 1: Reading all recipes.
        _recipesUserInteraction.PrintExistingRecipes(allRecipes); // Step 2: Display on the screen all recepies.
        _recipesUserInteraction.PromtToCreateRecipe(); // Step 3: Prompting the user to create recipy.
        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser(); //Step 4: // Reading ingredirnts from user.

        if (ingredients.Any()) // Check: is user selected any ingredients?
        {
            var recipe = new Recipe(ingredients); // Using ingrediens for creating recipe.
            allRecipes.Add(recipe); // Add recepe in recipes list.
            _recipesRepository.Write(filePath, allRecipes); // Write recipes to a file.
            _recipesUserInteraction.ShowMessage("Recipe added."); //Show messade about adding.
            _recipesUserInteraction.ShowMessage(recipe.ToString()); // Show recepe.
        }
        else // Message if recepies are not added.
        {
            _recipesUserInteraction.ShowMessage("No ingredients has been selected. Recipe will not be saved.");
        }

        _recipesUserInteraction.Exit(); //Exit
    }
}
