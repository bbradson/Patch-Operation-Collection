using Verse;
// ReSharper disable StringIndexOfIsCultureSpecific.1

namespace LanguageOperation;

public static class LanguageUtility
{
	private static string? _currentLegacyLanguageFolderName;

	public static string CurrentLegacyLanguageFolderName
		=> _currentLegacyLanguageFolderName ??= GetLegacyFolderName(Prefs.LangFolderName);
	
	private static string GetLegacyFolderName(string selectedLanguageFolderName)
		=> (selectedLanguageFolderName.Contains("(")
			? selectedLanguageFolderName.Substring(0, selectedLanguageFolderName.IndexOf("(") - 1)
			: selectedLanguageFolderName).Trim();
}