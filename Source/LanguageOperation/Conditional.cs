// Written in 2023 by bradson
// To the extent possible under law, the author(s) have dedicated all
// copyright and related and neighboring rights to this software to the
// public domain worldwide. This software is distributed without any
// warranty. You should have received a copy of the CC0 Public Domain
// Dedication along with this software. If not, see
// http://creativecommons.org/publicdomain/zero/1.0/.

using System.Xml;
using JetBrains.Annotations;
using Verse;
// ReSharper disable InconsistentNaming

namespace LanguageOperation;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class Conditional : PatchOperation
{
	protected string? language;

	protected bool debug;

	protected PatchOperation?
		match,
		nomatch;

	protected override bool ApplyWorker(XmlDocument xml)
	{
		if (debug)
		{
			Log.Message($"Running '{this}' from file '{sourceFile}' with target language '{
				language}' and currently active language '{Prefs.LangFolderName}'. This is considered '{(
					LanguageMatches ? nameof(match) : nameof(nomatch))}' and leads to '{
					(LanguageMatches ? match : nomatch).ToStringSafe()}' running.");
		}

		return language is null
			? LogError(this, "is missing a language node.")
			: LanguageMatches
				? match is null ? nomatch != null : ApplyPatch(xml, match)
				: nomatch is null
					? match != null
					: ApplyPatch(xml, nomatch);
	}

	protected bool LanguageMatches
	{
		get
		{
			var selectedLanguageFolderName = Prefs.LangFolderName;

			if (selectedLanguageFolderName is null)
				return language == LanguageDatabase.DefaultLangFolderName;

			return language == selectedLanguageFolderName
				|| language == LanguageUtility.CurrentLegacyLanguageFolderName;
		}
	}

	protected static bool ApplyPatch(XmlDocument xml, PatchOperation patch)
		=> patch.Apply(xml) || LogError(patch, "failed.");

	protected static bool LogError(PatchOperation patch, string message)
	{
		Log.Error($"'{patch}' in file '{patch.sourceFile}' {
			(patch is PatchOperationPathed pathed ? $"with xpath '{pathed.xpath}' " : "")}{message}");
		return false;
	}
}