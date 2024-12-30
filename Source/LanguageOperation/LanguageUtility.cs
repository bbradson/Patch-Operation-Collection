// Written in 2024 by bradson
// To the extent possible under law, the author(s) have dedicated all
// copyright and related and neighboring rights to this software to the
// public domain worldwide. This software is distributed without any
// warranty. You should have received a copy of the CC0 Public Domain
// Dedication along with this software. If not, see
// http://creativecommons.org/publicdomain/zero/1.0/.

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