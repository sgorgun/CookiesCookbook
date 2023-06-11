using CookiesCookbook.Recipes.Ingredients;

namespace CookiesCookbook.Recipes;


/// <summary>
/// A Recipe is simply a wrapper for a collection of Ingredients.
/// </summary>
public class Recipe
{
    public IEnumerable<Ingredient> Ingredients { get; } // Once an instance is initialized, we can't change the created ingredients (get only).

    public Recipe(IEnumerable<Ingredient> ingredients) // Any type that implements the IEnunerable interface that can be iterated foreach loop.
    {
        Ingredients = ingredients;
    }

    /// <summary>
    /// Override ToString method which alow all ingredients names display
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var steps = new List<string>();
        foreach(var ingredient in Ingredients)
        {
            steps.Add($"{ingredient.Name}. {ingredient.PreparationInstructions}");
        }

        return string.Join(Environment.NewLine, steps);
    }
}
