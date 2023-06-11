using CookiesCookbook.DataAccess;
using CookiesCookbook.Recipes.Ingredients;

namespace CookiesCookbook.Recipes;

/// <summary>
/// All recirpes repository.
/// </summary>
public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientsRegister _ingredientsRegister;
    private const string Separator = ",";

    public RecipesRepository(IStringsRepository stringsRepository, IIngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    /// <summary>
    /// Read the Recpies from the file.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public List<Recipe> Read(string filePath)
    {
        List<string> recipesFromFile = _stringsRepository.Read(filePath);
        var recipes = new List<Recipe>();

        foreach (var recipeFromFile in recipesFromFile)
        {
            var recipe = RecipeFromString(recipeFromFile);
            recipes.Add(recipe);
        }

        return recipes;
    }

    /// <summary>
    /// This method accept the string with ingredirnt IDs as a parameter an use them to build a recipe object.
    /// </summary>
    /// <param name="recipeFromFile"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private Recipe RecipeFromString(string recipeFromFile)
    {
        var textualIds = recipeFromFile.Split(Separator);
        var ingredients = new List<Ingredient>();

        foreach (var textualId in textualIds)
        {
            var id = int.Parse(textualId);
            var ingredient = _ingredientsRegister.GetById(id);
            ingredients.Add(ingredient);
        }

        return new Recipe(ingredients);
    }

    /// <summary>
    /// Make recipes list (IDs are saparated by comma.)
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="allRecipes"></param>
    public void Write(string filePath, List<Recipe> allRecipes)
    {
        var recipesAsStrings = new List<string>();
        foreach (var recipe in allRecipes)
        {
            var allIds = new List<int>();
            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipesAsStrings.Add(string.Join(Separator, allIds));
        }
        _stringsRepository.Write(filePath, recipesAsStrings);
    }
}
