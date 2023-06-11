using CookiesCookbook.App;
using CookiesCookbook.DataAccess;
using CookiesCookbook.FileAccess;
using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;
using System.Text.Json;

const FileFormat Format = FileFormat.Txt;

IStringsRepository stringsRepository = Format == FileFormat.Json ?
    new StringsJsonRepository() :
    new StringsTextualRepository();

const string Filename = "recipes";
var fileMetadata = new FileMetadata(Filename, Format);

var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(stringsRepository, ingredientsRegister),
    new RecipesConsoleUserInteraction(ingredientsRegister)); // Create an instance of the class using the constructor below. The way to communicate with user. In the future we can change it.

cookiesRecipesApp.Run(fileMetadata.ToPath()); // File name for storing recipes.