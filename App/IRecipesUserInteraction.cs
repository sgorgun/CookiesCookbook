using CookiesCookbook.Recipes.Ingredients;
using CookiesCookbook.Recipes;

namespace CookiesCookbook.App;

/// <summary>
/// Declares whst methods are needed to communicate with a user.
/// </summary>
public interface IRecipesUserInteraction
{
    /// <summary>
    /// Show the message
    /// </summary>
    /// <param name="message"></param>
    public void ShowMessage(string message);

    /// <summary>
    /// Exit from program
    /// </summary>
    public void Exit();

    /// <summary>
    /// Display all recepies.
    /// </summary>
    /// <param name="allRecipes">List of all created  recepies.</param>
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);

    /// <summary>
    /// Prompt to creating recipes.
    /// </summary>
    /// <returns></returns>
    void PromtToCreateRecipe();

    /// <summary>
    /// Infing item in collection by Id.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}
