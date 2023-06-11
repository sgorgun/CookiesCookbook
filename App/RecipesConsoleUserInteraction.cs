using CookiesCookbook.Recipes.Ingredients;
using CookiesCookbook.Recipes;

namespace CookiesCookbook.App;

/// <summary>
/// Cosole user interface.
/// </summary>
public class RecipesConsoleUserInteraction : IRecipesUserInteraction // Dependency Inversion Principle (SOLID). This type should depend on abstractions, not on concrete.
{
    private readonly IIngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IIngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }

    /// <summary>
    /// Show the message
    /// </summary>
    /// <param name="message"></param>
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Exit from program
    /// </summary>
    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }

    /// <summary>
    /// Display all recepies.
    /// </summary>
    /// <param name="allRecipes">List of all created  recepies.</param>
    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if (allRecipes.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);
            var counter = 1;

            foreach (var recipe in allRecipes)
            {
                Console.WriteLine($"*****{counter}*****");
                Console.WriteLine(recipe);
                Console.WriteLine();
                counter++;
            }
        }
    }

    /// <summary>
    /// Prompt to creating recipes.
    /// </summary>
    /// <returns>Print all ingredients.</returns>
    public void PromtToCreateRecipe()
    {
        Console.WriteLine("Create a newcookie recipe! Available ingredients are:");
        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    /// <summary>
    /// User select ingredients or exit if he push any key.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        bool shallStop = false;

        var ingredients = new List<Ingredient>();

        while (!shallStop)
        {
            Console.WriteLine("Add an ingredient by itsID or type anything else if finished.");
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetById(id);
                if (selectedIngredient is not null)
                {
                    ingredients.Add(selectedIngredient);
                }
            }
            else
            {
                shallStop = true; //Exit
            }
        }

        return ingredients;
    }
}
