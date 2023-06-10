using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;
using System.Xml.Linq;

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction()); // Create an instance of the class using the constructor below. The way to communicate with user. In the future we can change it.

cookiesRecipesApp.Run("recipes.txt");

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
        var allRecipes = _recipesRepository.Read(filePath); // Step 1: Reading all recipes. TODO: make filePath.
        _recipesUserInteraction.PrintExistingRecipes(allRecipes); // Step 2: Display on the screen all recepies.
        //_recipesUserInteraction.PromtToCreateRecipe(); // Step 3: Prompting the user to create recipy.
        //var ingredients = _recipesUserInteraction.ReadIngredientsFromUser(); //Step 4: // Reading ingredirnts from user.

        //if (ingredients.Count > 0) // Check: is user selected any ingredients?
        //{
        //    var recipes = new Recipy(ingredients); // Using ingrediens for creating recipe.
        //    allRecipes.Add(recipe); // Add recepe in recipes list.
        //    _recipesRepository.Write(filePath, allRecipes); // Write recipes to file.
        //    _recipesUserInteraction.ShowMessage("Recipe added."); //Show messade about adding.
        //    _recipesUserInteraction.ShowMessage(recipe.ToString()); // Show recepe.
        //}
        //else // Message if recepies are not added.
        //{
        //    _recipesUserInteraction.ShowMessage("No ingredients has been selected. Recipe will not be saved.");
        //}

        _recipesUserInteraction.Exit(); //Exit       
    }
}

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
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if(allRecipes.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);
            var counter = 1;
            
            foreach(var recipe in allRecipes)
            {
                Console.WriteLine($"*****{counter}*****");
                Console.WriteLine(recipe);
                Console.WriteLine();
                counter++;
            }
        }
    }
}

/// <summary>
/// Cosole user interface.
/// </summary>
public class RecipesConsoleUserInteraction : IRecipesUserInteraction // Dependency Inversion Principle (SOLID). This type should depend on abstractions, not on concrete.
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
}
public class RecipesRepository : IRecipesRepository
{
    public List<Recipe> Read(string filePath)
    {
        return new List<Recipe>()
        {
            new Recipe(new List<Ingredient>
            {
                new WheatFlour(),
                new Butter(),
                new Sugar()
            }),
            new Recipe(new List<Ingredient>
            {
                new CocoaPowder(),
                new CoconutFlour(),
                new Cinnamon()
            }),
        };
    }
}