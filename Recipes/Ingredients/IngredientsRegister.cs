namespace CookiesCookbook.Recipes.Ingredients;

/// <summary>
/// Base of all ingredients.
/// </summary>
public class IngredientsRegister : IIngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>()
{
    new WheatFlour(),
    new CoconutFlour(),
    new Butter(),
    new Chocolate(),
    new Sugar(),
    new Cardamom(),
    new Cinnamon(),
    new CocoaPowder()
};

    /// <summary>
    /// Get recipe ID
    /// </summary>
    /// <param name="id">recipe ID</param>
    /// <returns>ingredient from All(Base of all ingredients) or null if it cant find</returns>
    public Ingredient GetById(int id)
    {
        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
            {
                return ingredient;
            }
        }

        return null;
    }
}
