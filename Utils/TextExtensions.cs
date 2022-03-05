using System.Collections.Generic;

public static class TextExtensions{
    public static string Clean(this string text){
        var nonTextCharacters = new List<string>(){"\n", "\f"};
        nonTextCharacters.ForEach(c => {text = text.Replace(c, " ");});
        return text;
    }
}